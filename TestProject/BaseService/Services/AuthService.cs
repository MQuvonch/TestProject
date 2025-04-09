using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TestProject.BaseService.Dtos.AuthDto;
using TestProject.BaseService.Helpers;
using TestProject.BaseService.IServices;
using TestProject.Data.Models.Entities;
using TestProject.Data.Models.Entities.Roles;
using TestProject.Data.Repositories;

namespace TestProject.BaseService.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IRepository<User,Guid> _userRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IRepository<UserRole, int> _userRoleRepository;

    public AuthService(IRepository<User, Guid> userRepository,
                       IConfiguration configuration,
                       IHttpContextAccessor contextAccessor,
                       IRepository<UserRole, int> userRoleRepository)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _contextAccessor = contextAccessor;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<LoginForResultDto> AuthenticateAsync(LoginDto loginDto)
    {
        var users = _userRepository.GetAll();
        var filtereUser = users.Where(x => x.UserName == loginDto.username).FirstOrDefault();

        if (filtereUser != null)
        {
            var hashedPassword = PasswordHelper.Verify(loginDto.Password, filtereUser.PasswordHash);
            if (!hashedPassword)
                throw new Exception("parol yoki login xato");

            return new LoginForResultDto()
            {
                Token = GenerateUserToken(filtereUser),
                fullname = filtereUser.FirstName + filtereUser.LastName
            };

        }

        throw new Exception("Palor yoki Login notug'ri ");
    }

    public Guid TokenFromUserId()
    {
        var user = _contextAccessor.HttpContext?.User;
        if (user is null || !user.Identity.IsAuthenticated)
            throw new Exception("Foydalanuvchi Authentication qilnmagan");

        var idClaim = user.Claims.Where(claim => claim.Type == "Id").FirstOrDefault()?.Value;

        if (string.IsNullOrEmpty(idClaim))
            throw new Exception("Token ichida Id claim mavjuda emas");
        return Guid.Parse(idClaim);
    }

    private string GenerateUserToken(User user)
    {
        try
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            var userRoles = _userRoleRepository.GetAll()
                .Where(u => u.UserId == user.Id)
                .Include(u => u.Role).ToList();

            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Name, user.Email),
            };

            foreach (var role in userRoles)
            {
                if (role.Role?.Name is not null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
                }
            }


            var tokenDescription = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(claims),
                Audience = _configuration["JWT:Audience"],
                Issuer = _configuration["JWT:Issuer"],
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:Expire"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            var respone = tokenHandler.WriteToken(token);

            return respone;
        }
        catch (Exception ex)
        {   
            throw new Exception(ex.Message);
        }
    }
}

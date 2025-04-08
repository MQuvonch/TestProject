using Microsoft.EntityFrameworkCore;
using TestProject.BaseService.Dtos.StudentDto;
using TestProject.BaseService.IServices;
using TestProject.Data.Models.Entities;
using TestProject.Data.Repositories;

namespace TestProject.BaseService.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student, Guid> _studentRepository;
        private readonly IRepository<StudentGroup, Guid> _studentGroupRepository;

        public StudentService(IRepository<Student, Guid> studentRepository, IRepository<StudentGroup, Guid> studentGroupRepository)
        {
            _studentRepository = studentRepository;
            _studentGroupRepository = studentGroupRepository;
        }

        public async Task<bool> CreateAsync(StudentCreateDto dto)
        {
            var student = await _studentRepository.GetAll()
                .Where(g => g.PhoneNumber == dto.PhoneNumber)
                .FirstOrDefaultAsync();

            if (student is not null)
                throw new Exception($"bu {dto.PhoneNumber} nomer mavjud");

            var studentEntity = new Student()
            {
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender
            };

            await _studentRepository.CreateAsync(studentEntity);
            return true;
        }

        public Task<bool> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StudentResultDto>> GetAllAsync()
        {
            var students =  _studentRepository.GetAll().Select(s=> new StudentResultDto()
            {
                Id = s.Id,
                FullName = s.FullName,
                PhoneNumber= s.PhoneNumber,
                DateOfBirth= s.DateOfBirth, 
                Gender = s.Gender,
            }).AsEnumerable();  

            return students;
        }


        public async Task<StudentResultDto> GetByIdAsync(Guid Id)
        {
            var student = await _studentRepository
                .GetAll()
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (student == null)
                throw new Exception("Student topilmadi");

            var studentGroups = await _studentGroupRepository
                .GetAll()
                .Where(sg => sg.StudentId == Id)
                .Include(sg => sg.Group)
                    .ThenInclude(g => g.Mentor)
                .ToListAsync();

            var result = new StudentResultDto
            {
                Id = student.Id,
                FullName = student.FullName,
                PhoneNumber = student.PhoneNumber,
                DateOfBirth = student.DateOfBirth,
                Groups = studentGroups.Select(sg => new GroupDto
                {
                    Id = sg.Group.Id,
                    Name = sg.Group.Name,
                    Price = sg.Group.Price,
                    Mentor = sg.Group.Mentor != null
                        ? sg.Group.Mentor.FirstName + " " + sg.Group.Mentor.LastName
                        : "Mentor biriktirilmagan"
                }).ToList()
            };

            return result;
        }


        public async Task<bool> UpdateAsync(Guid Id, StudentUpdateDto dto)
        {
            var student = await _studentRepository.GetByIdAsync(Id);
            if (student is null)
                throw new Exception("bu student mavjud emas");

            student.FullName = dto.FullName;
            student.PhoneNumber = dto.PhoneNumber;
            student.Gender = dto.Gender;

            await _studentRepository.UpdateAsync(student);
            return true;
        }
    }

}

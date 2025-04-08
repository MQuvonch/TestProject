using System.ComponentModel.DataAnnotations.Schema;
using TestProject.Data.Models.Entities;

namespace TestProject.BaseService.Dtos.GroupDto;

public class GroupCreateDto
{
    public string Name { get; set; }
    public double Price { get; set; }
}

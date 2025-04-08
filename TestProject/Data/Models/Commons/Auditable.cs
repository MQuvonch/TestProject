namespace TestProject.Data.Models.Commons;

public class Auditable<TId>
    where TId : struct
{
    public TId Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}

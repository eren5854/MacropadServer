namespace MacropadServer.Domain.Abstractions;
public abstract class EntityAbstraction
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActived { get; set; }
}

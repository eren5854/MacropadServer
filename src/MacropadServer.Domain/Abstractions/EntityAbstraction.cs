namespace MacropadServer.Domain.Abstractions;
public abstract class EntityAbstraction
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool IsActived { get; set; }
}

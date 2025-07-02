namespace MacropadServer.Domain.Abstractions;
public abstract class EntityAbstraction
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string CreatedBy{ get; set; } = default!;
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeleteAt { get; set; }
    public string? DeleteBy{ get; set; }
    public bool IsActived { get; set; }
}

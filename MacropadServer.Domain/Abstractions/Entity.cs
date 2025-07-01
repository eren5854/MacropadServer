namespace MacropadServer.Domain.Abstractions;
public abstract class Entity : EntityAbstraction
{
    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
        IsActived = true;
    }
    public bool IsDeleted { get; set; }
}

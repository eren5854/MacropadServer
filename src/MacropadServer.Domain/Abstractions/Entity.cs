namespace MacropadServer.Domain.Abstractions;
public abstract class Entity : EntityAbstraction
{
    protected Entity()
    {
        Id = Guid.CreateVersion7();//Normal guid den farklı olarak sıralanabilir bir yapıs unuyor. Fakat ağırlığı artıyor.
        CreatedAt = DateTimeOffset.UtcNow;
        CreatedBy = "Admin";
        IsDeleted = false;
        IsActived = true;
    }
    public bool IsDeleted { get; set; }
}

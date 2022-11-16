namespace Todo.Domain.Entities
{
    public class BaseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
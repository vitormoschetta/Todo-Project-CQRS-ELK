namespace Todo.Domain.Entities
{
    public class TodoItem : BaseModel
    {
        public string Title { get; set; } = string.Empty;
        public bool Done { get; set; }

        public void Update(string title, bool done)
        {
            Title = title;
            Done = done;
        }

        public void MarkAsDone()
        {
            Done = true;
        }
    }
}
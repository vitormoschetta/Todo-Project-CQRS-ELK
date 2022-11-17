namespace Todo.Domain.Events.Notifications
{
    public class TodoItemNotification : IDomainEvent
    {
        public TodoItemNotification(TodoItem todoItem)
        {
            Id = todoItem.Id;
            Title = todoItem.Title;
            Done = todoItem.Done;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public bool Done { get; set; }
    }
}
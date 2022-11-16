namespace Todo.Domain.Events.Notifications
{
    public class TodoItemNotification : INotification
    {
        public TodoItemNotification(TodoItem todoItem)
        {
            Id = todoItem.Id;
            Title = todoItem.Title;
            Done = todoItem.Done;
        }

        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool Done { get; set; }
    }
}
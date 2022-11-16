namespace Todo.Domain.Events.Notifications
{
    public class CreatedTodoItemNotification : TodoItemNotification
    {
        public CreatedTodoItemNotification(TodoItem todoItem) : base(todoItem)
        {
        }
    }
}
namespace Todo.Domain.Events.Notifications
{
    public class UpdatedTodoItemNotification : TodoItemNotification
    {
        public UpdatedTodoItemNotification(TodoItem todoItem) : base(todoItem)
        {
        }
    }
}
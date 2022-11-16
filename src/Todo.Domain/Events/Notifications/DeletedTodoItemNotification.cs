namespace Todo.Domain.Events.Notifications
{
    public class DeletedTodoItemNotification : TodoItemNotification
    {
        public DeletedTodoItemNotification(TodoItem todoItem) : base(todoItem)
        {
        }
    }
}
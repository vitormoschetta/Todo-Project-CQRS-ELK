namespace Todo.Domain.Events.Notifications
{
    public class MarkedAsDoneTodoItemNotification : TodoItemNotification
    {
        public MarkedAsDoneTodoItemNotification(TodoItem todoItem) : base(todoItem)
        {
        }
    }
}
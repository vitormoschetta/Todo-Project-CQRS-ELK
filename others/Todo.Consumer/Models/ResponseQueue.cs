namespace Todo.Consumer.Models
{
    public class ResponseQueue
    {
        public string Type { get; set; } = string.Empty;
        public TodoItem Data { get; set; }= new TodoItem();
    }
}
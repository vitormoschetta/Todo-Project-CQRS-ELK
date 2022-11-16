using Todo.Consumer.Models;

namespace Todo.Consumer.Data
{
    public class DataFake
    {
        private List<TodoItem> _todoItems;

        public DataFake()
        {
            _todoItems = new List<TodoItem>();
        }

        public void Add(TodoItem todoItem)
        {
            _todoItems.Add(todoItem);
        }

        public List<TodoItem> GetAll()
        {
            return _todoItems;
        }

        public TodoItem? GetById(Guid id)
        {
            return _todoItems.FirstOrDefault(x => x.Id == id);
        }

        public void Update(TodoItem todoItem)
        {
            var item = _todoItems.FirstOrDefault(x => x.Id == todoItem.Id);
            if (item != null)
            {
                item.Title = todoItem.Title;
                item.Done = todoItem.Done;
            }
        }

        public void Delete(Guid id)
        {
            var item = _todoItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _todoItems.Remove(item);
            }
        }

        public void PrintAll()
        {
            Console.WriteLine($"Lista de tarefas atualizada em {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}:");
            _todoItems.ForEach(x => Console.WriteLine($"- {x.Title} | {(x.Done ? "Concluída" : "Pendente")}"));
            Console.WriteLine();
        }
    }
}
namespace Todo.Application.Commands.Requests
{
    public class CreateTodoItemRequest : Command
    {
        public string Title { get; set; } = string.Empty;
        public bool Done { get; set; }

        public override bool IsInvalid
        {
            get
            {
                ValidationResult = new CreateTodoItemRequestValidator().Validate(this);
                return !ValidationResult.IsValid;
            }
        }

        public TodoItem ToEntity()
        {
            return new TodoItem
            {
                Title = Title,
                Done = Done
            };
        }
    }
}
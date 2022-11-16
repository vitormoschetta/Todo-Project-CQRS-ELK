namespace Todo.Application.Commands.Requests
{
    public class UpdateTodoItemRequest : Command, IRequest<CommandResponse>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool Done { get; set; }

        public override bool IsInvalid
        {
            get
            {
                ValidationResult = new UpdateTodoItemRequestValidator().Validate(this);
                return !ValidationResult.IsValid;
            }
        }
    }
}
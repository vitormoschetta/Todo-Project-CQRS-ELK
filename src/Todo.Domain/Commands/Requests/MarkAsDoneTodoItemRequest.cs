namespace Todo.Application.Commands.Requests
{
    public class MarkAsDoneTodoItemRequest : Command, IRequest<CommandResponse>
    {
        public Guid Id { get; set; }

        public override bool IsInvalid
        {
            get
            {
                ValidationResult = new MarkAsDoneTodoItemRequestValidator().Validate(this);
                return !ValidationResult.IsValid;
            }
        }
    }
}
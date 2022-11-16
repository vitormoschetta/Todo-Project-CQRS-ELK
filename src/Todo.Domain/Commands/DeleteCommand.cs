namespace Todo.Domain.Commands
{
    public class DeleteCommand : Command
    {
        public Guid Id { get; set; }

        public override bool IsInvalid
        {
            get
            {
                ValidationResult = new DeleteCommandValidator().Validate(this);
                return !ValidationResult.IsValid;
            }
        }
    }
}
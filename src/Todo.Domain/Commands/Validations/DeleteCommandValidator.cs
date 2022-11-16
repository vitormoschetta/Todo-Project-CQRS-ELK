namespace Todo.Domain.Commands.Validations
{
    public class DeleteCommandValidator: AbstractValidator<DeleteCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O Id não pode ser vazio");
        }
    }
}
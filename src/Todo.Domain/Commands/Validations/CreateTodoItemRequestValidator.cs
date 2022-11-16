namespace Todo.Domain.Commands.Validations
{
    public class CreateTodoItemRequestValidator : AbstractValidator<CreateTodoItemRequest>
    {
        public CreateTodoItemRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("O título deve conter pelo menos 3 caracteres");
        }
    }
}
namespace Todo.Domain.Commands.Validations
{
    public class UpdateTodoItemRequestValidator : AbstractValidator<UpdateTodoItemRequest>
    {
        public UpdateTodoItemRequestValidator()
        {
            RuleFor(x => x.Id)
                .Must(guid => Guid.TryParse(guid.ToString(), out Guid result))
                .WithMessage("O Id não pode ser vazio");

            RuleFor(x => x.Id)
                .Must(guid => guid != Guid.Empty)
                .WithMessage("O Id não pode ser vazio");

            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("O título deve conter pelo menos 3 caracteres");
        }
    }
}
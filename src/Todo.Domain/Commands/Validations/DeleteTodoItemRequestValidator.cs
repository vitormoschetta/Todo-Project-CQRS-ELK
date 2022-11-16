namespace Todo.Domain.Commands.Validations
{
    public class DeleteTodoItemRequestValidator: AbstractValidator<DeleteTodoItemRequest>
    {
        public DeleteTodoItemRequestValidator()
        {
            RuleFor(x => x.Id)
                .Must(guid => Guid.TryParse(guid.ToString(), out Guid result))
                .WithMessage("O Id não pode ser vazio");

            RuleFor(x => x.Id)
                .Must(guid => guid != Guid.Empty)
                .WithMessage("O Id não pode ser vazio");
        }
    }
}
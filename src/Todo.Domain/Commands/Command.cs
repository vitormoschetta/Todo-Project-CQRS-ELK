namespace Todo.Domain.Commands
{
    public abstract class Command
    {
        [JsonIgnore]
        public ValidationResult? ValidationResult { get; set; }

        [JsonIgnore]
        public abstract bool IsInvalid
        {
            get;
        }

        [JsonIgnore]
        public IEnumerable<string> Errors
        {
            get
            {
                if (ValidationResult == null) return new List<string>();

                return ValidationResult.Errors.Select(e => e.ErrorMessage);
            }
        }
    }
}
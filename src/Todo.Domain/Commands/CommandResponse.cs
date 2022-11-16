namespace Todo.Domain.Commands
{
    public class CommandResponse
    {
        public CommandResponse()
        {
            Errors = new List<string>();
        }

        public CommandResponse(List<string> errors)
        {
            Errors = errors;
        }

        public CommandResponse(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public CommandResponse(string error)
        {
            Errors = new List<string> { error };
        }

        public IEnumerable<string> Errors { get; set; }

        public bool Success => !Errors.Any();

        public static CommandResponse Ok => new CommandResponse();

        public static CommandResponse Fail(string error) => new CommandResponse(error);

        public static CommandResponse Fail(IEnumerable<string> errors) => new CommandResponse(errors);

        public static CommandResponse Fail(List<string> errors) => new CommandResponse(errors);

        public static CommandResponse Fail(string[] errors) => new CommandResponse(errors);
    }
}
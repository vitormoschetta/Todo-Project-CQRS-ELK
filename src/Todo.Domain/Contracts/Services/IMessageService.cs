namespace Todo.Domain.Contracts.Services
{
    public interface IMessageService
    {
        void Send(string message);
    }
}
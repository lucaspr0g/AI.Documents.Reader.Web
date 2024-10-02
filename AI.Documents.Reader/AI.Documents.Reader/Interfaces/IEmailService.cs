namespace AI.Documents.Reader.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string name, string from, string phone, string message, CancellationToken cancellationToken);
    }
}
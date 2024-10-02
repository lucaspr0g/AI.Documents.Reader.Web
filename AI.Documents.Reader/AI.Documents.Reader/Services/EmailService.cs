using System.Net.Mail;
using System.Net;
using AI.Documents.Reader.Interfaces;

namespace AI.Documents.Reader.Services
{
	public sealed class EmailService(IConfiguration configuration) : IEmailService
	{
		private readonly string _server = configuration.GetSection("email")["smtp"]!;
		private readonly string _email = configuration.GetSection("email")["email"]!;
		private readonly string _password = configuration.GetSection("email")["password"]!;

		public async Task SendAsync(string name, string from, string phone, string message, CancellationToken cancellationToken)
		{
			var client = new SmtpClient(_server, 587)
			{
				Credentials = new NetworkCredential(_email, _password),
				EnableSsl = true
			};

			var mailMessage = new MailMessage(from, _email, $"{name} - {phone} - Conversor de Documentos", message);
			mailMessage.CC.Add(from);

			await client.SendMailAsync(mailMessage, cancellationToken);
		}
	}
}
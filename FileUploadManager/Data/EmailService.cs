using System.Net.Mail;

namespace FileUploadManager.Data
{
    public class EmailService
    {
        private SmtpClient _client;

        public EmailService(SmtpClient client) => _client = client;

        public void Send(EmailLetter letter)
        {
            _client.SendAsync(letter.From, letter.To, "Successful upload", letter.Message, null);
        }
    }
}

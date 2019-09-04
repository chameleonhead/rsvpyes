using System.Security;

namespace RsvpYes.Infrastructure
{
    public class MessageServiceOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public SecureString Password { get; set; }
    }
}
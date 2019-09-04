using RsvpYes.Domain.Users;

namespace RsvpYes.Application
{
    public class IdentityRegisterWithoutUserCommand
    {
        public string AccountName { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public string OrganizationName { get; set; }
        public MailAddress UserMailAddress { get; set; }
    }
}
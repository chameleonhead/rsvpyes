namespace RsvpYes.Domain.Users
{
    public class Identity
    {
        public Identity(string accountName, string passwordHash, UserId userId)
        {
            Id = new IdentityId();
            AccountName = accountName;
            PasswordHash = passwordHash;
            UserId = userId;
        }

        public Identity(
            IdentityId identityId,
            string accountName,
            string passwordHash,
            UserId userId)
        {
            Id = identityId;
            AccountName = accountName;
            PasswordHash = passwordHash;
            UserId = userId;
        }

        public IdentityId Id { get; }
        public string AccountName { get; private set; }
        public string PasswordHash { get; private set; }
        public UserId UserId { get; private set; }

        public void ChangeAccountName(string accountName)
        {
            AccountName = accountName;
        }

        public void ChangePassword(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public void ChangeUserId(UserId userId)
        {
            UserId = userId;
        }
    }
}

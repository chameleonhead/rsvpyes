namespace RsvpYes.Domain
{
    public class User
    {
        public User(string name, MailAddress mailAddress)
        {
            Id = new UserId();
            Name = name;
            MailAddress = mailAddress;
        }

        public User(UserId id, string name, MailAddress mailAddress)
        {
            Id = id;
            Name = name;
            MailAddress = mailAddress;
        }

        public UserId Id { get; }
        public string Name { get; private set; }
        public MailAddress MailAddress { get; private set; }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateMailAddress(MailAddress mailAddress)
        {
            MailAddress = mailAddress;
        }
    }
}

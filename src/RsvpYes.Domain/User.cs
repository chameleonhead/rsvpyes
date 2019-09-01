namespace RsvpYes.Domain
{
    public class User
    {
        public User(UserId id, string name)
        {
            Id = id;
            Name = name;
        }

        public UserId Id { get; }
        public string Name { get; }
    }
}

namespace RsvpYes.Domain
{
    public class Place
    {
        public Place(string name, Url url)
        {
            Id = new PlaceId();
            Name = name;
            Url = url;
        }

        public Place(PlaceId id, string name, Url url)
        {
            Id = id;
            Name = name;
            Url = url;
        }

        public PlaceId Id { get; }
        public string Name { get; private set; }
        public Url Url { get; private set; }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateUrl(Url url)
        {
            Url = url;
        }
    }
}

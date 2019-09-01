namespace RsvpYes.Domain
{
    public class Place
    {
        public Place(string name)
        {
            Id = new PlaceId();
            Name = name;
        }

        public PlaceId Id { get; }
        public string Name { get; private set; }
        public string Url { get; private set; }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetUrl(string url)
        {
            Url = url;
        }
    }
}

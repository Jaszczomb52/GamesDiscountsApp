namespace APIServicesClassLibrary.Models
{
    public class GameGiveawayConvertedModel : IGameGiveawayConvertedModel
    {
        public enum platforms
        {
            PC,
            PS4,
            PS5,
            XboxOne,
            XboxSeries,
            Switch,
            Android,
            IOS
        }

        public enum types
        {
            DLC,
            Game,
            EarlyAccess,
        }

        public string title { get; set; }
        public decimal worth { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public string instructions { get; set; }
        public string open_giveaway_url { get; set; }
        public DateTime? published_date { get; set; }
        public types type { get; set; }
        public List<platforms> device { get; set; } = new List<platforms>();
        public DateTime? end_date { get; set; }
        public string status { get; set; }

        public override string ToString()
        {
            return $"{title} ;worth: {worth}; type: {type}; status: {status}";
        }
    }

}

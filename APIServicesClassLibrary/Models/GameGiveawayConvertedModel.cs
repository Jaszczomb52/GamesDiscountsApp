namespace APIServicesClassLibrary.Models
{
    public class GameGiveawayConvertedModel : IGameGiveawayConvertedModel
    {
        

        public string title { get; set; }
        public decimal worth { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public string instructions { get; set; }
        public string open_giveaway_url { get; set; }
        public DateTime? published_date { get; set; }
        public APIFactory.types type { get; set; }
        public List<APIFactory.platforms> device { get; set; } = new List<APIFactory.platforms>();
        public DateTime? end_date { get; set; }
        public string status { get; set; }

        public override string ToString()
        {
            return $"{title} ;worth: {worth}; type: {type}; status: {status}";
        }
    }

}

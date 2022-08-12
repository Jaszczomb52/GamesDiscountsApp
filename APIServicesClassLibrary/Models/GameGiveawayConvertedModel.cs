namespace APIServicesClassLibrary.Models
{
    public class GameGiveawayConvertedModel : IGameGiveawayConvertedModel
    {


        public string title { get; set; } = "";
        public decimal worth { get; set; } = 0M;
        public string image { get; set; } = "";
        public string description { get; set; } = "";
        public string instructions { get; set; } = "";
        public string open_giveaway_url { get; set; } = "";
        public DateTime published_date { get; set; }
        public APIFactory.types type { get; set; }
        public List<APIFactory.platforms> device { get; set; } = new List<APIFactory.platforms>();
        public DateTime end_date { get; set; }
        public string status { get; set; } = "";

        public override string ToString()
        {
            return $"{title} ;worth: {worth}; type: {type}; status: {status};published: {published_date.ToShortDateString()}; end date: {check(end_date)}; platforms: {PrintDevices(device)}";
        }

        private string check(DateTime date)
        {
            if (date == DateTime.MinValue)
                return "N/A";
            else
                return date.ToShortDateString();
        }

        private string PrintDevices(List<APIFactory.platforms> input)
        {
            string output = "";
            foreach (var device in input)
            {
                output += device;
            }
            return output;

        }
    }

}

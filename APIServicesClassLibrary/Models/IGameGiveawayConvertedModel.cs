namespace APIServicesClassLibrary.Models
{
    public interface IGameGiveawayConvertedModel
    {
        string description { get; set; }
        List<GameGiveawayConvertedModel.platforms> device { get; set; }
        DateTime? end_date { get; set; }
        string image { get; set; }
        string instructions { get; set; }
        string open_giveaway_url { get; set; }
        DateTime? published_date { get; set; }
        string status { get; set; }
        string title { get; set; }
        GameGiveawayConvertedModel.types type { get; set; }
        decimal worth { get; set; }
    }
}
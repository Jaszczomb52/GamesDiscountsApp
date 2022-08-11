using APIServicesClassLibrary.Models;

namespace FormsControlsClassLibrary
{
    public class EndDateFilter : IFilter, IAscDescFilter, IEndDateFilter
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public EndDateFilter()
        {
            Name = "Filter by end date";
            Description = "Filtering by the date of the end of giveaway for item.";
        }

        public List<IGameGiveawayConvertedModel> FilterASC(List<IGameGiveawayConvertedModel> input)
        {
            return input.OrderBy(x => x.end_date).ToList();
        }

        public List<IGameGiveawayConvertedModel> FilterDESC(List<IGameGiveawayConvertedModel> input)
        {
            return input.OrderByDescending(x => x.end_date).ToList();
        }
    }
}

using APIServicesClassLibrary.Models;

namespace FormsControlsClassLibrary
{
    public class PublishedDateFilter : IFilter, IAscDescFilter, IPublishedDateFilter
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public PublishedDateFilter()
        {
            Name = "Filter by published date";
            Description = "Filtering by the date of publishion of item.";
        }

        public List<IGameGiveawayConvertedModel> FilterASC(List<IGameGiveawayConvertedModel> input)
        {
            return input.OrderBy(x => x.published_date).ToList();
        }

        public List<IGameGiveawayConvertedModel> FilterDESC(List<IGameGiveawayConvertedModel> input)
        {
            return input.OrderByDescending(x => x.published_date).ToList();
        }
    }
}

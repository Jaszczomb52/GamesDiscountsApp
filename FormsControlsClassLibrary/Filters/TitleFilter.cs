using APIServicesClassLibrary.Models;

namespace FormsControlsClassLibrary
{
    public class TitleFilter : IAscDescFilter, ITitleFilter
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public TitleFilter()
        {
            Name = "Filter by title";
            Description = "Filtering by the title of item.";
        }

        public List<IGameGiveawayConvertedModel> FilterASC(List<IGameGiveawayConvertedModel> input)
        {
            input.OrderBy(x => x.title);
            return input;
        }

        public List<IGameGiveawayConvertedModel> FilterDESC(List<IGameGiveawayConvertedModel> input)
        {
            input.OrderByDescending(x => x.title);
            return input;
        }
    }
}

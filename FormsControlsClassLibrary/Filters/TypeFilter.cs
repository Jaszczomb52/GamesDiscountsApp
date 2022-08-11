using APIServicesClassLibrary;
using APIServicesClassLibrary.Models;

namespace FormsControlsClassLibrary
{
    public class TypeFilter : IFilter, IChoiceFilter, ITypeFilter
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public APIFactory.types? Types { get; private set; }

        public TypeFilter()
        {
            Name = "Filter by type";
            Description = "Filtering by the type the item.";
        }

        public void SetType(APIFactory.types type)
        {
            Types = type;
        }

        public List<IGameGiveawayConvertedModel> FilterHasChoice(List<IGameGiveawayConvertedModel> input)
        {
            if (Types is null)
                return input;
            return input.Where(x => x.type == Types).ToList();
        }

        public List<IGameGiveawayConvertedModel> FilterHasNotChoice(List<IGameGiveawayConvertedModel> input)
        {
            if (Types is null)
                return input;
            var temp = input.Where(x => x.type == Types).ToList();
            temp = input.Except(temp).ToList();
            return temp;
        }
    }
}

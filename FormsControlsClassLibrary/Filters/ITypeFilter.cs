using APIServicesClassLibrary;
using APIServicesClassLibrary.Models;

namespace FormsControlsClassLibrary
{
    public interface ITypeFilter
    {
        string Description { get; }
        string Name { get; }
        APIFactory.types? Types { get; }

        List<IGameGiveawayConvertedModel> FilterHasChoice(List<IGameGiveawayConvertedModel> input);
        List<IGameGiveawayConvertedModel> FilterHasNotChoice(List<IGameGiveawayConvertedModel> input);
        void SetType(APIFactory.types type);
    }
}
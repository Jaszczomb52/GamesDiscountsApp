using APIServicesClassLibrary;
using APIServicesClassLibrary.Models;

namespace FormsControlsClassLibrary
{
    public interface IChoiceFilter : IFilter
    {

        List<IGameGiveawayConvertedModel> FilterHasChoice(List<IGameGiveawayConvertedModel> input);
        List<IGameGiveawayConvertedModel> FilterHasNotChoice(List<IGameGiveawayConvertedModel> input);
    }
}
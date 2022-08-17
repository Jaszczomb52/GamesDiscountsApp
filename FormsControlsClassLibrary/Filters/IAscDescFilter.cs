using APIServicesClassLibrary.Models;

namespace FormsControlsClassLibrary
{
    public interface IAscDescFilter : IFilter
    {
        List<IGameGiveawayConvertedModel> FilterASC(List<IGameGiveawayConvertedModel> input);
        List<IGameGiveawayConvertedModel> FilterDESC(List<IGameGiveawayConvertedModel> input);
    }
}
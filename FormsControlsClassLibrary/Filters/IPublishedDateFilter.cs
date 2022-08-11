using APIServicesClassLibrary.Models;

namespace FormsControlsClassLibrary
{
    public interface IPublishedDateFilter
    {
        string Description { get; }
        string Name { get; }

        List<IGameGiveawayConvertedModel> FilterASC(List<IGameGiveawayConvertedModel> input);
        List<IGameGiveawayConvertedModel> FilterDESC(List<IGameGiveawayConvertedModel> input);
    }
}
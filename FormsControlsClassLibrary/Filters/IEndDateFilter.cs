using APIServicesClassLibrary.Models;

namespace FormsControlsClassLibrary
{
    public interface IEndDateFilter
    {
        string Description { get; }
        string Name { get; }

        List<IGameGiveawayConvertedModel> FilterASC(List<IGameGiveawayConvertedModel> input);
        List<IGameGiveawayConvertedModel> FilterDESC(List<IGameGiveawayConvertedModel> input);
    }
}
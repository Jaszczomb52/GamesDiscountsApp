using APIServicesClassLibrary;
using APIServicesClassLibrary.Models;

namespace FormsControlsClassLibrary
{
    public interface IDeviceFilter
    {
        string Description { get; }
        APIFactory.platforms Device { get; }
        string Name { get; }

        List<IGameGiveawayConvertedModel> FilterHasChoice(List<IGameGiveawayConvertedModel> input);
        List<IGameGiveawayConvertedModel> FilterHasNotChoice(List<IGameGiveawayConvertedModel> input);
        void SetDevice(APIFactory.platforms device);
    }
}
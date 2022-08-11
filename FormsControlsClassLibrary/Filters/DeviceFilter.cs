using APIServicesClassLibrary;
using APIServicesClassLibrary.Models;

namespace FormsControlsClassLibrary
{
    public class DeviceFilter : IFilter, IChoiceFilter, IDeviceFilter
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public APIFactory.platforms Device { get; private set; }

        public DeviceFilter()
        {
            Name = "Filter by device";
            Description = "Filtering by the type of device the item is for.";
        }

        public void SetDevice(APIFactory.platforms device)
        {
            Device = device;
        }

        public List<IGameGiveawayConvertedModel> FilterHasChoice(List<IGameGiveawayConvertedModel> input)
        {
            return input.Where(x => x.device.Contains(Device)).ToList();
        }

        public List<IGameGiveawayConvertedModel> FilterHasNotChoice(List<IGameGiveawayConvertedModel> input)
        {
            var temp = input.Where(x => x.device.Contains(Device)).ToList();
            temp = input.Except(temp).ToList();
            return temp;
        }
    }
}

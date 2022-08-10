using APIServicesClassLibrary.Models;

namespace APIServicesClassLibrary
{
    public interface IModelsConverter
    {
        IGameGiveawayConvertedModel ConvertGameGiveawayModel(GameGiveawayRawModel raw);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIServicesClassLibrary.Models;

namespace APIServicesClassLibrary
{
    public interface IGameGiveawayConverter
    {
        IGameGiveawayConvertedModel ConvertGameGiveawayModel(GameGiveawayRawModel raw);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIServicesClassLibrary.Models;

namespace APIServicesClassLibrary
{
    public class APIFactory
    {
        public static GameGiveawayRawModel CreateRawModel()
        {
            return new GameGiveawayRawModel();
        }

        public static IGameGiveawayConvertedModel CreateConvertedModel()
        {
            return new GameGiveawayConvertedModel();
        }

        public static IModelsConverter CreateModelsConverter()
        {
            return new ModelsConverter();
        }
    }
}

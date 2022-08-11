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
        public enum platforms
        {
            PC,
            PS4,
            PS5,
            XboxOne,
            XboxSeries,
            Switch,
            Android,
            IOS,
            Steam,
            EpicGames,
            GOG,
            Other
        }

        public enum types
        {
            DLC,
            Game,
            EarlyAccess,
            Other
        }

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

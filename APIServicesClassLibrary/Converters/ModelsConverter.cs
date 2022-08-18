using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIServicesClassLibrary.Models;
using Windows.Foundation.Metadata;

namespace APIServicesClassLibrary
{
    public class ModelsConverter : IGameGiveawayConverter, IModelsConverter
    {
        public IGameGiveawayConvertedModel ConvertGameGiveawayModel(GameGiveawayRawModel raw)
        {
            var converted = APIFactory.CreateConvertedModel();
            converted.published_date = convertDate(raw.published_date);
            converted.end_date = convertDate(raw.end_date);
            converted.image = raw.image;
            converted.open_giveaway_url = raw.open_giveaway_url;
            converted.title = raw.title;

            if (raw.worth == "N/A") 
                converted.worth = 0; 
            else 
                converted.worth = decimal.Parse(raw.worth.Trim().Split(".")[0].Substring(1) + "," + raw.worth.Trim().Split(".")[1]);

            converted.device = getDevices(raw.platforms);
            converted.type = getTypes(raw.type);

            if (converted.published_date < DateTime.Today.AddMonths(-3) && converted.end_date < DateTime.Today)
                converted.status = "Inactive";
            else
                converted.status = raw.status;

            //if (converted.published_date < DateTime.Today.AddYears(-1))
            //    converted.status = "Dispose";

            return converted;
        }

        private APIFactory.types getTypes(string type)
        {
            if (type == "Game")
                return APIFactory.types.Game;
            else if (type == "Early access")
                return APIFactory.types.EarlyAccess;
            else if (type == "DLC")
                return APIFactory.types.DLC;
            else
                return APIFactory.types.Other;
        }

        private List<APIFactory.platforms> getDevices(string platforms)
        {
            List<APIFactory.platforms> output = new List<APIFactory.platforms>();
            var splitted = platforms.Split(",").ToArray();
            foreach (var platform in splitted)
            {
                var temp = platform.Trim();
                if (temp == "PC")
                    output.Add(APIFactory.platforms.PC);
                else if (temp == "Playstation 4")
                    output.Add(APIFactory.platforms.PS4);
                else if (temp == "Playstation 5")
                    output.Add(APIFactory.platforms.PS5);
                else if (temp == "Android")
                    output.Add(APIFactory.platforms.Android);
                else if (temp == "iOS")
                    output.Add(APIFactory.platforms.IOS);
                else if (temp == "Xbox Series X|S")
                    output.Add(APIFactory.platforms.XboxSeries);
                else if (temp == "Xbox One")
                    output.Add(APIFactory.platforms.XboxOne);
                else if (temp == "Nintendo Switch")
                    output.Add(APIFactory.platforms.Switch);
                else if (temp == "Steam")
                    output.Add(APIFactory.platforms.Steam);
                else if (temp == "Epic Games Store")
                    output.Add(APIFactory.platforms.EpicGames);
                else if (temp == "GOG")
                    output.Add(APIFactory.platforms.GOG);
                else
                    output.Add(APIFactory.platforms.Other);
            }
            return output;
        }

        private DateTime convertDate(string date)
        {
            DateTime output = DateTime.MinValue;
            try
            {
                output = Convert.ToDateTime(date);
                return output;
            }
            catch
            {
                return output;
            }
        }
    }
}

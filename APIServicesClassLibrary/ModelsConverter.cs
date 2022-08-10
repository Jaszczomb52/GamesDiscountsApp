using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIServicesClassLibrary.Models;

namespace APIServicesClassLibrary
{
    public class ModelsConverter : IGameGiveawayConverter, IModelsConverter
    {
        public IGameGiveawayConvertedModel ConvertGameGiveawayModel(GameGiveawayRawModel raw)
        {
            var converted = APIFactory.CreateConvertedModel();
            converted.description = raw.description;
            converted.status = raw.status;
            converted.image = raw.image;
            converted.instructions = raw.instructions;
            converted.open_giveaway_url = raw.open_giveaway_url;
            converted.title = raw.title;

            if (raw.worth == "N/A") 
                converted.worth = 0; 
            else 
                converted.worth = decimal.Parse(raw.worth.Trim().Split(".")[0].Substring(1) + "," + raw.worth.Trim().Split(".")[1]);

            converted.published_date = convertDate(raw.published_date);
            converted.end_date = convertDate(raw.end_date);
            converted.device = getDevices(raw.platforms);
            converted.type = getTypes(raw.type);

            return converted;
        }

        private GameGiveawayConvertedModel.types getTypes(string type)
        {
            if (type == "Game")
                return GameGiveawayConvertedModel.types.Game;
            else if (type == "Early access")
                return GameGiveawayConvertedModel.types.EarlyAccess;
            else
                // returning DLC because it's in 90% dlc, so if it's not game or early access... it's most likely dlc and it'll always return smthn.
                return GameGiveawayConvertedModel.types.DLC;
        }

        private List<GameGiveawayConvertedModel.platforms> getDevices(string platforms)
        {
            List<GameGiveawayConvertedModel.platforms> output = new List<GameGiveawayConvertedModel.platforms>();
            var splitted = platforms.Split(",");
            foreach (var platform in splitted)
            {
                if (platform == "PC")
                    output.Add(GameGiveawayConvertedModel.platforms.PC);
                else if (platform == "PS4")
                    output.Add(GameGiveawayConvertedModel.platforms.PS4);
                else if (platform == "PS5")
                    output.Add(GameGiveawayConvertedModel.platforms.PS5);
                else if (platform == "Android")
                    output.Add(GameGiveawayConvertedModel.platforms.Android);
                else if (platform == "iOS")
                    output.Add(GameGiveawayConvertedModel.platforms.IOS);
                else if (platform == "Xbox Series X|S")
                    output.Add(GameGiveawayConvertedModel.platforms.XboxSeries);
                else if (platform == "Xbox One")
                    output.Add(GameGiveawayConvertedModel.platforms.XboxOne);
                else if (platform == "Nintendo Switch")
                    output.Add(GameGiveawayConvertedModel.platforms.Switch);
            }
            return output;
        }

        private DateTime? convertDate(string date)
        {
            DateTime? output = null;
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

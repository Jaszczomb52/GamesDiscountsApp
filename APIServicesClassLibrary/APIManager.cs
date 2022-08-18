using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIServicesClassLibrary.Models;
using Newtonsoft.Json;

namespace APIServicesClassLibrary
{
    public class APIManager
    {
        private IModelsConverter Converter { get; } = APIFactory.CreateModelsConverter();
        private List<GameGiveawayRawModel> raws { get; set; } = new();
        public List<IGameGiveawayConvertedModel> converted { get; set; } = new();
        public string APILink { get; set; } = "";

        public void SetAPILink(string link)
        {
            APILink = link;
        }

        public async Task GetDataFromAPI()
        {
            using (HttpClient client = new())
            {
                client.BaseAddress = new Uri(APILink);
                var response = await client.GetAsync(APILink);
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        List<GameGiveawayRawModel> output = JsonConvert.DeserializeObject<GameGiveawayRawModel[]>(await response.Content.ReadAsStringAsync())!.ToList();
                        raws = output;
                    }
                    catch
                    {
                        raws = new();
                    }
                }
                else
                    raws = new();
            }
        }

        public void OrderByTitleAsc() => converted = converted.OrderBy(i => i.title).ToList();

        public async Task ConvertModel()
        {
            await Parallel.ForEachAsync(raws, async (x, CancellationToken) => 
            {
                converted.Add(ConvertModel(x));
            });
        }

        private IGameGiveawayConvertedModel ConvertModel(GameGiveawayRawModel model)
        {
            return Converter.ConvertGameGiveawayModel(model);
        }
    }
}

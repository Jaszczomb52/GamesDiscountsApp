using System;
using System.Collections.Generic;
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
        private List<GameGiveawayRawModel> raws { get; set; } = new List<GameGiveawayRawModel>();
        public List<IGameGiveawayConvertedModel> converted { get; set; } = new List<IGameGiveawayConvertedModel>();
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
            }
        }

        public async Task ConvertModel()
        {
            List<Task<IGameGiveawayConvertedModel>> tasks = new();
            foreach (GameGiveawayRawModel rm in raws)
            {
                tasks.Add(Task.Run(() => ConvertModel(rm)));
            }
            var results = await Task.WhenAll(tasks);
            foreach(IGameGiveawayConvertedModel item in results)
            {
                converted.Add(item);
            }
        }

        private IGameGiveawayConvertedModel ConvertModel(GameGiveawayRawModel model)
        {
            return Converter.ConvertGameGiveawayModel(model);
        }
    }
}

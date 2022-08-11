using APIServicesClassLibrary;
using ToastNotificationsClassLibrary;
using FormsControlsClassLibrary;
using UserControlsLibrary;
using APIServicesClassLibrary.Models;

namespace GamesDiscounts
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            
        }

        private async void MainWindow_Load(object sender, EventArgs e)
        {
            APIManager manager = new APIManager();
            manager.SetAPILink("https://www.gamerpower.com/api/giveaways");
            await manager.GetDataFromAPI();
            await manager.ConvertModel();
            listBox1.DataSource = manager.converted;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FilterManager filterManager = new FilterManager();
            var sorts = filterManager.GetSorts();
            var output = sorts.FirstOrDefault(x => x is IWorthFilter).FilterASC(listBox1.Items.Cast<IGameGiveawayConvertedModel>().ToList());
            listBox1.DataSource = output;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FilterManager filterManager = new FilterManager();
            var sorts = filterManager.GetSorts();
            var output = sorts.FirstOrDefault(x => x is IWorthFilter).FilterDESC(listBox1.Items.Cast<IGameGiveawayConvertedModel>().ToList());
            listBox1.DataSource = output;
        }
    }
}
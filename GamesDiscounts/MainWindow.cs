using APIServicesClassLibrary;
using ToastNotificationsClassLibrary;

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
            APIManager manager = new APIManager();
            manager.SetAPILink("https://www.gamerpower.com/api/giveaways");
            await manager.GetDataFromAPI();
            await manager.ConvertModel();
            listBox1.DataSource = manager.converted;
        }
    }
}
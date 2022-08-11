using APIServicesClassLibrary;
using ToastNotificationsClassLibrary;
using FormsControlsClassLibrary;

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
            //manager.SetAPILink("https://www.gamerpower.com/api/giveaways");
            //await manager.GetDataFromAPI();
            //await manager.ConvertModel();
            //listBox1.DataSource = manager.converted.Where(x => x.type == APIFactory.types.Game).ToList();
            FilterManager filterManager = new FilterManager();
            listBox1.DataSource = filterManager.GetSorts();
        }
    }
}
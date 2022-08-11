using APIServicesClassLibrary;
using ToastNotificationsClassLibrary;
using FormsControlsClassLibrary;
using UserControlsLibrary;
using APIServicesClassLibrary.Models;

namespace GamesDiscounts
{
    public partial class MainWindow : Form
    {
        APIManager manager;
        FilterManager filterManager;
        List<IAscDescFilter> sorts;
        List<IChoiceFilter> filters;
        public MainWindow()
        {
            InitializeComponent();
            filterManager = new FilterManager();
            sorts = filterManager.GetSorts();
            filters = filterManager.GetFilters();
        }

        private async void MainWindow_Load(object sender, EventArgs e)
        {
            manager = new APIManager();
            LoadFromAPI();
            comboBox1.DataSource = Enum.GetValues(typeof(APIFactory.platforms)).Cast<APIFactory.platforms>();
            comboBox2.DataSource = Enum.GetValues(typeof(APIFactory.types)).Cast<APIFactory.types>();
        }

        private async void LoadFromAPI()
        {
            manager.converted.Clear();
            manager.SetAPILink("https://www.gamerpower.com/api/giveaways");
            await manager.GetDataFromAPI();
            await manager.ConvertModel();
            listBox1.DataSource = manager.converted;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IWorthFilter).FilterASC(listBox1.Items.Cast<IGameGiveawayConvertedModel>().ToList());
            listBox1.DataSource = output;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IWorthFilter).FilterDESC(listBox1.Items.Cast<IGameGiveawayConvertedModel>().ToList());
            listBox1.DataSource = output;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is ITitleFilter).FilterASC(listBox1.Items.Cast<IGameGiveawayConvertedModel>().ToList());
            listBox1.DataSource = output;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is ITitleFilter).FilterDESC(listBox1.Items.Cast<IGameGiveawayConvertedModel>().ToList());
            listBox1.DataSource = output;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IPublishedDateFilter).FilterASC(listBox1.Items.Cast<IGameGiveawayConvertedModel>().ToList());
            listBox1.DataSource = output;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IPublishedDateFilter).FilterDESC(listBox1.Items.Cast<IGameGiveawayConvertedModel>().ToList());
            listBox1.DataSource = output;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IEndDateFilter).FilterASC(listBox1.Items.Cast<IGameGiveawayConvertedModel>().ToList());
            listBox1.DataSource = output;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IEndDateFilter).FilterDESC(listBox1.Items.Cast<IGameGiveawayConvertedModel>().ToList());
            listBox1.DataSource = output;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var filter = filters.FirstOrDefault(x => x is IDeviceFilter) as IDeviceFilter;
            filter.SetDevice((APIFactory.platforms)comboBox1.SelectedItem);
            var output = filter.FilterHasChoice(listBox1.Items.Cast<IGameGiveawayConvertedModel>().ToList());
            listBox1.DataSource = output;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var filter = filters.FirstOrDefault(x => x is ITypeFilter) as ITypeFilter;
            filter.SetType((APIFactory.types)comboBox2.SelectedItem);
            var output = filter.FilterHasChoice(listBox1.Items.Cast<IGameGiveawayConvertedModel>().ToList());
            listBox1.DataSource = output;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            LoadFromAPI();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(((IGameGiveawayConvertedModel)listBox1.SelectedItem).open_giveaway_url);
        }
    }
}
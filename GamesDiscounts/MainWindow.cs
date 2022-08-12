using APIServicesClassLibrary;
using ToastNotificationsClassLibrary;
using FormsControlsClassLibrary;
using UserControlsLibrary;
using APIServicesClassLibrary.Models;
using System.Reflection;

namespace GamesDiscounts
{
    public partial class MainWindow : Form
    {
        APIManager? manager;
        FilterManager filterManager;
        List<IAscDescFilter> sorts;
        List<IChoiceFilter> filters;
        public MainWindow()
        {
            InitializeComponent();
            filterManager = new FilterManager();
            sorts = filterManager.GetSorts();
            filters = filterManager.GetFilters();
            panel1.Size = tableLayoutPanel11.Size;
        }

        private async void MainWindow_Load(object sender, EventArgs e)
        {
            manager = new APIManager();
            await LoadFromAPI();
            comboBox1.DataSource = Enum.GetValues(typeof(APIFactory.platforms)).Cast<APIFactory.platforms>();
            comboBox2.DataSource = Enum.GetValues(typeof(APIFactory.types)).Cast<APIFactory.types>();
            flowLayoutPanel2.Hide();
            await LoadPanels(manager.converted);
            flowLayoutPanel2.Show();
            panel1.Visible = false;
        }

        private async Task LoadFromAPI()
        {
            manager!.converted.Clear();
            manager.SetAPILink("https://www.gamerpower.com/api/giveaways");
            await manager.GetDataFromAPI();
            await manager.ConvertModel();
        }

        private async Task LoadPanels(List<IGameGiveawayConvertedModel> models)
        {
            flowLayoutPanel2.Controls.Clear();

            //foreach (var model in models)
            //    flowLayoutPanel2.Controls.Add(new DiscountPanel(model));



            IGameGiveawayConvertedModel[] arr = new IGameGiveawayConvertedModel[models.Count];

            for (int i = 0; i < models.Count; i++)
                arr[i] = models[i];

            ParallelOptions po = new();
            po.MaxDegreeOfParallelism = 4;
            List<Task<DiscountPanel>> tasks = new();
            Parallel.For(0, models.Count - 1, (i) =>
            {
                //somehow fucking works lol. can't be "i" cuz it'll just generate 1 card
                tasks.Add(Task.Run(() => new DiscountPanel(arr[int.Parse(i.ToString())])));
            });
            DiscountPanel[] result = await Task.WhenAll(tasks);
            foreach (DiscountPanel panel in result)
                flowLayoutPanel2.Controls.Add(panel);



            //List<Task<DiscountPanel>> tasks = new();
            //foreach (var model in models)
            //    tasks.Add(Task.Run(() => new DiscountPanel(model)));

            //var result = Task.WhenAll(tasks);
            //foreach (var panel in result)
            //    flowLayoutPanel2.Controls.Add(panel);


            //List<Task<DiscountPanel>> tasks = new();
            //foreach (var model in models)
            //    tasks.Add(Task.Run(() => new DiscountPanel(model)));

            //var result = await Task.WhenAll(tasks);

            //foreach (var panel in result)
            //    flowLayoutPanel2.Controls.Add(panel);
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IWorthFilter)!.FilterASC(manager.converted);
            LoadPanels(output);
            manager.converted = output;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IWorthFilter)!.FilterDESC(manager.converted);
            LoadPanels(output);
            manager.converted = output;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is ITitleFilter)!.FilterASC(manager.converted);
            LoadPanels(output);
            manager.converted = output;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is ITitleFilter)!.FilterDESC(manager.converted);
            LoadPanels(output);
            manager.converted = output;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IPublishedDateFilter)!.FilterASC(manager.converted);
            LoadPanels(output);
            manager.converted = output;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IPublishedDateFilter)!.FilterDESC(manager.converted);
            LoadPanels(output);
            manager.converted = output;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IEndDateFilter)!.FilterASC(manager.converted);
            LoadPanels(output);
            manager.converted = output;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IEndDateFilter)!.FilterDESC(manager.converted);
            LoadPanels(output);
            manager.converted = output;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var filter = filters.FirstOrDefault(x => x is IDeviceFilter) as IDeviceFilter;
            filter!.SetDevice((APIFactory.platforms)comboBox1.SelectedItem);
            var output = filter.FilterHasChoice(manager.converted);
            LoadPanels(output);
            manager.converted = output;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var filter = filters.FirstOrDefault(x => x is ITypeFilter) as ITypeFilter;
            filter!.SetType((APIFactory.types)comboBox2.SelectedItem);
            var output = filter.FilterHasChoice(manager.converted);
            LoadPanels(output);
            manager.converted = output;
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            await LoadFromAPI();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            //Clipboard.SetText(((IGameGiveawayConvertedModel)listBox1.SelectedItem).open_giveaway_url);
        }
    }
}
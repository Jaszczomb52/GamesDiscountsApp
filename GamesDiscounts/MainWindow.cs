using APIServicesClassLibrary;
using ToastNotificationsClassLibrary;
using FormsControlsClassLibrary;
using UserControlsLibrary;
using APIServicesClassLibrary.Models;
using System.Reflection;
using System.ComponentModel;

namespace GamesDiscounts
{
    public partial class MainWindow : Form
    {
        APIManager? manager;
        FilterManager filterManager;
        List<IAscDescFilter> sorts;
        List<IChoiceFilter> filters;
        PageManager pageManager = new();
        enum Page
        {
            Left,
            Right,
            None,
            First
        }
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
            this.DoubleBuffered = true;
            manager = new APIManager();
            comboBox1.DataSource = Enum.GetValues(typeof(APIFactory.platforms)).Cast<APIFactory.platforms>();
            comboBox2.DataSource = Enum.GetValues(typeof(APIFactory.types)).Cast<APIFactory.types>();
            await LoadFromAPI();
            pageManager.Configure(20, manager.converted.Count);
            LoadPanels(manager.converted,Page.None);
        }

        private void EnableLoading()
        {
            panel1.Visible = true;
            panel1.Size = flowLayoutPanel2.Size;
            panel1.Location = flowLayoutPanel2.Location;
        }

        private void DisableLoading()
        {
            panel1.Visible = false;
        }

        private async Task LoadFromAPI()
        {
            EnableLoading();
            manager!.converted.Clear();
            manager.SetAPILink("https://www.gamerpower.com/api/giveaways");
            await manager.GetDataFromAPI();
            await manager.ConvertModel();
        }

        private void LoadPanels(List<IGameGiveawayConvertedModel> models, Page page)
        {
            EnableLoading();
            flowLayoutPanel2.Controls.Clear();
            List<Control> TempList = new List<Control>();

            button13.Enabled = button12.Enabled = true;

            if (page == Page.Left)
                pageManager.MoveLeft();
            if (page == Page.Right)
                pageManager.MoveRight();
            if (page == Page.First)
            {
                button12.Enabled = false;
                pageManager.FirstPage();
            }

            Parallel.ForEach(models.OrderByDescending(x => x.published_date)
                .Skip((pageManager.GetCurrentPage()-1) * pageManager.GetCardsPerPage())
                    .Take(pageManager.GetCardsPerPage()), M =>
                    {
                        TempList.Add(new DiscountPanel(M));
                    });

            label5.Text = $"{pageManager.GetCurrentPage()} out of {pageManager.GetPages()}";
            flowLayoutPanel2.Controls.AddRange(TempList.OrderByDescending(x => DateTime.Parse((x as DiscountPanel).label7.Text)).ToArray());
            DisableLoading();

            if (pageManager.GetPages() == 1)
                button13.Enabled = button12.Enabled = false;
        }


        private async void button1_Click_1(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IWorthFilter)!.FilterASC(manager.converted);
            LoadPanels(output,Page.None);
            manager.converted = output;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IWorthFilter)!.FilterDESC(manager.converted);
            LoadPanels(output, Page.None);
            manager.converted = output;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is ITitleFilter)!.FilterASC(manager.converted);
            LoadPanels(output, Page.None);
            manager.converted = output;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is ITitleFilter)!.FilterDESC(manager.converted);
            LoadPanels(output, Page.None);
            manager.converted = output;
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IPublishedDateFilter)!.FilterASC(manager.converted);
            LoadPanels(output, Page.None);
            manager.converted = output;
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IPublishedDateFilter)!.FilterDESC(manager.converted);
            LoadPanels(output, Page.None);
            manager.converted = output;
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IEndDateFilter)!.FilterASC(manager.converted);
            LoadPanels(output, Page.None);
            manager.converted = output;
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            var output = sorts.FirstOrDefault(x => x is IEndDateFilter)!.FilterDESC(manager.converted);
            LoadPanels(output, Page.None);
            manager.converted = output;
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            var filter = filters.FirstOrDefault(x => x is IDeviceFilter) as IDeviceFilter;
            filter!.SetDevice((APIFactory.platforms)comboBox1.SelectedItem);
            var output = filter.FilterHasChoice(manager.converted);
            manager.converted = output;
            pageManager.Configure(20, manager.converted.Count);
            label5.Text = $"{pageManager.GetCurrentPage()} out of {pageManager.GetPages()}";
            LoadPanels(output, Page.First);
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            var filter = filters.FirstOrDefault(x => x is ITypeFilter) as ITypeFilter;
            filter!.SetType((APIFactory.types)comboBox2.SelectedItem);
            var output = filter.FilterHasChoice(manager.converted);
            manager.converted = output;
            pageManager.Configure(20, manager.converted.Count);
            label5.Text = $"{pageManager.GetCurrentPage()} out of {pageManager.GetPages()}";
            LoadPanels(output, Page.First);
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            await LoadFromAPI();
            pageManager.Configure(20, manager.converted.Count);
            LoadPanels(manager.converted,Page.First);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {

        }

        private async void RightPageClick(object sender, EventArgs e)
        {
            LoadPanels(manager.converted, Page.Right);
            if (pageManager.GetCurrentPage() == pageManager.GetPages())
                button13.Enabled = false;
            if (button12.Enabled == false && pageManager.GetPages() != 1)
                button12.Enabled = true;
            label5.Text = $"{pageManager.GetCurrentPage()} out of {pageManager.GetPages()}";
        }

        private async void LeftPageClick(object sender, EventArgs e)
        {
            LoadPanels(manager.converted, Page.Left);
            if (pageManager.GetCurrentPage() == 1)
                button12.Enabled = false;
            if (button13.Enabled == false && pageManager.GetPages() != 1)
                button13.Enabled = true;
            label5.Text = $"{pageManager.GetCurrentPage()} out of {pageManager.GetPages()}";
        }
    }
}
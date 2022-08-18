using APIServicesClassLibrary;
using FormsControlsClassLibrary;
using UserControlsLibrary;
using APIServicesClassLibrary.Models;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Forms;

namespace GamesDiscounts
{
    public partial class MainWindow : Form
    {
        APIManager? manager;
        FilterManager filterManager;
        List<IAscDescFilter> sorts;
        List<IChoiceFilter> filters;
        PageManager pageManager = new();
        DiscountsPanelFilter.FilterTypes sortType;
        bool Desc;
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
            manager = new APIManager();
            checkBox1.CheckedChanged += (s, e) => LoadPanels(manager!.converted, Page.First,DiscountsPanelFilter.FilterTypes.PublishDate,true);
        }

        private async void MainWindow_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Enum.GetValues(typeof(APIFactory.platforms)).Cast<APIFactory.platforms>();
            comboBox2.DataSource = Enum.GetValues(typeof(APIFactory.types)).Cast<APIFactory.types>();
            await LoadFromAPI();
            pageManager.Configure(20, manager!.converted.Where(x => x.status != "Dispose").Count());
            LoadPanels(manager.converted,Page.First, DiscountsPanelFilter.FilterTypes.PublishDate, true);
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

        private void LoadPanels(List<IGameGiveawayConvertedModel> models, Page page, DiscountsPanelFilter.FilterTypes filterType, bool Desc)
        {
            EnableLoading();
            flowLayoutPanel2.Controls.Clear();
            List<Control> TempList = new List<Control>();

            #region sorting

            if (filterType == DiscountsPanelFilter.FilterTypes.AsIs)
            {
                filterType = sortType;
                Desc = this.Desc;
            }
            else
            {
                sortType = filterType;
                this.Desc = Desc;
            }

            // sorting
            if (filterType == DiscountsPanelFilter.FilterTypes.Title && !Desc)
                models = models.OrderBy(x => x.title).ToList();
            else if (filterType == DiscountsPanelFilter.FilterTypes.Title && Desc)
                models = models.OrderByDescending(x => x.title).ToList(); 
            else if (filterType == DiscountsPanelFilter.FilterTypes.PublishDate && !Desc)
                models = models.OrderBy(x => x.published_date).ToList(); 
            else if (filterType == DiscountsPanelFilter.FilterTypes.PublishDate && Desc)
                models = models.OrderByDescending(x => x.published_date).ToList(); 
            else if (filterType == DiscountsPanelFilter.FilterTypes.EndDate && !Desc)
                models = models.OrderBy(x => x.end_date).ToList(); 
            else if (filterType == DiscountsPanelFilter.FilterTypes.EndDate && Desc)
                models = models.OrderByDescending(x => x.end_date).ToList(); 
            else if (filterType == DiscountsPanelFilter.FilterTypes.Worth && !Desc)
                models = models.OrderBy(x => x.worth).ToList(); 
            else if (filterType == DiscountsPanelFilter.FilterTypes.Worth && Desc)
                models = models.OrderByDescending(x => x.worth).ToList();

            manager!.converted = models;

            #endregion

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

            Parallel.ForEach(models
                .Skip((pageManager.GetCurrentPage() - 1) * pageManager.GetCardsPerPage())
                    .Take(pageManager.GetCardsPerPage()), M =>
                    {
                        if (checkBox1.Checked)
                        {
                            if (M.status == "Active")
                                TempList.Add(new DiscountPanel(M));
                            else if(M.status == "Inactive")
                                TempList.Add(new DiscountPanel(M).Disable());
                        }
                        else
                        {
                            TempList.Add(new DiscountPanel(M));
                        }
                    });

            label5.Text = $"{pageManager.GetCurrentPage()} out of {pageManager.GetPages()}";

            #region AddingWithSorting
            //adding panels with filtering
            if (filterType == DiscountsPanelFilter.FilterTypes.Title && !Desc)
                flowLayoutPanel2.Controls.AddRange(TempList.OrderBy(x => (x as DiscountPanel)!.title).ToArray());
            else if (filterType == DiscountsPanelFilter.FilterTypes.Title && Desc)
                flowLayoutPanel2.Controls.AddRange(TempList.OrderByDescending(x => (x as DiscountPanel)!.title).ToArray());
            else if (filterType == DiscountsPanelFilter.FilterTypes.PublishDate && !Desc)
                flowLayoutPanel2.Controls.AddRange(TempList.OrderBy(x => (x as DiscountPanel)!.published_date).ToArray());
            else if (filterType == DiscountsPanelFilter.FilterTypes.PublishDate && Desc)
                flowLayoutPanel2.Controls.AddRange(TempList.OrderByDescending(x => (x as DiscountPanel)!.published_date).ToArray());
            else if (filterType == DiscountsPanelFilter.FilterTypes.EndDate && !Desc)
                flowLayoutPanel2.Controls.AddRange(TempList.OrderBy(x => (x as DiscountPanel)!.end_date).ToArray());
            else if (filterType == DiscountsPanelFilter.FilterTypes.EndDate && Desc)
                flowLayoutPanel2.Controls.AddRange(TempList.OrderByDescending(x => (x as DiscountPanel)!.end_date).ToArray());
            else if (filterType == DiscountsPanelFilter.FilterTypes.Worth && !Desc)
                flowLayoutPanel2.Controls.AddRange(TempList.OrderBy(x => (x as DiscountPanel)!.worth).ToArray());
            else if (filterType == DiscountsPanelFilter.FilterTypes.Worth && Desc)
                flowLayoutPanel2.Controls.AddRange(TempList.OrderByDescending(x => (x as DiscountPanel)!.worth).ToArray());
            else if (filterType == DiscountsPanelFilter.FilterTypes.AsIs)
                flowLayoutPanel2.Controls.AddRange(TempList.ToArray());
            #endregion

            //flowLayoutPanel2.Controls.AddRange(TempList.OrderByDescending(x => DateTime.Parse((x as DiscountPanel)!.label7.Text)).ToArray());
            //flowLayoutPanel2.Controls.AddRange(TempList.ToArray());
            DisableLoading();

            if (pageManager.GetPages() == 1)
                button13.Enabled = button12.Enabled = false;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            LoadPanels(manager.converted, Page.First, DiscountsPanelFilter.FilterTypes.Worth, false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadPanels(manager.converted, Page.First, DiscountsPanelFilter.FilterTypes.Worth, true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //manager.OrderByTitleAsc();
            LoadPanels(manager.converted, Page.First,DiscountsPanelFilter.FilterTypes.Title, false);


            //List<DiscountPanel> discountPanels = new List<DiscountPanel>();
            //foreach (var item in flowLayoutPanel2.Controls)
            //{
            //    if(item is DiscountPanel)
            //    {
            //        discountPanels.Add((DiscountPanel)item);    
            //    }
            //}
            //discountPanels = discountPanels.OrderBy(I=>I.title).ToList();
            //flowLayoutPanel2.Controls.Clear();
            //flowLayoutPanel2.Controls.AddRange(discountPanels.ToArray());





            /*ClickLoad(
                sorts.FirstOrDefault(x => x is ITitleFilter)!.FilterASC(manager!.converted)
            );*/

            // list.AsParallel().AsOrdered();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadPanels(manager.converted, Page.First, DiscountsPanelFilter.FilterTypes.Title, true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadPanels(manager.converted, Page.First, DiscountsPanelFilter.FilterTypes.PublishDate, false);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadPanels(manager.converted, Page.First, DiscountsPanelFilter.FilterTypes.PublishDate, true);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoadPanels(manager.converted, Page.First, DiscountsPanelFilter.FilterTypes.EndDate, false);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            LoadPanels(manager.converted, Page.First, DiscountsPanelFilter.FilterTypes.EndDate, true);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var filter = filters.FirstOrDefault(x => x is IDeviceFilter) as IDeviceFilter;
            filter!.SetDevice((APIFactory.platforms)comboBox1.SelectedItem);
            FilterCombosWrapper((IChoiceFilter)filter);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var filter = filters.FirstOrDefault(x => x is ITypeFilter) as ITypeFilter;
            filter!.SetType((APIFactory.types)comboBox2.SelectedItem);
            FilterCombosWrapper((IChoiceFilter)filter);
        }

        private void FilterCombosWrapper(IChoiceFilter filter)
        {
            var output = filter.FilterHasChoice(manager!.converted);
            manager.converted = output;
            pageManager.Configure(20, manager.converted.Where(x => x.status != "Dispose").Count());
            label5.Text = $"{pageManager.GetCurrentPage()} out of {pageManager.GetPages()}";
            LoadPanels(output, Page.First, DiscountsPanelFilter.FilterTypes.PublishDate, true);
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            await LoadFromAPI();
            pageManager.Configure(20, manager!.converted.Where(x => x.status != "Dispose").Count());
            LoadPanels(manager.converted,Page.First, DiscountsPanelFilter.FilterTypes.PublishDate, true);
        }

        private void RightPageClick(object sender, EventArgs e)
        {
            LoadPanels(manager!.converted, Page.Right, DiscountsPanelFilter.FilterTypes.AsIs, true);
            if (pageManager.GetCurrentPage() == pageManager.GetPages())
                button13.Enabled = false;
            if (button12.Enabled == false && pageManager.GetPages() != 1)
                button12.Enabled = true;
            label5.Text = $"{pageManager.GetCurrentPage()} out of {pageManager.GetPages()}";
        }

        private void LeftPageClick(object sender, EventArgs e)
        {
            LoadPanels(manager!.converted, Page.Left, DiscountsPanelFilter.FilterTypes.AsIs, true);
            if (pageManager.GetCurrentPage() == 1)
                button12.Enabled = false;
            if (button13.Enabled == false && pageManager.GetPages() != 1)
                button13.Enabled = true;
            label5.Text = $"{pageManager.GetCurrentPage()} out of {pageManager.GetPages()}";
        }

        
    }
}
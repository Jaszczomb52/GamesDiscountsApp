using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormsControlsClassLibrary;
using APIServicesClassLibrary;

namespace UserControlsLibrary
{
    public partial class FilterPanel : UserControl
    {
        FilterManager manager = new FilterManager();
        public FilterPanel()
        {
            InitializeComponent();
            comboBox1.DataSource = new APIFactory.platforms();
            comboBox2.DataSource = new APIFactory.types();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            manager.GetSorts().FirstOrDefault(x => x is IWorthFilter).FilterASC();
        }
    }
}

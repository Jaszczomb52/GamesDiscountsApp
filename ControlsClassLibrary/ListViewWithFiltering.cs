using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using APIServicesClassLibrary.Models;

namespace UserControlsLibrary
{
    public partial class ListViewWithFiltering : UserControl
    {
        public ListViewWithFiltering()
        {
            InitializeComponent();
        }

        public void SetList(List<IGameGiveawayConvertedModel> list)
        {
            listBox1.DataSource = list;
        }

        public List<IGameGiveawayConvertedModel> GetList()
        {
            return listBox1.Items.Cast<IGameGiveawayConvertedModel>().ToList();
        }
    }
}

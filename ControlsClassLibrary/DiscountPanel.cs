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
    public partial class DiscountPanel : UserControl
    {
        string link = "";
        public DiscountPanel(IGameGiveawayConvertedModel model)
        {
            link = model.image;
            InitializeComponent();
            if (link != "")
                pictureBox1.Load(link);
            label1.Text = model.title;
            label3.Text = convertPlatforms(model.device);
            label5.Text = model.type.ToString();
            label7.Text = model.published_date.ToShortDateString();
            label9.Text = model.end_date.ToShortDateString();
            label11.Text = "$" + model.worth.ToString();
            link = model.open_giveaway_url;
        }

        private string convertPlatforms(List<APIServicesClassLibrary.APIFactory.platforms> platforms)
        {
            string output = "";
            foreach (APIServicesClassLibrary.APIFactory.platforms p in platforms)
            {
                output += p.ToString() + ", ";
            }
            return output;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(link);
        }
    }
}

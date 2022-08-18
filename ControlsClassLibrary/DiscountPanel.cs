using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using APIServicesClassLibrary.Models;

namespace UserControlsLibrary
{
    /// <summary>
    /// Panel for discount offer.
    /// </summary>
    public partial class DiscountPanel : UserControl
    {
        /// <summary>
        /// Link used firstly for image link, then for link for giveaway
        /// </summary>
        string _link = "";
        public string title;
        public DateTime published_date;
        public DateTime end_date;
        public decimal worth;

        /// <summary>
        /// Constructor converting properties from converted model to proper properties in panel.
        /// </summary>
        /// <param name="model">Model implementing IGameGiveawayConvertedModel interface.</param>
        public DiscountPanel(IGameGiveawayConvertedModel model)
        {
            InitializeComponent();
            _link = model.image;
            if (_link != "")
                pictureBox1.Load(_link);
            label1.Text = title = model.title;
            label3.Text = ConvertPlatforms(model.device);
            label5.Text = model.type.ToString();
            label7.Text = model.published_date.ToShortDateString();
            if (model.end_date == DateTime.MinValue)
                label9.Text = "Unknown";
            else
                label9.Text = model.end_date.ToShortDateString();
            label11.Text = "$" + model.worth.ToString();

            _link = model.open_giveaway_url;
            published_date = model.published_date;
            end_date = model.end_date;
            worth = model.worth;
        }

        private string ConvertPlatforms(List<APIServicesClassLibrary.APIFactory.platforms> platforms)
        {
            string output = "";
            foreach (APIServicesClassLibrary.APIFactory.platforms p in platforms)
            {
                if (output == "")
                    output = p.ToString();
                else
                    output += ", " + p.ToString() ;
            }
            return output;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(
                new System.Diagnostics.ProcessStartInfo() 
                { 
                    UseShellExecute = true,
                    FileName = _link 
                }
            );
        }

        public Control Disable()
        {
            button1.Enabled = false;
            button1.BackColor = Color.DarkGray;
            button1.ForeColor = Color.Black;
            foreach(Control co in Controls)
            {
                co.BackColor = Color.Gray;
            }
            Bitmap bmp = new(pictureBox1.Image);

            int width = bmp.Width;
            int height = bmp.Height;

            Color c;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    c = bmp.GetPixel(x, y);

                    int a = c.A;
                    int r = c.R;
                    int g = c.G;
                    int b = c.B;

                    int avg = (r + g + b) / 3;

                    bmp.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));
                }
            }
            pictureBox1.Image = bmp;
            pictureBox1.Refresh();
            return this;
        }
    }
}

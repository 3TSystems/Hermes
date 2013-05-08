using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Device.Location;
using System.Reflection;

namespace Hermes
{
    public partial class Form1 : Form
    {
        GeoCoordinate baseCoord = new GeoCoordinate(49.606950, -97.048447);

        public Form1()
        {
            InitializeComponent();

            textBox1.Text = baseCoord.Latitude.ToString();
            textBox2.Text = baseCoord.Longitude.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text += " v" + Assembly.GetExecutingAssembly().GetName().Version.Major + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GeoCoordinate remoteCoord = new GeoCoordinate();
            double remoteLat, remoteLong, calcDist;

            try
            {
                remoteLat = Convert.ToDouble(textBox4.Text);
                remoteLong = Convert.ToDouble(textBox3.Text);
            }
            catch
            {
                label5.ForeColor = System.Drawing.Color.Black;
                label5.Text = "Error 1";
                return;
            }

            if (Math.Abs(remoteLat) > 180 || Math.Abs(remoteLong) > 180)
            {
                label5.ForeColor = System.Drawing.Color.Black;
                label5.Text = "Error 2";
                return;
            }

            remoteCoord.Latitude = remoteLat;
            remoteCoord.Longitude = remoteLong;

            calcDist = Math.Round((baseCoord.GetDistanceTo(remoteCoord) / 1000), 2);

            if (calcDist < 11) label5.ForeColor = System.Drawing.Color.ForestGreen;
            else if (calcDist > 11 && calcDist < 13) label5.ForeColor = System.Drawing.Color.DarkOrange;
            else label5.ForeColor = System.Drawing.Color.DarkRed;

            label5.Text = calcDist + " km";
        }
    }
}

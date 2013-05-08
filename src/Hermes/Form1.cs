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
        GeoCoordinate baseCoord = new GeoCoordinate();

        public Form1()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text += " v" + Assembly.GetExecutingAssembly().GetName().Version.Major + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GeoCoordinate remoteCoord = new GeoCoordinate();
            double baseLat, baseLong, remoteLat, remoteLong, calcDist;

            try
            {
                baseLat = Convert.ToDouble(textBox1.Text);
                baseLong = Convert.ToDouble(textBox2.Text);
                remoteLat = Convert.ToDouble(textBox4.Text);
                remoteLong = Convert.ToDouble(textBox3.Text);
            }
            catch
            {
                label5.ForeColor = System.Drawing.Color.Black;
                label5.Text = "Error 1";
                return;
            }

            if (Math.Abs(remoteLat) > 90 || Math.Abs(remoteLong) > 180 || Math.Abs(baseLat) > 90 || Math.Abs(baseLong) > 180)
            {
                label5.ForeColor = System.Drawing.Color.Black;
                label5.Text = "Error 2";
                return;
            }

            baseCoord.Latitude = baseLat;
            baseCoord.Longitude = baseLong;
            remoteCoord.Latitude = remoteLat;
            remoteCoord.Longitude = remoteLong;

            calcDist = Math.Round((baseCoord.GetDistanceTo(remoteCoord) / 1000), 2);

            if (calcDist < 11) label5.ForeColor = System.Drawing.Color.ForestGreen;
            else if (calcDist > 11 && calcDist < 13) label5.ForeColor = System.Drawing.Color.DarkOrange;
            else label5.ForeColor = System.Drawing.Color.DarkRed;

            label5.Text = calcDist + " km";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Niverville":
                    baseCoord.Latitude = 49.606950;
                    baseCoord.Longitude = -97.048447;
                    break;
                case "(Custom)":
                    baseCoord.Latitude = 0;
                    baseCoord.Longitude = 0;
                    break;
            }

            if (comboBox1.SelectedItem.ToString().Equals("(Custom)"))
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Enabled = true;
                textBox2.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox1.Text = baseCoord.Latitude.ToString();
                textBox2.Text = baseCoord.Longitude.ToString();
            }
        }
    }
}

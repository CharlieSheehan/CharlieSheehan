using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Working_Graph_Little_Oink
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            temperatureBindingSource.DataSource = new List<Temperature>();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var objChart = chart1.ChartAreas[0];
            objChart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            //month 1-12
            objChart.AxisX.Minimum = 1;
            objChart.AxisX.Maximum = 12;
            //temperature (the class that we creates m1, m2, m3, etc. in
            objChart.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            objChart.AxisY.Minimum = -50;
            objChart.AxisY.Maximum = 50;
            //clear
            chart1.Series.Clear();
            //randomcolor
            Random random = new Random();
            //loop rows
            foreach(Temperature t in temperatureBindingSource.DataSource as List<Temperature>)
            {
                chart1.Series.Add(t.Location);
                chart1.Series[t.Location].Color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                chart1.Series[t.Location].Legend = "Legend1";
                chart1.Series[t.Location].ChartArea = "ChartArea1";
                chart1.Series[t.Location].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                //adding Data
                for (int i = 1; i <= 12; i++)
                    chart1.Series[t.Location].Points.AddXY(i, Convert.ToInt32(t[$"M{i}"]));

            }
        }
    }
}


//The working graph needs a bit of tweaking, reasons are given below:
/// You have to make the program not be able to add a second graph line
/// Have the "Location" button be autofilled by the program e.g. "2022, 2023" or "Week 1, Week 2"
/// Change "Location" to "Week" or "Year"
/// Change the colors of everything in order to create a thing that's cooler looking
/// Stop the colors of the line from being random and have them be set to green
///
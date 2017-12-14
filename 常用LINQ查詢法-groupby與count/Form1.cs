using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace 常用LINQ查詢法_groupby與count
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'northwindDataSet.Customers' 資料表。您可以視需要進行移動或移除。
            this.customersTableAdapter.Fill(this.northwindDataSet.Customers);
            NorthwindEntities dc = new NorthwindEntities();

            var query = (from c in dc.Customers
                         group c by c.Country into CountryGroup
                         select new
                         {
                             國家 = CountryGroup.Key,
                             人數 = CountryGroup.Count()
                         }).OrderByDescending(group => group.人數);
            dataGridView1.DataSource = query.ToArray();
        }

        private void chart1_PostPaint(object sender, System.Windows.Forms.DataVisualization.Charting.ChartPaintEventArgs e)
        {
            foreach(DataPoint p in chart1.Series[0].Points)
            {
                p["Exploded"] = "True";
            }
        }
    }
}

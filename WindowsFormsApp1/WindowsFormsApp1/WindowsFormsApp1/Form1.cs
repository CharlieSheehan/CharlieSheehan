using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadExcel();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateExcel();
        }
        
        private void ReadExcel()
        {
            //System.Data.DataTable dtTablesList = default(System.Data.DataTable);
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("Date", typeof(DateTime));
            dt.Columns.Add("Sales", typeof(double));

            string connetionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='D:\Temp\Sales.xls';Extended Properties='Excel 8.0;HDR=YES'";
            System.Data.OleDb.OleDbConnection salesConn = new System.Data.OleDb.OleDbConnection(connetionString);
            salesConn.Open();
            //dtTablesList = salesConn.GetSchema("Tables");

            System.Data.OleDb.OleDbDataAdapter oda = new System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [Sales$]", salesConn);
            DataSet ds = new DataSet();
            oda.Fill(ds);
            SalesGrid.DataSource = ds.Tables[0];
            salesConn.Close();
            
            chart1.Series["Sales"].XValueMember = "Date";
            chart1.Series["Sales"].YValueMembers = "Sales";
            chart1.DataSource = SalesGrid.DataSource;
            chart1.DataBind();
        }

        private void UpdateExcel()
        {
            if (File.Exists(@"D:\Temp\Sales.xls"))
            {
                File.Delete(@"D:\Temp\Sales.xls");
            }

            string connetionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='D:\Temp\Sales.xls';Extended Properties='Excel 8.0;HDR=YES'";
            System.Data.OleDb.OleDbConnection salesConn = new System.Data.OleDb.OleDbConnection(connetionString);
            salesConn.Open(); System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
            cmd.Connection = salesConn;
            cmd.CommandText = "CREATE TABLE [Sales]([Date] DateTime, Sales Money)";
            cmd.ExecuteNonQuery();

            string newDate = null;
            string newSales = null;
            string newQuery = null;
            foreach (DataGridViewRow row in SalesGrid.Rows)
            {
                newDate = Convert.ToString(row.Cells["Date"].Value);
                newSales = Convert.ToString(row.Cells["Sales"].Value);
                newQuery = String.Format("INSERT INTO [Sales] VALUES ('{0}', {1})", newDate, newSales);
                if (newDate != "")
                {
                    cmd.CommandText = newQuery;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void SalesGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            chart1.DataSource = SalesGrid.DataSource;
            chart1.DataBind();
        }
                
        private void SalesGrid_RowStateChanged(object sender, System.Windows.Forms.DataGridViewRowStateChangedEventArgs e)
        {
            chart1.DataSource = SalesGrid.DataSource;
            chart1.DataBind();
        }

    }
}

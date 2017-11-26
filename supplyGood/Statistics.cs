using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace supplyGood
{
    public enum StatisticMode { Empty, GoodProfit, GoodSelling }
    public partial class Statistics : Form
    {
        StatisticMode _Mode = StatisticMode.Empty;


        public Statistics()
        {
            InitializeComponent();
        }


        private void SetGoodOptions()
        {
            cbxOptions.Visible = true;
            cbxOptions.Items.Add("За последний месяц");
            cbxOptions.Items.Add("За последний квартал");
            cbxOptions.Items.Add("За последний год");
            cbxOptions.Items.Add("За все время");
            cbxOptions.SelectedIndex = 0;
        }
        private void UnsetOptions()
        {
            cbxOptions.Items.Clear();
            cbxOptions.Visible = false;
        }
        private DataTable GetTable()
        {
            switch (_Mode)
            {
                case StatisticMode.GoodSelling:
                    {
                        if (cbxOptions.SelectedIndex == 0)
                        {
                            return ExecuteQuery(@"SELECT Good, Amount FROM GoodSellingByMonth");
                        }
                        else if (cbxOptions.SelectedIndex == 1)
                        {
                            return ExecuteQuery(@"SELECT Good, Amount FROM GoodSellingByQuarter");
                        }
                        else if (cbxOptions.SelectedIndex == 2)
                        {
                            return ExecuteQuery(@"SELECT Good, Amount FROM GoodSellingByYear");
                        }
                        else
                        {
                            return ExecuteQuery(@"SELECT Good, Amount FROM GoodSellingAll");
                        }
                    }
                case StatisticMode.GoodProfit:
                    {
                        if (cbxOptions.SelectedIndex == 0)
                        {
                            return ExecuteQuery(@"SELECT Good, Amount FROM GoodProfitByMonth");
                        }
                        else if (cbxOptions.SelectedIndex == 1)
                        {
                            return ExecuteQuery(@"SELECT Good, Amount FROM GoodProfitByQuarter");
                        }
                        else if (cbxOptions.SelectedIndex == 2)
                        {
                            return ExecuteQuery(@"SELECT Good, Amount FROM GoodProfitByYear");
                        }
                        else
                        {
                            return ExecuteQuery(@"SELECT Good, Amount FROM GoodProfitAll");
                        }
                    }
                default:
                    {
                        return null;
                    }
            }
        }
        private DataTable ExecuteQuery(string query)
        {
            try
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["supplyGood.Properties.Settings.MainDBConnectionString"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(ConnectionString);
                sqlconn.Open();
                SqlDataAdapter oda = new SqlDataAdapter(query, sqlconn);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                sqlconn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        private void GoodSellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnsetOptions();
            SetGoodOptions();
            _Mode = StatisticMode.GoodSelling;

            ShowGoodSelling();
        }
        private void ShowGoodSelling()
        {
            try
            {
                lblCaption.Text = "Самые продаваемые товары";
                string[] header = new string[]
                {
                    "Наименование",
                    "Количество проданных единиц товара"
                };

                DataTable dt = GetTable();
                dgvMain.DataSource = dt;
                dgvMain.Sort(dgvMain.Columns["Amount"], ListSortDirection.Descending);

                for (int i = 0; i < header.Length; i++)
                {
                    dgvMain.Columns[i].HeaderText = header[i];
                }
            }
            catch (Exception ex) { }
        }
        private void GoodProfitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnsetOptions();
            SetGoodOptions();
            _Mode = StatisticMode.GoodProfit;

            ShowGoodProfit();
        }
        private void ShowGoodProfit()
        {
            try
            {
                lblCaption.Text = "Самые прибыльные товары";
                string[] header = new string[]
                {
                    "Наименование",
                    "Общий доход с продаж товара"
                };

                DataTable dt = GetTable();
                dgvMain.DataSource = dt;
                dgvMain.Sort(dgvMain.Columns["Amount"], ListSortDirection.Descending);

                for (int i = 0; i < header.Length; i++)
                {
                    dgvMain.Columns[i].HeaderText = header[i];
                }
            }
            catch (Exception ex) { }
        }
        private void CbxOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (_Mode)
            {
                case StatisticMode.GoodSelling:
                    {
                        ShowGoodSelling();
                        break;
                    }
                case StatisticMode.GoodProfit:
                    {
                        ShowGoodProfit();
                        break;
                    }
            }
        }
        private void Statistics_Load(object sender, EventArgs e)
        {
            GoodSellToolStripMenuItem_Click(null, null);
        }

    }
}

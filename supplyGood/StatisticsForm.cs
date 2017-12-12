using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Word = Microsoft.Office.Interop.Word;

namespace supplyGood
{
    public enum StatisticMode { Empty, GoodProfit, GoodSelling }
    public partial class StatisticsForm : Form
    {
        StatisticMode _Mode = StatisticMode.Empty;
        string _reportPathGoodProfit = Application.StartupPath + @"\reports\report_good_profit.docx";
        string _reportPathGoodSelling = Application.StartupPath + @"\reports\report_good_selling.docx";


        public StatisticsForm()
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
        private double GetTotal(string command, string table, string attr)
        {
            double sum = 0;
            try
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["supplyGood.Properties.Settings.MainDBConnectionString"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(ConnectionString);
                sqlconn.Open();
                SqlCommand comm = new SqlCommand(string.Format(@"SELECT {0}({1}) FROM {2}", command, attr, table), sqlconn);
                sum = Math.Round(Convert.ToDouble(comm.ExecuteScalar()), 2);
                sqlconn.Close();
            }
            catch (Exception ex) { }
            return sum;
        }
        private string GetFormattedNumber(double n, bool hasComa = true)
        {
            string tmp = n.ToString();
            string number = tmp.Contains(",") ? tmp.Split(',')[0] : tmp;
            string res = tmp.Contains(",") ? (tmp.Split(',')[1].Length < 2 ? "," + tmp.Split(',')[1] + "0" : "," + tmp.Split(',')[1]) : (hasComa ? ",00" : "");
            for (int i = number.Length; i > 0; i--)
            {
                if ((number.Length - i) % 3 == 0)
                {
                    res = " " + res;
                }
                res = number[i - 1] + res;
            }
            return res;
        }
        private DataTable GetTable()
        {
            switch (_Mode)
            {
                case StatisticMode.GoodSelling:
                    {
                        string tableName = "GoodSellingByMonth";
                        if (cbxOptions.SelectedIndex == 1)
                        {
                            tableName = "GoodSellingByQuarter";
                        }
                        else if (cbxOptions.SelectedIndex == 2)
                        {
                            tableName = "GoodSellingByYear";
                        }
                        else if (cbxOptions.SelectedIndex == 3)
                        {
                            tableName = "GoodSellingAll";
                        }
                        lblTotal.Text = "Общее количество товара, ед.: " + GetFormattedNumber(GetTotal("SUM", tableName, "Amount"), false);
                        return ExecuteQuery(string.Format(@"SELECT Good, Amount FROM {0}", tableName));
                    }
                case StatisticMode.GoodProfit:
                    {
                        string tableName = "GoodProfitByMonth";
                        if (cbxOptions.SelectedIndex == 1)
                        {
                            tableName = "GoodProfitByQuarter";
                        }
                        else if (cbxOptions.SelectedIndex == 2)
                        {
                            tableName = "GoodProfitByYear";
                        }
                        else if (cbxOptions.SelectedIndex == 3)
                        {
                            tableName = "GoodProfitAll";
                        }
                        lblTotal.Text = "Общая прибыль, грн.: " + GetFormattedNumber(GetTotal("SUM", tableName, "Amount"));
                        return ExecuteQuery(string.Format(@"SELECT Good, Amount FROM {0}", tableName));
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
        private void FormatReport()
        {
            var wordApp = new Word.Application();
            string reportSourcePath = "";
            string reportPath = "";
            try
            {
                switch (_Mode)
                {
                    case StatisticMode.GoodSelling:
                        {
                            reportSourcePath = _reportPathGoodSelling;
                            break;
                        }
                    case StatisticMode.GoodProfit:
                        {
                            reportSourcePath = _reportPathGoodProfit;
                            break;
                        }
                }

                reportPath = 
                    Application.StartupPath + @"\report" +
                    DateTime.Now.Year +
                    DateTime.Now.Month +
                    DateTime.Now.Day +
                    DateTime.Now.Hour +
                    DateTime.Now.Minute +
                    DateTime.Now.Second +
                    ".docx";
                File.Copy(reportSourcePath, reportPath);
                
                wordApp.Visible = false;
                var wordDoc = wordApp.Documents.Open(reportPath);

                switch (_Mode)
                {
                    case StatisticMode.GoodSelling:
                    case StatisticMode.GoodProfit:
                        {
                            ReplaceWordStub("{title}", lblCaption.Text, wordDoc);
                            ReplaceWordStub("{date_to}", DateTime.Today.Date.ToShortDateString(), wordDoc);
                            ReplaceWordStub("{title_total}", lblTotal.Text.Split(':')[0], wordDoc);
                            ReplaceWordStub("{total_val}", lblTotal.Text.Split(':')[1], wordDoc);

                            DateTime today = DateTime.Today.Date;
                            string res = "";
                            if (cbxOptions.SelectedIndex == 0)
                            {
                                res = today.AddMonths(-1).ToShortDateString();
                            }
                            if (cbxOptions.SelectedIndex == 1)
                            {
                                res = today.AddMonths(-3).ToShortDateString();
                            }
                            else if (cbxOptions.SelectedIndex == 2)
                            {
                                res = today.AddMonths(-12).ToShortDateString();
                            }
                            else if (cbxOptions.SelectedIndex == 3)
                            {
                                res = "начало работы";
                            }
                            ReplaceWordStub("{date_from}", res, wordDoc);
                            break;
                        }
                }

                wordDoc.Save();
                
                Word.Table table = wordDoc.Tables[1];

                table.Rows[1].Cells[1].Range.Text = dgvMain.Columns[0].HeaderText;
                table.Rows[1].Cells[2].Range.Text = dgvMain.Columns[1].HeaderText;
                table.Rows.Add();
                for (int i = 0; i < dgvMain.RowCount; i++)
                {
                    table.Rows[i + 2].Cells[1].Range.Text = dgvMain.Rows[i].Cells[0].Value.ToString();
                    table.Rows[i + 2].Cells[2].Range.Text = dgvMain.Rows[i].Cells[1].Value.ToString();
                    if (i != dgvMain.RowCount - 1)
                    {
                        table.Rows.Add();
                    }
                }

                object missing = System.Reflection.Missing.Value;
                wordDoc.Save();
                wordApp.Visible = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Не удалось сформировать отчет" + Environment.NewLine + e.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void ReplaceWordStub(string stub, string text, Word.Document wordDoc)
        {
            var range = wordDoc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stub, ReplaceWith: text);
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
                lblCaption.Text = "Самые популярные товары";
                string[] header = new string[]
                {
                    "Наименование",
                    "Количество проданных единиц товара, ед."
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
                    "Общий доход с продаж товара, грн."
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

        private void btnReport_Click(object sender, EventArgs e)
        {
            FormatReport();
        }
    }
}

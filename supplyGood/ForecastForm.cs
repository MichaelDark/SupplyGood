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
    public partial class ForecastForm : Form
    {
        string _reportPathGoodProfit = Application.StartupPath + @"\reports\report_forecast.docx";

        public ForecastForm()
        {
            InitializeComponent();
        }


        private void SetOptions()
        {
            cbxOptions.Visible = true;
            cbxOptions.Items.Add("за последний месяц");
            cbxOptions.Items.Add("за последний квартал");
            cbxOptions.Items.Add("за последний год");
            cbxOptions.Items.Add("за все время");
            cbxOptions.SelectedIndex = 0;
        }
        private void UnsetOptions()
        {
            cbxOptions.Items.Clear();
            cbxOptions.Visible = false;
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
        private DataTable GetTable()
        {
            int period = -1;

            if (cbxOptions.SelectedIndex == 0)
            {
                period = -1;
            }
            else if (cbxOptions.SelectedIndex == 1)
            {
                period = -3;
            }
            else if (cbxOptions.SelectedIndex == 2)
            {
                period = -12;
            }

            string query = 
                @"SELECT " +
                    @"g.g_name as GoodName, " +
                    @"COALESCE(AVG(con_amount), 0) as AvgAmount , " +
                    @"COALESCE((SELECT SUM(st_amount) FROM Stock WHERE id_good=g.id), 0) as Amount , " +
                    @"COALESCE(ROUND(AVG(con_price), 2), 0) as AvgPrice, " +
                    @"g.g_price as Price " +
                    @"FROM Consignment " + 
                @"JOIN Good g ON Consignment.id_good = g.id " +
                @"JOIN Supply ON Consignment.id_supply = Supply.id " + 
                (cbxOptions.SelectedIndex == cbxOptions.Items.Count - 1 ? "" : "WHERE s_contract >= DATEADD(month, " + period.ToString() + ", CAST(GETDATE() As date)) ") + 
                "GROUP BY g.g_name, g.id, g.g_price ORDER BY AvgAmount DESC";

            return ExecuteQuery(query);
        }
        private void ShowPredict()
        {
            try
            {
                string[] header = new string[]
                {
                    "Наименование",
                    "Предполагаемое кол-во товара на месяц",
                    "Кол-во товара на складах",
                    "Предполагаемая цена товара на месяц",
                    "Текущая цена товара"
                };

                DataTable dt = GetTable();
                dgvMain.DataSource = dt;

                for (int i = 0; i < header.Length; i++)
                {
                    dgvMain.Columns[i].HeaderText = header[i];
                }
                dgvMain.Columns[0].Width = 400;
            }
            catch (Exception ex) { }
        }
        private void CbxOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPredict();
        }
        private void PredictForm_Load(object sender, EventArgs e)
        {
            SetOptions();
        }

        private void FormatReport()
        {
            var wordApp = new Word.Application();
            string reportPath = "";
            try
            {
                reportPath =
                    Application.StartupPath + @"\forecast" + "_" +
                    DateTime.Now.Year + "_" + 
                    DateTime.Now.Month + "_" +
                    DateTime.Now.Day + "_" +
                    DateTime.Now.Hour + "_" +
                    DateTime.Now.Minute + "_" +
                    DateTime.Now.Second +
                    ".docx";
                File.Copy(_reportPathGoodProfit, reportPath);

                wordApp.Visible = false;
                var wordDoc = wordApp.Documents.Open(reportPath);
                
                ReplaceWordStub("{title}", "Запрос на пополнение запасов товаров", wordDoc);
                ReplaceWordStub("{date_to}", DateTime.Today.Date.ToShortDateString(), wordDoc);

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

                wordDoc.Save();

                Word.Table table = wordDoc.Tables[1];
                int cols = dgvMain.ColumnCount;
                int rows = dgvMain.RowCount;
                for (int i = 0; i < cols; i++)
                {
                    table.Rows[1].Cells[i + 1].Range.Text = dgvMain.Columns[i].HeaderText;
                }
                table.Rows[1].Cells[cols + 1].Range.Text = "Кол-во для пополнения запасов";
                table.Rows.Add();
                for (int i = 0, row = 0; i < rows; i++)
                {
                    if (Convert.ToInt32(dgvMain.Rows[i].Cells[2].Value) - Convert.ToInt32(dgvMain.Rows[i].Cells[1].Value) > 0)
                        continue;

                    for (int j = 0; j < cols; j++)
                    {
                        table.Rows[row + 2].Cells[j + 1].Range.Text = dgvMain.Rows[i].Cells[j].Value.ToString();
                    }
                    table.Rows[row + 2].Cells[cols + 1].Range.Text = 
                        Convert.ToInt32(dgvMain.Rows[i].Cells[2].Value) - Convert.ToInt32(dgvMain.Rows[i].Cells[1].Value) >= 0 ? 
                            "0" : 
                            (Convert.ToInt32(dgvMain.Rows[i].Cells[1].Value) - Convert.ToInt32(dgvMain.Rows[i].Cells[2].Value)).ToString();
                    row++;
                    if (i != rows - 1)
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

        private void btnReport_Click(object sender, EventArgs e)
        {
            FormatReport();
        }
    }
}

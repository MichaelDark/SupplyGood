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
    public partial class PredictForm : Form
    {
        public PredictForm()
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
            string tableName = "PredictMonth";
            if (cbxOptions.SelectedIndex == 1)
            {
                tableName = "PredictQuarter";
            }
            else if (cbxOptions.SelectedIndex == 2)
            {
                tableName = "PredictYear";
            }
            else if (cbxOptions.SelectedIndex == 3)
            {
                tableName = "PredictAll";
            }
            return ExecuteQuery(string.Format(@"SELECT Good, AvgAmount, AvgPrice FROM {0} ORDER BY Good DESC", tableName));
        }
        private void ShowPredict()
        {
            try
            {
                string[] header = new string[]
                {
                    "Наименование",
                    "Предполагаемое кол-во товара на месяц",
                    "Предполагаемая цена товара на месяц",
                };

                DataTable dt = GetTable();
                dgvMain.DataSource = dt;

                for (int i = 0; i < header.Length; i++)
                {
                    dgvMain.Columns[i].HeaderText = header[i];
                }
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using supplyGood.MainDBDataSetTableAdapters;

namespace supplyGood
{
    public partial class ChooseGoodForm : Form
    {
        bool IsDataValid;
        int ID;
        int ID_Good { get { return Convert.ToInt32(cbxGood.Text.Split(',')[0].Substring(1)); } }
        int Amount;
        double Price;
        bool IsConsignment { get { return txtPrice.Enabled; } }

        public ChooseGoodForm(int id, bool isConsignment=true)
        {
            ID = id;
            InitializeComponent();
            txtPrice.Enabled = isConsignment;
            IsDataValid = false;
        }

        private void LoadInfo()
        {
            string myConnectionString = ConfigurationManager.ConnectionStrings["supplyGood.Properties.Settings.MainDBConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(myConnectionString);
            myConnection.Open();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand;
                if (IsConsignment)
                {
                    myCommand = new SqlCommand(string.Format("SELECT * FROM [Good] WHERE id NOT IN (SELECT id_good FROM Consignment WHERE id_supply='{0}')",
                    ID.ToString()),
                    myConnection);
                }
                else
                {
                    myCommand = new SqlCommand(string.Format("SELECT * FROM [Good] WHERE id NOT IN (SELECT id_good FROM Stock WHERE id_storage='{0}')",
                    ID.ToString()),
                    myConnection);
                }
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    cbxGood.Items.Add("#" + myReader["id"].ToString() + ", " + myReader["g_name"].ToString());
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            myConnection.Close();
        }
        private void LoadAdditionInfo()
        {
            string myConnectionString = ConfigurationManager.ConnectionStrings["supplyGood.Properties.Settings.MainDBConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(myConnectionString);
            myConnection.Open();
            try
            {
                SqlDataReader myReader = null;
                string query = string.Format("SELECT * FROM [Good] WHERE id='{0}'", ID_Good.ToString());
                SqlCommand myCommand = new SqlCommand(query, myConnection);
                myReader = myCommand.ExecuteReader();
                myReader.Read();
                txtPrice.Text = myReader["g_price"].ToString();
                myReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            myConnection.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            IsDataValid = false;
            try
            {
                Amount = Convert.ToInt32(txtAmount.Text);
                Price = Convert.ToDouble(txtPrice.Text);
                IsDataValid = true;
                if (IsConsignment)
                {
                    consignmentTableAdapter.Insert(ID, ID_Good, Amount, (float)Price);
                }
                else
                {
                    stockTableAdapter.Insert(ID, ID_Good, Amount);
                }
                MessageBox.Show("Товар добавлен", "Добавление товара", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChooseGoodForm_Load(object sender, EventArgs e)
        {
            LoadInfo();
            cbxGood.SelectedIndex = 0;
        }

        private void cbxGood_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAdditionInfo();
        }
    }
}

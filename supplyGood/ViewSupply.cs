using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace supplyGood
{
    public partial class ViewSupply : Form
    {
        int _ID = -1;
        int _ID_CLIENT = -1;
        int _ID_CAR = -1;
        int _ID_STORAGE = -1;
        SubFormMode _Mode;
        List<TextBox> _Fields;
        bool HasObligatoryNullFields => _Fields.Exists(x => (String.IsNullOrEmpty(x.Text) || String.IsNullOrWhiteSpace(x.Text)));
        string SumForSupply
        {
            get
            {
                double? d = (double?)consignmentTableAdapter.SumBySupply(_ID);
                if (d == null)
                    return "-";
                return Math.Round((double)d, 2).ToString();
            }
        }
        int GetIDByAllAttributes
        {
            get
            {
                int? id = (int?)supplyTableAdapter.GetIDByAllAttributes(
                    GetID(cbxClient.Text),
                    GetID(cbxCar.Text),
                    GetID(cbxStorage.Text),
                    txtAddress.Text,
                    dateContract.Value,
                    Convert.ToInt32(txtPeriod.Text),
                    cbxDelivered.Checked,
                    cbxShipped.Checked);
                if (id == null)
                    return -1;
                return (int)id;
            }
        }


        public ViewSupply()
        {
            InitializeComponent();

            _Mode = SubFormMode.Add;


            _Fields = new List<TextBox>();
            _Fields.Add(txtAddress);
            _Fields.Add(txtPeriod);
        }
        public ViewSupply(int ID, SubFormMode Mode)
        {
            InitializeComponent();

            _ID = ID;
            _Mode = Mode;
            dgvGoods.Columns[0].ReadOnly = true;

            _Fields = new List<TextBox>();
            _Fields.Add(txtAddress);
            _Fields.Add(txtPeriod);
        }

        private void ViewSupply_Load(object sender, EventArgs e)
        {
            this.consignmentTableAdapter.Fill(this.mainDBDataSet.Consignment);
            if (_Mode != SubFormMode.Add)
            {
                LoadInfo();
            }
            LoadClient();
            LoadCars();
            LoadStorages();
            RefreshSum();
            SetMode();
            if (_Mode != SubFormMode.Add)
            {
                consignmentBindingSource.Filter = "id_supply = " + _ID;
            }
        }
        private void ViewSupply_FormClosing(object sender, FormClosingEventArgs e)
        {
            consignmentTableAdapter.Update(mainDBDataSet.Consignment);
        }

        private void SetMode()
        {
            bool state = _Mode == SubFormMode.Edit || _Mode == SubFormMode.Add;
            foreach (TextBox c in _Fields)
            {
                c.ReadOnly = !state;
            }
            cbxClient.Enabled = state;
            cbxCar.Enabled = state;
            cbxStorage.Enabled = state;

            dateContract.Enabled = state;

            cbxShipped.Enabled = state;
            cbxDelivered.Enabled = state;

            if (_Mode == SubFormMode.Add)
            {
                btnSave.Text = "Добавить";
                lblTitle.Text = "Добавление поставки";
                Text = "Добавление - Поставка";
                dgvGoods.Enabled = false;
                Width = 440;
            }
            else if (_Mode == SubFormMode.View)
            {
                btnSave.Text = "Редактировать";
                lblTitle.Text = "Поставка #" + _ID;
                Text = "Просмотр - Поставка #" + _ID;
                dgvGoods.Enabled = true;
                Width = 1040;
            }
            else if (_Mode == SubFormMode.Edit)
            {
                btnSave.Text = "Сохранить";
                lblTitle.Text = "Редактирование Поставки #" + _ID;
                Text = "Редактирование - Поставка #" + _ID;
                dgvGoods.Enabled = true;
                Width = 1040;
            }
        }
        private void RefreshSum()
        {
            lblTotalSum.Text = "Общая сумма: " + SumForSupply;
        }
        private void LoadInfo()
        {
            string myConnectionString = ConfigurationManager.ConnectionStrings["supplyGood.Properties.Settings.MainDBConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(myConnectionString);
            myConnection.Open();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(
                    string.Format("SELECT * FROM [Supply] WHERE id = {0}",
                    _ID.ToString()),
                    myConnection);
                myReader = myCommand.ExecuteReader();
                myReader.Read();

                txtAddress.Text = myReader["s_address"].ToString();
                txtPeriod.Text = myReader["s_period"].ToString();

                dateContract.Value = DateTime.Parse(myReader["s_contract"].ToString());

                _ID_CLIENT = Convert.ToInt32(myReader["id_client"]);
                _ID_CAR = Convert.ToInt32(myReader["id_car"]);
                _ID_STORAGE = Convert.ToInt32(myReader["id_storage"]);

                cbxShipped.Checked = Convert.ToBoolean(myReader["s_shipped"]);
                cbxDelivered.Checked = Convert.ToBoolean(myReader["s_delivered"]);

                myReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            myConnection.Close();
        }
        private void LoadClient()
        {
            string myConnectionString = ConfigurationManager.ConnectionStrings["supplyGood.Properties.Settings.MainDBConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(myConnectionString);
            myConnection.Open();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(
                    "SELECT * FROM [Client]",
                    myConnection);
                myReader = myCommand.ExecuteReader();

                cbxClient.Items.Clear();

                string currentEmp = null;
                while (myReader.Read())
                {
                    cbxClient.Items.Add("#" + myReader["id"].ToString() + ", " + myReader["cl_company"].ToString() + ", " + myReader["cl_surname"].ToString() + " " + myReader["cl_name"].ToString());
                    if (myReader["id"].ToString() == _ID_CLIENT.ToString())
                        currentEmp = "#" + myReader["id"].ToString() + ", " + myReader["cl_company"].ToString() + ", " + myReader["cl_surname"].ToString() + " " + myReader["cl_name"].ToString();

                }
                if (currentEmp == null)
                {
                    cbxClient.SelectedIndex = 0;
                    _ID_CLIENT = GetID(cbxClient.Text);
                }
                else
                {
                    cbxClient.SelectedIndex = cbxClient.Items.IndexOf(currentEmp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            myConnection.Close();
        }
        private void LoadCars()
        {
            string myConnectionString = ConfigurationManager.ConnectionStrings["supplyGood.Properties.Settings.MainDBConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(myConnectionString);
            myConnection.Open();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(
                    "SELECT * FROM [Car] WHERE id_driver IS NOT NULL",
                    myConnection);
                myReader = myCommand.ExecuteReader();

                cbxCar.Items.Clear();

                string currentCar = null;
                while (myReader.Read())
                {
                    cbxCar.Items.Add("#" + myReader["id"].ToString() + ", " + myReader["car_model"].ToString() + " " + myReader["car_number"].ToString());
                    if (myReader["id"].ToString() == _ID_CLIENT.ToString())
                        currentCar = "#" + myReader["id"].ToString() + ", " + myReader["car_model"].ToString() + " " + myReader["car_number"].ToString();

                }
                if (currentCar == null)
                {
                    cbxCar.SelectedIndex = 0;
                    _ID_CAR = GetID(cbxCar.Text);
                }
                else
                {
                    cbxCar.SelectedIndex = cbxCar.Items.IndexOf(currentCar);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            myConnection.Close();
        }
        private void LoadStorages()
        {
            string myConnectionString = ConfigurationManager.ConnectionStrings["supplyGood.Properties.Settings.MainDBConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(myConnectionString);
            myConnection.Open();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(
                    "SELECT * FROM [Storage] WHERE id_storekeeper IS NOT NULL",
                    myConnection);
                myReader = myCommand.ExecuteReader();

                cbxStorage.Items.Clear();

                string currentStorage = null;
                while (myReader.Read())
                {
                    cbxStorage.Items.Add("#" + myReader["id"].ToString() + ", " + myReader["stor_address"].ToString());
                    if (myReader["id"].ToString() == _ID_CLIENT.ToString())
                        currentStorage = "#" + myReader["id"].ToString() + ", " + myReader["stor_address"].ToString();

                }
                if (currentStorage == null)
                {
                    cbxStorage.SelectedIndex = 0;
                    _ID_STORAGE = GetID(cbxStorage.Text);
                }
                else
                {
                    cbxStorage.SelectedIndex = cbxStorage.Items.IndexOf(currentStorage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            myConnection.Close();
        }
        private int GetID(string text)
        {
            text = text.Split(',')[0];
            return Convert.ToInt32(text.Remove(0, 1).Trim());
        }

        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_Mode == SubFormMode.Add)
            {
                if (HasObligatoryNullFields)
                {
                    MessageBox.Show("Заполните все обязательные поля (помечены звездочкой - *)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                supplyTableAdapter.InsertQuery(
                    GetID(cbxClient.Text),
                    GetID(cbxCar.Text),
                    GetID(cbxStorage.Text),
                    txtAddress.Text,
                    dateContract.Value.Date,
                    Convert.ToInt32(txtPeriod.Text),
                    cbxDelivered.Checked,
                    cbxShipped.Checked);

                _ID = GetIDByAllAttributes;

                _Mode = SubFormMode.View;
            }
            else if(_Mode == SubFormMode.Edit)
            {
                if (HasObligatoryNullFields)
                {
                    MessageBox.Show("Заполните все обязательные поля (помечены звездочкой - *)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                supplyTableAdapter.UpdateQuery(
                    GetID(cbxClient.Text),
                    GetID(cbxCar.Text),
                    GetID(cbxStorage.Text),
                    txtAddress.Text,
                    dateContract.Value.Date,
                    Convert.ToInt32(txtPeriod.Text),
                    cbxDelivered.Checked,
                    cbxShipped.Checked,
                    _ID);

                _Mode = SubFormMode.View;
            }
            else
            {
                _Mode = SubFormMode.Edit;
            }

            LoadClient();
            LoadCars();
            LoadStorages();
            SetMode();
        }

        private void dgvGoods_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[0].Value = _ID;
        }
        private void dgvGoods_SelectionChanged(object sender, EventArgs e)
        {
            RefreshSum();
        }

    }
}

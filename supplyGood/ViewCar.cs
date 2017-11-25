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
    public partial class ViewCar : Form
    {
        int _ID = -1;
        int _ID_DRIVER = -1;
        SubFormMode _Mode;
        List<TextBox> _Fields;
        bool HasObligatoryNullFields => _Fields.Exists(x => (String.IsNullOrEmpty(x.Text) || String.IsNullOrWhiteSpace(x.Text)) && x.Name != "txtChildren" && x.Name != "txtFamily" && x.Name != "txtConvictions");


        public ViewCar()
        {
            InitializeComponent();

            _Mode = SubFormMode.Add;

            _Fields = new List<TextBox>();
            _Fields.Add(txtGov);
            _Fields.Add(txtColor);
            _Fields.Add(txtModel);
        }
        public ViewCar(int ID, SubFormMode Mode)
        {
            InitializeComponent();

            _ID = ID;
            _Mode = Mode;

            _Fields = new List<TextBox>();
            _Fields.Add(txtGov);
            _Fields.Add(txtColor);
            _Fields.Add(txtModel);
        }


        private void SetMode()
        {
            bool state = _Mode == SubFormMode.Edit || _Mode == SubFormMode.Add;
            foreach (TextBox c in _Fields)
            {
                c.ReadOnly = !state;
            }
            cbxEmployee.Enabled = state;

            if (_Mode == SubFormMode.Add)
            {
                btnSave.Text = "Добавить";
                lblTitle.Text = "Добавление машины";
                Text = "Добавление - Машина";
            }
            else if (_Mode == SubFormMode.View)
            {
                btnSave.Text = "Редактировать";
                lblTitle.Text = "Информация о машине #" + _ID;
                Text = "Просмотр - Машина #" + _ID;
            }
            else if (_Mode == SubFormMode.Edit)
            {
                btnSave.Text = "Сохранить";
                lblTitle.Text = "Редактирование машины #" + _ID;
                Text = "Редактирование - Машина #" + _ID;
            }
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
                    string.Format("SELECT * FROM [Car] WHERE id = {0}",
                    _ID.ToString()),
                    myConnection);
                myReader = myCommand.ExecuteReader();
                myReader.Read();
                txtGov.Text = myReader["car_number"].ToString();
                txtModel.Text = myReader["car_model"].ToString();
                txtColor.Text = myReader["car_color"].ToString();
                if (!String.IsNullOrEmpty(myReader["id_driver"].ToString()))
                    _ID_DRIVER = Convert.ToInt32(myReader["id_driver"]);
                myReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            myConnection.Close();
        }
        private void LoadEmployees()
        {
            string myConnectionString = ConfigurationManager.ConnectionStrings["supplyGood.Properties.Settings.MainDBConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(myConnectionString);
            myConnection.Open();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(
                    "SELECT * FROM [Employee] WHERE em_discharge IS NULL",
                    myConnection);
                myReader = myCommand.ExecuteReader();

                cbxEmployee.Items.Clear();

                string currentEmp = "Нет закрепленного сотрудника";
                cbxEmployee.Items.Add(currentEmp);

                while (myReader.Read())
                {
                    cbxEmployee.Items.Add("#" + myReader["id"].ToString() + ", " + myReader["em_surname"].ToString() + " " + myReader["em_name"].ToString());
                    if (myReader["id"].ToString() == _ID_DRIVER.ToString())
                        currentEmp = "#" + myReader["id"].ToString() + ", " + myReader["em_surname"].ToString() + " " + myReader["em_name"].ToString();

                }
                cbxEmployee.SelectedIndex = cbxEmployee.Items.IndexOf(currentEmp);
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


        private void ViewCar_Load(object sender, EventArgs e)
        {
            if (_Mode != SubFormMode.Add)
            {
                LoadInfo();
            }
            LoadEmployees();
            SetMode();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_Mode == SubFormMode.Add || _Mode == SubFormMode.Edit)
            {
                if (HasObligatoryNullFields)
                {
                    MessageBox.Show("Заполните все обязательные поля (помечены звездочкой - *)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int? driverID;

                if (cbxEmployee.Text == "Нет закрепленного сотрудника")
                {
                    driverID = null;
                }
                else
                {
                    driverID = GetID(cbxEmployee.Text);
                }

                _ID_DRIVER = driverID == null ? -1 : (int)driverID;

                if (_Mode == SubFormMode.Add)
                {
                    carTableAdapter.InsertQuery(driverID,
                        txtGov.Text,
                        txtModel.Text,
                        txtColor.Text);
                    Close();
                }
                else
                {
                    carTableAdapter.UpdateQuery(driverID,
                        txtGov.Text,
                        txtModel.Text,
                        txtColor.Text,
                        _ID);
                }

                _Mode = SubFormMode.View;
            }
            else
            {
                _Mode = SubFormMode.Edit;
            }

            LoadEmployees();
            SetMode();
        }


    }
}

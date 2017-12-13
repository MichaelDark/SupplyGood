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
    public partial class ViewClient : Form
    {
        int _ID = -1;
        SubFormMode _Mode;
        List<TextBox> _Fields;
        bool HasObligatoryNullFields => _Fields.Exists(x => (String.IsNullOrEmpty(x.Text) || String.IsNullOrWhiteSpace(x.Text)) && x.Name != "txtChildren" && x.Name != "txtFamily" && x.Name != "txtConvictions");


        public ViewClient(int ID, SubFormMode Mode)
        {
            InitializeComponent();

            _ID = ID;
            _Mode = Mode;

            _Fields = new List<TextBox>();
            _Fields.Add(txtSurname);
            _Fields.Add(txtName);
            _Fields.Add(txtPatron);

            _Fields.Add(txtCompany);
            _Fields.Add(txtAddress);
            _Fields.Add(txtContacts);
        }
        public ViewClient()
        {
            InitializeComponent();

            _Mode = SubFormMode.Add;

            _Fields = new List<TextBox>();
            _Fields.Add(txtSurname);
            _Fields.Add(txtName);
            _Fields.Add(txtPatron);

            _Fields.Add(txtCompany);
            _Fields.Add(txtAddress);
            _Fields.Add(txtContacts);
        }


        private void SetMode()
        {
            bool state = _Mode == SubFormMode.Edit || _Mode == SubFormMode.Add;
            foreach (TextBox c in _Fields)
            {
                c.ReadOnly = !state;
            }

            if (_Mode == SubFormMode.Add)
            {
                btnSave.Text = "Добавить";
                lblTitle.Text = "Добавление заказчика";
                Text = "Добавление - Заказчики";
            }
            else if (_Mode == SubFormMode.View)
            {
                btnSave.Text = "Редактировать";
                lblTitle.Text = "Информация о заказчике";
                Text = "Просмотр - Заказчики";
            }
            else if (_Mode == SubFormMode.Edit)
            {
                btnSave.Text = "Сохранить";
                lblTitle.Text = "Редактирование заказчика";
                Text = "Редактирование - Заказчики";
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
                    string.Format("SELECT * FROM [Client] WHERE id = {0}",
                    _ID.ToString()),
                    myConnection);
                myReader = myCommand.ExecuteReader();
                myReader.Read();
                txtSurname.Text = myReader["cl_surname"].ToString();
                txtName.Text = myReader["cl_name"].ToString();
                txtPatron.Text = myReader["cl_patron"].ToString();
                txtCompany.Text = myReader["cl_company"].ToString();
                txtContacts.Text = myReader["cl_contacts"].ToString();
                txtAddress.Text = myReader["cl_address"].ToString();
                myReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            myConnection.Close();
        }


        private void ViewEmployee_Load(object sender, EventArgs e)
        {
            if (_Mode != SubFormMode.Add)
            {
                LoadInfo();
            }
            SetMode();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool close = false;
            if (_Mode == SubFormMode.Add || _Mode == SubFormMode.Edit)
            {
                if (HasObligatoryNullFields)
                {
                    MessageBox.Show("Заполните все обязательные поля (помечены звездочкой - *)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _Mode = SubFormMode.View;
                
                if (_Mode != SubFormMode.Add)
                {
                    clientTableAdapter.UpdateQuery(
                        txtCompany.Text,
                        txtAddress.Text,
                        txtSurname.Text,
                        txtName.Text,
                        txtPatron.Text,
                        txtContacts.Text,
                        _ID);
                }
                else
                {
                    clientTableAdapter.InsertQuery(
                        txtCompany.Text,
                        txtAddress.Text,
                        txtSurname.Text,
                        txtName.Text,
                        txtPatron.Text,
                        txtContacts.Text);
                    close = true;
                }
            }
            else
            {
                _Mode = SubFormMode.Edit;
            }

            if (close)
            {
                Close();
            }

            SetMode();
        }
    }
}

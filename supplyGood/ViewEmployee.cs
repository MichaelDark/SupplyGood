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
    public partial class ViewEmployee : Form
    {
        int _ID = -1;
        SubFormMode _Mode;
        List<TextBox> _Fields;
        bool HasObligatoryNullFields => _Fields.Exists(x => (String.IsNullOrEmpty(x.Text) || String.IsNullOrWhiteSpace(x.Text)) && x.Name != "txtChildren" && x.Name != "txtFamily" && x.Name != "txtConvictions");


        public ViewEmployee(int ID, SubFormMode Mode)
        {
            InitializeComponent();

            _ID = ID;
            _Mode = Mode;

            _Fields = new List<TextBox>();
            _Fields.Add(txtSurname);
            _Fields.Add(txtName);
            _Fields.Add(txtPatron);
            _Fields.Add(txtSalary);

            _Fields.Add(txtConvictions);
            _Fields.Add(txtFamily);
            _Fields.Add(txtChildren);
        }
        public ViewEmployee()
        {
            InitializeComponent();
            
            _Mode = SubFormMode.Add;

            _Fields = new List<TextBox>();
            _Fields.Add(txtSurname);
            _Fields.Add(txtName);
            _Fields.Add(txtPatron);
            _Fields.Add(txtSalary);

            _Fields.Add(txtConvictions);
            _Fields.Add(txtFamily);
            _Fields.Add(txtChildren);
        }


        private void SetMode()
        {
            bool state = _Mode == SubFormMode.Edit || _Mode == SubFormMode.Add;
            foreach (TextBox c in _Fields)
            {
                c.ReadOnly = !state;
            }
            dateIn.Enabled = state;
            dateOut.Enabled = state;
            cbxDischarged.Enabled = state;

            if (_Mode == SubFormMode.Add)
            {
                btnSave.Text = "Добавить";
                lblTitle.Text = "Добавление сотрудника";
                Text = "Добавление - Сотрудники";
            }
            else if (_Mode == SubFormMode.View)
            {
                btnSave.Text = "Редактировать";
                lblTitle.Text = "Информация о сотруднике";
                Text = "Просмотр - Сотрудники";
            }
            else if (_Mode == SubFormMode.Edit)
            {
                btnSave.Text = "Сохранить";
                lblTitle.Text = "Редактирование информации";
                Text = "Редактирование - Сотрудники";
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
                    string.Format("SELECT * FROM [Employee] WHERE id = {0}", 
                    _ID.ToString()), 
                    myConnection);
                myReader = myCommand.ExecuteReader();
                myReader.Read();
                txtName.Text = myReader["em_name"].ToString();
                txtSurname.Text = myReader["em_surname"].ToString();
                txtPatron.Text = myReader["em_patron"].ToString();
                dateIn.Value = DateTime.Parse(myReader["em_acceptance"].ToString());
                var tmp = myReader["em_discharge"].ToString();
                if (String.IsNullOrEmpty(tmp))
                {
                    cbxDischarged.Checked = false;
                }
                else
                {
                    dateOut.Value = DateTime.Parse(tmp);
                }
                txtSalary.Text = myReader["em_salary"].ToString();
                myReader.Close();

                myReader = null;
                myCommand = new SqlCommand(
                    string.Format("SELECT * FROM [PersonalInfo] WHERE id_employee = {0}",
                    _ID.ToString()),
                    myConnection);
                myReader = myCommand.ExecuteReader();
                if (myReader.HasRows)
                {
                    myReader.Read();
                    txtConvictions.Text = myReader["p_convictions"].ToString();
                    txtFamily.Text = myReader["p_family"].ToString();
                    txtChildren.Text = myReader["p_children"].ToString();
                }
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
                if(HasObligatoryNullFields)
                {
                    MessageBox.Show("Заполните все обязательные поля (помечены звездочкой - *)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _Mode = SubFormMode.View;

                string dateO;
                if (cbxDischarged.Checked)
                {
                    dateO = dateOut.Value.Date.ToShortDateString();
                }
                else
                {
                    dateO = null;
                }

                if ((int)employeeTableAdapter.ExistsQuery(_ID) > 0 && _Mode != SubFormMode.Add)
                {
                    employeeTableAdapter.UpdateQuery(
                        txtSurname.Text,
                        txtName.Text,
                        txtPatron.Text,
                        dateIn.Value.Date.ToShortDateString(),
                        dateO,
                        (float)Convert.ToDouble(txtSalary.Text),
                        _ID);

                    if (cbxDischarged.Checked)
                    {
                        string ConnectionString = ConfigurationManager.ConnectionStrings["supplyGood.Properties.Settings.MainDBConnectionString"].ConnectionString;
                        SqlConnection sqlconn = new SqlConnection(ConnectionString);
                        sqlconn.Open();
                        SqlCommand oda = new SqlCommand("UPDATE Storage SET id_storekeeper=null WHERE id_storekeeper=" + _ID.ToString(), sqlconn);
                        oda.ExecuteNonQuery();
                        oda = new SqlCommand("UPDATE Car SET id_driver=null WHERE id_driver=" + _ID.ToString(), sqlconn);
                        oda.ExecuteNonQuery();
                        sqlconn.Close();
                    }
                }
                else
                {
                    employeeTableAdapter.InsertQuery(
                           txtSurname.Text,
                           txtName.Text,
                           txtPatron.Text,
                           dateIn.Value.Date.ToShortDateString(),
                           dateO,
                           (float)Convert.ToDouble(txtSalary.Text));
                    string ConnectionString = ConfigurationManager.ConnectionStrings["supplyGood.Properties.Settings.MainDBConnectionString"].ConnectionString;
                    SqlConnection sqlconn = new SqlConnection(ConnectionString);
                    sqlconn.Open();
                    SqlCommand oda = new SqlCommand("SELECT TOP 1 id FROM Employee ORDER BY id DESC", sqlconn);
                    _ID = Convert.ToInt32(oda.ExecuteScalar());
                    sqlconn.Close();
                    close = true;
                }
                if ((int)personalInfoTableAdapter.ExistsQuery(_ID) > 0 && _Mode != SubFormMode.Add)
                {
                    personalInfoTableAdapter.UpdateQuery(
                        txtFamily.Text,
                        txtChildren.Text,
                        txtConvictions.Text,
                        _ID);
                }
                else
                {
                    personalInfoTableAdapter.Insert(
                       _ID,
                       txtFamily.Text,
                       txtChildren.Text,
                       txtConvictions.Text);
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
        private void cbxDischarged_CheckedChanged(object sender, EventArgs e)
        {
            dateOut.Visible = cbxDischarged.Checked;
        }
    }
}

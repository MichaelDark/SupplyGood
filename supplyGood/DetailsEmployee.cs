﻿using System;
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
    public enum DetailsEmployeeMode { View, Edit, Add}
    public partial class DetailsEmployee : Form
    {
        int _ID = -1;
        DetailsEmployeeMode _Mode;
        List<TextBox> _Fields;
        bool HasObligatoryNullFields => _Fields.Exists(
            x => (String.IsNullOrEmpty(x.Text) || String.IsNullOrWhiteSpace(x.Text)) && x.Name != "txtChildren" && x.Name != "txtFamily" && x.Name != "txtConvictions");

        public DetailsEmployee(int ID, DetailsEmployeeMode Mode)
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
        public DetailsEmployee()
        {
            InitializeComponent();
            
            _Mode = DetailsEmployeeMode.Add;

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
            bool state = _Mode == DetailsEmployeeMode.Edit || _Mode == DetailsEmployeeMode.Add;
            foreach (TextBox c in _Fields)
            {
                c.ReadOnly = !state;
            }
            dateIn.Enabled = state;
            dateOut.Enabled = state;
            cbxDischarged.Enabled = state;

            if (_Mode == DetailsEmployeeMode.Add)
            {
                btnSave.Text = "Добавить";
            }
            else if (_Mode == DetailsEmployeeMode.View)
            {
                btnSave.Text = "Редактировать";
            }
            else if (_Mode == DetailsEmployeeMode.Edit)
            {
                btnSave.Text = "Сохранить";
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


        private void DetailsEmployee_Load(object sender, EventArgs e)
        {
            if (_Mode != DetailsEmployeeMode.Add)
            {
                LoadInfo();
            }
            SetMode();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_Mode == DetailsEmployeeMode.Add || _Mode == DetailsEmployeeMode.Edit)
            {
                if(HasObligatoryNullFields)
                {
                    MessageBox.Show("Заполните все обязательные поля (помечены звездочкой - *)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _Mode = DetailsEmployeeMode.View;

                DateTime? dateO;
                if (cbxDischarged.Checked)
                {
                    dateO = dateOut.Value.Date;
                }
                else
                {
                    dateO = null;
                }

                if ((int)employeeTableAdapter.ExistsQuery(_ID) > 0 && _Mode != DetailsEmployeeMode.Add)
                {
                    employeeTableAdapter.UpdateQuery(
                        txtSurname.Text,
                        txtName.Text,
                        txtPatron.Text,
                        dateIn.Value.Date,
                        dateO,
                        (float)Convert.ToDouble(txtSalary.Text),
                        _ID);
                }
                else
                {
                    employeeTableAdapter.Insert(
                           txtSurname.Text,
                           txtName.Text,
                           txtPatron.Text,
                           dateIn.Value,
                           dateO,
                           (float)Convert.ToDouble(txtSalary.Text));
                    string ConnectionString = ConfigurationManager.ConnectionStrings["supplyGood.Properties.Settings.MainDBConnectionString"].ConnectionString;
                    SqlConnection sqlconn = new SqlConnection(ConnectionString);
                    sqlconn.Open();
                    SqlCommand oda = new SqlCommand("SELECT TOP 1 id FROM Employee ORDER BY id DESC", sqlconn);
                    _ID = Convert.ToInt32(oda.ExecuteScalar());
                    sqlconn.Close();
                }
                if ((int)personalInfoTableAdapter.ExistsQuery(_ID) > 0 && _Mode != DetailsEmployeeMode.Add)
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
                _Mode = DetailsEmployeeMode.Edit;
            }

            SetMode();
        }

        private void cbxDischarged_CheckedChanged(object sender, EventArgs e)
        {
            dateOut.Visible = cbxDischarged.Checked;
        }
    }
}

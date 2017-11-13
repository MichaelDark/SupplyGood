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
    public partial class ViewGood : Form
    {
        int _ID = -1;
        SubFormMode _Mode;
        List<TextBox> _Fields;
        bool HasObligatoryNullFields => _Fields.Exists(x => String.IsNullOrEmpty(x.Text) || String.IsNullOrWhiteSpace(x.Text));


        public ViewGood(int ID, SubFormMode Mode)
        {
            InitializeComponent();

            _ID = ID;
            _Mode = Mode;

            _Fields = new List<TextBox>();
            _Fields.Add(txtName);
            _Fields.Add(txtUnit);
            _Fields.Add(txtPrice);
        }
        public ViewGood()
        {
            InitializeComponent();

            _Mode = SubFormMode.Add;

            _Fields = new List<TextBox>();
            _Fields.Add(txtName);
            _Fields.Add(txtUnit);
            _Fields.Add(txtPrice);
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
                lblTitle.Text = "Добавление товара";
                Text = "Добавление - Товар";
            }
            else if (_Mode == SubFormMode.View)
            {
                btnSave.Text = "Редактировать";
                lblTitle.Text = "Информация о товаре";
                Text = "Просмотр - товар";
            }
            else if (_Mode == SubFormMode.Edit)
            {
                btnSave.Text = "Сохранить";
                lblTitle.Text = "Редактирование информации";
                Text = "Редактирование - Товар";
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
                    string.Format("SELECT * FROM [Good] WHERE id = {0}",
                    _ID.ToString()),
                    myConnection);
                myReader = myCommand.ExecuteReader();
                myReader.Read();
                txtName.Text = myReader["g_name"].ToString();
                txtUnit.Text = myReader["g_unit"].ToString();
                txtPrice.Text = myReader["g_price"].ToString();
                myReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            myConnection.Close();
        }

        private void ViewGood_Load(object sender, EventArgs e)
        {
            if (_Mode != SubFormMode.Add)
            {
                LoadInfo();
            }
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
                _Mode = SubFormMode.View;

                if ((int)goodTableAdapter.ExistsQuery(_ID) > 0 && _Mode != SubFormMode.Add)
                {
                    goodTableAdapter.UpdateQuery(
                        txtName.Text,
                        txtUnit.Text,
                        (float)Convert.ToDouble(txtPrice.Text),
                        _ID);
                }
                else
                {
                    goodTableAdapter.Insert(
                        txtName.Text,
                        txtUnit.Text,
                        (float)Convert.ToDouble(txtPrice.Text));
                    Close();
                }
            }
            else
            {
                _Mode = SubFormMode.Edit;
            }

            SetMode();
        }
    }
}

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
    public partial class ViewStorage : Form
    {
        int _ID = -1;
        int _ID_STOREKEEPER = -1;
        SubFormMode _Mode;
        List<TextBox> _Fields;
        bool HasObligatoryNullFields => _Fields.Exists(x => (String.IsNullOrEmpty(x.Text) || String.IsNullOrWhiteSpace(x.Text)));
        int GetIDByAllAttributes
        {
            get
            {
                int? storekeeperID;

                if (cbxStorekeeper.Text == "Нет закрепленного сотрудника")
                {
                    storekeeperID = null;
                }
                else
                {
                    storekeeperID = GetID(cbxStorekeeper.Text);
                }

                int? id = (int?)storageTableAdapter.GetIDByAllAttributes(
                    storekeeperID,
                    txtAddress.Text);
                if (id == null)
                    return -1;
                return (int)id;
            }
        }


        public ViewStorage()
        {
            InitializeComponent();

            _Mode = SubFormMode.Add;


            _Fields = new List<TextBox>();
            _Fields.Add(txtAddress);
        }
        public ViewStorage(int ID, SubFormMode Mode)
        {
            InitializeComponent();

            _ID = ID;
            _Mode = Mode;
            dgvGoods.Columns[0].ReadOnly = true;

            _Fields = new List<TextBox>();
            _Fields.Add(txtAddress);
        }

        private void ViewSupply_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "mainDBDataSet.StockUF". При необходимости она может быть перемещена или удалена.
            this.stockUFTableAdapter.Fill(this.mainDBDataSet.StockUF);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "mainDBDataSet.Stock". При необходимости она может быть перемещена или удалена.
            this.stockTableAdapter.Fill(this.mainDBDataSet.Stock);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "mainDBDataSet.Good". При необходимости она может быть перемещена или удалена.
            this.goodTableAdapter.Fill(this.mainDBDataSet.Good);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "mainDBDataSet.ConsignmentUF". При необходимости она может быть перемещена или удалена.
            if (_Mode != SubFormMode.Add)
            {
                LoadInfo();
            }
            LoadEmployee();
            SetMode();
            if (_Mode != SubFormMode.Add)
            {
                stockBindingSource.Filter = string.Format("id_storage = {0}", _ID);
            }
        }
        private void ViewSupply_FormClosing(object sender, FormClosingEventArgs e)
        {
            stockTableAdapter.Update(mainDBDataSet.Stock);
        }

        private void SetMode()
        {
            bool state = _Mode == SubFormMode.Edit || _Mode == SubFormMode.Add;
            foreach (TextBox c in _Fields)
            {
                c.ReadOnly = !state;
            }
            cbxStorekeeper.Enabled = state;

            if (_Mode == SubFormMode.Add)
            {
                btnSave.Text = "Добавить";
                lblTitle.Text = "Добавление склада";
                Text = "Добавление - Склад";
                dgvGoods.Enabled = false;
                Width = 440;
            }
            else if (_Mode == SubFormMode.View)
            {
                btnSave.Text = "Редактировать";
                lblTitle.Text = "Склад #" + _ID;
                Text = "Просмотр - Склад #" + _ID;
                dgvGoods.Enabled = true;
                Width = 1040;
            }
            else if (_Mode == SubFormMode.Edit)
            {
                btnSave.Text = "Сохранить";
                lblTitle.Text = "Редактирование Склад #" + _ID;
                Text = "Редактирование - Склад #" + _ID;
                dgvGoods.Enabled = true;
                Width = 1040;
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
                    string.Format("SELECT * FROM [Storage] WHERE id = {0}",
                    _ID.ToString()),
                    myConnection);
                myReader = myCommand.ExecuteReader();
                myReader.Read();
                txtAddress.Text = myReader["stor_address"].ToString();
                if (!String.IsNullOrEmpty(myReader["id_storekeeper"].ToString()))
                    _ID_STOREKEEPER = Convert.ToInt32(myReader["id_storekeeper"]);
                myReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            myConnection.Close();
        }
        private void LoadEmployee()
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

                cbxStorekeeper.Items.Clear();

                string currentEmp = "Нет закрепленного сотрудника";
                cbxStorekeeper.Items.Add(currentEmp);

                while (myReader.Read())
                {
                    cbxStorekeeper.Items.Add("#" + myReader["id"].ToString() + ", " + myReader["em_surname"].ToString() + " " + myReader["em_name"].ToString());
                    if (myReader["id"].ToString() == _ID_STOREKEEPER.ToString())
                        currentEmp = "#" + myReader["id"].ToString() + ", " + myReader["em_surname"].ToString() + " " + myReader["em_name"].ToString();

                }
                cbxStorekeeper.SelectedIndex = cbxStorekeeper.Items.IndexOf(currentEmp);
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
            if (_Mode == SubFormMode.Add || _Mode == SubFormMode.Edit)
            {
                if (HasObligatoryNullFields)
                {
                    MessageBox.Show("Заполните все обязательные поля (помечены звездочкой - *)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int? storekeeperID;

                if (cbxStorekeeper.Text == "Нет закрепленного сотрудника")
                {
                    storekeeperID = null;
                }
                else
                {
                    storekeeperID = GetID(cbxStorekeeper.Text);
                }

                _ID_STOREKEEPER = storekeeperID == null ? -1 : (int)storekeeperID;

                if (_Mode == SubFormMode.Add)
                {
                    storageTableAdapter.InsertQuery(storekeeperID,
                        txtAddress.Text);
                    Close();
                }
                else
                {
                    storageTableAdapter.UpdateQuery(storekeeperID,
                        txtAddress.Text,
                        _ID);
                }

                _Mode = SubFormMode.View;
            }
            else
            {
                _Mode = SubFormMode.Edit;
            }
            
            LoadEmployee();
            SetMode();
        }

        private void dgvGoods_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[0].Value = _ID;
        }
        private void dgvGoods_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvGoods.SelectedRows.Count != 0)
            {
                txtAmount.Text = dgvGoods.SelectedRows[0].Cells[2].Value.ToString();
            }
        }

        private void btnGoodAdd_Click(object sender, EventArgs e)
        {
            Form NextForm = new ChooseGoodForm(_ID, false);
            NextForm.ShowDialog();

            stockBindingSource.EndEdit();
            stockUFTableAdapter.Fill(mainDBDataSet.StockUF);
        }

        private void btnGoodSave_Click(object sender, EventArgs e)
        {
            int amount, id_good;
            try
            {
                amount = Convert.ToInt32(txtAmount.Text);
                id_good = Convert.ToInt32(txtGoodName.Text.Split(',')[0].Substring(1));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Некорректно введенные данные, повторите попытку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvGoods_SelectionChanged(null, null);
                return;
            }
            stockTableAdapter.UpdateQuery(amount, _ID, id_good);

            stockBindingSource.EndEdit();
            stockUFTableAdapter.Fill(mainDBDataSet.StockUF);
            MessageBox.Show("Информация изменена", "Успех!!1!1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvGoods_SelectionChanged(null, null);
        }

        private void GoodAdd_TextChanged(object sender, EventArgs e)
        {
            if (dgvGoods.SelectedRows.Count != 0)
            {
                if (txtAmount.Text == dgvGoods.SelectedRows[0].Cells[2].Value.ToString())
                {
                    btnGoodSave.Visible = false;
                }
                else
                {
                    btnGoodSave.Visible = true;
                }
            }
        }
    }
}

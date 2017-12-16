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
    public partial class ViewSupply : Form
    {
        string _supply = Application.StartupPath + @"\reports\supply.docx";
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
                double d = 0;
                for (int i = 0; i < dgvGoods.Rows.Count; i++)
                {
                    try
                    {
                        d += Convert.ToDouble(dgvGoods.Rows[i].Cells[2].Value) * Convert.ToDouble(dgvGoods.Rows[i].Cells[1].Value);
                    }
                    catch (Exception ex) { }
                }
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
                    dateContract.Value.Date.ToShortDateString(),
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
            // TODO: данная строка кода позволяет загрузить данные в таблицу "mainDBDataSet.Good". При необходимости она может быть перемещена или удалена.
            this.goodTableAdapter.Fill(this.mainDBDataSet.Good);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "mainDBDataSet.ConsignmentUF". При необходимости она может быть перемещена или удалена.
            this.consignmentUFTableAdapter.Fill(this.mainDBDataSet.ConsignmentUF);
            this.consignmentTableAdapter.Fill(this.mainDBDataSet.Consignment);
            if (_Mode != SubFormMode.Add)
            {
                LoadInfo();
            }
            LoadClient();
            LoadCars();
            LoadStorages();
            SetMode();
            if (_Mode != SubFormMode.Add)
            {
                consignmentBindingSource.Filter = string.Format("id_supply = {0}", _ID);
            }
            RefreshSum();
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

            cbxShipped.Enabled =  state;
            cbxDelivered.Enabled = cbxShipped.Checked && state;

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
                    if (myReader["id"].ToString() == _ID_CAR.ToString())
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
                    if (myReader["id"].ToString() == _ID_STORAGE.ToString())
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
                    dateContract.Value.Date.ToShortDateString(),
                    Convert.ToInt32(txtPeriod.Text),
                    cbxShipped.Checked,
                    cbxDelivered.Checked);

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
                    dateContract.Value.Date.ToShortDateString(),
                    Convert.ToInt32(txtPeriod.Text),
                    cbxShipped.Checked,
                    cbxDelivered.Checked,
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
            if (dgvGoods.SelectedRows.Count != 0)
            {
                txtAmount.Text = dgvGoods.SelectedRows[0].Cells["amount"].Value.ToString();
                txtPrice.Text = dgvGoods.SelectedRows[0].Cells["price"].Value.ToString();
            }
        }

        private void dgvGoods_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            RefreshSum();
        }

        private void dgvGoods_Leave(object sender, EventArgs e)
        {
            RefreshSum();
        }

        private void cbxShipped_CheckedChanged(object sender, EventArgs e)
        {
            cbxDelivered.Enabled = cbxShipped.Checked;
            if (!cbxDelivered.Enabled)
            {
                cbxDelivered.Checked = false;
            }
        }

        private void btnGoodAdd_Click(object sender, EventArgs e)
        {
            Form NextForm = new ChooseGoodForm(_ID);
            NextForm.ShowDialog();

            consignmentBindingSource.EndEdit();
            consignmentUFTableAdapter.Fill(mainDBDataSet.ConsignmentUF);

            RefreshSum();
        }

        private void btnGoodSave_Click(object sender, EventArgs e)
        {
            int amount;
            int old_amount;
            double price;
            int id_good;
            try
            {
                amount = Convert.ToInt32(txtAmount.Text);
                old_amount = Convert.ToInt32(dgvGoods.SelectedRows[0].Cells["amount"].Value);
                price = Convert.ToDouble(txtPrice.Text);
                id_good = Convert.ToInt32(dgvGoods.SelectedRows[0].Cells["id_good"].Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Некорректно введенные данные, повторите попытку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvGoods_SelectionChanged(null, null);
                return;
            }
            
            consignmentTableAdapter.UpdateQuery(amount, (float)price, _ID, id_good);

            consignmentBindingSource.EndEdit();
            consignmentUFTableAdapter.Fill(mainDBDataSet.ConsignmentUF);
            MessageBox.Show("Информация изменена", "Успех!!1!1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvGoods_SelectionChanged(null, null);
        }

        private void FormatReport()
        {
            string client = "";
            string car = "";
            string driver = "";
            string address_from = "";

            string myConnectionString = ConfigurationManager.ConnectionStrings["supplyGood.Properties.Settings.MainDBConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(myConnectionString);
            myConnection.Open();
            try
            {
                SqlCommand myCommand = new SqlCommand(
                    "SELECT cl_company FROM [Client] c JOIN [Supply] s ON c.id=s.id_client WHERE s.id=" + _ID.ToString(),
                    myConnection);
                SqlDataReader myReader = myCommand.ExecuteReader();
                myReader.Read();
                client = myReader[0].ToString();

                myCommand = new SqlCommand(
                    "SELECT car_model, car_number FROM [Car] c JOIN [Supply] s ON c.id=s.id_car WHERE s.id=" + _ID.ToString(),
                    myConnection);
                myReader = myCommand.ExecuteReader();
                myReader.Read();
                car = myReader["car_model"].ToString() + " " + myReader["car_number"].ToString();

                myCommand = new SqlCommand(
                    "SELECT em_surname, em_name, em_patron FROM  [Supply] s JOIN [Car] c ON s.id_car=c.id JOIN [Employee] e ON e.id=c.id_driver WHERE s.id=" + _ID.ToString(),
                    myConnection);
                myReader = myCommand.ExecuteReader();
                myReader.Read();
                driver = myReader["em_surname"].ToString() + " " + myReader["em_name"].ToString() + " " + myReader["em_patron"].ToString();

                myCommand = new SqlCommand(
                    "SELECT stor_address FROM  [Supply] s JOIN [Storage] st ON s.id_storage=st.id WHERE s.id=" + _ID.ToString(),
                    myConnection);
                myReader = myCommand.ExecuteReader();
                myReader.Read();
                address_from = myReader["stor_address"].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            finally
            {
                myConnection.Close();
            }

            var wordApp = new Word.Application();
            string reportPath = "";
            try
            {
                reportPath =
                    Application.StartupPath + @"\supply_#" + _ID.ToString() + "_" +
                    DateTime.Now.Year + "_" +
                    DateTime.Now.Month + "_" +
                    DateTime.Now.Day + "_" +
                    DateTime.Now.Hour + "_" +
                    DateTime.Now.Minute + "_" +
                    DateTime.Now.Second +
                    ".docx";
                File.Copy(_supply, reportPath);

                wordApp.Visible = false;
                var wordDoc = wordApp.Documents.Open(reportPath);

                ReplaceWordStub("{id}", _ID.ToString(), wordDoc);
                ReplaceWordStub("{shipped}", cbxShipped.Checked ? "Отгружено" : "", wordDoc);
                ReplaceWordStub("{delivered}", cbxDelivered.Checked ? "Доставлено" : "", wordDoc);
                ReplaceWordStub("{client}", client, wordDoc);
                ReplaceWordStub("{car}", car, wordDoc);
                ReplaceWordStub("{driver}", driver, wordDoc);
                ReplaceWordStub("{address_from}", address_from, wordDoc);
                ReplaceWordStub("{address_to}", txtAddress.Text, wordDoc);
                ReplaceWordStub("{period}", txtPeriod.Text, wordDoc);
                ReplaceWordStub("{date}", dateContract.Value.Date.ToShortDateString(), wordDoc);

                Word.Table table = wordDoc.Tables[1];
                for (int i = 0; i < 3; i++)
                {
                    table.Rows[1].Cells[i + 1].Range.Text = dgvGoods.Columns[i].HeaderText;
                }
                table.Rows.Add();
                for (int i = 0; i < dgvGoods.Rows.Count; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        table.Rows[i + 2].Cells[j + 1].Range.Text = dgvGoods.Rows[i].Cells[j].Value.ToString();
                    }
                    if (i != dgvGoods.Rows.Count - 1)
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

        private void GoodAdd_TextChanged(object sender, EventArgs e)
        {
            if (dgvGoods.SelectedRows.Count != 0)
            {
                if (txtAmount.Text == dgvGoods.SelectedRows[0].Cells["amount"].Value.ToString() &&
                txtPrice.Text == dgvGoods.SelectedRows[0].Cells["price"].Value.ToString())
                {
                    btnGoodSave.Visible = false;
                }
                else
                {
                    btnGoodSave.Visible = true;
                }
            }
        }

        private void btnGoodDelete_Click(object sender, EventArgs e)
        {
            consignmentTableAdapter.DeleteQuery(_ID, Convert.ToInt32(dgvGoods.SelectedRows[0].Cells["id_good"].Value));

            consignmentBindingSource.EndEdit();
            consignmentUFTableAdapter.Fill(mainDBDataSet.ConsignmentUF);

            RefreshSum();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (_Mode == SubFormMode.Add || _Mode == SubFormMode.Edit)
            {
                MessageBox.Show("Отчет не может быть сформирован во время редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                FormatReport();
            }
        }
    }
}

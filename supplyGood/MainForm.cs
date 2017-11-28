using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace supplyGood
{
    public enum FilterType { Text, Number, Date, Bool }
    public enum Rights { Admin, HR, Manager, Storage }
    public enum SearchDirection { Previous, Next }
    public enum SubFormMode { Empty, View, Edit, Add }
    public enum TableView { Empty, Supply, Good, Car, Storage, Client, Employee, User }

    public partial class MainForm : Form
    {
        Rights _Rights;
        List<Filter> Filters;
        string RightsCaption
        {
            get
            {
                switch(_Rights)
                {
                    case Rights.Admin:
                        {
                            return "Администратор";
                        }
                    case Rights.HR:
                        {
                            return "Отдел кадров";
                        }
                    case Rights.Manager:
                        {
                            return "Менеджер по продажам";
                        }
                    case Rights.Storage:
                        {
                            return "Менеджер по продажам";
                        }
                    default:
                        {
                            return "";
                        }
                }
            }
        }
        
        TableView _View = TableView.Empty;
        List<TextBox> txtFilters = new List<TextBox>();
        List<Label> lblFilters = new List<Label>();

        string[] _headerEmployee = new string[]
        {
                "ID",
                "Фамилия",
                "Имя",
                "Отчество",
                "Зачисление",
                "Увольнение",
                "Оклад",
        };
        string[] _fieldsEmployee = new string[]
        {
                "id",
                "em_surname",
                "em_name",
                "em_patron",
                "em_acceptance",
                "em_discharge",
                "em_salary"
        };

        string[] _headerGood = new string[]
        {
                "ID",
                "Наименование",
                "Ед. измерения",
                "Цена за ед. (грн)"
        };
        string[] _fieldsGood = new string[]
        {
                "id",
                "g_name",
                "g_unit",
                "g_price"
        };

        string[] _headerCar = new string[]
        {
                "ID",
                "ID Водителя",
                "Гос. номер",
                "Модель",
                "Цвет"
        };
        string[] _fieldsCar = new string[]
        {
                "id",
                "id_driver",
                "car_number",
                "car_model",
                "car_color"
        };

        string[] _headerUser = new string[]
        {
                "Логин",
                "Пароль",
                "Права"
        };
        string[] _fieldsUser = new string[]
        {
                "login",
                "password",
                "rights"
        };

        string[] _headerSupply = new string[]
        {
                "ID поставки",
                "ID заказчика",
                "ID машины",
                "ID склада",
                "Адрес доставки",
                "Дата договора",
                "Срок поставки, мес",
                "Отгр.",
                "Дост."
        };
        string[] _fieldsSupply = new string[]
        {
                "id",
                "id_client",
                "id_car",
                "id_storage",
                "s_address",
                "s_contract",
                "s_period",
                "s_shipped",
                "s_delivered"
        };


        public MainForm(Rights cRights = Rights.Admin)
        {
            InitializeComponent();
            Filters = new List<Filter>();
            _Rights = cRights;
            Text = RightsCaption;
            _View = TableView.Empty;

            if (_Rights == Rights.HR)
            {
                suppliesToolStripMenuItem.Visible = false;
                goodsToolStripMenuItem.Visible = false;
                clientsToolStripMenuItem.Visible = false;
                usersToolStripMenuItem.Visible = false;
            }
            else if (_Rights == Rights.Manager)
            {
                employeeToolStripMenuItem.Visible = false;
                usersToolStripMenuItem.Visible = false;
            }
            else if (_Rights == Rights.Storage)
            {
                suppliesToolStripMenuItem.Visible = false;
                carsToolStripMenuItem.Visible = false;
                clientsToolStripMenuItem.Visible = false;
                employeeToolStripMenuItem.Visible = false;
                usersToolStripMenuItem.Visible = false;
            }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            userTableAdapter.Fill(this.mainDBDataSet.User);
            supplyTableAdapter.Fill(this.mainDBDataSet.Supply);
            employeeTableAdapter.Fill(this.mainDBDataSet.Employee);
            goodTableAdapter.Fill(this.mainDBDataSet.Good);
            carTableAdapter.Fill(this.mainDBDataSet.Car);

            mainMenuStrip.Renderer = new MainStripRenderer();
            if (_Rights == Rights.HR)
            {
                EmployeeToolStripMenuItem_Click(null, null);
            }
            else if (_Rights == Rights.Manager)
            {
                SuppliesToolStripMenuItem_Click(null, null);
            }
            else if (_Rights == Rights.Storage)
            {
                GoodsToolStripMenuItem_Click(null, null);
            }
            else
            {
                UsersToolStripMenuItem_Click(null, null);
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            userTableAdapter.Update(mainDBDataSet.User);
            supplyTableAdapter.Update(mainDBDataSet.Supply);
            employeeTableAdapter.Update(mainDBDataSet.Employee);
            goodTableAdapter.Update(mainDBDataSet.Good);
            carTableAdapter.Update(mainDBDataSet.Car);
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Search(SearchDirection searchDirection)
        {
            if (txtSearch.Text == "")
            {
                UpdateCurrentData();
                MessageBox.Show("Nothing was found.");
                return;
            }
            int stopID;
            try
            {
                stopID = dgvMain.SelectedRows[0].Index;
            }
            catch
            {
                dgvMain.Rows[0].Selected = true;
                stopID = 0;
            }
            while (true)
            {
                if (dgvMain.SelectedRows[0].Cells[cbxSearch.SelectedIndex].Value.ToString().ToLower().Contains(txtSearch.Text.ToLower())
                    && dgvMain.SelectedRows[0].Cells[cbxSearch.SelectedIndex].Style.SelectionBackColor != Color.LightGreen)
                {
                    dgvMain.SelectedRows[0].Cells[cbxSearch.SelectedIndex].Style.SelectionBackColor = Color.LightGreen;
                    return;
                }
                if (searchDirection == SearchDirection.Previous)
                {
                    if (bnMain.BindingSource.Position - 1 >= 0)
                    {
                        dgvMain.SelectedRows[0].Cells[cbxSearch.SelectedIndex].Style.SelectionBackColor = Color.LightSkyBlue;
                        bnMain.BindingSource.MovePrevious();
                    }
                    else
                    {
                        dgvMain.SelectedRows[0].Cells[cbxSearch.SelectedIndex].Style.SelectionBackColor = Color.LightSkyBlue;
                        bnMain.BindingSource.MoveLast();
                    }
                }
                else if (searchDirection == SearchDirection.Next)
                {
                    if (bnMain.BindingSource.Position + 1 < bnMain.BindingSource.Count)
                    {
                        dgvMain.SelectedRows[0].Cells[cbxSearch.SelectedIndex].Style.SelectionBackColor = Color.LightSkyBlue;
                        bnMain.BindingSource.MoveNext();
                    }
                    else
                    {
                        dgvMain.SelectedRows[0].Cells[cbxSearch.SelectedIndex].Style.SelectionBackColor = Color.LightSkyBlue;
                        bnMain.BindingSource.MoveFirst();
                    }
                }
                else
                    return;
                if (dgvMain.SelectedRows[0].Index == stopID && 
                    !(dgvMain.SelectedRows[0].Cells[cbxSearch.SelectedIndex].Value.ToString().ToLower().Contains(txtSearch.Text.ToLower())))
                {
                    UpdateCurrentData();
                    MessageBox.Show("Nothing was found.");
                    return;
                }
            }
        }
        private void UpdateCurrentData()
        {
            SaveToDB();
            ApplyVisualAppearence();

            if (_View == TableView.Car || _View == TableView.Good)
            {
                dgvMain.Columns[0].Width = 75;
            }
            if (_View == TableView.Car)
            {
                dgvMain.Columns[1].Width = 75;
            }

            if (_View == TableView.Supply)
            {
                dgvMain.Columns[0].Width = 40;
                dgvMain.Columns[1].Width = 40;
                dgvMain.Columns[2].Width = 40;
                dgvMain.Columns[3].Width = 40;
                dgvMain.Columns[4].Width = 320;
            }

            PerformFiltering();

            foreach (DataGridViewRow d in dgvMain.Rows)
            {
                d.ContextMenuStrip = contextDGV;
            }
        }
        private void ApplyVisualAppearence()
        {
            string[] headers = new string[0];
            switch (_View)
            {
                case TableView.Good:
                case TableView.Car:
                case TableView.Employee:
                case TableView.Supply:
                    {
                        if (_View == TableView.Good)
                        {
                            dgvMain.DataSource = goodBindingSource;
                            bnMain.BindingSource = goodBindingSource;
                            headers = _headerGood;
                            cbxSearch.Items.Clear();
                            cbxSearch.Items.AddRange(_headerGood);
                        }
                        else if (_View == TableView.Car)
                        {
                            dgvMain.DataSource = carBindingSource;
                            bnMain.BindingSource = carBindingSource;
                            headers = _headerCar;
                            cbxSearch.Items.Clear();
                            cbxSearch.Items.AddRange(_headerCar);
                        }
                        else if (_View == TableView.Employee)
                        {
                            dgvMain.DataSource = employeeBindingSource;
                            bnMain.BindingSource = employeeBindingSource;
                            headers = _headerEmployee;
                            cbxSearch.Items.Clear();
                            cbxSearch.Items.AddRange(_headerEmployee);
                        }
                        else if (_View == TableView.Supply)
                        {
                            dgvMain.DataSource = supplyBindingSource;
                            bnMain.BindingSource = supplyBindingSource;
                            headers = _headerSupply;
                            cbxSearch.Items.Clear();
                            cbxSearch.Items.AddRange(_headerSupply);
                        }
                        btnFunc.Visible = true;
                        dgvMain.ReadOnly = true;
                        dgvMain.AllowUserToAddRows = false;
                        dgvMain.AllowUserToDeleteRows = false;
                        break;
                    }
                case TableView.User:
                    {
                        dgvMain.DataSource = userBindingSource;
                        bnMain.BindingSource = userBindingSource;
                        headers = _headerUser;
                        cbxSearch.Items.Clear();
                        cbxSearch.Items.AddRange(_headerUser);

                        btnFunc.Visible = false;
                        dgvMain.ReadOnly = false;
                        dgvMain.AllowUserToAddRows = true;
                        dgvMain.AllowUserToDeleteRows = true;
                        break;
                    }
            }
            cbxSearch.SelectedIndex = 0;

            if (headers.Length > 0)
            {
                for (int i = 0; i < headers.Length; i++)
                {
                    dgvMain.Columns[i].HeaderText = headers[i];
                }
            }
        }
        private void SaveToDB()
        {
            try
            {
                Validate();
                supplyBindingSource.EndEdit();
                supplyTableAdapter.Update(this.mainDBDataSet.Supply);
                employeeBindingSource.EndEdit();
                employeeTableAdapter.Update(this.mainDBDataSet.Employee);
                goodBindingSource.EndEdit();
                goodTableAdapter.Update(this.mainDBDataSet.Good);
                carBindingSource.EndEdit();
                carTableAdapter.Update(this.mainDBDataSet.Car);
                userBindingSource.EndEdit();
                userTableAdapter.Update(this.mainDBDataSet.User);


                userTableAdapter.Fill(this.mainDBDataSet.User);
                supplyTableAdapter.Fill(this.mainDBDataSet.Supply);
                employeeTableAdapter.Fill(this.mainDBDataSet.Employee);
                goodTableAdapter.Fill(this.mainDBDataSet.Good);
                carTableAdapter.Fill(this.mainDBDataSet.Car);
            }
            catch
            {
                MessageBox.Show("Update failed");
            }
        }
        private void ClearAllHandlers(Control control)
        {
            FieldInfo f1 = typeof(Control).GetField("EventClick",
                BindingFlags.Static | BindingFlags.NonPublic);
            object obj = f1.GetValue(control);
            PropertyInfo pi = control.GetType().GetProperty("Events",
                BindingFlags.NonPublic | BindingFlags.Instance);
            EventHandlerList list = (EventHandlerList)pi.GetValue(control, null);
            list.RemoveHandler(obj, list[obj]);
        }
        private void Context_ViewEdit(SubFormMode mode)
        {
            try
            {
                var currRowIndex = dgvMain.SelectedCells[0].RowIndex;
                int currID = Convert.ToInt32(dgvMain.Rows[currRowIndex].Cells[0].Value);
                int currRowOnTop = dgvMain.FirstDisplayedScrollingRowIndex;
                var NextForm = new Form();

                switch (_View)
                {
                    case TableView.Supply:
                        {
                            NextForm = new ViewSupply(currID, mode);
                            break;
                        }
                    case TableView.Good:
                        {
                            NextForm = new ViewGood(currID, mode);
                            break;
                        }
                    case TableView.Car:
                        {
                            NextForm = new ViewCar(currID, mode);
                            break;
                        }
                    case TableView.Employee:
                        {

                            NextForm = new ViewEmployee(currID, mode);
                            break;
                        }
                }

                NextForm.ShowDialog();

                UpdateCurrentData();

                dgvMain.FirstDisplayedScrollingRowIndex = currRowOnTop;
                dgvMain.Rows[currRowIndex].Selected = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private string GetDeletingName(int curr, int currID)
        {
            switch (_View)
            {
                case TableView.Supply:
                    {
                        return " поставку (" + dgvMain.Rows[curr].Cells[0].Value.ToString() + ") ";
                    }
                case TableView.Good:
                    {
                        return "(" + dgvMain.Rows[curr].Cells[0].Value.ToString() + ") " +
                            dgvMain.Rows[curr].Cells[1].Value.ToString();
                    }
                case TableView.Car:
                    {
                        return "(" + dgvMain.Rows[curr].Cells[0].Value.ToString() + ") " +
                            dgvMain.Rows[curr].Cells[3].Value.ToString() + " " +
                            dgvMain.Rows[curr].Cells[2].Value.ToString();
                    }
                case TableView.Employee:
                    {
                        return "(" + dgvMain.Rows[curr].Cells[0].Value.ToString() + ") " +
                            dgvMain.Rows[curr].Cells[1].Value.ToString() + " " +
                            dgvMain.Rows[curr].Cells[2].Value.ToString();
                    }
                default:
                    {
                        return "(" + dgvMain.Rows[curr].Cells[0].Value.ToString() + ") ";
                    }
            }
        }
        private void PerformFiltering()
        {
            string filterString = "";
            foreach (Filter filter in Filters)
            {
                if (!filter.Checked)
                    continue;

                switch (filter.Type)
                {
                    case FilterType.Bool:
                        {
                            if (filterString.Length > 0) filterString += " AND ";
                            filterString += filter.Field + ((bool)filter.OnlyTrue ? "=true" : "=false");
                            break;
                        }
                    case FilterType.Date:
                        {
                            if (filterString.Length > 0) filterString += " AND ";
                            filterString += string.Format("CONVERT({0}, 'System.DateTime')>=CONVERT('{1}', 'System.DateTime') AND CONVERT({0}, 'System.DateTime')<=CONVERT('{2}', 'System.DateTime')",
                                filter.Field, filter.FromDate.Date.ToShortDateString(), filter.ToDate.Date.ToShortDateString());
                            break;
                        }
                    case FilterType.Number:
                        {
                            if (filterString.Length > 0) filterString += " AND ";
                            if (filter.From == null && filter.To == null)
                            {
                                break;
                            }
                            else if (filter.From == null)
                            {
                                filterString += filter.Field + "<=" + filter.To;
                            }
                            else if (filter.To == null)
                            {
                                filterString += filter.Field + ">=" + filter.From;
                            }
                            else 
                            {
                                filterString += filter.Field + ">=" + filter.From + " AND " + filter.Field + "<=" + filter.To;
                            }
                            break;
                        }
                    case FilterType.Text:
                        {
                            if (filterString.Length > 0) filterString += " AND ";
                            filterString += filter.Field + " LIKE '%" + filter.Value.ToLower() + "%'";
                            break;
                        }
                }
            }
            switch (_View)
            {
                case TableView.Good:
                    {
                        goodBindingSource.Filter = filterString;
                        break;
                    }
                case TableView.Car:
                    {
                        carBindingSource.Filter = filterString;
                        break;
                    }
                case TableView.Employee:
                    {
                        employeeBindingSource.Filter = filterString;
                        break;
                    }
                case TableView.Supply:
                    {
                        supplyBindingSource.Filter = filterString;
                        break;
                    }
                case TableView.User:
                    {
                        userBindingSource.Filter = filterString;
                        break;
                    }
            }
        }
        private void ClearFilters()
        {
            foreach (Filter f in Filters)
            {
                f.Clear();
            }
        }

        private void SuppliesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.Supply;

            Filters.Clear();
            Filters.Add(new Filter(false, _fieldsSupply[0], _headerSupply[0], FilterType.Number));
            Filters.Add(new Filter(false, _fieldsSupply[1], _headerSupply[1], FilterType.Number));
            Filters.Add(new Filter(false, _fieldsSupply[2], _headerSupply[2], FilterType.Number));
            Filters.Add(new Filter(false, _fieldsSupply[3], _headerSupply[3], FilterType.Number));
            Filters.Add(new Filter(false, _fieldsSupply[4], _headerSupply[4], FilterType.Text));
            Filters.Add(new Filter(false, _fieldsSupply[5], _headerSupply[5], FilterType.Date));
            Filters.Add(new Filter(false, _fieldsSupply[6], _headerSupply[6], FilterType.Number));
            Filters.Add(new Filter(false, _fieldsSupply[7], _headerSupply[7], FilterType.Bool));
            Filters.Add(new Filter(false, _fieldsSupply[8], _headerSupply[8], FilterType.Bool));

            lblMain.Text = "Поставки";
            lblHint.Text = "Подсказка: существует возможность просмотра расширенной " +
                "информации о поставке, её редактирования и удаления поставки. " +
                "Для этого необходимо выбрать поставку в таблице, нажать по нему " +
                "правой кнопкой мыши и выбрать необходимое действие";
            btnFunc.Text = "Добавить поставку";
            Text = lblMain.Text + " - " + RightsCaption;
            UpdateCurrentData();
        }
        private void EmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.Employee;

            Filters.Clear();
            Filters.Add(new Filter(false, _fieldsEmployee[0], _headerEmployee[0], FilterType.Number));
            Filters.Add(new Filter(false, _fieldsEmployee[1], _headerEmployee[1], FilterType.Text));
            Filters.Add(new Filter(false, _fieldsEmployee[2], _headerEmployee[2], FilterType.Text));
            Filters.Add(new Filter(false, _fieldsEmployee[3], _headerEmployee[3], FilterType.Text));
            Filters.Add(new Filter(false, _fieldsEmployee[4], _headerEmployee[4], FilterType.Date));
            Filters.Add(new Filter(false, _fieldsEmployee[5], _headerEmployee[5], FilterType.Date));
            Filters.Add(new Filter(false, _fieldsEmployee[6], _headerEmployee[6], FilterType.Number));

            lblMain.Text = "Сотрудники";
            lblHint.Text = "Подсказка: существует возможность просмотра расширенной " +
                "информации о сотруднике, её редактирования и удаления сотрудника. " +
                "Для этого необходимо выбрать сотрудника в таблице, нажать по нему " +
                "правой кнопкой мыши и выбрать необходимое действие";
            btnFunc.Text = "Добавить сотрудника";
            Text = lblMain.Text + " - " + RightsCaption;
            UpdateCurrentData();
        }
        private void GoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.Good;

            Filters.Clear();
            Filters.Add(new Filter(false, _fieldsGood[0], _headerGood[0], FilterType.Number));
            Filters.Add(new Filter(false, _fieldsGood[1], _headerGood[1], FilterType.Text));
            Filters.Add(new Filter(false, _fieldsGood[2], _headerGood[2], FilterType.Text));
            Filters.Add(new Filter(false, _fieldsGood[3], _headerGood[3], FilterType.Number));

            lblMain.Text = "Товары";
            lblHint.Text = "Подсказка: существует возможность просмотра расширенной " +
                "информации о товаре, её редактирования и удаления товара. " +
                "Для этого необходимо выбрать товар в таблице, нажать по нему " +
                "правой кнопкой мыши и выбрать необходимое действие";
            btnFunc.Text = "Добавить товар";
            Text = lblMain.Text + " - " + RightsCaption;
            UpdateCurrentData();
        }
        private void CarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.Car;

            Filters.Clear();
            Filters.Add(new Filter(false, _fieldsCar[0], _headerCar[0], FilterType.Number));
            Filters.Add(new Filter(false, _fieldsCar[1], _headerCar[1], FilterType.Number));
            Filters.Add(new Filter(false, _fieldsCar[2], _headerCar[2], FilterType.Text));
            Filters.Add(new Filter(false, _fieldsCar[3], _headerCar[3], FilterType.Text));
            Filters.Add(new Filter(false, _fieldsCar[4], _headerCar[4], FilterType.Text));

            lblMain.Text = "Машины";
            lblHint.Text = "Подсказка: существует возможность просмотра расширенной " +
                "информации о машине, её редактирования и удаления машины. " +
                "Для этого необходимо выбрать машину в таблице, нажать по ней " +
                "правой кнопкой мыши и выбрать необходимое действие";
            btnFunc.Text = "Добавить машину";
            Text = lblMain.Text + " - " + RightsCaption;
            UpdateCurrentData();
        }
        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.User;

            Filters.Clear();
            Filters.Add(new Filter(false, _fieldsUser[0], _headerUser[0], FilterType.Text));
            Filters.Add(new Filter(false, _fieldsUser[1], _headerUser[1], FilterType.Text));
            Filters.Add(new Filter(false, _fieldsUser[2], _headerUser[2], FilterType.Text));

            lblMain.Text = "Пользователи";
            lblHint.Text = "Подсказка: редактирование доступно прямо в таблицу. Все поля обязательны для заполнения";
            Text = lblMain.Text + " - " + RightsCaption;
            UpdateCurrentData();
        }
        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var NextForm = new StatisticsForm();
            NextForm.ShowDialog();
        }
        private void PredictGoodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var NextForm = new PredictForm();
            NextForm.ShowDialog();
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void BtnSearchNext_Click(object sender, EventArgs e)
        {
            Search(SearchDirection.Next);
        }
        private void BtnSearchPrev_Click(object sender, EventArgs e)
        {
            Search(SearchDirection.Previous);
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            int currRowIndex = -1;
            int currID;
            try
            {
                currRowIndex = dgvMain.SelectedCells[0].RowIndex;
                currID = Convert.ToInt32(dgvMain.Rows[currRowIndex].Cells[0].Value);
            }
            catch { }

            var NextForm = new Form();

            switch (_View)
            {
                case TableView.Supply:
                    {
                        NextForm = new ViewSupply();
                        break;
                    }
                case TableView.Good:
                    {
                        NextForm = new ViewGood();
                        break;
                    }
                case TableView.Car:
                    {
                        NextForm = new ViewCar();
                        break;
                    }
                case TableView.Employee:
                    {
                        NextForm = new ViewEmployee();
                        break;
                    }
            }

            NextForm.ShowDialog();

            int currRowOnTop = dgvMain.FirstDisplayedScrollingRowIndex;

            UpdateCurrentData();

            try
            {
                dgvMain.FirstDisplayedScrollingRowIndex = currRowOnTop;
                dgvMain.Rows[currRowIndex].Selected = true;
            }
            catch { }
        }
        private void BtnFilter_Click(object sender, EventArgs e)
        {
            var NextForm = new FilterForm(Filters);
            NextForm.ShowDialog();
            Filters = NextForm.Filters.ToList();
            PerformFiltering();
        }
        private void BtnClearFilters_Click(object sender, EventArgs e)
        {
            ClearFilters();
            UpdateCurrentData();
        }
        private void BtnSort_Click(object sender, EventArgs e)
        {
            switch (_View)
            {
                case TableView.Good:
                    {
                        goodBindingSource.Sort = _fieldsGood[cbxSearch.SelectedIndex];
                        break;
                    }
                case TableView.Car:
                    {
                        carBindingSource.Sort = _fieldsCar[cbxSearch.SelectedIndex];
                        break;
                    }
                case TableView.Employee:
                    {
                        employeeBindingSource.Sort = _fieldsEmployee[cbxSearch.SelectedIndex];
                        break;
                    }
                case TableView.Supply:
                    {
                        supplyBindingSource.Sort = _fieldsSupply[cbxSearch.SelectedIndex];
                        break;
                    }
                case TableView.User:
                    {
                        userBindingSource.Sort = _fieldsUser[cbxSearch.SelectedIndex];
                        break;
                    }
            }
        }


        private void Context_DetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Context_ViewEdit(SubFormMode.View);
        }
        private void Context_EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Context_ViewEdit(SubFormMode.Edit);
        }
        private void Context_DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var curr = dgvMain.SelectedCells[0].RowIndex;
            int currID = Convert.ToInt32(dgvMain.Rows[curr].Cells[0].Value);
            string NameOfDeletingItem = GetDeletingName(curr, currID);
            if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить " + NameOfDeletingItem + "?", "Удаление",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                try
                {
                    switch (_View)
                    {
                        case TableView.Supply:
                            {
                                supplyTableAdapter.DeleteQuery(currID);
                                break;
                            }
                        case TableView.Good:
                            {
                                goodTableAdapter.DeleteQuery(currID);
                                break;
                            }
                        case TableView.Car:
                            {
                                carTableAdapter.DeleteQuery(currID);
                                break;
                            }
                        case TableView.Employee:
                            {
                                personalInfoTableAdapter.DeleteQuery(currID);
                                employeeTableAdapter.DeleteQuery(currID);
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("При удалении произошла ошибка. Обьект НЕ удален", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            int currRowOnTop = dgvMain.FirstDisplayedScrollingRowIndex - 1;

            UpdateCurrentData();

            try
            {
                dgvMain.FirstDisplayedScrollingRowIndex = currRowOnTop;
            }
            catch { }
        }
        private void dgvMain_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < dgvMain.ColumnCount && e.RowIndex < dgvMain.RowCount && e.Button == MouseButtons.Right)
            {
                try
                {
                    dgvMain.CurrentCell = dgvMain.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
                catch { }
            }
        }
        private void dgvMain_DoubleClick(object sender, EventArgs e)
        {
            Context_DetailsToolStripMenuItem_Click(null, null);
        }
        private void dgvMain_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Вы не заполнили все необходимые поля" + Environment.NewLine + "Повторите ввод данных");
        }
    }

    public class PalmBackgroundPanel : Panel
    {
        private const int WS_EX_TRANSPARENT = 0x20;
        public PalmBackgroundPanel()
        {
            SetStyle(ControlStyles.Opaque, true);
        }

        private int opacity = 50;
        [DefaultValue(50)]
        public int Opacity
        {
            get
            {
                return this.opacity;
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("value must be between 0 and 100");
                this.opacity = value;
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_TRANSPARENT;
                return cp;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            using (var brush = new SolidBrush(Color.FromArgb(this.opacity * 255 / 100, this.BackColor)))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
            base.OnPaint(e);
        }
    }
    public class MainStripRenderer : ToolStripSystemRenderer
    {
        public MainStripRenderer()
        {
        }
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBorder(e);
        }
    }
    public class Filter
    {
        public bool Checked;

        public string Field;
        public string Header;

        public FilterType Type;

        public double? From = null;
        public double? To = null;
        public DateTime FromDate = DateTime.Now.Date;
        public DateTime ToDate = DateTime.Now.Date;
        public string Value = null;
        public bool? OnlyTrue = null;
        public Filter(bool check, string field, string header, FilterType filter)
        {
            Checked = check;
            Field = field;
            Header = header;
            Type = filter;
        }
        public Filter(bool check, string field, string header, double? from = null, double? to = null) : this(check, field, header, FilterType.Number)
        {
            From = from;
            To = to;
        }
        public Filter(bool check, string field, string header, DateTime from, DateTime to) : this(check, field, header, FilterType.Date)
        {
            FromDate = from;
            ToDate = to;
        }
        public Filter(bool check, string field, string header, string value) : this(check, field, header, FilterType.Text)
        {
            Value = value;
        }
        public Filter(bool check, string field, string header, bool? value = null) : this(check, field, header, FilterType.Bool)
        {
            OnlyTrue = value;
        }

        public void Clear()
        {
            Checked = false;
            From = null;
            To = null;
            FromDate = DateTime.Now.Date;
            ToDate = DateTime.Now.Date;
            Value = null;
            OnlyTrue = null;
        }
    }
}
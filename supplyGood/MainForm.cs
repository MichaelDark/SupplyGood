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
        List<FormState> State;
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
        
        public MainForm(Rights cRights = Rights.Admin)
        {
            InitializeComponent();
            InitializeStates();
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

        private void InitializeStates()
        {
            State = new List<FormState>();
            string caption = "";
            string hint = "";
            string btnText = "";
            List<string> head = new List<string>();
            List<string> field = new List<string>();
            List<Filter> filter = new List<Filter>();

            FormState empty = new FormState();
            State.Add(empty);

            //Supply
            caption = "Поставки";
            hint = "Подсказка: существует возможность просмотра расширенной " +
                "информации о поставке, её редактирования и удаления поставки. " +
                "Для этого необходимо выбрать поставку в таблице, нажать по нему " +
                "правой кнопкой мыши и выбрать необходимое действие";
            btnText = "Добавить поставку";
            head = new List<string>()
            {
                "ID",
                "Заказчик",
                "Машина",
                "Склад",
                "Адрес доставки",
                "Дата договора",
                "Срок поставки, мес",
                "Отгр.",
                "Дост."
            };
            field = new List<string>()
            {
                "id",
                "company",
                "car",
                "storage",
                "address",
                "contact",
                "period",
                "shipped",
                "delivered"
            };
            filter = new List<Filter>()
            {
                new Filter(false, field[0], head[0], FilterType.Number),
                new Filter(false, field[1], head[1], FilterType.Text),
                new Filter(false, field[2], head[2], FilterType.Text),
                new Filter(false, field[3], head[3], FilterType.Text),
                new Filter(false, field[4], head[4], FilterType.Text),
                new Filter(false, field[5], head[5], FilterType.Date),
                new Filter(false, field[6], head[6], FilterType.Number),
                new Filter(false, field[7], head[7], FilterType.Bool),
                new Filter(false, field[8], head[8], FilterType.Bool)
            };
            FormState supply = new FormState(caption, hint, btnText, head, field, filter, supplyBindingSource);
            State.Add(supply);

            //Good
            caption = "Товары";
            hint = "Подсказка: существует возможность просмотра расширенной " +
                "информации о товаре, её редактирования и удаления товара. " +
                "Для этого необходимо выбрать товар в таблице, нажать по нему " +
                "правой кнопкой мыши и выбрать необходимое действие";
            btnText = "Добавить товар";
            head = new List<string>()
            {
                "ID",
                "Наименование",
                "Ед. измерения",
                "Цена за ед. (грн)"
            };
            field = new List<string>()
            {
                "id",
                "g_name",
                "g_unit",
                "g_price"
            };
            filter = new List<Filter>()
            {
                new Filter(false, field[0], head[0], FilterType.Number),
                new Filter(false, field[1], head[1], FilterType.Text),
                new Filter(false, field[2], head[2], FilterType.Text),
                new Filter(false, field[3], head[3], FilterType.Number)
            };
            FormState good = new FormState(caption, hint, btnText, head, field, filter, goodBindingSource);
            State.Add(good);

            //Car
            caption = "Машины";
            hint = "Подсказка: существует возможность просмотра расширенной " +
                "информации о машине, её редактирования и удаления машины. " +
                "Для этого необходимо выбрать машину в таблице, нажать по ней " +
                "правой кнопкой мыши и выбрать необходимое действие";
            btnText = "Добавить машину";
            head = new List<string>()
            {
                "ID",
                "Водитель",
                "Гос. номер",
                "Модель",
                "Цвет"
            };
            field = new List<string>()
            {
                "id",
                "driver",
                "number",
                "model",
                "color"
            };
            filter = new List<Filter>()
            {
                new Filter(false, field[0], head[0], FilterType.Number),
                new Filter(false, field[1], head[1], FilterType.Number),
                new Filter(false, field[2], head[2], FilterType.Text),
                new Filter(false, field[3], head[3], FilterType.Text),
                new Filter(false, field[4], head[4], FilterType.Text)
            };
            FormState car = new FormState(caption, hint, btnText, head, field, filter, carBindingSource);
            State.Add(car);

            //Storage 
            caption = "Склады";
            hint = "Подсказка: существует возможность просмотра расширенной " +
                "информации о складе, её редактирования и удаления склада. " +
                "Для этого необходимо выбрать склад в таблице, нажать по нему " +
                "правой кнопкой мыши и выбрать необходимое действие";
            btnText = "Добавить склад";
            head = new List<string>()
            {
                "ID",
                "Кладовщик",
                "Адрес"
            };
            field = new List<string>()
            {
                "id",
                "storekeeper",
                "address"
            };
            filter = new List<Filter>()
            {
                new Filter(false, field[0], head[0], FilterType.Number),
                new Filter(false, field[1], head[1], FilterType.Text),
                new Filter(false, field[2], head[2], FilterType.Text)
            };
            FormState storage = new FormState(caption, hint, btnText, head, field, filter, storageBindingSource);
            State.Add(storage);

            //Client
            caption = "Заказчики";
            hint = "Подсказка: существует возможность просмотра расширенной " +
                "информации о заказчике, её редактирования и удаления заказчика. " +
                "Для этого необходимо выбрать заказчика в таблице, нажать по нему " +
                "правой кнопкой мыши и выбрать необходимое действие";
            btnText = "Добавить заказчика";
            head = new List<string>()
            {
                "ID",
                "Компания",
                "Адрес",
                "Конт. лицо: Фамилия",
                "Конт. лицо: Имя",
                "Конт. лицо: Отчество",
                "Контакты"
            };
            field = new List<string>()
            {
                "id",
                "cl_company",
                "cl_address",
                "cl_surname",
                "cl_name",
                "cl_patron",
                "cl_contacts"
            };
            filter = new List<Filter>()
            {
                new Filter(false, field[0], head[0], FilterType.Number),
                new Filter(false, field[1], head[1], FilterType.Text),
                new Filter(false, field[2], head[2], FilterType.Text),
                new Filter(false, field[3], head[3], FilterType.Text),
                new Filter(false, field[4], head[4], FilterType.Text),
                new Filter(false, field[5], head[5], FilterType.Text),
                new Filter(false, field[6], head[6], FilterType.Text)
            };
            FormState client = new FormState(caption, hint, btnText, head, field, filter, clientBindingSource);
            State.Add(client);

            //Employee
            caption = "Сотрудники";
            hint = "Подсказка: существует возможность просмотра расширенной " +
                "информации о сотруднике, её редактирования и удаления сотрудника. " +
                "Для этого необходимо выбрать сотрудника в таблице, нажать по нему " +
                "правой кнопкой мыши и выбрать необходимое действие";
            btnText = "Добавить сотрудника";
            head = new List<string>()
            {
                "ID",
                "Фамилия",
                "Имя",
                "Отчество",
                "Зачисление",
                "Увольнение",
                "Оклад",
            };
            field = new List<string>()
            {
                "id",
                "em_surname",
                "em_name",
                "em_patron",
                "em_acceptance",
                "em_discharge",
                "em_salary"
            };
            filter = new List<Filter>()
            {
                new Filter(false, field[0], head[0], FilterType.Number),
                new Filter(false, field[1], head[1], FilterType.Text),
                new Filter(false, field[2], head[2], FilterType.Text),
                new Filter(false, field[3], head[3], FilterType.Text),
                new Filter(false, field[4], head[4], FilterType.Date),
                new Filter(false, field[5], head[5], FilterType.Date),
                new Filter(false, field[6], head[6], FilterType.Number)
            };
            FormState emp = new FormState(caption, hint, btnText, head, field, filter, employeeBindingSource);
            State.Add(emp);

            //User
            caption = "Пользователи";
            hint = "Подсказка: редактирование доступно прямо в таблицу. Все поля обязательны для заполнения";
            btnText = "";
            head = new List<string>()
            {
                "Логин",
                "Пароль",
                "Права"
            };
            field = new List<string>()
            {
                "login",
                "password",
                "rights"
            };
            filter = new List<Filter>()
            {
                new Filter(false, field[0], head[0], FilterType.Text),
                new Filter(false, field[1], head[1], FilterType.Text),
                new Filter(false, field[2], head[2], FilterType.Text)
            };
            FormState user = new FormState(caption, hint, btnText, head, field, filter, userBindingSource);
            State.Add(user);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            clientTableAdapter.Fill(this.mainDBDataSet.Client);
            storageUFTableAdapter.Fill(this.mainDBDataSet.StorageUF);
            storageTableAdapter.Fill(this.mainDBDataSet.Storage);
            carUFTableAdapter.Fill(this.mainDBDataSet.CarUF);
            supplyUFTableAdapter.Fill(this.mainDBDataSet.SupplyUF);
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
            
            supplyUFTableAdapter.Fill(this.mainDBDataSet.SupplyUF);
            goodTableAdapter.Fill(this.mainDBDataSet.Good);
            clientTableAdapter.Fill(this.mainDBDataSet.Client);
            carUFTableAdapter.Fill(this.mainDBDataSet.CarUF);
            storageUFTableAdapter.Fill(this.mainDBDataSet.StorageUF);
            employeeTableAdapter.Fill(this.mainDBDataSet.Employee);
            userTableAdapter.Fill(this.mainDBDataSet.User);

            dgvMain.DataSource = State[(int)_View].Binding;
            bnMain.BindingSource = State[(int)_View].Binding;
            cbxSearch.Items.Clear();
            cbxSearch.Items.AddRange(State[(int)_View].Headers.ToArray());

            switch (_View)
            {
                case TableView.Supply:
                case TableView.Good:
                case TableView.Car:
                case TableView.Storage:
                case TableView.Employee:
                case TableView.Client:
                    {
                        btnFunc.Visible = true;
                        dgvMain.ReadOnly = true;
                        dgvMain.AllowUserToAddRows = false;
                        dgvMain.AllowUserToDeleteRows = false;
                        break;
                    }
                case TableView.User:
                    {
                        btnFunc.Visible = false;
                        dgvMain.ReadOnly = false;
                        dgvMain.AllowUserToAddRows = true;
                        dgvMain.AllowUserToDeleteRows = true;
                        break;
                    }
            }
            cbxSearch.SelectedIndex = 0;
            txtSearch.Text = "";

            if (State[(int)_View].Headers.Count > 0)
            {
                for (int i = 0; i < State[(int)_View].Headers.Count; i++)
                {
                    dgvMain.Columns[i].HeaderText = State[(int)_View].Headers[i];
                }
            }

            if (_View == TableView.Car || _View == TableView.Good || _View ==TableView.Storage || _View == TableView.Client)
            {
                dgvMain.Columns[0].Width = 75;
            }
            if (_View == TableView.Car)
            {
                dgvMain.Columns[1].Width = 250;
            }
            if (_View == TableView.Supply)
            {
                dgvMain.Columns[0].Width = 40;
                dgvMain.Columns[1].Width = 150;
                dgvMain.Columns[2].Width = 100;
                dgvMain.Columns[3].Width = 250;
                dgvMain.Columns[4].Width = 250;
                dgvMain.Columns[5].Width = 120;
            }

            PerformFiltering();

            foreach (DataGridViewRow d in dgvMain.Rows)
            {
                d.ContextMenuStrip = contextDGV;
            }
        }
        private void SaveToDB()
        {
            try
            {
                Validate();
                userBindingSource.EndEdit();
                userTableAdapter.Update(this.mainDBDataSet.User);
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
                case TableView.Storage:
                    {
                        return "склад (" + dgvMain.Rows[curr].Cells[0].Value.ToString() + ") по адресу " +
                            dgvMain.Rows[curr].Cells[2].Value.ToString() + " (заведующий " +
                            dgvMain.Rows[curr].Cells[3].Value.ToString() + ") ";
                    }
                case TableView.Employee:
                    {
                        return "(" + dgvMain.Rows[curr].Cells[0].Value.ToString() + ") " +
                            dgvMain.Rows[curr].Cells[1].Value.ToString() + " " +
                            dgvMain.Rows[curr].Cells[2].Value.ToString();
                    }
                case TableView.Client:
                    {
                        return "(" + dgvMain.Rows[curr].Cells[0].Value.ToString() + ") " +
                            dgvMain.Rows[curr].Cells[1].Value.ToString() + " по адресу " +
                            dgvMain.Rows[curr].Cells[2].Value.ToString();
                    }
                default:
                    {
                        return "(" + dgvMain.Rows[curr].Cells[0].Value.ToString() + ") ";
                    }
            }
        }
        private void PerformState()
        {
            lblMain.Text = State[(int)_View].Caption;
            lblHint.Text = State[(int)_View].Hint;
            btnFunc.Text = State[(int)_View].ButtonText;
            Text = lblMain.Text + " - " + RightsCaption;
            UpdateCurrentData();
        }
        private void PerformFiltering()
        {
            string filterString = "";
            foreach (Filter filter in State[(int)_View].Filters)
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
            State[(int)_View].Binding.Filter = filterString;
        }
        private void ClearFilters()
        {
            State[(int)_View].ClearFilters();
        }

        private void SuppliesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.Supply;

            PerformState();
        }
        private void EmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.Employee;

            PerformState();
        }
        private void GoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.Good;

            PerformState();
        }
        private void CarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.Car;

            PerformState();
        }
        private void StorageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.Storage;

            PerformState();
        }
        private void ClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.Client;

            PerformState();
        }
        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.User;

            PerformState();
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
                case TableView.Storage:
                    {
                        NextForm = new ViewStorage();
                        break;
                    }
                case TableView.Client:
                    {
                        NextForm = new ViewClient();
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
            var NextForm = new FilterForm(State[(int)_View].Filters);
            NextForm.ShowDialog();
            State[(int)_View].Filters = NextForm.Filters.ToList();
            PerformFiltering();
        }
        private void BtnClearFilters_Click(object sender, EventArgs e)
        {
            ClearFilters();
            UpdateCurrentData();
        }
        private void BtnSort_Click(object sender, EventArgs e)
        {
            dgvMain.Sort(dgvMain.Columns[cbxSearch.SelectedIndex], ListSortDirection.Ascending);
        }


        private void Context_DetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Context_ViewEdit(SubFormMode.View);
        }
        private void Context_EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Context_ViewEdit(SubFormMode.Edit);
        }
        private void Context_ViewEdit(SubFormMode mode)
        {
            if (_View == TableView.User)
                return;

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
                    case TableView.Storage:
                        {
                            NextForm = new ViewStorage(currID, mode);
                            break;
                        }
                    case TableView.Employee:
                        {

                            NextForm = new ViewEmployee(currID, mode);
                            break;
                        }
                    case TableView.Client:
                        {

                            NextForm = new ViewClient(currID, mode);
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
                        case TableView.Storage:
                            {
                                storageTableAdapter.DeleteQuery(currID);
                                break;
                            }
                        case TableView.Client:
                            {
                                clientTableAdapter.DeleteQuery(currID);
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
    public class FormState
    {
        public string Caption;
        public string Hint;
        public string ButtonText;
        public List<string> Headers;
        public List<string> Fields;
        public List<Filter> Filters;
        public BindingSource Binding;

        public FormState()
        {
            Caption = "";
            Hint = "";
            ButtonText = "";
            Headers = new List<string>();
            Fields = new List<string>();
            Filters = new List<Filter>();
            Binding = null;
        }
        public FormState(
            string caption, 
            string hint, 
            string buttonText, 
            List<string> headers, 
            List<string> fields, 
            List<Filter> filters, 
            BindingSource binding)
        {
            Caption = caption;
            Hint = hint;
            ButtonText = buttonText;
            Headers = headers;
            Fields = fields;
            Filters = filters;
            Binding = binding;
        }

        public void ClearFilters()
        {
            foreach (Filter f in Filters)
            {
                f.Clear();
            }
        }
        public void Clear()
        {
            Caption = "";
            Hint = "";
            ButtonText = "";
            Headers.Clear();
            Fields.Clear();
            Filters.Clear();
            Binding = null;
        }
    }
}
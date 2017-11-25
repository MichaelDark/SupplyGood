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
    public enum SubFormMode { Empty, View, Edit, Add }
    public enum TableView { Empty, Supply, Good, Car, Storage, Client, Employee, User }

    public partial class MainForm : Form
    {
        string _Rights = "";
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
                "em_dalary",
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

        
        public MainForm(string cRights = "Администратор")
        {
            InitializeComponent();
            Width = 1000;
            _Rights = cRights;
            Text = _Rights;
            _View = TableView.Empty;

            //Init filters' UI
            {
                txtFilters.Add(txtFilter1);
                txtFilters.Add(txtFilter2);
                txtFilters.Add(txtFilter3);
                txtFilters.Add(txtFilter4);
                txtFilters.Add(txtFilter5);
                txtFilters.Add(txtFilter6);
                txtFilters.Add(txtFilter7);
                txtFilters.Add(txtFilter8);
                txtFilters.Add(txtFilter9);
            }
            {
                lblFilters.Add(lblFilter1);
                lblFilters.Add(lblFilter2);
                lblFilters.Add(lblFilter3);
                lblFilters.Add(lblFilter4);
                lblFilters.Add(lblFilter5);
                lblFilters.Add(lblFilter6);
                lblFilters.Add(lblFilter7);
                lblFilters.Add(lblFilter8);
                lblFilters.Add(lblFilter9);
            }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            userTableAdapter.Fill(this.mainDBDataSet.User);
            mainMenuStrip.Renderer = new MainStripRenderer();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            userTableAdapter.Update(mainDBDataSet.User);
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        

        private bool UpdateDataGridView(string query, DataGridView dgv, string[] headers, ContextMenuStrip menuStrip, int selectedRow = 0)
        {
            try
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["supplyGood.Properties.Settings.MainDBConnectionString"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(ConnectionString);
                sqlconn.Open();
                SqlDataAdapter oda = new SqlDataAdapter(query, sqlconn);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                dgv.DataSource = dt;
                sqlconn.Close();
                if (headers.Length > 0)
                {
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dgv.Columns[i].HeaderText = headers[i];
                    }
                }
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dgv.Rows[i].ContextMenuStrip = menuStrip;
                }
                dgv.Rows[selectedRow].Selected = true;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void UpdateCurrentData()
        {
            switch (_View)
            {
                case TableView.Employee:
                    {
                        ApplyVisualAppearence();
                        UpdateDataGridView(
                            @"SELECT * FROM Employee",
                            dgvMain,
                            _headerEmployee,
                            contextDGV,
                            0);
                        break;
                    }
                case TableView.Good:
                    {
                        ApplyVisualAppearence();
                        UpdateDataGridView(
                            @"SELECT * FROM Good",
                            dgvMain,
                            _headerGood,
                            contextDGV,
                            0);
                        break;
                    }
                case TableView.Car:
                    {
                        ApplyVisualAppearence();
                        UpdateDataGridView(
                            @"SELECT * FROM Car",
                            dgvMain,
                            _headerCar,
                            contextDGV,
                            0);
                        break;
                    }
                case TableView.User:
                    {
                        ApplyVisualAppearence();
                        break;
                    }
            }

            if(_View == TableView.Car || _View == TableView.Good)
            {
                dgvMain.Columns[0].Width = 75;
            }
        }
        private void ApplyVisualAppearence()
        {
            switch (_View)
            {
                case TableView.Good:
                case TableView.Car:
                case TableView.Employee:
                    {
                        dgvMain.DataSource = null;
                        btnFunc.Visible = true;
                        dgvMain.ReadOnly = true;
                        dgvMain.AllowUserToAddRows = false;
                        dgvMain.AllowUserToDeleteRows = false;
                        dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        break;
                    }
                case TableView.User:
                    {
                        dgvMain.DataSource = userBindingSource;
                        btnFunc.Visible = false;
                        dgvMain.ReadOnly = false;
                        dgvMain.AllowUserToAddRows = true;
                        dgvMain.AllowUserToDeleteRows = true;
                        dgvMain.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        break;
                    }
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
                    case TableView.Employee:
                        {

                            NextForm = new ViewEmployee(currID, mode);
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
                }

                NextForm.ShowDialog();

                UpdateCurrentData();

                dgvMain.FirstDisplayedScrollingRowIndex = currRowOnTop;
                dgvMain.Rows[currRowIndex].Selected = true;
            }
            catch (Exception ex) { }
        }
        private string GetDeletingName(int curr, int currID)
        {
            switch (_View)
            {
                case TableView.Employee:
                    {
                        return "(" + dgvMain.Rows[curr].Cells[0].Value.ToString() + ") " +
                            dgvMain.Rows[curr].Cells[1].Value.ToString() + " " +
                            dgvMain.Rows[curr].Cells[2].Value.ToString();
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
                default:
                    {
                        return "(" + dgvMain.Rows[curr].Cells[0].Value.ToString() + ") ";
                    }
            }
        }


        private void EmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.Employee;

            lblMain.Text = "Сотрудники";
            lblHint.Text = "Подсказка: существует возможность просмотра расширенной " +
                "информации о сотруднике, её редактирования и удаления сотрудника. " +
                "Для этого необходимо выбрать сотрудника в таблице, нажать по нему " +
                "правой кнопкой мыши и выбрать необходимое действие";
            btnFunc.Text = "Добавить сотрудника";
            Text = lblMain.Text + " - " + _Rights;
            UpdateCurrentData();
            //(dgvMain.DataSource as DataTable).DefaultView.RowFilter = string.Format("em_surname LIKE '%{0}%'", "Лук");

        }
        private void GoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.Good;

            lblMain.Text = "Товары";
            lblHint.Text = "Подсказка: существует возможность просмотра расширенной " +
                "информации о товаре, её редактирования и удаления товара. " +
                "Для этого необходимо выбрать товар в таблице, нажать по нему " +
                "правой кнопкой мыши и выбрать необходимое действие";
            btnFunc.Text = "Добавить товар";
            Text = lblMain.Text + " - " + _Rights;
            UpdateCurrentData();
        }
        private void CarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.Car;

            lblMain.Text = "Машины";
            lblHint.Text = "Подсказка: существует возможность просмотра расширенной " +
                "информации о машине, её редактирования и удаления машины. " +
                "Для этого необходимо выбрать машину в таблице, нажать по ней " +
                "правой кнопкой мыши и выбрать необходимое действие";
            btnFunc.Text = "Добавить машину";
            Text = lblMain.Text + " - " + _Rights;
            UpdateCurrentData();
        }
        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.User;

            lblMain.Text = "Пользователи";
            lblHint.Text = "Подсказка: редактирование доступно прямо в таблицу. Все поля обязательны для заполнения";
            Text = lblMain.Text + " - " + _Rights;
            UpdateCurrentData();
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var currRowIndex = dgvMain.SelectedCells[0].RowIndex;
            int currID = Convert.ToInt32(dgvMain.Rows[currRowIndex].Cells[0].Value);

            var NextForm = new Form();

            switch (_View)
            {
                case TableView.Employee:
                    {
                        NextForm = new ViewEmployee();
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
            }

            NextForm.ShowDialog();

            int currRowOnTop = dgvMain.FirstDisplayedScrollingRowIndex;

            UpdateCurrentData();

            dgvMain.FirstDisplayedScrollingRowIndex = currRowOnTop;
            dgvMain.Rows[currRowIndex].Selected = true;

        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (Width == 1000)
            {
                Width = 1200;
            }
            else
            {
                Width = 1000;
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
                switch (_View)
                {
                    case TableView.Employee:
                        {
                            personalInfoTableAdapter.DeleteQuery(currID);
                            employeeTableAdapter.DeleteQuery(currID);
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
                }

            int currRowOnTop = dgvMain.FirstDisplayedScrollingRowIndex - 1;

            UpdateCurrentData();

            try
            {
                dgvMain.FirstDisplayedScrollingRowIndex = currRowOnTop;
            }
            catch (Exception ex) { }
        }
        private void dgvMain_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < dgvMain.ColumnCount && e.RowIndex < dgvMain.RowCount && e.Button == MouseButtons.Right)
            {
                try
                {
                    dgvMain.CurrentCell = dgvMain.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
                catch (Exception ex) { }
            }
        }

        private void dgvMain_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Вы не заполнили все необходимые поля" + Environment.NewLine + "Повторите ввод данных");
        }

        private void dgvMain_DoubleClick(object sender, EventArgs e)
        {
            Context_DetailsToolStripMenuItem_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {

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
            //base.OnRenderToolStripBorder(e);
        }
    }
}

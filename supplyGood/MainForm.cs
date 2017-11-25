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
    public enum TableView { Empty, Supply, Good, Car, Storage, Client, Employee}
    public partial class MainForm : Form
    {
        string _Rights = "";
        TableView _View = TableView.Empty;
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
        string[] _headerGood = new string[]
        {
                "ID",
                "Наименование",
                "Ед. измерения",
                "Цена за ед. (грн)"
        };


        public MainForm()
        {
            InitializeComponent();
            _Rights = "Администратор";
            Text = _Rights;
            _View = TableView.Empty;
        }
        public MainForm(string cRights)
        {
            InitializeComponent();
            _Rights = cRights;
            Text = _Rights;
            _View = TableView.Empty;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            mainMenuStrip.Renderer = new MainStripRenderer();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

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


        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.Employee;
            lblMain.Text = "Сотрудники";
            lblHint.Text = "Подсказка: существует возможность просмотра расширенной " +
                "информации о сотруднике, её редактирования и удаления сотрудника. " +
                "Для этого необходимо выбрать сотрудника в таблице, нажать по нему " +
                "правой кнопкой мыши и выбрать необходимое действие";
            ClearAllHandlers(btnFunc);
            btnFunc.Text = "Добавить сотрудника";
            btnFunc.Click += btnFunc_AddEmployee;
            Text = lblMain.Text + " - " + _Rights;
            UpdateDataGridView(
                @"SELECT * FROM Employee",
                dgvMain,
                _headerEmployee,
                contextDGV,
                0);
        }
        private void GoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _View = TableView.Good;
            lblMain.Text = "Товары";
            lblHint.Text = "Подсказка: существует возможность просмотра расширенной " +
                "информации о товаре, её редактирования и удаления товара. " +
                "Для этого необходимо выбрать товар в таблице, нажать по нему " +
                "правой кнопкой мыши и выбрать необходимое действие";
            ClearAllHandlers(btnFunc);
            btnFunc.Text = "Добавить товар";
            btnFunc.Click += btnFunc_AddGood;
            Text = lblMain.Text + " - " + _Rights;
            UpdateDataGridView(
                @"SELECT * FROM Good",
                dgvMain,
                _headerGood,
                contextDGV,
                0);
            dgvMain.Columns[0].Width = 75;
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            Context_Delete();
        }


        private void Context_ViewEdit(SubFormMode mode)
        {
            switch (_View)
            {
                case TableView.Employee:
                    {
                        var currRowIndex = dgvMain.SelectedCells[0].RowIndex;
                        int currID = Convert.ToInt32(dgvMain.Rows[currRowIndex].Cells[0].Value);

                        var NextForm = new ViewEmployee(currID, mode);
                        NextForm.ShowDialog();

                        int currRowOnTop = dgvMain.FirstDisplayedScrollingRowIndex;
                        UsersToolStripMenuItem_Click(new object(), EventArgs.Empty);
                        dgvMain.FirstDisplayedScrollingRowIndex = currRowOnTop;
                        dgvMain.Rows[currRowIndex].Selected = true;
                        break;
                    }
                case TableView.Good:
                    {
                        var currRowIndex = dgvMain.SelectedCells[0].RowIndex;
                        int currID = Convert.ToInt32(dgvMain.Rows[currRowIndex].Cells[0].Value);

                        var NextForm = new ViewGood(currID, mode);
                        NextForm.ShowDialog();

                        int currRowOnTop = dgvMain.FirstDisplayedScrollingRowIndex;
                        GoodsToolStripMenuItem_Click(new object(), EventArgs.Empty);
                        dgvMain.FirstDisplayedScrollingRowIndex = currRowOnTop;
                        dgvMain.Rows[currRowIndex].Selected = true;
                        break;
                    }
                default:
                    {

                        break;
                    }
            }
        }
        private void Context_Delete()
        {
            switch (_View)
            {
                case TableView.Employee:
                    {
                        var curr = dgvMain.SelectedCells[0].RowIndex;
                        int currID = Convert.ToInt32(dgvMain.Rows[curr].Cells[0].Value);
                        string name = "(" + dgvMain.Rows[curr].Cells[0].Value.ToString() + ") " +
                            dgvMain.Rows[curr].Cells[1].Value.ToString() + " " +
                            dgvMain.Rows[curr].Cells[2].Value.ToString();
                        if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить " + name + "?", "Удаление",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                        {
                            personalInfoTableAdapter.DeleteQuery(currID);
                            employeeTableAdapter.DeleteQuery(currID);

                            int currRowOnTop = dgvMain.FirstDisplayedScrollingRowIndex - 1;
                            UsersToolStripMenuItem_Click(new object(), EventArgs.Empty);
                            try
                            {
                                dgvMain.FirstDisplayedScrollingRowIndex = currRowOnTop;
                            }
                            catch (Exception ex) { }
                        }
                        break;
                    }
                case TableView.Good:
                    {
                        var curr = dgvMain.SelectedCells[0].RowIndex;
                        int currID = Convert.ToInt32(dgvMain.Rows[curr].Cells[0].Value);
                        string name = "(" + dgvMain.Rows[curr].Cells[0].Value.ToString() + ") " +
                            dgvMain.Rows[curr].Cells[1].Value.ToString();
                        if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить " + name + "?", "Удаление",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                        {
                            goodTableAdapter.DeleteQuery(currID);

                            int currRowOnTop = dgvMain.FirstDisplayedScrollingRowIndex - 1;
                            GoodsToolStripMenuItem_Click(new object(), EventArgs.Empty);
                            try
                            {
                                dgvMain.FirstDisplayedScrollingRowIndex = currRowOnTop;
                            }
                            catch (Exception ex) { }
                        }
                        break;
                    }
                default:
                    {

                        break;
                    }
            }
        }


        private void dgvMain_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < dgvMain.ColumnCount && e.RowIndex < dgvMain.RowCount && e.Button == MouseButtons.Right)
            {
                try
                {
                    dgvMain.CurrentCell = dgvMain.Rows[e.RowIndex].Cells[e.ColumnIndex];
                } catch (Exception ex) { }
            }
        }


        private void btnFunc_AddEmployee(object sender, EventArgs e)
        {
            var currRowIndex = dgvMain.SelectedCells[0].RowIndex;
            int currID = Convert.ToInt32(dgvMain.Rows[currRowIndex].Cells[0].Value);

            var NextForm = new ViewEmployee();
            NextForm.ShowDialog();

            int currRowOnTop = dgvMain.FirstDisplayedScrollingRowIndex;
            UsersToolStripMenuItem_Click(new object(), EventArgs.Empty);
            dgvMain.FirstDisplayedScrollingRowIndex = currRowOnTop;
            dgvMain.Rows[currRowIndex].Selected = true;

        }
        private void btnFunc_AddGood(object sender, EventArgs e)
        {
            var currRowIndex = dgvMain.SelectedCells[0].RowIndex;
            int currID = Convert.ToInt32(dgvMain.Rows[currRowIndex].Cells[0].Value);

            var NextForm = new ViewGood();
            NextForm.ShowDialog();

            int currRowOnTop = dgvMain.FirstDisplayedScrollingRowIndex;
            GoodsToolStripMenuItem_Click(new object(), EventArgs.Empty);
            dgvMain.FirstDisplayedScrollingRowIndex = currRowOnTop;
            dgvMain.Rows[currRowIndex].Selected = true;
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

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
    public partial class MainForm : Form
    {
        delegate void CurrentAction();
        public MainForm()
        {
            InitializeComponent();
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



        private bool UpdateDataGridView(string query, DataGridView dgv, params string[] headers)
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
                    dgv.Rows[i].ContextMenuStrip = cmsViewEditDelete;
                }
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


        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblMain.Text = "Сотрудники";
            lblHint.Text = "Подсказка: существует возможность просмотра расширенной " +
                "информации о сотруднике, её редактирования и удаления сотрудника. " +
                "Для этого необходимо выбрать сотрудника в таблице, нажать по нему " +
                "правой кнопкой мыши и выбрать необходимое действие";
            var captions = new string[]
            {
                "ID",
                "Фамилия",
                "Имя",
                "Отчество",
                "Дата зачисления",
                "Дата увольнения",
                "Оклад",
            };
            UpdateDataGridView(
                @"SELECT * FROM EMPLOYEE", 
                dgvMain,
                captions);


            //string myConnectionString = ConfigurationManager.ConnectionStrings["supplyGood.Properties.Settings.MainDBConnectionString"].ConnectionString;
            //SqlConnection myConnection = new SqlConnection(myConnectionString);
            //myConnection.Open();
            //try
            //{
            //    SqlDataReader myReader = null;
            //    SqlCommand myCommand = new SqlCommand("Select * from [User]",
            //                                             myConnection);
            //    myReader = myCommand.ExecuteReader();
            //    flpMain.Controls.Clear();
            //    while (myReader.Read())
            //    {
            //        UserBox user = new UserBox();
            //        user.Login.Text = myReader["login"].ToString();
            //        user.Password.Text = myReader["password"].ToString();
            //        user.Rights.Text = myReader["rights"].ToString();
            //        user.SendToBack();
            //        flpMain.Controls.Add(user);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //myConnection.Close();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var curr = dgvMain.SelectedCells[0];
            MessageBox.Show(dgvMain.Rows[curr.RowIndex].Cells[0].Value.ToString());
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var curr = dgvMain.SelectedCells[0];
            MessageBox.Show(dgvMain.Rows[curr.RowIndex].Cells[0].Value.ToString());
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var curr = dgvMain.SelectedCells[0];
            MessageBox.Show(dgvMain.Rows[curr.RowIndex].Cells[0].Value.ToString());
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

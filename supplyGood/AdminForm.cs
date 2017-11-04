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
    public partial class AdminForm : Form
    {
        delegate void CurrentAction();
        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            this.userTableAdapter.Fill(this.mainDataSet.User);
            mainMenuStrip.Renderer = new MainStripRenderer();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvMain.DataSource = userBindingSource;
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

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            userTableAdapter.Update(mainDataSet);
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

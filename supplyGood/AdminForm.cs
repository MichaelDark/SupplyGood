using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            string myConnectionString = ConfigurationManager.ConnectionStrings["supplyGood.Properties.Settings.MainDBConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(myConnectionString);
            myConnection.Open();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("Select * from [User]",
                                                         myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    MessageBox.Show(myReader["login"].ToString() + " " + myReader["password"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            myConnection.Close();
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

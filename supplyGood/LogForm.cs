using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace supplyGood
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string myConnectionString = ConfigurationManager.ConnectionStrings["supplyGood.Properties.Settings.MainDBConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(myConnectionString);
            myConnection.Open();
            string _login = txtLogin.Text;
            string _passw = txtPassword.Text;
            try
            {
                int loginExists = 0;
                using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) from [User] where login like @login", myConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@login", _login);
                    loginExists = (int)sqlCommand.ExecuteScalar();
                }
                if (loginExists > 0)
                {
                    using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) from [User] where login like @login AND password like @password", myConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@login", _login);
                        sqlCommand.Parameters.AddWithValue("@password", _passw);
                        int userCount = (int)sqlCommand.ExecuteScalar();
                        if (userCount > 0)
                        {
                            var NextForm = new MainForm();
                            NextForm.Owner = this;
                            NextForm.Show();
                            this.Hide();
                            lblError.Text = "";
                        }
                        else
                        {
                            lblError.Text = "Неверный пароль";
                        }
                    }
                }
                else
                {
                    lblError.Text = "Пользователь не найден";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            myConnection.Close();
        }
    }
}

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
                    int userCount;
                    using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) from [User] where login like @login AND password like @password", myConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@login", _login);
                        sqlCommand.Parameters.AddWithValue("@password", _passw);
                        userCount = (int)sqlCommand.ExecuteScalar();
                    }
                    if (userCount > 0)
                    {
                        Rights rights;
                        using (SqlCommand sqlCommand = new SqlCommand("SELECT * from [User] where login like @login AND password like @password", myConnection))
                        {
                            sqlCommand.Parameters.AddWithValue("@login", _login);
                            sqlCommand.Parameters.AddWithValue("@password", _passw);
                            SqlDataReader reader = sqlCommand.ExecuteReader();
                            reader.Read();
                            switch (reader["rights"].ToString())
                            {
                                case "admin":
                                    {
                                        rights = Rights.Admin;
                                        break;
                                    }
                                case "hr":
                                    {
                                        rights = Rights.HR;
                                        break;
                                    }
                                case "storage":
                                    {
                                        rights = Rights.Storage;
                                        break;
                                    }
                                default:
                                    {
                                        rights = Rights.Manager;
                                        break;
                                    }
                            }
                        }
                        var NextForm = new MainForm(rights);
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

        private void Field_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSignIn_Click(sender, EventArgs.Empty);
            }
        }
    }
}

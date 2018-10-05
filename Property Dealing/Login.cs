using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Property_Dealing
{
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection(Connection_Properties.GetConnectionString());
        SqlDataAdapter da;
        DataTable dt;

        public Login()
        {
            InitializeComponent();
        }
        private void Login_Load(object sender, EventArgs e)
        {

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Please enter user name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                return;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }
            try
            {
                if(txtUserName.Text=="admin" && txtPassword.Text=="saib")
                {
                    this.Hide();
                    Main frm = new Main();
                    frm.Show();
                }
                //else if (txtUserName.Text == "sheri" && txtPassword.Text == "9003270")
                //{
                //    this.Hide();
                //    Main frm = new Main();
                //    frm.Show();
                //}
                else
                {
                    MessageBox.Show("Please enter valid user name or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception)
            {
                
                throw;
            }
            //con.Open();
            //da = new SqlDataAdapter("select username, _password, _role from registration where username='"+txtUserName.Text+"' AND _password='"+txtPassword.Text+"'",con);
            //dt = new DataTable();
            //da.Fill(dt);
            //try
            //{
                //if (dt.Rows[0][2].ToString() == "admin")
                //{
                //    if (txtUserName.Text == dt.Rows[0][0].ToString() && txtPassword.Text == dt.Rows[0][1].ToString())
                //    {
                //        this.Hide();
                //        Main frm = new Main();
                //        frm.Show();
                //        con.Close();
                //    }
                //    else if(dt.Rows[0][2].ToString() == "clerk")
                //    {
                //        this.Hide();
                //        Main_2 frm = new Main_2();
                //        frm.Show();
                //        con.Close();
                //    }
                //    else
                //    {
                //        MessageBox.Show("Login is Failed...Try again !", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        txtUserName.Clear();
                //        txtPassword.Clear();
                //        txtUserName.Focus();
                //        con.Close();
                //    }

                //}
                //else
                //{
                //    MessageBox.Show("Login is Failed...Try again !", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    con.Close();
                //}
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Login is Failed...Try again !", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    con.Close();
            //}
            
            //con.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

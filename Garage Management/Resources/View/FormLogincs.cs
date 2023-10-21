using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Garage_Management.DAO;
using Guna.UI2.WinForms;

namespace Garage_Management
{
    public partial class FormLogincs : Form
    {
        public FormLogincs()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            if (Login(userName,password))
            {
                FormTrangChu f = new FormTrangChu();
                f.UserNameFromLogin = txtUserName.Text;
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng !");
            }
            txtUserName.Text = "";
            txtPassword.Text = "";
        }

        bool Login(string userName, string passWord)
        {
            return AccountDAO.Instance.Login(userName, passWord);
        }
    }
}

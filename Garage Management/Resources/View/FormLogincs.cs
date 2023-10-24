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

        private void FormLogincs_Load(object sender, EventArgs e)
        {

        }
     

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text; 
            string password = txtPassword.Text;
            if (Login(userName,password))
            {
                FormTrangChu f = new FormTrangChu();
                f.lbAcount.Text = userName;
                this.Hide();               
                if (MessageBox.Show("Đăng nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    f.ShowDialog();

                    this.Show();
                         
                }
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng !");
            }
            txtPassword.Text = "";
            
        }

        bool Login(string userName, string passWord)
        {
            return AccountDAO.Instance.Login(userName, passWord);
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
          
        }
    }
}

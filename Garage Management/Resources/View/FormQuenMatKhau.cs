using Garage_Management.DAO;
using Garage_Management.DAO.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Garage_Management.Resources.View
{
    public partial class FormQuenMatKhau : Form
    {
        public FormQuenMatKhau()
        {
            InitializeComponent();
        }
        Modify modify = new Modify();

        private void btnLaylaiMK_Click(object sender, EventArgs e)
        {
            string email = txtEmailQuenMatKhau.Text;
            if (email.Trim() == "")
            {
                MessageBox.Show("Vui Lòng nhập email đăng kí!");
            }
            else
            {
                string query = "Select * from Account where Email = '" + email + "'";
                if (modify.Accounts(query).Count != 0)
                {
                    lblKetQua.ForeColor = Color.Blue;
                    lblKetQua.Text = "Mật Khẩu: " + modify.Accounts(query)[0].PassWord;
                }
                else
                {
                    lblKetQua.ForeColor = Color.Red;
                    lblKetQua.Text = "Email này chưa được đăng kí";
                }
            }
        }

        private void FormQuenMatKhau_Load(object sender, EventArgs e)
        {

        }
    }
}

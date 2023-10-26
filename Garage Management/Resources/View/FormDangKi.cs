using Garage_Management.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Garage_Management.Resources.View
{
    public partial class FormDangKi : Form
    {
        public FormDangKi()
        {
            InitializeComponent();
        }
        public bool CheckAccount (string ac )
        {
            return Regex.IsMatch(ac, "^[a-zA-Z0-9]{6,24}$");
        }
        public bool CheckEmail(string em)
        {
            return Regex.IsMatch(em, "^[a-zA-Z0-9]{3,20}@gmail.com$");
        }

        private void txtXacNhanPassWord_TextChanged(object sender, EventArgs e)
        {

        }
        Modify modify = new Modify();

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string UserName = txtDangKiUserName.Text;
            string DisPlayName = txtDisplayName.Text;
            string PassWord = txtDangKiPassWord.Text;
            string Email = txtTenEmailDangKi.Text;
            string xNpassWord = txtXacNhanPassWord.Text;
            if (!CheckAccount(UserName))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản có 6 đến 24 ký tự, với các ký tự chữ và số, chữ in hoa và in thường~");
                return;
            }
            if (DisPlayName == null)
            {
                MessageBox.Show("Vui lòng nhập tên người dùng!");
                return;
            }
            if (!CheckAccount(PassWord))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu có 6 đến 24 ký tự, với các ký tự chữ và số, chữ in hoa và in thường~");
                return;
            }
            if (xNpassWord != PassWord)
            {
                MessageBox.Show("Vui lòng xác nhận đúng mật khẩu~");
                return;
            }
            if (!CheckEmail(Email))
            {
                MessageBox.Show("Vui lòng cập nhật đúng định dạng email~");
                return;
            }
            if (modify.Accounts("Select * from Account where Email = '" + Email + "'").Count != 0)
            {
                MessageBox.Show("Email này đã được sử dụng, vui lòng liên kết email khác~");
                return;
            }
            try
            {
                string query = "Insert Into Account values ('" + UserName + "','" + DisPlayName + "','" + PassWord + "','" + Email + "')";
                modify.Accounts(query);
                if (MessageBox.Show("Đăng kí tài khoản thành công ! Bạn có muốn đăng nhập?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Tên tài khoản này đã được đăng kí, vui lòng nhập tên tài khoản khác~");
                return;
            }
        }
    }
}

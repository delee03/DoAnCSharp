using Garage_Management.DAO;
using Garage_Management.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        private void btnDangKi_Click(object sender, EventArgs e)
        {
            string disPlayName = txtDisplayName.Text;
            string userName = txtDangKiUserName.Text;
            string passWord = txtDangKiPassWord.Text;
            string xNpassWord = txtXacNhanPassWord.Text;
            string email = txtTenEmailDangKi.Text;
            string type = txtType.Text;
            if (!CheckAccount(userName))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản có 6 đến 24 ký tự, với các ký tự chữ và số, chữ in hoa và in thường~");
                return;
            }
            if (disPlayName == null)
            {
                MessageBox.Show("Vui lòng nhập tên người dùng!");
                return;
            }
            if (!CheckAccount(passWord))
            {
                MessageBox.Show("Vui lòng nhập tên mật khẩu có 6 đến 24 ký tự, với các ký tự chữ và số, chữ in hoa và in thường~");
                return;
            }
            if(xNpassWord != passWord)
            {
                MessageBox.Show("Vui lòng xác nhận đúng mật khẩu~");
                return;
            }
            if (!CheckEmail(email))
            {
                MessageBox.Show("Vui lòng cập nhật đúng định dạng email~");
                return;
            }
            if(modify.Accounts("Select * from Account where Email = '"+ email +"'").Count != 0)
            {
                MessageBox.Show("Email này đã được sử dụng, vui lòng liên kết email khác~");
                return;
            }
            try
            {
                string query = "Insert Into Account values ('" + userName + "','"+disPlayName+"','" + passWord + "','"+type+"','" + email + "')";
                modify.Accounts(query);
                if (MessageBox.Show("Đăng kí tài khoản thành công!Bạn có muốn đăng nhập?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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

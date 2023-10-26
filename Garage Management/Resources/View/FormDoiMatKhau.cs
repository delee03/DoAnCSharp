using Garage_Management.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Garage_Management.Resources.View
{
    public partial class FormDoiMatKhau : Form
    {
        FormDangKi fdki = new FormDangKi();
        Connection cn = new Connection();
        public FormDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(
            "select count (*) from Account where UserName = N'" + txtTenDN.Text + "' and PassWord = N'" + txtMKcu.Text + "'", cn.GetSqlConnection()
            );
            DataTable dt = new DataTable();
            adapter.Fill( dt );
            if (dt.Rows[0][0].ToString() == "1")
            {
                if(txtMKmoi.Text == txtXacnhanMK.Text)
                {
                    if(fdki.CheckAccount(txtMKmoi.Text)) {
                        SqlDataAdapter adapter1 = new SqlDataAdapter(
                        "update Account set PassWord = N'" + txtMKmoi.Text + "' where UserName = N'" + txtTenDN.Text + "' and PassWord = N'" + txtMKcu.Text + "'", cn.GetSqlConnection()
                       );
                        DataTable dt1 = new DataTable();
                        adapter1.Fill( dt1 );
                        MessageBox.Show("Đổi mật khẩu thành công !", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTenDN.Clear();
                        txtMKcu.Clear();
                        txtMKmoi.Clear();
                        txtXacnhanMK.Clear();
                    }
                    else
                    {
                        txtMKmoi.Focus();
                        MessageBox.Show("Mật khẩu mới phải có 6 đến 24 ký tự, với các ký tự chữ và số, chữ in hoa và in thường~");
                        return;
                    }
                }
                else
                {
                    txtXacnhanMK.Focus();
                    MessageBox.Show("Mật khẩu được nhập lại chưa đúng !", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                txtMKcu.Focus();
                MessageBox.Show("Tên Đăng nhập hoặc mật khẩu cũ không đúng ! Nhập lại?","Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
        }
    }
}

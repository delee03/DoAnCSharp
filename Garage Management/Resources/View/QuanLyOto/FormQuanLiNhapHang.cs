using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Garage_Management.Resources.View.QuanLyOto
{
    public partial class FormQuanLiNhapHang : Form
    {
        public FormQuanLiNhapHang()
        {
            InitializeComponent();
        }

        private void FormQuanLiNhapHang_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLapPhieu_Click(object sender, EventArgs e)
        {
            FormNhapHang f = new FormNhapHang();
            f.Show();
        }
    }
}

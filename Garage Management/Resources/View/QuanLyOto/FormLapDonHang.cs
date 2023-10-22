using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Garage_Management.Entities;

namespace Garage_Management.Resources.View.QuanLyOto
{
    public partial class FormLapDonHang : Form
    {
        public FormLapDonHang()
        {
            InitializeComponent();
        }
        FormQuanLiDonHang form;
        public FormLapDonHang(FormQuanLiDonHang form)
        {
            InitializeComponent();
            this.form = form;
        }
        CarModel context = new CarModel();
        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }
     
        private void btnAdd_Click(object sender, EventArgs e)
        {
          
           
            int index = form.dgvDonHang.Rows.Add();
            form.dgvDonHang.Rows[index].Cells[0].Value = txtIDHoaDon.Text;
            form.dgvDonHang.Rows[index].Cells[1].Value = txtTenKH.Text;
            form.dgvDonHang.Rows[index].Cells[2].Value = txtSDT.Text;
            form.dgvDonHang.Rows[index].Cells[3].Value = txtTenNV.Text;
            form.dgvDonHang.Rows[index].Cells[4].Value = txtTenXe.Text;
            form.dgvDonHang.Rows[index].Cells[5].Value = picCar.Image;
            form.dgvDonHang.Rows[index].Cells[6].Value = txtGiaXe.Text;
            form.dgvDonHang.Rows[index].Cells[7].Value = dtPicker.Text;

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

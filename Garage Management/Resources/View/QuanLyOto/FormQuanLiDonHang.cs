using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Garage_Management.Entities;
using Guna.UI2.WinForms;
namespace Garage_Management.Resources.View.QuanLyOto
{
    public partial class FormQuanLiDonHang : Form
    {
       // public QuanLyOto form;
        CarModel context = new CarModel();
       
        
        public FormQuanLiDonHang()
        {
            InitializeComponent();

        }
        

        private void btnAddBill_Click(object sender, EventArgs e)
        {
            FormLapDonHang f = new FormLapDonHang(this);
            f.Show();
        }

        private void FormQuanLiDonHang_Load(object sender, EventArgs e)
        {
            try
            {
                List<HoaDon> listBill = context.HoaDons.ToList();
                BindGrid(listBill);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindGrid(List<HoaDon> listBill)
        {
            dgvDonHang.Rows.Clear();
            foreach (var item in listBill)
            {
                int index = dgvDonHang.Rows.Add();
                dgvDonHang.Rows[index].Cells[0].Value = item.idHoaDon;
                dgvDonHang.Rows[index].Cells[1].Value = item.tenKH;
                dgvDonHang.Rows[index].Cells[2].Value = item.sdt + "";
                dgvDonHang.Rows[index].Cells[3].Value = item.tenNV;
                dgvDonHang.Rows[index].Cells[4].Value = item.Car.nameCar;
                dgvDonHang.Rows[index].Cells[5].Value = item.imageCar;
                dgvDonHang.Rows[index].Cells[6].Value = item.Car.price + "";
                dgvDonHang.Rows[index].Cells[7].Value = item.ngayLap;
               
            }
        }
    }
}

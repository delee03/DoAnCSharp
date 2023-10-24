using System;
using System.Collections.Generic;
using Garage_Management.BUS;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Garage_Management.Entities;
using Guna.UI2.WinForms;
using System.Xml.Linq;

namespace Garage_Management.Resources.View.QuanLyOto
{
    public partial class FormQuanLiDonHang : Form
    {
       // public QuanLyOto form;
        CarModel context = new CarModel();
       DataQuery query = new DataQuery();
        private bool isUpdate = false;
        public FormQuanLiDonHang()
        {
            InitializeComponent();
            fEdit = new FormLapDonHang(this);
        }
        FormLapDonHang fEdit;
       

        public void FormQuanLiDonHang_Load(object sender, EventArgs e)
        {
            try
            {
                List<HoaDon> listBill = query.GetHoaDons();
                BindGrid(listBill);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load data " + ex.Message);
            }
        }

        public void BindGrid(List<HoaDon> listBill)
        {
            dgvDonHang.Rows.Clear();
           /* // Thêm HoaDon mới để truy xuất thuộc tính từ table Car bằng phương thức lazy load trong entiti
            var hoadon = new HoaDon();
            hoadon.idCar = Car.idCar;
            // Lấy thông tin CAr          
            var car = hoadon.Car;
            // Truy xuất các property của Car
            string thuonghieu = car.nameCar;
            double gia = car.price;*/


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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            for (int i = 0; i < dgvDonHang.Rows.Count - 1; i++)
            {
                if (dgvDonHang.Rows[i].Cells[0].Value.ToString().ToLower().Contains(keyword.ToLower()))
                {
                    dgvDonHang.Rows[i].Visible = true;
                }
                else
                {
                    dgvDonHang.Rows[i].Visible = false;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {         
            try
            {            
                 DialogResult rs = MessageBox.Show("Bạn có chắc muốn xóa ", "Xóa", MessageBoxButtons.OKCancel);
                 if (rs == DialogResult.OK)
                 {          
                     query.DeleteByID(txtSearch.Text);
                     context.SaveChanges();
                     FormQuanLiDonHang_Load(sender, e);
                 }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tồn tại hóa đơn nào" + ex.Message);
            }
        }

        private void txtSearch_MouseHover(object sender, EventArgs e)
        {
            txtSearch.Text = "Nhập id Hóa đơn để tìm kiếm";
        }

        private void dgvDonHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                dgvDonHang_CellClick(sender, e);
                fEdit.ShowDialog();
            }
        }

        private void dgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           // isUpdate = true;
            if (dgvDonHang.Rows.Count > 0)
            {
                DataGridViewRow row = dgvDonHang.Rows[e.RowIndex];
                txtSearch.Text =row.Cells[0].Value.ToString();
                txtTongTien.Text = row.Cells[6].Value.ToString();
                string valueId = row.Cells[0].Value.ToString();
                fEdit.SetTextBoxValues(valueId);
            }
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
        }
    }
}

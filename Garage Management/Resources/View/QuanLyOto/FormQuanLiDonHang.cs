using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using Guna.UI2.WinForms;
using System.Xml.Linq;
using Garage_Management.DAO;
using Garage_Management.DAO.Entities;
using Garage_Management.BUS;
using iTextSharp.text.pdf;

namespace Garage_Management.Resources.View.QuanLyOto
{
    public partial class FormQuanLiDonHang : Form
    {
        
        CarModel context = new CarModel();
        DataQuery query = new DataQuery();
        private bool isUpdate = false;
        public FormQuanLiDonHang()
        {
            InitializeComponent();
            fEdit = new FormLapDonHang(this);
            dataform = new Garage_Management.QuanLyOto(this);
        }
        FormLapDonHang fEdit;

        Garage_Management.QuanLyOto dataform;
       


        public void FormQuanLiDonHang_Load(object sender, EventArgs e)
        {
            try
            {
               
                List<HoaDon> listBill = query.GetHoaDons();
                dgvDonHang.Rows.Clear();
                BindGrid(listBill);             

            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load data " + ex.Message);
            }
        }

        public void SetTextBoxValues(string value)
        {
            txtSearch.Text = value;
        }

        public void BindGrid(List<HoaDon> listBill)
        {
            dgvDonHang.Rows.Clear();
          
            double Tong = 0;

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
                Tong += item.Car.price;
 
            }
            txtTongTien.Text = Tong + "";
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
                fEdit.btnAdd.Visible = false;
                fEdit.btnCancel.Visible = false;
                fEdit.ShowDialog();

            }
           
            if (e.ColumnIndex == 9)
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
            if (e.ColumnIndex == 10)
            {
               
                using (var existingFileStream = new FileStream(@"Resources\Template\invoiceCar.pdf", FileMode.Open))
                using (var newFileStream = new FileStream("hoadon.pdf", FileMode.Create))
                {
                    // mở file PDF có trong máy để đọc
                    var pdfReader = new PdfReader(existingFileStream);

                    // PdfStampe để chỉnh sửa file 
                    using (var stamper = new PdfStamper(pdfReader, newFileStream))
                    {

                        var form = stamper.AcroFields;
                        var fieldKeys = form.Fields.Keys;

                        var row = dgvDonHang.Rows[e.RowIndex];
                        string txtTenKH = row.Cells[1].Value.ToString();
                        string txtSDT = row.Cells[2].Value.ToString();
                        string txtTenNV = row.Cells[3].Value.ToString();
                        string txtnameCar = row.Cells[4].Value.ToString();
                        string txtGia = row.Cells[6].Value.ToString();
                        string txtTong = row.Cells[6].Value.ToString();

                        // đổ vào fields
                        form.SetField("txtTenKH", txtTenKH);
                        form.SetField("txtSDT", txtSDT);
                        form.SetField("txtTenNV", txtTenNV);
                        form.SetField("txtTenXe", txtnameCar);
                        form.SetField("txtGia", txtGia);
                        form.SetField("txtTongTien", txtTong);
                        form.SetField("txtTenKH", txtTenKH);
                        form.SetField("txtSDT", txtSDT);

                        stamper.Close();
                    }
                    pdfReader.Close();
                    MessageBox.Show("Xuất file hóa đơn thành công !", "Xuất hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                System.Diagnostics.Process.Start("hoadon.pdf");
            }          
        }

        private void dgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // isUpdate = true;
            if (dgvDonHang.Rows.Count > 0)
            {
                DataGridViewRow row = dgvDonHang.Rows[e.RowIndex];
                txtSearch.Text = row.Cells[0].Value.ToString();
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

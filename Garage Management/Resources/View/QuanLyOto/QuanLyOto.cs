﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using Garage_Management.DAO;
using Garage_Management.DAO.Entities;
using Garage_Management.Resources.View.QuanLyOto;
using Image = System.Drawing.Image;
using Garage_Management.BUS;

namespace Garage_Management
{
    public partial class QuanLyOto : Form
    {
        FormLapDonHang fLap;

        FormQuanLiDonHang fDonHang;
      
        public QuanLyOto()
        {
            InitializeComponent();
           fLap = new FormLapDonHang();
          
        }
        public QuanLyOto(FormQuanLiDonHang form)
        {
            InitializeComponent();
            fDonHang = form;

        }

        private bool isCreateBill = false;
        CarModel context = new CarModel();
        DataQuery query = new DataQuery();
        public void QuanLyOto_Load(object sender, EventArgs e)
        {
            try
            {  
                List<Car> listCar = context.Cars.ToList();
                List<Suplier> listSup = context.Supliers.ToList();
                FillCmbSuplier(listSup);
                BindGrid(listCar);           
                cboNcc.SelectedIndex = 0;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Không load được" + ex.Message);
            }
        }

        private void FillCmbSuplier(List<Suplier> list)
        {
            cboNcc.DataSource = list;
            cboNcc.DisplayMember = "nameSup";
            cboNcc.ValueMember = "idSup";
        }
        public void BindGrid(List<Car> listCar)
        {
            dgvOto.Rows.Clear();
            foreach (var item in listCar)
            {
                int index = dgvOto.Rows.Add();
                dgvOto.Rows[index].Cells[0].Value = item.idCar;
                dgvOto.Rows[index].Cells[1].Value = item.nameCar;
                dgvOto.Rows[index].Cells[2].Value = item.imageCar;
                dgvOto.Rows[index].Cells[3].Value = item.Suplier.nameSup;
                dgvOto.Rows[index].Cells[4].Value = item.ngayNhap.ToString();
                dgvOto.Rows[index].Cells[5].Value = item.price + "";
            }
        }
        public void SetNameValues(string value)
        {
            txtTemp.Text = value;
        }
        //sau khi nhấn load xe nằm trong Form Quản lí đơn hàng và xóa nó ở QUản lí oto;
        private void btnLoad_Click(object sender, EventArgs e)
        {
            DeleteCarAfterAddBill(txtTemp.Text);
        }

        private bool isNumeric(string Nam)
        {
            bool checkNum = long.TryParse(txtGia.Text, out long result);
            if (Nam.Contains(","))
                return false;
            return checkNum;
        }

        public bool DataBinding()
        {
            if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtTen.Text) ||
                string.IsNullOrEmpty(txtGia.Text) || string.IsNullOrEmpty(cboNcc.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !");
                return false;
            }

            if (query.FindByIDCar(txtID.Text) != null)
            {
                txtID.Focus();
                MessageBox.Show("ID Oto đã tồn tại!");
                return false;
            }

            if (Regex.IsMatch(txtTen.Text, "^[0-9]*$"))
            {
                //txtTenKH.Focus();
                MessageBox.Show("Tên Oto chỉ gồm các ký tự chữ cái !");
                return false;
            }
          
           
            if (!isNumeric(txtGia.Text))
            {
                txtGia.Focus();
                MessageBox.Show("Giá không bao gồm chữ !");
                return false;
            }
            return true;
        }

        public bool DataBindingUpdate()
        {
            if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtTen.Text) ||
                string.IsNullOrEmpty(txtGia.Text) || string.IsNullOrEmpty(cboNcc.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !");
                return false;
            }

            if (Regex.IsMatch(txtTen.Text, "^[0-9]*$"))
            {
                //txtTenKH.Focus();
                MessageBox.Show("Tên Oto chỉ gồm các ký tự chữ cái !");
                return false;
            }

            if (!isNumeric(txtGia.Text))
            {
                txtGia.Focus();
                MessageBox.Show("Giá không bao gồm chữ !");
                return false;
            }
            return true;
        }

        //hàm chuyển image sang byte[]
        public byte[] ImageToByteArray(PictureBox pic)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                pic.Image.Save(ms, GetRawFormat(pic));
                return ms.ToArray();
            }
        }
        /*MemoryStream ms = new MemoryStream((byte[])row.Cells[2].Value);
        picImage.Image = System.Drawing.Image.FromStream(ms);*/
        public Image ByteArrayToImage(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Image img = Image.FromStream(ms);
                return img;
            }
        }
        private static ImageFormat GetRawFormat(PictureBox pic)
        {
            return pic.Image.RawFormat;
        }


        //Hàm tìm kiếm thông tin
        private string RemoveDiacritics(string text)
        {
            string formD = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char ch in formD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }


        private void btnImage_Click_1(object sender, EventArgs e)
        {

            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Chọn ảnh";
            open.FileName = "Image Files (*.gif; *.jpg; *.png; *.jpeg; *.bmp) |*.gif; *.wmf; *.jpg; *.jpeg; *.bmp; *.png;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                picImage.ImageLocation = open.FileName;
                MessageBox.Show("Thêm ảnh thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (DataBindingUpdate())
            {
                var id = dgvOto.SelectedCells[0].OwningRow.Cells["Column1"].Value;
                Car car = context.Cars.Find(id);
                context.Cars.Remove(car);
                Car newcar = new Car();
                newcar.idCar = txtID.Text;
                newcar.nameCar = txtTen.Text;
                newcar.imageCar = ImageToByteArray(picImage);
                newcar.idSup = Convert.ToInt32(cboNcc.SelectedValue);
                newcar.ngayNhap = Convert.ToDateTime(dtPicker.Text);
                newcar.price = Convert.ToDouble(txtGia.Text);
                context.Cars.Add(newcar);
                context.SaveChanges();
                QuanLyOto_Load(sender, e);
             }
           
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
               if(DataBinding())
                {
                    Car car = new Car()
                    {
                        idCar = txtID.Text,
                        nameCar = txtTen.Text,
                        imageCar = ImageToByteArray(picImage),
                        idSup = Convert.ToInt32(cboNcc.SelectedValue),
                        ngayNhap = Convert.ToDateTime(dtPicker.Text),
                        price = Convert.ToDouble(txtGia.Text),

                    };
                    context.Cars.Add(car);
                    context.SaveChanges();
                    QuanLyOto_Load(sender, e);
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteCarAfterAddBill(string nameCarr)
        {
            try
            {
                query.DeleteByIDCar(nameCarr);

                Car car = query.FindByNameCar(nameCarr);
                if (car == null)
                {
                    MessageBox.Show("Xe đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK);
                    List<Car> list = query.GetCar();
                    BindGrid(list);
                }
                else
                {
                    MessageBox.Show("Không thể xóa xe có ID: " + nameCarr, "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi truy vấn cơ sở dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không xác định: " + ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
        }

        public void btnDelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult rs = MessageBox.Show("Bạn có chắc muốn xóa Oto này ", "THÔNG BÁO", MessageBoxButtons.OKCancel);
                if (rs == DialogResult.OK)
                {
                    Car s = context.Cars.FirstOrDefault(p => p.idCar == txtID.Text);
                    if (s == null)
                        throw new System.Exception("Không tìm thấy Oto cần xóa");
                    context.Cars.Remove(s);
                    context.SaveChanges();
                    QuanLyOto_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tồn tại oto nào" + ex.Message);
            }


        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            string findName = txtSearch.Text;
            findName = RemoveDiacritics(findName);
            for (int i = 0; i < dgvOto.Rows.Count - 1; i++)
            {
                string name = dgvOto.Rows[i].Cells[1].Value.ToString();

                name = RemoveDiacritics(name);

                bool contains = name.IndexOf(findName, StringComparison.OrdinalIgnoreCase) >= 0;
                if (contains)
                {
                    dgvOto.Rows[i].Visible = true;
                }
                else
                {
                    dgvOto.Rows[i].Visible = false;
                }
            }
        }

        private void dgvOto_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            isCreateBill = true;
            if (dgvOto.Rows.Count > 0)
            {
                DataGridViewRow row = dgvOto.SelectedRows[0];
                txtID.Text = row.Cells[0].Value.ToString();
                txtTen.Text = row.Cells[1].Value.ToString();
                if (row.Cells[2].Value != null)
                {
                    MemoryStream ms = new MemoryStream((byte[])row.Cells[2].Value);
                    picImage.Image = Image.FromStream(ms);

                }
                else
                {
                    picImage.Image = null;
                }
                cboNcc.Text = row.Cells[3].Value.ToString();
                dtPicker.Text = row.Cells[4].Value.ToString();
                txtGia.Text = row.Cells[5].Value.ToString();
            }
        }

        private Form currentForm;
        private void OpenChildForm(Form childForm)
        {
            if (currentForm != null)
            {
                currentForm.Opacity = 50;
                currentForm.Close();
            }
            currentForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            guna2Panel1.Controls.Add(childForm);
            guna2Panel1.Tag = childForm;
            childForm.BringToFront();
            childForm.Opacity = 50;
            childForm.Show();
            childForm.Opacity = 100;

        }



        private void cHỨCNĂNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentForm != null)
            {
                currentForm.Close();
            }
        }

        private void btnQuanLiDonHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQuanLiDonHang());
        }


        private void btnLapHoaDon_Click_1(object sender, EventArgs e)
        {
            if (isCreateBill)
            {
                FormLapDonHang f = new FormLapDonHang();
                f.txtIdCar.Text = txtID.Text;
                f.picCar.Image = picImage.Image;
                f.Show();
                f.btnLoad.Visible = false;
                f.btnUpdate.Visible = false;            }
            
            else
            {
                MessageBox.Show("Bạn chưa chọn oto nào để lập hóa đơn !!");
            }
            isCreateBill = false;

        }

        private void txtSearch_MouseHover(object sender, EventArgs e)
        {
            txtSearch.Text = "Nhập tên xe";
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
        }

      
    }
}


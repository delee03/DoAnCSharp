using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Garage_Management.BUS;
using Garage_Management.DAO;
using Garage_Management.DAO.Entities;
using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;

namespace Garage_Management.Resources.View.QuanLyOto
{
    public partial class FormLapDonHang : Form
    {

        private readonly CarModel context = new CarModel();
        private readonly DataQuery query = new DataQuery();
       
        HoaDon newHD = new HoaDon();
        public FormLapDonHang()
        {
            InitializeComponent();
        }
        FormQuanLiDonHang mainform;
        public FormLapDonHang(FormQuanLiDonHang form)
        {
            InitializeComponent();
            mainform = form;
        }
        public void SetTextBoxValues(string value)
        {
            txtIDHoaDon.Text = value;
        }
        public void FormLapDonHang_Load(object sender, EventArgs e)
        {
            //newHD = query.FindByID(txtIDHoaDon.Text);
            //txtTenKH.Text = newHD.tenKH;
            //txtSDT.Text = newHD.sdt;
            //txtTenNV.Text = newHD.tenNV;    
            //txtIdCar.Text = newHD.idCar;
            //picCar.Image = ByteArrayToImage(newHD.imageCar);
            //dtPicker.Text = newHD.ngayLap.ToString();
        }
        public Image ByteArrayToImage(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Image img = Image.FromStream(ms);
                return img;
            }
        }
        public byte[] ImageToByteArray(PictureBox pic)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                pic.Image.Save(ms, GetRawFormat(pic));
                return ms.ToArray();
            }
        }
        private static ImageFormat GetRawFormat(PictureBox pic)
        {
            return pic.Image.RawFormat;
        }             
        private void btnAdd_Click(object sender, EventArgs e)
        {                    
            try
            {
                if (DataBinding())
                {
                     HoaDon hd = new HoaDon()
                     {
                         idHoaDon = txtIDHoaDon.Text,
                         tenKH = txtTenKH.Text,
                         sdt = txtSDT.Text,
                         tenNV = txtTenNV.Text,  
                         idCar = txtIdCar.Text,                                          
                         imageCar = ImageToByteArray(picCar),
                         ngayLap = Convert.ToDateTime(dtPicker.Text)
                     };
                     context.HoaDons.Add(hd);
                    context.SaveChanges();
                    //form.FormQuanLiDonHang_Load(sender, e);
                    MessageBox.Show("Thêm hóa đơn thành công !", "Thông báo",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }                        
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);
            }         
        }

        private bool isNumberic(string SDT)
        {
            bool checkNumb = long.TryParse(SDT, out long result);
            if (SDT.Contains(","))
                return false;
            return checkNumb;
        }
        public bool DataBinding()
        {
            if (string.IsNullOrEmpty(txtIDHoaDon.Text) || string.IsNullOrEmpty(txtTenKH.Text) ||
                string.IsNullOrEmpty(txtTenNV.Text) || string.IsNullOrEmpty(txtSDT.Text))             
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !");
                return false;
            }

            if (query.FindByID(txtIDHoaDon.Text) != null)
            {
                txtIDHoaDon.Focus();
                MessageBox.Show("Mã số hóa đơn đã tồn tại!");
                return false;
            }        

            if (Regex.IsMatch(txtTenKH.Text, "^[0-9]*$"))
            {
                //txtTenKH.Focus();
                MessageBox.Show("Tên khách hàng chỉ gồm các ký tự chữ cái !");
                return false;
            }
            if (Regex.IsMatch(txtTenNV.Text, "^[0-9]*$"))
            {
                txtTenNV.Focus();
                MessageBox.Show("Tên nhân viên chỉ gồm các ký tự chữ cái !");
                return false;
            }
            if (txtSDT.Text.Length != 10)
            {
                MessageBox.Show("số điện thoại không đúng format!");
                return false;
            }
            if (!isNumberic(txtSDT.Text))
            {
                txtSDT.Focus();
                MessageBox.Show("số điện thoại không đúng format !");
                return false;
            }        
            return true;
        }

        //Chọn ảnh cho edit
        private void picCar_Click(object sender, EventArgs e)
        {
          /*  OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select a picture";
            open.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                picCar.Image = new Bitmap(open.FileName);
                has_img = true;
            }*/
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
          
               newHD.idHoaDon = txtIDHoaDon.Text;
               newHD.tenKH = txtTenKH.Text;
               newHD.tenNV = txtTenNV.Text;
               newHD.sdt = txtSDT.Text;
               newHD.ngayLap = Convert.ToDateTime(dtPicker.Text);           
               newHD.imageCar = ImageToByteArray(picCar);
               query.UpdateBill(newHD);
               MessageBox.Show("Cập nhật thông tin HÓA ĐƠN thành công !", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<HoaDon> updateList = query.GetHoaDons();
                mainform.BindGrid(updateList);
                this.Close();
            
        }
    }
}

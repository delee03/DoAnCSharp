using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

        public FormLapDonHang()
        {
            InitializeComponent();
        }

        
        HoaDon newHD = new HoaDon();
       
    
        FormQuanLiDonHang mainform;
        Garage_Management.QuanLyOto data;
        public FormLapDonHang(FormQuanLiDonHang form)
        {
            InitializeComponent();
            mainform = form;
        }
        public FormLapDonHang(Garage_Management.QuanLyOto f)
        {
            InitializeComponent();
            data = f;
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
            checkBoxSDT.Visible = false;
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
                using (CarModel context = new CarModel())
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
                        query.AddBill(hd);              
                        MessageBox.Show("Thêm hóa đơn thành công! Bạn vui lòng xem thông tin trong Quản lí đơn hàng nhé! !", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);             
                        this.Close();
                    }
                };
            }

            catch (FormatException)
            {
                MessageBox.Show("Ngày bạn nhập không hợp lệ.", "Thông báo", MessageBoxButtons.OK);
            }
         /*   catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);
            }*/
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
                MessageBox.Show("số điện thoại gồm 10 chữ số");
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

        private bool isNotNullValue()
        {
            if (txtIdCar.Text == "")
                return false;
            if (txtIDHoaDon.Text == "")
                return false;
            if (txtTenKH.Text == "")
                return false;
            if (txtSDT.Text == "")
                return false;
            if (txtTenNV.Text == "")
                return false;
            return true;
        }

        private bool isNumeric(string Nam)
        {
            bool checkNum = long.TryParse(Nam, out long result);
            if (Nam.Contains(","))
                return false;
            return checkNum;
        }
        private void isValidInputData()
        {
            if (!isNotNullValue())
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !.");
            if (txtIDHoaDon.Text.Length != 4)
                MessageBox.Show("ID hóa đơn phải có 4 kí tự !.");
            if (!isNumeric(txtSDT.Text))
                MessageBox.Show("Vui lòng nhập số !\n Bạn đang nhập chữ !");
            if (Regex.IsMatch(txtTenKH.Text, "^[0-9]*$"))
            {
                txtTenKH.Focus();
                MessageBox.Show("Tên khách hàng chỉ gồm các ký tự chữ cái !");
            }
            if (Regex.IsMatch(txtTenNV.Text, "^[0-9]*$"))
            {
                txtTenNV.Focus();
                MessageBox.Show("Tên nhân viên chỉ gồm các ký tự chữ cái !");
            }

        }
        public bool DataBindingUpdate()
        {
            if (string.IsNullOrEmpty(txtIDHoaDon.Text) || string.IsNullOrEmpty(txtTenKH.Text) ||
                string.IsNullOrEmpty(txtTenNV.Text) || string.IsNullOrEmpty(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !");
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
          /*  if (txtSDT.Text.Length != 10)
            {
                MessageBox.Show("số điện thoại gồm 10 chữ số");
                return false;
            }*/
            if (!isNumberic(txtSDT.Text))
            {
                txtSDT.Focus();
                MessageBox.Show("số điện thoại không đúng format !");
                return false;
            }
            return true;
        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            if (DataBindingUpdate())
            {
                newHD.idHoaDon = txtIDHoaDon.Text;
                newHD.tenKH = txtTenKH.Text;
                newHD.tenNV = txtTenNV.Text;
                newHD.sdt = txtSDT.Text;
                newHD.ngayLap = Convert.ToDateTime(dtPicker.Text);
                //   newHD.imageCar = ImageToByteArray(picCar);
                query.UpdateBill(newHD);
                MessageBox.Show("Cập nhật thông tin HÓA ĐƠN thành công !", "Thông báo",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<HoaDon> updateList = query.GetHoaDons();
                mainform.BindGrid(updateList);
                lbThongBao.Visible = false;
                this.Close();
            }
         
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            newHD = query.FindByID(txtIDHoaDon.Text);
            txtTenKH.Text = newHD.tenKH;
            txtSDT.Text = newHD.sdt;
            txtTenNV.Text = newHD.tenNV;
            txtIdCar.Text = newHD.idCar;
            picCar.Image = ByteArrayToImage(newHD.imageCar);
            dtPicker.Text = newHD.ngayLap.ToString();
            lbThongBao.Visible = true;
        }

        private void txtIdCar_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Text = string.Empty;
            this.Close();
        }
       
        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            checkBoxSDT.Visible = true;
            if(txtSDT.Text.Length == 10)
            {
                checkBoxSDT.Checked = true;
            }
            else
            {
                checkBoxSDT.Checked = false;
            }
        }
    }
}


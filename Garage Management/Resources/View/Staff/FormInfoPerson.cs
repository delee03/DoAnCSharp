using Garage_Management.BUS;
using Garage_Management.DAO.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace Garage_Management.Resources.View
{
    public partial class FormInfoPerson : Form
    {
        private readonly DataQuery dataQuery = new DataQuery();
        private readonly DataContext dataContext = new DataContext();
        private bool has_img = false;

        public FormInfoPerson()
        {
            InitializeComponent();
        }

        private void FormInfoPerson_Load(object sender, EventArgs e)
        {
            try
            {
                var listStaffs = dataQuery.GetStaff();
                //BindGridStaff(listStaffs);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindGridStaff(List<Staff> listStaffs)
        {
            foreach (var item in listStaffs)
            {
                txtMS.Text = item.id;
                txtHoVaTen.Text = item.name;
                txtSĐT.Text = item.phone;
                txtDiaChi.Text = item.address;
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select a picture";
            ofd.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pbAvatar.Image = new Bitmap(ofd.FileName);
                has_img = true;
            }
        }

        public void isValidInputData()
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataBinding())
                {
                    using (var dbcontext = new CarModel())
                    {
                        Staff staff = new Staff()
                        {
                            id = txtMS.Text,
                            //Avartar_image = null,
                            Avatar_image = has_img ? dataContext.ImageToByteArrary(pbAvatar) : dataContext.ImageToByteArrary(this.pbAvatar),
                            name = txtHoVaTen.Text,
                            phone = txtSĐT.Text.Trim(),
                            address = txtDiaChi.Text,
                        };
                        dbcontext.Staffs.Add(staff);
                        dbcontext.SaveChanges();
                    }
                    MessageBox.Show("Thêm nhân viên mới thành công !", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void txtHoVaTen_TextChanged(object sender, EventArgs e)
        {
            //txtHoVaTen.Text = Regex.Replace(txtHoVaTen.Text, "^[0-9]+", "");
        }

        public bool DataBinding()
        {
            if (string.IsNullOrEmpty(txtMS.Text) || string.IsNullOrEmpty(txtHoVaTen.Text) ||
                string.IsNullOrEmpty(txtSĐT.Text) || string.IsNullOrEmpty(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !");
                return false;
            }

            if (dataQuery.GetStaffByID(txtMS.Text) != null)
            {
                txtMS.Focus();
                MessageBox.Show("Mã số nhân viên tồn tại!");
                return false;
            }

            if (txtMS.Text.Length > 10)
            {
                txtMS.Focus();
                MessageBox.Show("Mã số nhân viên phải bé hơn 10 !");
                return false;
            }

            if (Regex.IsMatch(txtHoVaTen.Text, "^[0-9]*$"))
            {
                txtHoVaTen.Focus();
                MessageBox.Show("Tên nhân viên chỉ gồm các ký tự chữ cái !");
                return false;
            }

            if (Regex.IsMatch(txtSĐT.Text, "^[a-z]*$"))
            {
                txtSĐT.Focus();
                MessageBox.Show("Số điện thoại không được bao gồm ký tự chữ cái !");
                return false;
            }
            return true;
        }
    }
}

using Garage_Management.BUS;
using Garage_Management.DAO;
using Garage_Management.DAO.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Garage_Management.Resources.View.Nhân_sự
{
    public partial class FormEditPerson : Form
    {
        private readonly DataQuery query = new DataQuery();

        private readonly DataContext context = new DataContext();

        Staff staff = new Staff();

        private FormNhanSu mainForm;

        bool has_img = false;

        //public delegate void DataChangedEventHandler(object sender, EventArgs e);
        //public event DataChangedEventHandler DataChanged;
        public FormEditPerson()
        {
            InitializeComponent();
        }

        // khởi tạo tham chiếu tới form nhân sự
        public FormEditPerson(FormNhanSu form)
        {
            InitializeComponent();
            mainForm = form;
        }

        private void FormEditPerson_Load(object sender, EventArgs e)
        {
            staff = query.GetStaffByID(txtMS.Text);

            pbAvatar.Image = context.ByteArrayToImage(staff.Avatar_image);
            txtHoVaTen.Text = staff.name;
            txtSĐT.Text = staff.phone;
            txtDiaChi.Text = staff.address;
        }

        public void SetTextBoxValues(string value)
        {
            txtMS.Text = value;
        }

        private void btnChonAnhEdit_Click(object sender, EventArgs e)
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

        public void btnEdit_Click(object sender, EventArgs e)
        {
            if(DataBinding())
            {
                staff.Avatar_image   = has_img ? context.ImageToByteArrary(this.pbAvatar) : staff.Avatar_image;
                staff.name = txtHoVaTen.Text;
                staff.phone = txtSĐT.Text;
                staff.address = txtDiaChi.Text;

                query.UpdateStaff(staff);
                MessageBox.Show("Cập nhật thông tin nhân viên thành công !", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Staff> updateListStaff = query.GetStaff();
                mainForm.BindGridStaff(updateListStaff);
                this.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có chắc muốn xóa nhân viên này ?","Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(rs == DialogResult.Yes)
            {
                query.DeleteStaff(txtMS.Text);
                MessageBox.Show("Đã xóa nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Staff> deleteStaff = query.GetStaff();
                mainForm.BindGridStaff(deleteStaff);
                this.Close();
            }
        }

        public bool DataBinding()
        {
            if (string.IsNullOrEmpty(txtMS.Text) || string.IsNullOrEmpty(txtHoVaTen.Text) ||
                string.IsNullOrEmpty(txtSĐT.Text) || string.IsNullOrEmpty(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !");
                return false;
            }
            if (txtMS.Text.Length > 10)
            {
                txtMS.Focus();
                MessageBox.Show("Mã số nhân viên phải bé hơn 10 !");
                return false;
            }
            return true;
        }

        private void txtSĐT_TextChanged(object sender, EventArgs e)
        {
            txtSĐT.Text = Regex.Replace(txtSĐT.Text, "[^0-9]+", "");
        }

        private void txtHoVaTen_TextChanged(object sender, EventArgs e)
        {
          //  txtHoVaTen.Text = Regex.Replace(txtHoVaTen.Text, "[^a-z]+", "");
        }
    }
}

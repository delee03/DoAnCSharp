using Garage_Management.BUS;
using Garage_Management.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Garage_Management.Resources.View.Nhân_sự
{
    public partial class FormEditPerson : Form
    {
        DataQuery query = new DataQuery();

        DataContext context = new DataContext();

        //FormEditPerson fedit = new FormEditPerson();

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

            pbAvatar.Image = context.ByteArrayToImage(staff.Avartar_image);
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
            staff.Avartar_image = has_img ? context.ImageToByteArrary(pbAvatar) : context.ImageToByteArrary(this.pbAvatar);
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
    }
}

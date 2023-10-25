using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Garage_Management.DAO;
using Garage_Management.Resources.View;
using Garage_Management.DAO.Entities;
using Garage_Management.Resources.View.Statistical;

namespace Garage_Management
{
   
    public partial class FormTrangChu : Form
    {
        public string UserNameFromLogin { get; set; }
        public FormTrangChu()
        {
            InitializeComponent();
        }      
        CarModel context = new CarModel();
        private void guna2Button7_Click(object sender, EventArgs e)
        {
            
            if(MessageDialog.Show("Bạn có muốn đăng xuất không", "Thông báo", MessageDialogButtons.YesNo) == DialogResult.OK)
            {
                this.Hide();
            } 
        }  
       // public string info { get; set; }

        private void FormTrangChu_Load(object sender, EventArgs e)
        {
            lbFm.Text = "Bảng điều khiển";
           // lbAcount.Text = info;
            dgvDashBoard.Rows.Add(4);

            dgvDashBoard.Rows[0].Cells[0].Value = Image.FromFile(@"D:\Project\Garage Management\mycar\1.png");
            dgvDashBoard.Rows[1].Cells[0].Value = Image.FromFile(@"D:\Project\Garage Management\mycar\2.png");
            dgvDashBoard.Rows[2].Cells[0].Value = Image.FromFile(@"D:\Project\Garage Management\mycar\3.png");
            dgvDashBoard.Rows[3].Cells[0].Value = Image.FromFile(@"D:\Project\Garage Management\mycar\4.png");
            dgvDashBoard.Rows[4].Cells[0].Value = Image.FromFile(@"D:\Project\Garage Management\mycar\5.png");

            dgvDashBoard.Rows[0].Cells[1].Value = "Mec C200";
            dgvDashBoard.Rows[1].Cells[1].Value = "911 Turbo";
            dgvDashBoard.Rows[2].Cells[1].Value = "Camry Hyrid";
            dgvDashBoard.Rows[3].Cells[1].Value = "Honda Civic";
            dgvDashBoard.Rows[4].Cells[1].Value = "Mazda CX5";

            dgvDashBoard.Rows[0].Cells[3].Value = "Mercedes";
            dgvDashBoard.Rows[1].Cells[3].Value = "Porsche";
            dgvDashBoard.Rows[2].Cells[3].Value = "Toyota";
            dgvDashBoard.Rows[3].Cells[3].Value = "Honda";
            dgvDashBoard.Rows[4].Cells[3].Value = "Mazda";


            dgvDashBoard.Rows[0].Cells[4].Value = "4000       1.5       250";
            dgvDashBoard.Rows[1].Cells[4].Value = "6750       3.0       330";
            dgvDashBoard.Rows[2].Cells[4].Value = "5200       2.5       176hp";
            dgvDashBoard.Rows[3].Cells[4].Value = "4500       1.5       176hp";
            dgvDashBoard.Rows[4].Cells[4].Value = "4000       2.0       154hp";


            dgvDashBoard.Rows[0].Cells[2].Value = "D x R x C \t4.590 x 1.845 x 1.680\nChiều dài cơ sở\t 2865 (mm)\nDung tích công tác 1496 (cc)\nMô-men xoắn cực đại\t 300 Nm tại 1800 – 4000 rpm\nVận tốc tối đa 246 (km/h)"; 
            dgvDashBoard.Rows[1].Cells[2].Value = "D x R x C \t4.535 x 1.900x 1.303\nChiều dài cơ sở\t 2450 (mm)\nDung tích công tác 2981 (cc)\nMô-men xoắn cực đại\t 800 Nm tại 6750 rpm\nTốc độ tối đa 330 (km/h)";
            dgvDashBoard.Rows[2].Cells[2].Value = "D x R x C \t4.886 x 1.840 x 1.445\nChiều dài cơ sở\t 2825(mm)\nDung tích công tác 2495 (cc)\nMô - men xoắn cực đại\t 250nm tại 3000 - 5200 rpm\nCông suất 176hp / 5700 rpm";
            dgvDashBoard.Rows[3].Cells[2].Value = "D x R x C \t4.674 x 1.800 x 1.415\nChiều dài cơ sở\t 2735(mm)\nDung tích công tác 1494 (cc)\nMô - men xoắn cực đại\t 140 Nm tại 1700 - 4500 rpm\nCông suất 176hp / 6.000 rpm";
            dgvDashBoard.Rows[4].Cells[2].Value = "D x R x C \t4.590 x 1.845 x 1.680\nChiều dài cơ sở\t 2700 (mm)\nDung tích công tác 1998 (cc)\nMô-men xoắn cực đại\t 200 Nm tại 4000 rpm\nCông suất 154 hp / 6000 rpm";

          //  lblTrangChu.Text = UserNameFromLogin;
        }
        public float GetStar()
        {
            float star = 4;
            if (cpt == 2)
                return star += 1;
            if (cpt == 3)
                return star += 0.5f;
            if (cpt == 4)
                return star -= 0.5f;
            if (cpt == 5)
                return star -= 1;
            return star;
        }
        int cpt = 1;
        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {

            if (cpt <  dgvDashBoard.Rows.Count)//max count = 5
            {
                cpt++;
                labelNameCar.Text = dgvDashBoard.Rows[cpt - 1].Cells[1].Value.ToString();
                lbSup.Text = dgvDashBoard.Rows[cpt - 1].Cells[3].Value.ToString();
                lbApeal.Text = dgvDashBoard.Rows[cpt - 1].Cells[4].Value.ToString();
                ratingStar.Value = GetStar();
                lbThongSo.Text = dgvDashBoard.Rows[cpt - 1].Cells[2].Value.ToString();
                guna2PictureBox_Car.Image = (Image)dgvDashBoard.Rows[cpt - 1].Cells[0].Value;
                guna2PictureBox_car1.Load(@"D:\Project\Garage Management\mycar\" + cpt.ToString() + cpt.ToString() + ".png");
                guna2PictureBox_car2.Load(@"D:\Project\Garage Management\mycar\" + cpt.ToString() + cpt.ToString() + cpt.ToString() + ".png");
                guna2PictureBox_car3.Image = guna2PictureBox_Car.Image;
                
             }
            else
                cpt = 0;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {

            if (cpt > 1)
            {
                cpt--;
                //kiem tra optimize code dong nay overload

                labelNameCar.Text = dgvDashBoard.Rows[cpt - 1].Cells[1].Value.ToString();
                lbSup.Text = dgvDashBoard.Rows[cpt - 1].Cells[3].Value.ToString();
                lbThongSo.Text = dgvDashBoard.Rows[cpt - 1].Cells[2].Value.ToString();
                lbApeal.Text = dgvDashBoard.Rows[cpt - 1].Cells[4].Value.ToString();
                ratingStar.Value = GetStar();
                guna2PictureBox_Car.Image = (Image)dgvDashBoard.Rows[cpt - 1].Cells[0].Value;
                guna2PictureBox_car1.Load(@"D:\Project\Garage Management\mycar\" + (cpt).ToString() + (cpt).ToString() + ".png");
                guna2PictureBox_car2.Load(@"D:\Project\Garage Management\mycar\" + (cpt).ToString() + (cpt).ToString() + (cpt).ToString() + ".png");
                guna2PictureBox_car3.Image = guna2PictureBox_Car.Image;

            }
            else
            {
                cpt = 6;
            }
           
        }

        private void guna2PictureBox_car1_Click(object sender, EventArgs e)
        {
            guna2PictureBox_Car.Image = guna2PictureBox_car1.Image;
        }

        private void guna2PictureBox_car2_Click(object sender, EventArgs e)
        {
            guna2PictureBox_Car.Image = guna2PictureBox_car2.Image;
        }

        private void guna2PictureBox_car3_Click(object sender, EventArgs e)
        {
            guna2PictureBox_Car.Image = guna2PictureBox_car3.Image;
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
            guna2Panel3.Controls.Add(childForm);
            guna2Panel3.Tag = childForm;
            childForm.BringToFront();
            childForm.Opacity = 50;
            childForm.Show();
            childForm.Opacity = 100;
          
        }


        private void btnOto_Click(object sender, EventArgs e)
        {
            OpenChildForm(new QuanLyOto());
            lbFm.Text = btnOto.Text;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (currentForm != null)
            {
                lbFm.Text = "Bảng điều khiển";
                currentForm.Close();
            }
            
        }

        private void btnNhanSu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormNhanSu());
            lbFm.Text = btnNhanSu.Text;
        }

        private void btnMucKhac_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormDoiMatKhau());
            lbFm.Text = btnMucKhac.Text;
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormThongKe());
            lbFm.Text = btnThongKe.Text;
        }
    }
}

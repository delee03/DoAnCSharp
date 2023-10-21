using Garage_Management.Resources.View;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Garage_Management
{
    public partial class FormNhanSu1 : Form
    {
        public FormNhanSu1()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Guna2ContainerControl container = new Guna2ContainerControl();
            container.Size = new Size(479, 171);
            container.Location = new Point(12, 496);
            container.BackColor = System.Drawing.Color.Transparent;
            container.BorderRadius = 20;

            Guna2PictureBox pictureBox = new Guna2PictureBox();
            pictureBox.Size = new Size(177, 162);
            pictureBox.Location = new Point(5, 3);
            pictureBox.Image = Garage_Management.Properties.Resources.user__1_;
            pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox.BackColor = System.Drawing.Color.Transparent;

            Guna2TextBox textBox = new Guna2TextBox();
            textBox.Location = new Point(188, 13);
            textBox.Size = new Size(266, 39);

            Guna2TextBox txtSDT = new Guna2TextBox();
            txtSDT.Location = new Point(238, 60);
            txtSDT.Size = new Size(175, 39);

            Guna2TextBox txtDC = new Guna2TextBox();
            txtDC.Location = new Point(238, 107);
            txtDC.Size = new Size(175, 39);

            Guna2HtmlLabel sdt = new Guna2HtmlLabel();
            sdt.Font =  new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            sdt.Location = new Point(188, 70);
            sdt.Size = new Size();
            sdt.Text = "SĐT";

            Guna2HtmlLabel dc = new Guna2HtmlLabel();
            dc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dc.Location = new Point(188, 119);
            dc.Size = new Size(32, 27);
            dc.Text = "ĐC";

            container.Controls.Add(textBox);
            container.Controls.Add(txtSDT);
            container.Controls.Add(txtDC);
            container.Controls.Add(sdt);
            container.Controls.Add(dc);
            container.Controls.Add(pictureBox);
            this.Controls.Add(container);
        }

        private void guna2ContainerControl1_Click(object sender, EventArgs e)
        {
            FormInfoPerson f = new FormInfoPerson();
            f.ShowDialog();
            
        }

        private void guna2ContainerControl2_Click(object sender, EventArgs e)
        {
            FormInfoPerson f = new FormInfoPerson();
            f.ShowDialog();
        }

        private void guna2ContainerControl3_Click(object sender, EventArgs e)
        {
            FormInfoPerson f = new FormInfoPerson();
            f.ShowDialog();
        }

        private void guna2ContainerControl4_Click(object sender, EventArgs e)
        {
            FormInfoPerson f = new FormInfoPerson();
            f.ShowDialog();
        }

        public void ListAllStaff()
        {
            gunaContainer.Controls.Clear();
        }
    }
}

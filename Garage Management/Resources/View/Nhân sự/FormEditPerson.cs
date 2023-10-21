using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Garage_Management.Resources.View.Nhân_sự
{
    public partial class FormEditPerson : Form
    {
        public FormEditPerson()
        {
            InitializeComponent();
        }

        public void SetTextBoxValues(Image image, string value2, string value3, string value4, string value5)
        {
            pbAvatar.Image = image;
            txtMS.Text = value2;
            txtHoVaTen.Text = value3;
            txtSĐT.Text = value4;
            txtDiaChi.Text = value5;
        }
    }
}

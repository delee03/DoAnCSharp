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
using Garage_Management.DAO;

namespace Garage_Management
{
    public partial class QuanLyOto : Form
    {
        public QuanLyOto()
        {
            InitializeComponent();
            LoadData(); 
        }
        CarModel context = new CarModel();
        private void QuanLyOto_Load(object sender, EventArgs e)
        {
            
        }

        private void LoadData()
        {
            try
            {
                //setGridViewStyle(dgvOto);
                List<Car> listCar = context.Cars.ToList();
                List<Suplier> listSup = context.Supliers.ToList();
                FillCmbSuplier(listSup);
                BindGrid(listCar);
                cboNcc.SelectedIndex = 0;
                dgvOto.Rows[0].Cells[2].Value = Image.FromFile(@"D:\Project\Garage Management\Garage Management\Resources\Image\1.png");
                dgvOto.Rows[1].Cells[2].Value = Image.FromFile(@"D:\Project\Garage Management\Garage Management\Resources\Image\2.png");
                dgvOto.Rows[2].Cells[2].Value = Image.FromFile(@"D:\Project\Garage Management\Garage Management\Resources\Image\3.png");
                dgvOto.Rows[3].Cells[2].Value = Image.FromFile(@"D:\Project\Garage Management\Garage Management\Resources\Image\4.png");
                dgvOto.Rows[4].Cells[2].Value = Image.FromFile(@"D:\Project\Garage Management\Garage Management\Resources\Image\5.png");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FillCmbSuplier(List<Suplier> list)
        {
            cboNcc.DataSource = list;
            cboNcc.DisplayMember = "nameSup";
            cboNcc.ValueMember = "idSup";
        }

        public void setGridViewStyle(DataGridView dataGridView)
        {
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void BindGrid(List<Car> listCar)
        {
            dgvOto.Rows.Clear();
          
            foreach (var item in listCar)
            {
                int index = dgvOto.Rows.Add();
                dgvOto.Rows[index].Cells[0].Value = item.idCar;
           //   dgvOto.Rows[index].Cells[1].Value = "";
                dgvOto.Rows[index].Cells[1].Value = item.nameCar;
                dgvOto.Rows[index].Cells[3].Value = item.Suplier.nameSup;
                dgvOto.Rows[index].Cells[4].Value = item.ngayNhap.ToString();
                dgvOto.Rows[index].Cells[5].Value = item.price + "";
                dgvOto.Rows[index].Cells[6].Value = item.DaDatHang.info;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}

using Garage_Management.BUS;
using Garage_Management.DAO.Entities;
using Garage_Management.Resources.View.Nhân_sự;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Garage_Management.Resources.View
{
    public partial class FormNhanSu : Form
    {
        private readonly DataQuery query = new DataQuery();
        private readonly DataContext context = new DataContext();
        FormEditPerson fEdit;

        public FormNhanSu()
        {
            InitializeComponent();

            fEdit = new FormEditPerson(this);
        }

        public void FormNhanSu_Load(object sender, EventArgs e)
        {
            //dgvStaff.Rows.Add(7);
            //dgvStaff.Rows[0].Cells[1].Value = Image.FromFile(@"C:\Users\bhvie\OneDrive\Pictures\Acer\Oreki_houtarou.png");
            //dgvStaff.Rows[0].Cells[2].Value = "việt";
            //dgvStaff.Rows[0].Cells[3].Value = "NV001";
            //dgvStaff.Rows[0].Cells[4].Value = "000";
            //dgvStaff.Rows[0].Cells[5].Value = "Bình thạnh";

            var listStaff = query.GetStaff();
            BindGridStaff(listStaff);
        }

        public void BindGridStaff(List<Staff> listStaff)
        {
            dgvStaff.Rows.Clear();
            foreach (var item in listStaff)
            {
                int index = dgvStaff.Rows.Add();
                dgvStaff.Rows[index].Cells[1].Value = item.Avartar_image;
                dgvStaff.Rows[index].Cells[2].Value = item.name;
                if (item.id != null)
                {
                    dgvStaff.Rows[index].Cells[3].Value = item.id;
                }
                dgvStaff.Rows[index].Cells[4].Value = item.phone;
                dgvStaff.Rows[index].Cells[5].Value = item.address;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                FormInfoPerson f = new FormInfoPerson();
                f.ShowDialog();
                FormNhanSu_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SapXep(int order = 1)
        {
            var listStaff = query.GetStaff();
            switch (order)
            {
                case 1:
                    listStaff.OrderBy(s => s.id);
                    break;
                case 2:
                    listStaff.OrderByDescending(s => s.id);
                    break;
            }
            BindGridStaff(listStaff);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            for (int i = 0; i < dgvStaff.Rows.Count; i++)
            {
                if (dgvStaff.Rows[i].Cells[2].Value.ToString().ToLower().Contains(keyword.ToLower()))
                {
                    dgvStaff.Rows[i].Visible = true;
                }
                else
                {
                    dgvStaff.Rows[i].Visible = false;
                }
            }
        }

        private void dgvStaff_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                dgvStaff_CellClick(sender, e);
                fEdit.ShowDialog();
            }
        }

        private void dgvStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStaff.Rows[e.RowIndex];
                //byte[] img = (byte[])row.Cells[1].Value;
                //Image value1 = context.ByteArrayToImage(img);
                string valueId = row.Cells[3].Value.ToString();
                fEdit.SetTextBoxValues(valueId);
            }
        }

        //public void btnLaylaimatkhau_click(object sender, EventArgs e)
        //{
        //    string email = txtemailquenmatkhau.Text;
        //    if(email.Trim() == "")
        //    {
        //        MessageBox.Show("vui long dang ky");
        //    }
        //    else
        //    {
        //        string query = "select * from Account where Email = '" + email + "'";
        //        if(modify.Account(query).Count != 0)
        //        {
        //            lblketqua.Text = "Mat khau: " + modify.Account(query)[0].PassWord;
        //        }
        //        else
        //        {
        //            lblketqua.Text = "Email chua dang ky";
        //        }
        //    }
        //}

        //public Modify()
        //{

        //}

        //SqlCommand sqlCommand;
        //SqlDataReader dataReader;

        //public List<Account> Account(String query)
        //{
        //    List<Account> accounts = new List<Account>();
        //    using(SqlConnection sqlConnection = Connection.GetSqlConnection())
        //    {
        //        sqlConnection.Open();
        //        this.sqlCommand = new SqlCommand(query, sqlConnection);
        //        this.dataReader = sqlCommand.ExecuteReader();
        //        while(dataReader.Read())
        //        {
        //            accounts.Add(new DAO.Account(dataReader.GetString(0)));
        //        }
        //        SqlConnection.Close();
        //    }
        //    return accounts;
        //}
    }
}

using Garage_Management.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Garage_Management.DAO;

namespace Garage_Management
{
    public partial class FormDashBoard : Form
    {
        public FormDashBoard()
        {
            InitializeComponent();
            //loadAccountList();
        }

        private void FormDashBoard_Load(object sender, EventArgs e)
        {

        }

        //private void loadAccountList()
        //{
        //    // viết query cách dấu "," 1 khoảng trắng nếu không sẽ lỗi
        //    string query = "EXEC USP_GetAccountByUserName @userName";

        //    dgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query,new object[] { "viet"});
        //}
    }
}

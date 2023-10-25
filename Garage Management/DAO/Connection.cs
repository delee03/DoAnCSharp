using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Management.DAO
{
    public class Connection
    {
        private string stringConnection = @"Data Source=DESKTOP-DTFBKP2\SQLEXPRESS;Initial Catalog=QuanLyGarage;Integrated Security=True";
        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(stringConnection);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Management.Entities
{
    internal class Connection
    {
        private static string stringConnection = @"Data Source=LAPTOP-2MHF57KV;Initial Catalog=QuanLyGarage;Integrated Security=True";
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(stringConnection);
        }
    }
}

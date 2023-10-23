using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Garage_Management.Entities
{
    internal class Modify
    {
        public Modify() 
        {

        }
        SqlCommand sqlCommand;
        SqlDataReader DataReader;
        public List<Account> Accounts(string query)
        {
            List<Account> accounts = new List<Account>();
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                this.sqlCommand = new  SqlCommand(query, sqlConnection);
                this.DataReader = sqlCommand.ExecuteReader();
                while (DataReader.Read()) 
                {
                    accounts.Add(new Account(DataReader.GetString(0)));
                }
                sqlConnection.Close();
            }
            return accounts;
        }

    }
}

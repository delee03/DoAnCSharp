using Garage_Management.DAO.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Management.DAO
{
    public class Modify
    {
        public Modify()
        {

        }
        //SqlCommand sqlCommand;
        //SqlDataReader DataReader;
        Connection connection = new Connection();

        public List<Account> Accounts(string query)
        {
            List<Account> accounts = new List<Account>();
            using (SqlConnection sqlConnection = connection.GetSqlConnection())
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader DataReader = sqlCommand.ExecuteReader();
                while (DataReader.Read())
                {
                    accounts.Add(new Account(DataReader.GetString(0), DataReader.GetString(2)));
                }
                sqlConnection.Close();
            }
            return accounts;
        }
    }
}

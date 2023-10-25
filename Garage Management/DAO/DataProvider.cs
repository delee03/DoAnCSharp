    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Management.DAO
{
    public class DataProvider
    {
        // Design Pattern Singleton
        private static DataProvider instance; // duy nhất 1 thể hiện của dataprovider tồn tại trong suốt vòng đời chương trình
        public static DataProvider Instance {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        } // được phép set dữ liệu bên trong class và bên ngoài lấy được dữ liệu từ instance

        private DataProvider() { } // không cho phép truy cập từ bên ngoài

        private string connectionSTR = @"Data Source=DELEE03\PHATPC;Initial Catalog=QuanLyGarage;Integrated Security=True";

        // hàm trả về dòng kết quả 
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            /*
             Khối using giúp đảm bảo rằng đối tượng SqlConnection sẽ được giải phóng một cách đúng đắn
             khi không cần sử dụng nữa, giúp giảm nguy cơ gây rò rỉ bộ nhớ
             */

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                // Add cùng lúc nhiều parameters 1 lúc
                if(parameter != null)
                {
                    string[] listParameters = query.Split(' '); // split dùng để phân tách chuỗi
                    int i = 0;
                    foreach (string param in listParameters)
                    {
                        if (param.Contains('@'))
                        {
                            command.Parameters.AddWithValue(param, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }

            return data;
        }
            
        // hàm để trả về số dòng được thực thi (insert, update hoặc delete)
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listParameters = query.Split(' ');
                    int i = 0;
                    foreach (string param in listParameters)
                    {
                        if (param.Contains('@'))
                        {
                            command.Parameters.AddWithValue(param, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }

        // hàm thực hiện đếm số lượng trả ra (select *, select count * ... ) , trả về cột đầu tiên của hàng đầu tiên
        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listParameters = query.Split(' ');
                    int i = 0;
                    foreach (string param in listParameters)
                    {
                        if (param.Contains('@'))
                        {
                            command.Parameters.AddWithValue(param, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        }
    }
}

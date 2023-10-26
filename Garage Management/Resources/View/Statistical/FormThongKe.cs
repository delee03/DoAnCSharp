using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using SeriesCollection = LiveCharts.SeriesCollection;

namespace Garage_Management.Resources.View.Statistical
{
    public partial class FormThongKe : Form
    {
        public FormThongKe()
        {
            InitializeComponent();
        }

        private void FormThongKe_Load(object sender, EventArgs e)
        {
            chartThongKe.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Tháng",
                Labels = new[] {"1", "2", "3", "4", "5", "6",
                    "7", "8", "9", "10", "11", "12", }
            });
            chartThongKe.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Doanh Thu",
                LabelFormatter = value => value.ToString(),
            });
            chartThongKe.LegendLocation = LiveCharts.LegendLocation.Right;

            LoadDuLieuVaoDataGridView();

            CapNhatLiveChart();
        }

        private List<(int Nam, int Thang, double GiaTri)> LayDuLieuTuDataGridView()
        {
            List<(int Nam, int Thang, double GiaTri)> data = new List<(int Nam, int Thang, double GiaTri)>();

            foreach (DataGridViewRow row in dgvThongKe.Rows)
            {
                int nam = Convert.ToInt32(row.Cells[0].Value);
                int thang = Convert.ToInt32(row.Cells[1].Value);
                double giaTri = Convert.ToDouble(row.Cells[2].Value);
                data.Add((nam, thang, giaTri));
            }

            return data;
        }

        private void CapNhatLiveChart()
        {
            DataTable dataTable = LayDuLieuTuSQL();

            // Chuyển đổi dữ liệu từ DataTable sang dạng List<(int Nam, int Thang, double GiaTri)>
            List<(int Nam, int Thang, double GiaTri)> rawData = new List<(int Nam, int Thang, double GiaTri)>();
            foreach (DataRow row in dataTable.Rows)
            {
                int nam = int.Parse(row["Năm"].ToString());
                int thang = int.Parse(row["Tháng"].ToString());
                double giaTri = double.Parse(row["Doanh_Thu"].ToString());

                rawData.Add((Nam: nam, Thang: thang, GiaTri: giaTri));
            }

            // Xử lý dữ liệu
            var dataProcessed = XuLyDuLieuChoLiveChart(rawData);

            chartThongKe.Series.Clear();
            SeriesCollection series = new SeriesCollection();

            foreach (var data in dataProcessed)
            {
                series.Add(new LineSeries() { Title = data.Nam.ToString(), Values = new ChartValues<double>(data.GiaTriThang) });
            }

            chartThongKe.Series = series;
        }


        private List<(int Nam, double[] GiaTriThang)> XuLyDuLieuChoLiveChart(List<(int Nam, int Thang, double GiaTri)> data)
        {
            return data.GroupBy(d => d.Nam)
                       .Select(group =>
                       {
                           double[] giaTriThang = new double[12];
                           foreach (var item in group)
                           {
                               if (item.Thang >= 1 && item.Thang <= 12)
                               {
                                   giaTriThang[item.Thang - 1] = item.GiaTri;
                               }
                           }
                           return (Nam: group.Key, GiaTriThang: giaTriThang);
                       })
                       .ToList();
        }

        private void HienThiDuLieuLenLiveChart(List<(int Nam, double[] GiaTriThang)> yearlyData)
        {
            chartThongKe.Series.Clear();
            SeriesCollection series = new SeriesCollection();

            foreach (var data in yearlyData)
            {
                series.Add(new LineSeries
                {
                    Title = data.Nam.ToString(),
                    Values = new ChartValues<double>(data.GiaTriThang)
                });
            }

            chartThongKe.Series = series;
        }

        //private void CapNhatLiveChart()
        //{
        //    var duLieu = LayDuLieuTuDataGridView();
        //    var yearlyData = XuLyDuLieuChoLiveChart(duLieu);
        //    HienThiDuLieuLenLiveChart(yearlyData);
        //}

        private DataTable LayDuLieuTuSQL()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(@"Data Source=DELEE03\PHATPC;Initial Catalog=QuanLyGarage;Integrated Security=True"))
            {
                conn.Open();
                string query = @"
            SELECT 
                DATEPART(YEAR, h.ngayLap) AS Năm, 
                DATEPART(MONTH, h.ngayLap) AS Tháng, 
                SUM(c.price) AS Doanh_Thu
            FROM HoaDon h
            INNER JOIN Car c ON h.idCar = c.idCar  -- Kết hợp dữ liệu từ bảng HoaDon và Car
            GROUP BY DATEPART(YEAR, h.ngayLap), DATEPART(MONTH, h.ngayLap)
            ORDER BY Năm, Tháng";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                dataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        private void LoadDuLieuVaoDataGridView()
        {
            var duLieu = LayDuLieuTuSQL();
            dgvThongKe.DataSource = duLieu;
        }

    }
}
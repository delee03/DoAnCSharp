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
using System.Windows.Forms.DataVisualization.Charting;
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
                Labels = new[] {"Jan", "Feb", "Mar", "Apr", "May", "Jun", 
                    "Jul", "Aug", "Seb", "Oct", "Nov", "Dec", }
            });
            chartThongKe.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Doanh Thu",
                LabelFormatter = value => value.ToString(),
            });
            chartThongKe.LegendLocation = LiveCharts.LegendLocation.Right;

            LoadDuLieuVaoDataGridView();

            CapNhatLiveChart();

            //chartThongKe.Series.Clear();
            //SeriesCollection series = new SeriesCollection();
            //var years = (from o in revenueBindingSource.DataSource as List<Revenue>
            //             select new { Year = o.Year }).Distinct();
            //foreach (var year in years)
            //{
            //    List<double> values = new List<double>();
            //    for (int month = 1; month <= 12; month++)
            //    {
            //        double value = 0;
            //        var data = from o in revenueBindingSource.DataSource as List<Revenue>
            //                   where o.Year.Equals(year.Year) && o.Month.Equals(month)
            //                   orderby o.Month ascending
            //                   select new { o.Value, o.Month };
            //        if (data.SingleOrDefault() != null)
            //        {
            //            value = data.SingleOrDefault().Value;
            //        }
            //        values.Add(value);
            //    }
            //    series.Add(new LineSeries() { Title = year.Year.ToString(), Values = new ChartValues<double>(values) });
            //}
            //chartThongKe.Series = series;
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

        private void CapNhatLiveChart()
        {
            var duLieu = LayDuLieuTuDataGridView();
            var yearlyData = XuLyDuLieuChoLiveChart(duLieu);
            HienThiDuLieuLenLiveChart(yearlyData);
        }

        private DataTable LayDuLieuTuSQL()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-DTFBKP2\SQLEXPRESS;Initial Catalog=QuanLyGarage;Integrated Security=True"))
            {
                conn.Open();
                string query = "SELECT DATEPART(YEAR, ngayLap) AS Năm, DATEPART(MONTH, ngayLap) AS Tháng, DATEPART(DAY, ngayLap) AS GiáTrị FROM HoaDon";
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

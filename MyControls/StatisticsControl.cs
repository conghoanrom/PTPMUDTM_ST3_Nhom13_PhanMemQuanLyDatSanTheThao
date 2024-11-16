using System.Drawing;
using LINQ;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MyControls
{
    public partial class StatisticsControl : UserControl
    {
        private SportsFieldManagementContextDataContext db = new SportsFieldManagementContextDataContext();
        private static Random random = new Random();
        public StatisticsControl()
        {
            InitializeComponent();
        }

        private void StatisticsControl_Load(object sender, EventArgs e)
        {
            loadDataToChart(7);
            loadDataToChartDoughnut();
            loadDataToChartPie();
            loadTataToChartFastLine();
        }
        private void loadDataToChart(int days)
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            chart1.Titles.Clear(); // Xóa tiêu đề cũ (nếu có)
            chart1.Titles.Add("Biểu đồ doanh thu theo " + days + " ngày");

            // Thiết lập thuộc tính cho tiêu đề
            chart1.Titles[0].Font = new System.Drawing.Font("Arial", 16, FontStyle.Bold);
            chart1.Titles[0].ForeColor = Color.DarkBlue;
            chart1.Titles[0].Alignment = ContentAlignment.TopCenter;

            // Tạo và thêm một ChartArea mới
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            chartArea.Name = "MainArea";
            chart1.ChartAreas.Add(chartArea);

            // Lọc theo n ngày gần nhất
            var revenues = db.RevenueOfnDays.Where(r => r.BookingDate >= DateTime.Now.AddDays(-days)).OrderBy(r => r.BookingDate).ToList();

            System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
            series.Name = "Series1";

            for (int i = 0; i < revenues.Count; i++)
            {
                DateTime? nullableDate = revenues[i].BookingDate;
                DateTime date = nullableDate.Value; // Chỉ sử dụng nếu chắc chắn nullableDate không rỗng

                var point = series.Points.AddXY(date.ToString("dd/MM"), revenues[i].DoanhThu);

                // Thiết lập màu ngẫu nhiên cho từng cột
                series.Points[i].Color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            }

            series.ChartType = SeriesChartType.FastLine;

            chart1.Series.Add(series);
        }

        private void loadDataToChartDoughnut()
        {
            var purchaseQuantities = db.DrinkPurchaseQuantities.ToList();

            chart2.Series.Clear();
            chart2.ChartAreas.Clear();

            chart2.Titles.Clear(); // Xóa tiêu đề cũ (nếu có)
            chart2.Titles.Add("Các loại nước và số lần đặt");

            // Thiết lập thuộc tính cho tiêu đề
            chart2.Titles[0].Font = new System.Drawing.Font("Arial", 16, FontStyle.Bold);
            chart2.Titles[0].ForeColor = Color.DarkBlue;
            chart2.Titles[0].Alignment = ContentAlignment.TopCenter;

            // Tạo và thêm một ChartArea mới
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            chartArea.Name = "MainArea2";
            chart2.ChartAreas.Add(chartArea);

            System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
            series.Name = "Series2";

            for (int i = 0; i < purchaseQuantities.Count; i++)
            {

                var point = series.Points.AddXY(purchaseQuantities[i].DrinkName + "(" + purchaseQuantities[i].PurchaseQuantity + ")", purchaseQuantities[i].PurchaseQuantity);

                // Thiết lập màu ngẫu nhiên cho từng cột
                series.Points[i].Color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            }

            series.ChartType = SeriesChartType.Doughnut;

            chart2.Series.Add(series);

        }
        private void loadDataToChartPie()
        {
            var bookingOfFieldTypes = db.BookingOfFieldTypes.ToList();

            chart3.Series.Clear();
            chart3.ChartAreas.Clear();

            chart3.Titles.Clear(); // Xóa tiêu đề cũ (nếu có)
            chart3.Titles.Add("Các loại sân và số lần đặt");

            // Thiết lập thuộc tính cho tiêu đề
            chart3.Titles[0].Font = new System.Drawing.Font("Arial", 16, FontStyle.Bold);
            chart3.Titles[0].ForeColor = Color.DarkBlue;
            chart3.Titles[0].Alignment = ContentAlignment.TopCenter;

            // Tạo và thêm một ChartArea mới
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            chartArea.Name = "MainArea3";
            chart3.ChartAreas.Add(chartArea);

            System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
            series.Name = "Series 3";

            for (int i = 0; i < bookingOfFieldTypes.Count; i++)
            {

                var point = series.Points.AddXY(bookingOfFieldTypes[i].TypeName + "(" + bookingOfFieldTypes[i].NumberOfBOOKINGS + ")", bookingOfFieldTypes[i].NumberOfBOOKINGS);

                // Thiết lập màu ngẫu nhiên cho từng cột
                series.Points[i].Color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            }

            series.ChartType = SeriesChartType.Pie;

            chart3.Series.Add(series);
        }
        private void loadTataToChartFastLine()
        {
            var totalPriceOfDrinks = db.TotalPriceOfDrinks.ToList();

            chart4.Series.Clear();
            chart4.ChartAreas.Clear();

            chart4.Titles.Clear(); // Xóa tiêu đề cũ (nếu có)
            chart4.Titles.Add("Doanh thu của các loại nước");

            // Thiết lập thuộc tính cho tiêu đề
            chart4.Titles[0].Font = new System.Drawing.Font("Arial", 16, FontStyle.Bold);
            chart4.Titles[0].ForeColor = Color.DarkBlue;
            chart4.Titles[0].Alignment = ContentAlignment.TopCenter;

            // Tạo và thêm một ChartArea mới
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            chartArea.Name = "MainArea3";
            chart4.ChartAreas.Add(chartArea);

            System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
            series.Name = "Series 4";

            for (int i = 0; i < totalPriceOfDrinks.Count; i++)
            {

                var point = series.Points.AddXY(totalPriceOfDrinks[i].DrinkName, totalPriceOfDrinks[i].TotalPrice);

                // Thiết lập màu ngẫu nhiên cho từng cột
                series.Points[i].Color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            }

            series.ChartType = SeriesChartType.Column;

            chart4.Series.Add(series);
        }
        private void kryptonNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int days = (int)kryptonNumericUpDown1.Value;
            loadDataToChart(days);
        }
    }
}

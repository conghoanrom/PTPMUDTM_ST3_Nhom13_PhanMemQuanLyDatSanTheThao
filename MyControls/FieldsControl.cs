using DTO;
using LINQ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace MyControls
{
    public partial class FieldsControl : UserControl
    {
        public List<Field> fields { get; set; }

        private SportsFieldManagementContextDataContext db = new SportsFieldManagementContextDataContext();
        private int selectField = 1; // Hiển thị thông tin sân hiện tại đang chọn
        public delegate void openBookingForm(int fieldId, string fieldName);
        public openBookingForm openBookingFormEvent;
        public delegate void openFixedBookingForm(int fieldId);
        public openFixedBookingForm openFixedBookingFormEvent;
        public FieldsControl(List<Field> fields)
        {
            InitializeComponent();
            this.fields = fields;
        }

        private void FieldsControl_Load(object sender, EventArgs e)
        {
            int y = 0;
            foreach (var item in fields)
            {
                FieldControl field = new FieldControl(item);
                field.getFieldIdEvent = new FieldControl.getFieldId(getFieldIdEvent);
                field.Field = item;
                field.Location = new System.Drawing.Point(0, y);
                kryptonPanel1.Controls.Add(field);
                y += field.Height + 5;
            }

            kryptonLabel3.Text = "Bạn đang chọn sân: " + fields[0].FieldName;
            selectField = fields[0].FieldId;
            // Enable button
            kryptonButton2.Enabled = fields[0].Location == 0 ? true : false;
            kryptonButton1.Enabled = fields[0].Location != 0 ? true : false;
            this.loadCasualBooking(selectField, DateTime.Now);
            // Load thông tin đặt sân bên màn hình bên phải của sân vừa chọn
        }

        // Sự kiện mỗi khi chọn 1 sân để xem chi tiết
        public void getFieldIdEvent(int fieldId)
        {
            var field = fields.Where(f => f.FieldId == fieldId).FirstOrDefault();
            kryptonLabel3.Text = "Bạn đang chọn sân: " + field.FieldName;
            // Enable Button
            kryptonButton2.Enabled = field.Location == 0 ? true : false;
            kryptonButton1.Enabled = field.Location != 0 ? true : false;
            selectField = fieldId;
            this.loadCasualBooking(selectField, kryptonDateTimePicker1.Value);
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            var field = fields.Where(f => f.FieldId == selectField).FirstOrDefault();
            openBookingFormEvent(selectField, field.FieldName);
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            this.loadCasualBooking(selectField, DateTime.Now);
        }

        private void loadCasualBooking(int fieldId, DateTime date)
        {
            var casualBookings = db.CasualBookings.Where(cb => cb.FieldId == fieldId &&
            cb.StartDate.Value.Date == date.Date).OrderBy(cb => cb.StartTime)
                .ToList();
            kryptonDataGridView1.DataSource = casualBookings;
            kryptonDataGridView1.Columns["CustomerName"].Visible = false;
            kryptonDataGridView1.Columns["Phone"].Visible = false;
            kryptonDataGridView1.Columns["FieldId"].Visible = false;
            kryptonDataGridView1.Columns["EmployeeId"].Visible = false;
            kryptonDataGridView1.Columns["ServiceId"].Visible = false;
            kryptonDataGridView1.Columns["DiscountService"].Visible = false;
            kryptonDataGridView1.Columns["Employee"].Visible = false;
            kryptonDataGridView1.Columns["Field"].Visible = false;
        }

        private void kryptonDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.loadCasualBooking(selectField, kryptonDateTimePicker1.Value.Date);
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            openFixedBookingFormEvent(selectField);
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            // Xuất excel
            var casualBookings = db.CasualBookings.ToList();
            List<BookingExport> bookingExports = convertToBookingExport(casualBookings);
            ExportToExcel(bookingExports);
        }

        private List<BookingExport> convertToBookingExport(List<CasualBooking> casualBookings)
        {
            List<BookingExport> bookingExports = new List<BookingExport>();
            foreach(var item in casualBookings)
            {
                bookingExports.Add(new BookingExport
                {
                    BookingId = item.BookingId,
                    BookingDate = item.BookingDate,
                    StartDate = item.StartDate,
                    Minutes = item.Minutes,
                    FieldId = item.FieldId,
                    CustomerName = item.CustomerName,
                    Phone = item.Phone,
                    EmployeeId = item.EmployeeId,
                    TotalPrice = item.TotalPrice
                });
            }
            return bookingExports;
        }
        public void ExportToExcel(List<BookingExport> bookings)
        {
            // Khởi tạo Excel Application
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

            // Tạo một workbook và worksheet mới
            Workbook workbook = excelApp.Workbooks.Add();
            Worksheet worksheet = (Worksheet)workbook.Sheets[1];

            excelApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual;

            // Đặt tiêu đề "Danh sách booking" tại ô A1
            worksheet.Cells[1, 1] = "Danh sách đặt sân";

            // Chọn phạm vi cho dòng đầu tiên (dòng tiêu đề)
            Microsoft.Office.Interop.Excel.Range titleRange = worksheet.get_Range("A1", "I1");

            // Căn giữa và hợp nhất các ô để tiêu đề nằm giữa
            titleRange.Merge();
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Đặt font chữ, cỡ chữ và màu sắc
            titleRange.Font.Name = "Times New Roman";
            titleRange.Font.Size = 20;
            titleRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            titleRange.Font.Bold = true; // Đặt in đậm cho tiêu đề

            // Đặt tiêu đề cho các cột trong Excel
            worksheet.Cells[2, 1] = "BookingId";
            worksheet.Cells[2, 2] = "BookingDate";
            worksheet.Cells[2, 3] = "StartDate";
            worksheet.Cells[2, 4] = "Minutes";
            worksheet.Cells[2, 5] = "FieldId";
            worksheet.Cells[2, 6] = "CustomerName";
            worksheet.Cells[2, 7] = "Phone";
            worksheet.Cells[2, 8] = "EmployeeId";
            worksheet.Cells[2, 9] = "TotalPrice";

            // Set column widths (you can adjust the values as needed)
            worksheet.Columns[1].ColumnWidth = 15;  // BookingId
            worksheet.Columns[2].ColumnWidth = 20;  // BookingDate
            worksheet.Columns[3].ColumnWidth = 20;  // StartDate
            worksheet.Columns[4].ColumnWidth = 10;  // Minutes
            worksheet.Columns[5].ColumnWidth = 10;  // FieldId
            worksheet.Columns[6].ColumnWidth = 25;  // CustomerName
            worksheet.Columns[7].ColumnWidth = 15;  // Phone
            worksheet.Columns[8].ColumnWidth = 15;  // EmployeeId
            worksheet.Columns[9].ColumnWidth = 12;  // TotalPrice

            // Đặt font cho dòng tiêu đề (dòng 2)
            var headerRange = worksheet.get_Range("A2", "I2");
            headerRange.Font.Name = "Calibri";  // Đặt kiểu chữ
            headerRange.Font.Size = 12;      // Đặt kích thước chữ
            headerRange.Font.Bold = true;    // Đặt chữ đậm
            headerRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);  // Đặt màu chữ

            // Duyệt qua danh sách và thêm dữ liệu vào Excel
            int rowIndex = 3; // Dòng bắt đầu từ 2 (dòng 1 là tiêu đề)
            foreach (var booking in bookings)
            {
                worksheet.Cells[rowIndex, 1] = booking.BookingId;
                worksheet.Cells[rowIndex, 2] = booking.BookingDate?.ToString("yyyy-MM-dd");
                worksheet.Cells[rowIndex, 3] = booking.StartDate?.ToString("yyyy-MM-dd");
                worksheet.Cells[rowIndex, 4] = booking.Minutes?.ToString();
                worksheet.Cells[rowIndex, 5] = booking.FieldId?.ToString();
                worksheet.Cells[rowIndex, 6] = booking.CustomerName;
                worksheet.Cells[rowIndex, 7] = "'" + booking.Phone;
                worksheet.Cells[rowIndex, 8] = booking.EmployeeId;
                worksheet.Cells[rowIndex, 9] = booking.TotalPrice?.ToString();

                // Lấy phạm vi ô của dòng hiện tại
                Microsoft.Office.Interop.Excel.Range rowRange = worksheet.Rows[rowIndex] as Microsoft.Office.Interop.Excel.Range;

                // Căn lề văn bản sang trái cho toàn bộ dòng
                rowRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                // Đặt font là Calibri cho toàn bộ dòng
                rowRange.Font.Name = "Calibri";

                rowIndex++;
            }

            // Đặt font cho các dòng dữ liệu (dòng 2 trở đi)
            var dataRange = worksheet.get_Range("A2", $"I{rowIndex - 1}");
            dataRange.Font.Name = "Calibri";  // Đặt kiểu chữ
            dataRange.Font.Size = 10;        // Đặt kích thước chữ

            // Tùy chọn: Đặt căn chỉnh cho các ô dữ liệu
            dataRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Thiết lập tất cả các đường viền cho vùng dữ liệu
            dataRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            dataRange.Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;

            excelApp.Visible = true; // Hiển thị ứng dụng Excel

            // Chỉ hiển thị ứng dụng Excel mà không lưu
            // Không cần gọi workbook.Save hoặc workbook.Close
        }
    }
}

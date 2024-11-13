using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using API;
using ComponentFactory.Krypton.Toolkit;
using LINQ;
using Newtonsoft.Json;
using RestSharp;

namespace GUI
{
    public partial class FixedBookingForm : KryptonForm
    {
        private string employeeId;
        private int fieldId;
        private List<DateTime> datesSelect = new List<DateTime>();
        private SportsFieldManagementContextDataContext db = new SportsFieldManagementContextDataContext();
        private double totalPrice = 0;
        private static Random random = new Random();
        public FixedBookingForm(int fieldId, string employeeId)
        {
            InitializeComponent();
            this.employeeId = employeeId;
            this.fieldId = fieldId;
        }

        private void FixedBookingForm_Load(object sender, EventArgs e)
        {
            loadPanel();
            loadComboBox();
        }

        private void loadPanel()
        {
            List<DateTime> dates = new List<DateTime>();
            // Thêm 7 ngày vào list
            for (int i = 0; i < 7; i++)
            {
                dates.Add(DateTime.Now.AddDays(i));
            }
            //In vào panel
            for (int i = 0; i < 7; i++)
            {
                // Xác định thứ
                string day = this.dayOfWeek(dates[i]);
                string dateFormat = dates[i].ToString("dd/MM/yyyy");
                KryptonCheckBox kryptonCheckBox = new KryptonCheckBox();
                kryptonCheckBox.Text = day + " - " + dateFormat;
                kryptonCheckBox.Location = new Point(10, 10 + i * 30);
                kryptonCheckBox.Size = new Size(200, 20);
                // Sự kiện check
                DateTime selectedDate = dates[i];
                kryptonCheckBox.CheckedChanged += (s, ev) =>
                {
                    if (kryptonCheckBox.Checked)
                    {
                        datesSelect.Add(selectedDate);
                    }
                    else
                    {
                        datesSelect.Remove(selectedDate);
                    }
                };
                kryptonPanel1.Controls.Add(kryptonCheckBox);
            }
        }

        // Xác định thứ trong ngày
        private string dayOfWeek(DateTime date)
        {
            string day = "";
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    day = "Thứ 2";
                    break;
                case DayOfWeek.Tuesday:
                    day = "Thứ 3";
                    break;
                case DayOfWeek.Wednesday:
                    day = "Thứ 4";
                    break;
                case DayOfWeek.Thursday:
                    day = "Thứ 5";
                    break;
                case DayOfWeek.Friday:
                    day = "Thứ 6";
                    break;
                case DayOfWeek.Saturday:
                    day = "Thứ 7";
                    break;
                case DayOfWeek.Sunday:
                    day = "Chủ nhật";
                    break;
            }
            return day;
        }
        private void loadComboBox()
        {
            var services = db.DiscountServices
                .ToList();
            List<DiscountService> list = new List<DiscountService>();
            foreach (var item in services)
            {
                DateTime startDate = item.StartDate.Value;
                DateTime endDate = item.StartDate.Value.AddDays(Convert.ToDouble(item.Days));
                if (DateTime.Now.Date >= startDate.Date && DateTime.Now.Date <= endDate.Date)
                {
                    list.Add(item);
                }
            }
            kryptonComboBox1.DataSource = list;
            kryptonComboBox1.DisplayMember = "ServiceName";
            kryptonComboBox1.ValueMember = "ServiceId";
        }

        // Tính tiền
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            List<string> errorsList = validationForm();
            if (errorsList.Count > 0)
            {
                KryptonMessageBox.Show(errorsList[0], "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                DateTime bookingDate = DateTime.Now;
                TimeSpan startTimee = kryptonDateTimePicker1.Value.TimeOfDay;
                int minutes = int.Parse(kryptonNumericUpDown2.Value.ToString());
                int months = int.Parse(kryptonNumericUpDown1.Value.ToString());
                int serviceId = Convert.ToInt32(kryptonComboBox1.SelectedValue);
                // Lặp qua từng ngày đã chọn
                foreach (var item in datesSelect)
                {
                    // Lặp qua từng tuần
                    for (int i = 0; i < months * 4; i++)
                    {
                        DateTime nextWeek = item.AddDays(i * 7);
                        CasualBooking casualBooking = new CasualBooking();
                        casualBooking.BookingId = randomString(6);
                        casualBooking.BookingDate = bookingDate;
                        casualBooking.StartDate = nextWeek;
                        // Set giây là 0
                        TimeSpan startTime = startTimee;
                        startTime = new TimeSpan(startTime.Hours, startTime.Minutes, 0);
                        casualBooking.StartTime = startTime;
                        casualBooking.Minutes = minutes;
                        casualBooking.FieldId = fieldId;
                        casualBooking.CustomerName = kryptonTextBox1.Text;
                        casualBooking.Phone = kryptonTextBox2.Text;
                        casualBooking.EmployeeId = this.employeeId;
                        casualBooking.ServiceId = serviceId;
                        // Đổi phút sang giờ và tính tiền
                        // Lấy sân và loại sân đã đặt
                        var field = db.Fields.Where(f => f.FieldId == fieldId).FirstOrDefault();
                        var fieldType = db.FieldTypes.Where(ft => ft.TypeId == field.TypeId).FirstOrDefault();
                        // Đổi phút thành giờ
                        int minute = int.Parse(kryptonNumericUpDown2.Value.ToString());
                        double hour = minute * 1.0 / 60;
                        // Tính tiền sân
                        double fieldPrice = hour * (fieldType.FixedRentalPrice ?? 0); // Sử dụng 0 nếu CasualRentalPrice là null
                        double rounding = Math.Round(fieldPrice / 1000) * 1000;
                        // Tính tiền giảm giá
                        var discountService = db.DiscountServices.Where(ds => ds.ServiceId == serviceId).FirstOrDefault();
                        double discount = discountService.Discount ?? 0;
                        rounding = rounding - (rounding * discount) / 100;

                        casualBooking.TotalPrice = Convert.ToInt32(rounding);
                        totalPrice += Convert.ToInt32(rounding);

                        db.CasualBookings.InsertOnSubmit(casualBooking);
                    }
                }
                KryptonMessageBox.Show("Tổng tiền: " + totalPrice, "Tính tiền", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Hiển thị mã QR
                var apiRequest = new APIRequest();
                //apiRequest.acqId = Convert.ToInt32( cb_nganhang.EditValue.ToString());
                apiRequest.acqId = Convert.ToInt32("970415"); // Ngân hàng vietinbank
                apiRequest.accountNo = long.Parse("106876415729"); // Số tài khoản của teo
                apiRequest.accountName = "LE NGUYEN CONG HOAN"; // Tên tài khoản của teo
                apiRequest.amount = Convert.ToInt32(totalPrice);
                apiRequest.format = "text";
                apiRequest.template = "compact";
                //apiRequest.template = cb_template.Text;
                var jsonRequest = JsonConvert.SerializeObject(apiRequest);
                // use restsharp for request api.
                var client = new RestClient("https://api.vietqr.io/v2/generate");
                var request = new RestRequest();

                request.Method = Method.Post;
                request.AddHeader("Accept", "application/json");

                request.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);

                var response = client.Execute(request);
                var content = response.Content;
                var dataResult = JsonConvert.DeserializeObject<APIResponse>(content);


                var image = Base64ToImage(dataResult.data.qrDataURL.Replace("data:image/png;base64,", ""));
                pictureBox1.Image = image;

                kryptonLabel2.Visible = true;
            }
        }

        public Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }

        // Lưu thông tin đặt
        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            db.SubmitChanges();
            KryptonMessageBox.Show("Lưu thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        // Validation thông tin
        private List<string> validationForm()
        {
            List<string> errorsList = new List<string>();
            if (datesSelect.Count == 0)
            {
                errorsList.Add("Vui lòng chọn ngày đặt sân");
            }
            if (kryptonTextBox1.Text.Length < 1)
            {
                errorsList.Add("Vui lòng nhập họ tên");
            }
            if (kryptonTextBox2.Text.Length < 1)
            {
                errorsList.Add("Vui lòng nhập số điện thoại");
            }
            return errorsList;
        }
        // Hàm tạo mã booking ngẫu nhiên
        public string randomString(int length)
        {
            // Tạo chuỗi ký tự cho phép
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            // Tạo chuỗi ngẫu nhiên
            return "CSBK" + new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FixedBookingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Hỏi người dùng có muốn thoát không
            if (KryptonMessageBox.Show("Bạn có muốn thoát không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using API;
using ComponentFactory.Krypton.Toolkit;
using LINQ;
using RestSharp;
using Newtonsoft.Json;


namespace GUI
{
    public partial class BookingForm : KryptonForm
    {
        private SportsFieldManagementContextDataContext db = new SportsFieldManagementContextDataContext();
        private int fieldId;
        private string fieldName;
        private double totalPrice;
        private string employeeId;
        public BookingForm(int fieldId, string fieldName, string employeeId)
        {
            InitializeComponent();
            this.fieldId = fieldId;
            this.fieldName = fieldName;
            this.employeeId = employeeId;
        }

        private void BookingForm_Load(object sender, EventArgs e)
        {
            kryptonLabel2.Text = "Đặt sân số định cho sân: " + fieldName;

            using (WebClient client = new WebClient())
            {
                // Danh sách các ngân hàng hỗ trợ
                var htmlData = client.DownloadData("https://api.vietqr.io/v2/banks");
                // convert byte[] to string
                var bankRawJson = Encoding.UTF8.GetString(htmlData);
                var listBankData = JsonConvert.DeserializeObject<Bank>(bankRawJson);

                //cb_nganhang.Properties.DataSource = listBankData.data;   // list banks
                //cb_nganhang.Properties.DisplayMember = "custom_name";
                //cb_nganhang.Properties.ValueMember = "bin";
                //cb_nganhang.EditValue = listBankData.data.FirstOrDefault().bin;
                //cb_template.SelectedIndex = 0;
            }

            this.loadServices();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            // Tính tiền của sân đã đặt
            // Lấy sân và loại sân đã đặt
            var field = db.Fields.Where(f => f.FieldId == fieldId).FirstOrDefault();
            var fieldType = db.FieldTypes.Where(ft => ft.TypeId == field.TypeId).FirstOrDefault();
            // Đổi phút thành giờ
            int minute = int.Parse(kryptonNumericUpDown1.Value.ToString());
            double hour = minute * 1.0 / 60;

            // Tính tiền sân
            double fieldPrice = hour * (fieldType.CasualRentalPrice ?? 0); // Sử dụng 0 nếu CasualRentalPrice là null
            double rounding = Math.Round(fieldPrice / 1000) * 1000;
            this.totalPrice = rounding;

            // Tính tiền giảm giá
            int discountId = Convert.ToInt32(kryptonComboBox1.SelectedValue);
            var discountService = db.DiscountServices.Where(ds => ds.ServiceId == discountId).FirstOrDefault();
            double discount = discountService.Discount ?? 0;
            this.totalPrice = rounding - (rounding * discount) / 100;

            KryptonMessageBox.Show("Tiền sân: " + this.totalPrice, "Tính tiền", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Tính tiền dịch vụ
            // ...

            // Hiển thị mã QR
            var apiRequest = new APIRequest();
            //apiRequest.acqId = Convert.ToInt32( cb_nganhang.EditValue.ToString());
            apiRequest.acqId = Convert.ToInt32("970415"); // Ngân hàng vietinbank
            apiRequest.accountNo = long.Parse("106876415729"); // Số tài khoản của teo
            apiRequest.accountName = "LE NGUYEN CONG HOAN"; // Tên tài khoản của teo
            apiRequest.amount = Convert.ToInt32(rounding);
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
        }

        public Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            // Lưu lịch sử đặt sân vào csdl
            List<string> errorList = new List<string>();
            DateTime startDate = kryptonDateTimePicker1.Value.Date;
            // Kiểm tra thời gian được chọn trong datetimepicker so với ngày hiện tại
            if (startDate < DateTime.Now.Date)
            {
                errorList.Add("Ngày đặt sân không hợp lệ");
            }
            //Nếu ngày đặt là ngày hiện tại Kiểm tra giờ đặt sân phải trước giờ hiện tại ít nhất 1 tiếng

            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            DateTime currentDate = DateTime.Now.Date;
            DateTime selectedDate = kryptonDateTimePicker1.Value;
            TimeSpan startTime = kryptonDateTimePicker2.Value.TimeOfDay;
            if (selectedDate.Date == currentDate) // Kiểm tra xem ngày đặt có phải là ngày hiện tại
            {
                if (startTime <= currentTime.Add(TimeSpan.FromHours(1))) // Kiểm tra giờ đặt sân
                {
                    errorList.Add("Thời gian đặt sân phải trước giờ hiện tại ít nhất 1 tiếng");
                }
            }
            if (kryptonTextBox1.Text.Length < 1)
            {
                errorList.Add("Vui lòng nhập họ tên");
            }
            if (kryptonTextBox2.Text.Length < 1)
            {
                errorList.Add("Vui lòng nhập số điện thoại");
            }

            // Kiểm tra xem có bị trùng giờ đặt sân không
            int minutes = int.Parse(kryptonNumericUpDown1.Value.ToString());
            if (this.checkDuplicateBooking(startDate, startTime, minutes, fieldId))// Trả về true nghĩa là đã bị trùng
            {
                errorList.Add("Đã có người đặt sân vào thời gian này");
            }

            if (errorList.Count > 0)
            {
                // In ra lỗi đầu tiên
                KryptonMessageBox.Show(errorList[0], "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo và lưu
            CasualBooking casualBooking = new CasualBooking();
            casualBooking.BookingId = randomString(6);
            casualBooking.BookingDate = DateTime.Now;
            casualBooking.StartDate = kryptonDateTimePicker1.Value;
            // Set giây là 0
            startTime = new TimeSpan(startTime.Hours, startTime.Minutes, 0);
            casualBooking.StartTime = startTime;
            casualBooking.Minutes = minutes;
            casualBooking.FieldId = fieldId;
            casualBooking.CustomerName = kryptonTextBox1.Text;
            casualBooking.Phone = kryptonTextBox2.Text;
            casualBooking.EmployeeId = this.employeeId; // Chưa có chức năng đăng nhập
            casualBooking.ServiceId = Convert.ToInt32(kryptonComboBox1.SelectedValue); // Chưa có chức năng dịch vụ
            casualBooking.TotalPrice = Convert.ToInt32(this.totalPrice);

            try
            {
                // Lưu vào csdl
                db.CasualBookings.InsertOnSubmit(casualBooking);
                db.SubmitChanges();
                KryptonMessageBox.Show("Lưu thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message, "Lưu thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm tạo mã booking ngẫu nhiên
        public string randomString(int length)
        {
            // Tạo chuỗi ký tự cho phép
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            // Tạo chuỗi ngẫu nhiên
            var random = new Random();
            return "CSBK" + new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void kryptonCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Check vào thì mở enable cho button lưu
            if (pictureBox1.Image != null)
            {
                kryptonButton2.Enabled = kryptonCheckBox1.Checked;
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BookingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Hỏi người dùng có chắc chắn muốn thoát không
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        // Hàm load các dịch vụ vào combobox
        private void loadServices()
        {
            // load các dịch vụ có startDate endDate nằm trong ngày hiện tại
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

        // Hàm kiểm tra xem có bị trùng giờ đặt sân không
        private bool checkDuplicateBooking(DateTime date, TimeSpan time, int duration, int fieldId)
        {
            // Lấy danh sách các booking đã đặt sân thuộc ngày truyền vào
            var casualBookings = db.CasualBookings.Where(cb => cb.FieldId == fieldId && cb.StartDate.Value.Date == date.Date).ToList();
            if (casualBookings.Count == 0)
            {
                return false;
            }
            else
            {
                // Kiểm tra khoảng thời gian chơi có bị trùng không
                TimeSpan startTime = time;
                TimeSpan endTime = time.Add(TimeSpan.FromMinutes(duration));

                foreach (CasualBooking item in casualBookings)
                {
                    TimeSpan itemStartTime = item.StartTime.Value;
                    TimeSpan itemEndTime = item.StartTime.Value.Add(TimeSpan.FromMinutes(Convert.ToDouble(item.Minutes)));
                    // Kiểm tra startTime có nằm trong khoảng thời gian trên không
                    if (startTime >= itemStartTime && startTime <= itemEndTime)
                    {
                        return true;
                    }
                    // Kiểm tra endTime có nằm trong khoảng thời gian trên không
                    if (endTime >= itemStartTime && endTime <= itemEndTime)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

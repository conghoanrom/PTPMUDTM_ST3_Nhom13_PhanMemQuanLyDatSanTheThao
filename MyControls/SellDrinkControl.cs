using API;
using ComponentFactory.Krypton.Toolkit;
using DTO;
using LINQ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json;
using System.IO;

namespace MyControls
{
    public partial class SellDrinkControl : UserControl
    {
        // Mã nhân viên dang hoạt động
        private string EmployeeId;
        // Thao thác với csdl
        private SportsFieldManagementContextDataContext db = new SportsFieldManagementContextDataContext();
        // Danh sách nước đang chọn
        private List<DrinkSelect> drinkSelects = new List<DrinkSelect>();
        // Id hiện tại đang chọn trong drinkSelect dùng để xóa, sửa
        private int selectedDrinkId = -1;
        // Bill hiện tại đang chọn, hiển thị chi tiết bill
        private string selectedBillId = "";

        public SellDrinkControl(string EmployeeId)
        {
            InitializeComponent();
            this.EmployeeId = EmployeeId;
        }

        private void SellDrinkControl_Load(object sender, EventArgs e)
        {
            loadDataGridViewBill();
            loadDataGridViewDrink();
            editControl();
        }

        // Thêm 1 hàng mới vào listView1
        private void AddItemToListView(DrinkSelect drinkSelects)
        {
            ListViewItem item = new ListViewItem(drinkSelects.DrinkId.ToString()); // Tạo ListViewItem với cột "Mã"
            item.SubItems.Add(drinkSelects.DrinkName); // Thêm cột "Tên" vào hàng
            item.SubItems.Add(drinkSelects.Quantity.ToString()); // Thêm cột "Số lượng" vào hàng
            item.SubItems.Add(drinkSelects.TotalPrice.ToString()); // Thêm cột "Tổng tiền" vào hàng
            listView1.Items.Add(item); // Thêm hàng vào ListView
        }
        // Thêm 1 hàng mới vào listView2
        private void AddItemToListView(int stt, string name, int amount, int totalPrice)
        {
            ListViewItem item = new ListViewItem(stt.ToString()); // Tạo ListViewItem với cột "Mã"
            item.SubItems.Add(name); // Thêm cột "Số lượng" vào hàng
            item.SubItems.Add(amount.ToString()); // Thêm cột "Số lượng" vào hàng
            item.SubItems.Add(totalPrice.ToString()); // Thêm cột "Tổng tiền" vào hàng
            listView2.Items.Add(item); // Thêm hàng vào ListView
        }

        // Tính tiền và in mã QR
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (drinkSelects.Count == 0)
            {
                KryptonMessageBox.Show("Bạn chưa chọn nước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                int totalPrice = Convert.ToInt32(drinkSelects.Sum(dr => dr.TotalPrice));

                // Tạo hóa đơn
                BillDrink billDrink = new BillDrink();
                billDrink.BillId = randomString(6);
                billDrink.EmployeeId = EmployeeId;
                billDrink.CreateAt = DateTime.Now;
                billDrink.TotalPrice = Convert.ToInt32(totalPrice);
                db.BillDrinks.InsertOnSubmit(billDrink);

                // Chi tiết hóa đơn
                foreach (var item in drinkSelects)
                {
                    DetailBill detailBill = new DetailBill();
                    detailBill.BillId = billDrink.BillId;
                    detailBill.DrinkId = item.DrinkId;
                    detailBill.Amount = item.Quantity;
                    db.DetailBills.InsertOnSubmit(detailBill);
                }

                // Hỏi hiện mã QR Không
                DialogResult result = KryptonMessageBox.Show("Tổng tiền là: " + totalPrice + ", bạn có muốn in mã QR", "Tính tiền", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
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

                    // Xóa danh sách chọn và load lại listView1
                    drinkSelects = new List<DrinkSelect>();
                    loadListView();
                    return;
                }
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

        // Lưu vào csdl
        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            try
            {
                db.SubmitChanges();
                KryptonMessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // load lại các bảng cần thiết
                loadDataGridViewBill();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Lưu thất bại(" + ex.Message + ")", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public string randomString(int length)
        {
            // Tạo chuỗi ký tự cho phép
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            // Tạo chuỗi ngẫu nhiên
            var random = new Random();
            return "BILL" + new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Thêm hoặc tăng số lượng nước
        private void kryptonDataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == kryptonDataGridView2.Columns["Select"].Index)
            {
                // Lấy thông tin của hàng và cột đang nhấn
                int rowIndex = e.RowIndex;
                int columnIndex = e.ColumnIndex;

                // Lấy dữ liệu của hàng vừa nhấn
                DataGridViewRow row = kryptonDataGridView2.Rows[rowIndex];
                int drinkId = Convert.ToInt32(row.Cells["DrinkId"].Value);
                int price = Convert.ToInt32(row.Cells["Price"].Value);

                // Lấy dữ liệu và thêm vào listview
                // Kiểm tra xem đã tồn tại id đó chưa
                int count = drinkSelects.Where(dr => dr.DrinkId == drinkId).Count();
                if (count > 0)
                {
                    // Tìm và tăng số lượng
                    foreach (var item in drinkSelects)
                    {
                        if (item.DrinkId == drinkId)
                        {
                            item.Quantity++;
                            item.TotalPrice = item.Quantity * price;
                        }
                    }
                }
                else
                {
                    // Chưa có thì tạo và thêm
                    DrinkSelect drinkSelect = new DrinkSelect();
                    drinkSelect.DrinkId = drinkId;
                    drinkSelect.DrinkName = row.Cells["DrinkName"].Value.ToString();
                    drinkSelect.Quantity = 1;
                    drinkSelect.TotalPrice = Convert.ToInt32(row.Cells["Price"].Value);
                    drinkSelects.Add(drinkSelect);
                }
                loadListView();
            }
        }
        
        // load lại listView1
        private void loadListView()
        {
            listView1.Items.Clear();
            foreach (var item in drinkSelects)
            {
                AddItemToListView(item);
            }
        }

        // Sự kiện khi select vào 1 phần tử trong listView1
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                selectedDrinkId = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
            }
        }

        // Xóa item
        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            // Xóa item được chọn
            if (selectedDrinkId > -1)
            {
                var item = drinkSelects.SingleOrDefault(dr => dr.DrinkId == selectedDrinkId);
                if (item != null)
                {
                    drinkSelects.Remove(item);
                    loadListView();
                }
            }
            else
            {
                KryptonMessageBox.Show("Bạn chưa chọn nước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // Giảm số lượng
        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            // Giảm số lượng
            if (selectedDrinkId > -1)
            {
                var item = drinkSelects.SingleOrDefault(dr => dr.DrinkId == selectedDrinkId);

                if (item != null)
                {
                    if (item.Quantity > 1)
                    {
                        item.Quantity--;
                    }
                    else
                    {
                        drinkSelects.Remove(item);
                    }
                    loadListView();
                }
            }
            else
            {
                KryptonMessageBox.Show("Bạn chưa chọn nước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Click vào dòng của dgv1 thì hiển thị thông tin chi tiết của hóa đơn sang listView2
        private void kryptonDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Lấy BillId

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                int columnIndex = e.ColumnIndex;
                DataGridViewRow row = kryptonDataGridView1.Rows[rowIndex];

                // Lấy giá trị BillId từ cell tương ứng
                string selectedBillId = row.Cells["BillId"].Value.ToString();

                if (selectedBillId.Length > 0)
                {
                    // Lấy danh sách DetailBills từ database
                    List<DetailBill> detailBills = db.DetailBills.Where(dr => dr.BillId == selectedBillId).ToList();

                    // Xóa tất cả các mục trong ListView
                    listView2.Items.Clear();

                    // Thêm các mục vào ListView
                    int i = 1;
                    foreach (var item in detailBills)
                    {
                        Drink drink = db.Drinks.SingleOrDefault(dr => dr.DrinkId == item.DrinkId);
                        if (drink != null)
                        {
                            int amount = Convert.ToInt16(item.Amount);
                            int totalPrice = amount * Convert.ToInt16(drink.Price);
                            AddItemToListView(i, drink.DrinkName, amount, totalPrice);
                            i++;
                        }
                    }
                }
            }

        }

        private void loadDataGridViewBill()
        {
            var billDrinks = db.BillDrinks.ToList();
            kryptonDataGridView1.DataSource = billDrinks;
        }
        private void loadDataGridViewDrink()
        {
            var drinks = db.Drinks.ToList();
            kryptonDataGridView2.DataSource = drinks;
        }
        private void editControl()
        {
            // Edit dataGridView1
            kryptonDataGridView1.Columns["Employee"].Visible = false;

            KryptonDataGridViewButtonColumn deleteButton = new KryptonDataGridViewButtonColumn();
            deleteButton.HeaderText = "Select";
            deleteButton.Name = "Select";
            deleteButton.Text = "Chọn";
            deleteButton.UseColumnTextForButtonValue = true;
            kryptonDataGridView2.Columns.Add(deleteButton);
            // Edit listView1
            listView1.Columns.Add("Mã", 50); // Cột Tên, chiều rộng 150
            listView1.Columns.Add("Tên", 150); // Cột Tên, chiều rộng 150
            listView1.Columns.Add("Số lượng", 100); // Cột Số lượng, chiều rộng 100
            listView1.Columns.Add("Tổng tiền", 100); // Cột Số lượng, chiều rộng 100
            // Edit listView2
            listView2.Columns.Add("STT", 50); // Cột Tên, chiều rộng 150
            listView2.Columns.Add("Tên nước", 100); // Cột Tên, chiều rộng 150
            listView2.Columns.Add("Số lượng", 50); // Cột Số lượng, chiều rộng 100
            listView2.Columns.Add("Tổng tiền", 100); // Cột Số lượng, chiều rộng 100
        }
    }
}
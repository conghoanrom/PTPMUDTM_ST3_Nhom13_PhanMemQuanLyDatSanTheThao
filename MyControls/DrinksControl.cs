using ComponentFactory.Krypton.Toolkit;
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

namespace MyControls
{
    public partial class DrinksControl : UserControl
    {
        private int selectedDrinkId;
        private SportsFieldManagementContextDataContext db = new SportsFieldManagementContextDataContext();
        public DrinksControl()
        {
            InitializeComponent();
        }

        private void DrinksControl_Load(object sender, EventArgs e)
        {
            loadDataGridView();
            // Thêm nút xóa vào cột cuối cùng
            KryptonDataGridViewButtonColumn deleteButton = new KryptonDataGridViewButtonColumn();
            deleteButton.HeaderText = "Delete";
            deleteButton.Name = "Delete";
            deleteButton.Text = "Xóa";
            deleteButton.UseColumnTextForButtonValue = true;
            kryptonDataGridView1.Columns.Add(deleteButton);
        }
        private void loadDataGridView()
        {
            var drinks = db.Drinks.ToList();
            selectedDrinkId = drinks[0].DrinkId;
            kryptonDataGridView1.DataSource = drinks;
        }

        // Tạo nước ngọt mới
        private void kryptonDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy hàng được chọn
                DataGridViewRow row = kryptonDataGridView1.Rows[e.RowIndex];
                selectedDrinkId = Convert.ToInt32(row.Cells["DrinkId"].Value);
                kryptonTextBox1.Text = row.Cells["DrinkName"].Value.ToString();
                if (row.Cells["Quantity"].Value != null && decimal.TryParse(row.Cells["Quantity"].Value.ToString(), out decimal quantity))
                {
                    kryptonNumericUpDown1.Value = quantity;
                }
                else
                {
                    kryptonNumericUpDown1.Value = 0; // or handle the error as needed
                }

                if (row.Cells["Price"].Value != null && decimal.TryParse(row.Cells["Price"].Value.ToString(), out decimal price))
                {
                    kryptonNumericUpDown2.Value = price;
                }
                else
                {
                    kryptonNumericUpDown2.Value = 0; // or handle the error as needed
                }
            }
        }

        // Cập nhật
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            string drinkName = kryptonTextBox1.Text;
            int quantity = Convert.ToInt32(kryptonNumericUpDown1.Value);
            int price = Convert.ToInt32(kryptonNumericUpDown2.Value);

            if (drinkName.Length < 1)
            {
                KryptonMessageBox.Show("Tên nước ngọt không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Drink drink = db.Drinks.SingleOrDefault(x => x.DrinkId == selectedDrinkId);
            drink.DrinkName = drinkName;
            drink.Quantity = quantity;
            drink.Price = price;
            resetInput();
            loadDataGridView();
            KryptonMessageBox.Show("Sửa thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng nhấn vào cột nút (với tên cột là "Delete")
            if (e.ColumnIndex == kryptonDataGridView1.Columns["Delete"].Index)
            {
                // Lấy dữ liệu từ dòng hiện tại
                var row = kryptonDataGridView1.Rows[e.RowIndex];
                int drinkId = Convert.ToInt32(row.Cells["DrinkId"].Value);

                Drink drink = db.Drinks.SingleOrDefault(x => x.DrinkId == drinkId);
                if (drink != null)
                {
                    db.Drinks.DeleteOnSubmit(drink);
                    loadDataGridView();
                }
            }

        }

        // Tạo mới
        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            string drinkName = kryptonTextBox1.Text;
            int quantity = Convert.ToInt32(kryptonNumericUpDown1.Value);
            int price = Convert.ToInt32(kryptonNumericUpDown2.Value);

            if (drinkName.Length < 1)
            {
                KryptonMessageBox.Show("Tên nước ngọt không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Drink drink = new Drink();
            drink.DrinkName = drinkName;
            drink.Quantity = quantity;
            drink.Price = price;

            db.Drinks.InsertOnSubmit(drink);
            resetInput();
            loadDataGridView();
            KryptonMessageBox.Show("Tạo thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        // Lưu thay đổi
        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            try
            {
                db.SubmitChanges();
                KryptonMessageBox.Show("Lưu thay dổi thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Lưu thất bại: " + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                loadDataGridView();
            }
        }

        // Reset input
        private void resetInput()
        {
            kryptonTextBox1.Text = "";
            kryptonNumericUpDown1.Value = 0;
            kryptonNumericUpDown2.Value = 1000;
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            loadDataGridView();
        }
    }
}

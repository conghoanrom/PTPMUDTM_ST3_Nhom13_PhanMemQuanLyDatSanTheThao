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
    public partial class DiscountControl : UserControl
    {
        private SportsFieldManagementContextDataContext db = new SportsFieldManagementContextDataContext();
        private int selectedDiscountId = -1;
        public DiscountControl()
        {
            InitializeComponent();
        }

        private void DiscountControl_Load(object sender, EventArgs e)
        {
            loadDiscount();
        }
        private void loadDiscount()
        {
            var discounts = db.DiscountServices.ToList();
            kryptonDataGridView1.DataSource = discounts;
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            DiscountService discount = getObjectFromForm();
            string message = validateForm(discount);
            if (message != "")
            {
                KryptonMessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            db.DiscountServices.InsertOnSubmit(discount);
        }

        private DiscountService getObjectFromForm()
        {
            DiscountService discount = new DiscountService();
            discount.ServiceName = kryptonTextBox1.Text;
            discount.StartDate = kryptonDateTimePicker1.Value;
            discount.Days = (int)kryptonNumericUpDown1.Value;
            discount.Discount = (int)kryptonNumericUpDown2.Value;
            return discount;
        }
        private string validateForm(DiscountService discount)
        {
            if (discount.ServiceName == "")
            {
                return "Tên dịch vụ không được để trống";
            }
            return "";
        }

        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            // Sửa
            if(selectedDiscountId != -1)
            {
                DiscountService discount = db.DiscountServices.Where(d => d.ServiceId == selectedDiscountId).FirstOrDefault();
                DiscountService newDiscount = getObjectFromForm();
                discount.ServiceName = newDiscount.ServiceName;
                discount.StartDate = newDiscount.StartDate;
                discount.Days = newDiscount.Days;
                discount.Discount = newDiscount.Discount;
            }
            try
            {
                db.SubmitChanges();
                // Nhả ra mêssage box thông báo thành công
                KryptonMessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDiscount();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Lỗi: " + ex.Message, "Lưu thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kryptonDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                DataGridViewRow selectedRow = kryptonDataGridView1.Rows[index];
                selectedDiscountId = int.Parse(selectedRow.Cells["ServiceId"].Value.ToString());
                // Gán dữ liệu vào các testbox
                kryptonTextBox1.Text = selectedRow.Cells["ServiceName"].Value.ToString();
                kryptonDateTimePicker1.Value = DateTime.Parse(selectedRow.Cells["StartDate"].Value.ToString());
                kryptonNumericUpDown1.Value = decimal.Parse(selectedRow.Cells["Days"].Value.ToString());
                kryptonNumericUpDown2.Value = decimal.Parse(selectedRow.Cells["Discount"].Value.ToString());
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            selectedDiscountId = -1;

            // Reset các textbox
            kryptonTextBox1.Text = "";
            kryptonDateTimePicker1.Value = DateTime.Now;
            kryptonNumericUpDown1.Value = 1;
            kryptonNumericUpDown2.Value = 1;
        }
    }
}

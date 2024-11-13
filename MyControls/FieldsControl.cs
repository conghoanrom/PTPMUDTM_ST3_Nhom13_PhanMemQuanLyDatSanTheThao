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
                field.Location = new Point(0, y);
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using LINQ;
using MyControls;
using Services;

namespace GUI
{
    public partial class MainForm : KryptonForm
    {
        private Employee employee;
        private TimeSpan timeElapsed;
        private SportsFieldManagementContextDataContext db = new SportsFieldManagementContextDataContext();
        public delegate void Exit();
        public Exit ExitEvent;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            kryptonLabel1.Text = "Hi, " + this.employee.FullName + "!";
            kryptonLabel2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            kryptonLabel3.Text = "Thời gian làm việc của bạn: 00:00:00";
            // Khởi tạo thời gian đã trôi qua
            timeElapsed = TimeSpan.Zero;
            timer1.Start();

            quảnLýToolStripMenuItem.Enabled = this.employee.RoleId.Equals("MANAGER"); ;

            // Load bóng đá đầu tiên
            List<Field> fields = db.Fields.Where(f => f.FieldType.TypeId == 1 || f.FieldType.TypeId == 2).ToList();
            FieldsControl fieldsControl = new FieldsControl(fields);
            fieldsControl.openBookingFormEvent = new FieldsControl.openBookingForm(openBookingFormEvent);
            fieldsControl.openFixedBookingFormEvent = new FieldsControl.openFixedBookingForm(openFixedBookingFormEvent);
            openFieldsControl(fieldsControl);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Cập nhật thời gian đã trôi qua
            timeElapsed = timeElapsed.Add(TimeSpan.FromSeconds(1));
            UpdateDateTime();
        }
        private void UpdateDateTime()
        {
            // Cập nhật nội dung của Label
            kryptonLabel2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            kryptonLabel3.Text = "Thời gian làm việc của bạn: " + timeElapsed.ToString(@"hh\:mm\:ss");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = KryptonMessageBox.Show("Thời gian làm việc của bạn là " + timeElapsed.ToString(@"hh\:mm\:ss") + ", bạn có chắc chắn muốn thoát không?",
                                              "Xác nhận thoát",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

            // Nếu người dùng chọn "No", hủy sự kiện đóng form
            if (result == DialogResult.No)
            {
                e.Cancel = true;  // Hủy việc đóng form
            }
            else
            {
                // Chấm công nhân viên và thoát chương trình
                int Hours = timeElapsed.Hours;
                int Minutes = timeElapsed.Minutes;
                // Là nhân viên bình thường thì mới chấm công
                if (this.employee.RoleId.Equals("EMPLOYEE"))
                {
                    if (Minutes >= 30)
                    {
                        Hours++;
                        TimeKeeping timeKeeping = new TimeKeeping();
                        timeKeeping.EmployeeId = this.employee.EmployeeId;
                        timeKeeping.DayWorking = DateTime.Now;
                        timeKeeping.HOURS = Hours;
                        db.TimeKeepings.InsertOnSubmit(timeKeeping);
                        db.SubmitChanges();
                    }
                }
                ExitEvent();
            }
        }

        private void openFieldsControl(Control control)
        {
            control.Dock = DockStyle.Fill;
            kryptonPanel1.Controls.Clear();
            kryptonPanel1.Controls.Add(control);
        }

        private void bóngĐáToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Field> fields = db.Fields.Where(f => f.FieldType.TypeId == 1 || f.FieldType.TypeId == 2).ToList();
            FieldsControl fieldsControl = new FieldsControl(fields);
            fieldsControl.openBookingFormEvent = new FieldsControl.openBookingForm(openBookingFormEvent);
            fieldsControl.openFixedBookingFormEvent = new FieldsControl.openFixedBookingForm(openFixedBookingFormEvent);
            openFieldsControl(fieldsControl);
        }

        private void cầuLôngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Field> fields = db.Fields.Where(f => f.FieldType.TypeId == 3).ToList();
            FieldsControl fieldsControl = new FieldsControl(fields);
            fieldsControl.openBookingFormEvent = new FieldsControl.openBookingForm(openBookingFormEvent);
            fieldsControl.openFixedBookingFormEvent = new FieldsControl.openFixedBookingForm(openFixedBookingFormEvent);
            openFieldsControl(fieldsControl);
        }

        private void tennisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Field> fields = db.Fields.Where(f => f.FieldType.TypeId == 4).ToList();
            FieldsControl fieldsControl = new FieldsControl(fields);
            fieldsControl.openBookingFormEvent = new FieldsControl.openBookingForm(openBookingFormEvent);
            fieldsControl.openFixedBookingFormEvent = new FieldsControl.openFixedBookingForm(openFixedBookingFormEvent);
            openFieldsControl(fieldsControl);
        }

        private void bóngBànToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Field> fields = db.Fields.Where(f => f.FieldType.TypeId == 5).ToList();
            FieldsControl fieldsControl = new FieldsControl(fields);
            fieldsControl.openBookingFormEvent = new FieldsControl.openBookingForm(openBookingFormEvent);
            fieldsControl.openFixedBookingFormEvent = new FieldsControl.openFixedBookingForm(openFixedBookingFormEvent);
            openFieldsControl(fieldsControl);
        }

        public void openBookingFormEvent(int fieldId, string fieldName)
        {
            BookingForm bookingForm = new BookingForm(fieldId, fieldName, employee.EmployeeId);
            bookingForm.ShowDialog();
        }
        public void openFixedBookingFormEvent(int fieldId)
        {
            FixedBookingForm fixedBookingForm = new FixedBookingForm(fieldId, employee.EmployeeId);
            fixedBookingForm.ShowDialog();
        }

        private void mãGiảmGiáToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiscountControl discountControl = new DiscountControl();

            openFieldsControl(discountControl);
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeesControl employeesControl = new EmployeesControl();
            employeesControl.EmployeeId = this.employee.EmployeeId;
            openFieldsControl(employeesControl);
        }

        private void báoCáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatisticsControl statistics = new StatisticsControl();
            openFieldsControl(statistics);
        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatabaseControl database = new DatabaseControl();
            openFieldsControl(database);
        }

        private void quảnLýKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrinksControl drinksControl = new DrinksControl();
            openFieldsControl(drinksControl);
        }

        private void đăngXuấtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePasswordControl changePasswordControl = new ChangePasswordControl();
            changePasswordControl.handleChangePasswordEvent = new ChangePasswordControl.handleChangePassword(handleChangePasswordEvent);
            openFieldsControl(changePasswordControl);
        }

        private void handleChangePasswordEvent(DTO.ChangePassword changePassword)
        {
            // Mật khẩu mới có chính xác không
            Employee employee = db.Employees.Where(emp => emp.EmployeeId == this.employee.EmployeeId).FirstOrDefault();

            string password = EncryptionHelper.Decrypt(employee.Password);
            if (!changePassword.OldPassword.Equals(password))
            {
                KryptonMessageBox.Show("Mật khẩu cũ không chính xác", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string encryptedPassword = EncryptionHelper.Encrypt(changePassword.NewPassword);
                employee.Password = encryptedPassword;
                db.SubmitChanges();
                KryptonMessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Visible = false;
                Form1 form1 = new Form1();
                form1.ShowDialog();
            }
        }

        private void trangCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfileControl profileControl = new ProfileControl(employee.EmployeeId);
            profileControl.handleChangeProfileEvent = new ProfileControl.handleChangeProfile(handleChangeProfileEvent);
            openFieldsControl(profileControl);
        }
        private void handleChangeProfileEvent(Employee employeee)
        {
            employee = db.Employees.Where(emp => emp.EmployeeId == employee.EmployeeId).FirstOrDefault();
            employee.FullName = employeee.FullName;
            employee.Phone = employeee.Phone;
            employee.Birth = employeee.Birth;
            employee.Gender = employeee.Gender;
            db.SubmitChanges();
            KryptonMessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void nướcNgọtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SellDrinkControl sellDrinkControl = new SellDrinkControl(employee.EmployeeId);
            openFieldsControl(sellDrinkControl);
        }

        private void nướcNgọtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrinksControl drinksControl = new DrinksControl();
            openFieldsControl(drinksControl);
        }
    }
}

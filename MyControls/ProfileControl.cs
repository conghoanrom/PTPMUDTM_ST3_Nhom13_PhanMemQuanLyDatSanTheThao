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
    public partial class ProfileControl : UserControl
    {
        private string employeeId { get; set; }
        private Employee employee = new Employee();
        private SportsFieldManagementContextDataContext db = new SportsFieldManagementContextDataContext();
        public delegate void handleChangeProfile(Employee employee);
        public handleChangeProfile handleChangeProfileEvent;
        public ProfileControl(string employeeId)
        {
            InitializeComponent();
            this.employeeId = employeeId;
        }

        private void ProfileControl_Load(object sender, EventArgs e)
        {
            loadProfile();
        }
        private void loadProfile()
        {
            employee = db.Employees.Where(e => e.EmployeeId == employeeId).FirstOrDefault();
            kryptonTextBox1.Text = employee.FullName;
            kryptonComboBox1.Text = employee.Gender;
            kryptonDateTimePicker1.Value = employee.Birth.Value;
            kryptonTextBox4.Text = employee.Phone;
            kryptonTextBox6.Text = employee.Username;
            kryptonTextBox5.Text = employee.Password;
            kryptonTextBox7.Text = employee.RoleId;
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            employee.FullName = kryptonTextBox1.Text;
        }

        private void kryptonComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            employee.Gender = kryptonComboBox1.Text;
        }

        private void kryptonDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            employee.Birth = kryptonDateTimePicker1.Value;
        }

        private void kryptonTextBox4_TextChanged(object sender, EventArgs e)
        {
            employee.Phone = kryptonTextBox4.Text;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            // Lưu thông tin
            List<string> errors = validateForm();
            if(errors.Count > 0)
            {
                KryptonMessageBox.Show(string.Join("\n", errors), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            handleChangeProfileEvent(employee);
        }
        private List<string> validateForm()
        {
            List<string> errors = new List<string>();
            if(this.employee.FullName.Length < 1)
            {
                errors.Add("Họ tên không được để trống");
            }
            if (this.employee.Phone.Length < 1)
            {
                errors.Add("Số điện thoại không được để trống");
            }
            // Tuổi phải trên 16
            if (DateTime.Now.Year - this.employee.Birth.Value.Year < 16)
            {
                errors.Add("Tuổi phải trên 16");
            }
            return errors;
        }
    }
}

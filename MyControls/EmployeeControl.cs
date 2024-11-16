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
using Services;
using System.Text.RegularExpressions;

namespace MyControls
{
    public partial class EmployeeControl : UserControl
    {
        private SportsFieldManagementContextDataContext db = new SportsFieldManagementContextDataContext();
        public Employee Employee { get; set; }
        public EmployeeControl(Employee Employee)
        {
            InitializeComponent();
            this.Employee = Employee;
        }

        public EmployeeControl()
        {
            InitializeComponent();
        }

        private void EmployeeControl_Load(object sender, EventArgs e)
        {
            List<Role> roles = new List<Role>();
            roles = db.Roles.ToList();
            kryptonComboBox2.DataSource = roles;
            kryptonComboBox2.DisplayMember = "RoleName";
            kryptonComboBox2.ValueMember = "RoleId";

            // kiểm tra xem Employee có null không
            if (Employee != null)
            {
                kryptonTextBox1.Text = Employee.EmployeeId;
                kryptonTextBox3.Text = Employee.FullName;
                kryptonComboBox1.Text = Employee.Gender;
                kryptonDateTimePicker1.Value = Employee.Birth.Value;
                kryptonTextBox6.Text = Employee.Phone;
                kryptonTextBox7.Text = Employee.Username;
                kryptonTextBox2.Text = Employee.Password;
                kryptonComboBox2.SelectedValue = Employee.RoleId;
            }
        }

        private void kryptonComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (kryptonComboBox2.SelectedValue.ToString().Equals("LINQ"))
            //{
            //    KryptonMessageBox.Show("Bạn vừa chọn vào: " + kryptonComboBox2.SelectedValue.ToString());
            //}
            // Thay đổi vai trò của nhân viên
        }

        private void ClearInput()
        {
            kryptonTextBox1.Clear();
            kryptonTextBox3.Clear();
            kryptonDateTimePicker1.Value = DateTime.Now;
            kryptonTextBox6.Clear();
            kryptonTextBox7.Clear();
            kryptonTextBox2.Clear();
            kryptonComboBox2.SelectedIndex = -1;
        }


        private void unlockControls()
        {
            kryptonTextBox3.Enabled = true;
            kryptonComboBox1.Enabled = true;
            kryptonDateTimePicker1.Enabled = true;
            kryptonTextBox6.Enabled = true;
            kryptonTextBox7.Enabled = true;
            kryptonTextBox2.Enabled = true;
            kryptonComboBox2.Enabled = true;
        }

        private void lockControls()
        {
            kryptonTextBox3.Enabled = false;
            kryptonComboBox1.Enabled = false;
            kryptonDateTimePicker1.Enabled = false;
            kryptonTextBox6.Enabled = false;
            kryptonTextBox7.Enabled = false;
            kryptonTextBox2.Enabled = false;
            kryptonComboBox2.Enabled = false;
        }

        public static string GenerateUniqueEmployeeId()
        {
            string prefix = "NV"; // Tiền tố cố định
            //string datePart = DateTime.Now.ToString("yyyyMMddHHmmss"); // Lấy thời gian hiện tại
            int randomPart = new Random().Next(100, 999); // Số ngẫu nhiên 3 chữ số
            string employeeId = $"{prefix}{randomPart}"; // Ghép tiền tố, thời gian, và số ngẫu nhiên
            return employeeId;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Employee = null;
            ClearInput();
            unlockControls();
            kryptonTextBox1.Text = GenerateUniqueEmployeeId();
        }


        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(kryptonTextBox3.Text) || string.IsNullOrEmpty(kryptonTextBox7.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }
            if (!IsAgeValid(kryptonDateTimePicker1.Value))
            {
                MessageBox.Show("Nhân viên không đủ 16 tuổi.");
                return;
            }
            if (Employee == null)
            {
                // Thêm mới
                Employee newEmployee = new Employee
                {
                    EmployeeId = kryptonTextBox1.Text,
                    FullName = kryptonTextBox3.Text,
                    Gender = kryptonComboBox1.Text,
                    Birth = kryptonDateTimePicker1.Value,
                    Phone = kryptonTextBox6.Text,
                    Username = kryptonTextBox7.Text,
                    Password = EncryptionHelper.Encrypt(kryptonTextBox2.Text),
                    RoleId = kryptonComboBox2.SelectedValue.ToString()
                };
                db.Employees.InsertOnSubmit(newEmployee);
                MessageBox.Show("Thêm nhân viên thành công.");
            }
            else
            {
                // Sửa
                Employee newEmployee = db.Employees.FirstOrDefault(t => t.EmployeeId == Employee.EmployeeId);
                newEmployee.FullName = kryptonTextBox3.Text;
                newEmployee.Gender = kryptonComboBox1.Text;
                newEmployee.Birth = kryptonDateTimePicker1.Value;
                newEmployee.Phone = kryptonTextBox6.Text;
                newEmployee.Username = kryptonTextBox7.Text;
                newEmployee.Password = EncryptionHelper.Encrypt(kryptonTextBox2.Text);
                newEmployee.RoleId = kryptonComboBox2.SelectedValue.ToString();
                MessageBox.Show("Sửa thông tin nhân viên thành công.");
            }

            db.SubmitChanges();
            ClearInput();
            lockControls();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            if (Employee != null)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    Employee E = db.Employees.FirstOrDefault(t => t.EmployeeId == kryptonTextBox1.Text);
                    if (E == null)
                    {
                        MessageBox.Show("Không tồn tại");
                    }
                    db.Employees.DeleteOnSubmit(E);
                    db.SubmitChanges();

                    MessageBox.Show("Đã xóa thành công.");
                    ClearInput();
                    lockControls();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.");
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            if (Employee != null)
            {
                unlockControls();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa.");
            }
        }
        public bool IsAgeValid(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (today < birthDate.AddYears(age))
            {
                age--;
            }
            return age >= 16;
        }

        private void kryptonTextBox6_Leave(object sender, EventArgs e)
        {
            string phoneNumber = kryptonTextBox6.Text;
            string pattern = @"^\d{0,10}$";
            if (phoneNumber.Length != 10 || !Regex.IsMatch(phoneNumber, @"^\d{10}$"))
            {
                MessageBox.Show("Số điện thoại chỉ được chứa đúng 10 số.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                kryptonTextBox6.Clear();
            }
        }

        private void kryptonTextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void kryptonTextBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) || IsVietnameseWithAccent(e.KeyChar))
            {
                // Ngừng sự kiện nếu không phải là chữ cái, số, hoặc có dấu tiếng Việt
                // nhưng cho phép phím Backspace (xóa ký tự)
                if (e.KeyChar != '\b')
                {
                    e.Handled = true;  // Ngừng sự kiện với các ký tự không hợp lệ
                }
            }
        }
        private bool IsVietnameseWithAccent(char c)
        {
            // Kiểm tra nếu ký tự là một ký tự có dấu trong bảng chữ cái tiếng Việt
            string vietnameseWithAccent = "áàạảãâấầậẩẫăắằặẳẵéèẹẻẽêếềệểễíìịỉĩóòọỏõôốồộổỗơớờợởỡúùụủũưứừựửữýỳỵỷỹđ";
            return vietnameseWithAccent.Contains(c);
        }

        private void kryptonTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;  // Ngừng sự kiện nếu không phải là chữ cái hoặc phím Backspace
            }
        }
    }
}

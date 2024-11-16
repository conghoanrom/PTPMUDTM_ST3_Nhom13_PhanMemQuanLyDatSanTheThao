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
    public partial class EmployeeControl : UserControl
    {
        private SportsFieldManagementContextDataContext db = new SportsFieldManagementContextDataContext();
        public Employee Employee { get; set; }
        public string Mode { get; set; }
        public delegate void handleAddEmployee(Employee employee);
        public event handleAddEmployee AddEmployee;
        public delegate void handleEditEmployee(string employeeId, string roleId);
        public event handleEditEmployee EditEmployee;
        public delegate void handleDeleteEmployee(string employeeId);
        public event handleDeleteEmployee DeleteEmployee;
        public EmployeeControl()
        {
            InitializeComponent();
        }
        public Employee newEmployee()
        {
            Employee employee = new Employee();
            employee.EmployeeId = "";
            employee.FullName = "";
            employee.Birth = DateTime.Now;
            employee.Gender = "Nam";
            employee.Phone = "";
            employee.Username = "";
            employee.Password = "";
            employee.RoleId = "";
            return employee;
        }
        private void EmployeeControl_Load(object sender, EventArgs e)
        {
            loadForm(Mode);
        }
        private void loadForm(string mode)
        {
            if (mode == "add")
            {
                this.Employee = newEmployee();
                kryptonLabel1.Text = "Thêm nhân viên mới";

                kryptonTextBox3.Text = "";
                kryptonTextBox3.Enabled = true;
                kryptonTextBox6.Text = "";
                kryptonTextBox6.Enabled = true;
                kryptonTextBox7.Text = "";
                kryptonTextBox7.Enabled = true;
                kryptonTextBox8.Text = "";
                kryptonTextBox8.Enabled = true;
                loadComboBoxGender();
                kryptonComboBox1.Enabled = true;
                kryptonDateTimePicker1.Value = DateTime.Now;
                kryptonDateTimePicker1.Enabled = true;
                loadComboBoxRole();
            }
            else if (mode == "edit")
            {
                kryptonLabel1.Text = "Chỉnh sửa thông tin nhân viên";

                kryptonTextBox3.Text = Employee.FullName;
                kryptonTextBox3.Enabled = false;
                kryptonTextBox6.Text = Employee.Phone;
                kryptonTextBox6.Enabled = false;
                kryptonTextBox7.Text = Employee.Username;
                kryptonTextBox7.Enabled = false;
                kryptonTextBox8.Text = Employee.Password;
                kryptonTextBox8.Enabled = false;
                kryptonComboBox1.Text = Employee.Gender;
                kryptonComboBox1.Enabled = false;
                kryptonDateTimePicker1.Value = Employee.Birth.Value;
                kryptonDateTimePicker1.Enabled = false;
                loadComboBoxRole();
                kryptonComboBox2.SelectedValue = Employee.RoleId;
            }
            kryptonButton1.Enabled = mode == "add";
            kryptonButton2.Enabled = mode == "edit";
            kryptonButton3.Enabled = mode == "edit";
        }

        private void loadComboBoxGender()
        {
            kryptonComboBox1.Items.Add("Nam");
            kryptonComboBox1.Items.Add("Nữ");
        }
        private void loadComboBoxRole()
        {
            // clear combobox
            kryptonComboBox2.DataSource = null;
            var roles = db.Roles.ToList();
            kryptonComboBox2.DataSource = roles;
            kryptonComboBox2.DisplayMember = "RoleName";
            kryptonComboBox2.ValueMember = "RoleId";
        }

        // Thêm nhân viên
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            List<string> validateEmployees = validateEmployee();
            if (validateEmployees.Count > 0)
            {
                KryptonMessageBox.Show(validateEmployees[0], "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Employee.EmployeeId = generateEmployeeId();
            AddEmployee(this.Employee);
        }

        private string generateEmployeeId()
        {
            // Lấy danh sách nhân viên
            var employees = db.Employees.ToList();
            // Lấy mã nhân viên
            List<string> employeeIds = new List<string>();
            foreach (var employee in employees)
            {
                employeeIds.Add(employee.EmployeeId);
            }
            // Tạo mã nhân viên mới với format NV001, NV002, ...
            int i = 1;
            while (true)
            {
                string newEmployeeId = "NV" + i.ToString("D3");
                if (!employeeIds.Contains(newEmployeeId))
                {
                    return newEmployeeId;
                }
                i++;
            }

        }

        private List<string> validateEmployee()
        {
            List<string> errors = new List<string>();
            if (this.Employee.FullName.Length < 1)
            {
                errors.Add("Tên nhân viên không được để trống");
            }
            // Điện thoại trên 10 số và không chứa chữ
            if (this.Employee.Phone.Length < 10 || this.Employee.Phone.Length > 11 || !this.Employee.Phone.All(char.IsDigit))
            {
                errors.Add("Số điện thoại không hợp lệ");
            }
            // Kiểm tra ngày sinh phải là 16 tuổi
            if (DateTime.Now.Year - this.Employee.Birth.Value.Year < 16)
            {
                errors.Add("Nhân viên phải từ 16 tuổi trở lên");
            }
            // Kiểm tra username không được để trống và phải trên 6 kí tự
            if (this.Employee.Username.Length < 6)
            {
                errors.Add("Tên đăng nhập phải từ 6 kí tự trở lên");
            }
            // Kiểm tra password phải trên 8 kí tưj
            if (this.Employee.Password.Length < 8)
            {
                errors.Add("Mật khẩu phải từ 8 kí tự trở lên");
            }
            return errors;
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            List<string> validateEmployees = validateEmployee();
            if (validateEmployees.Count > 0)
            {
                KryptonMessageBox.Show(validateEmployees[0], "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            EditEmployee(Employee.EmployeeId, kryptonComboBox2.SelectedValue.ToString());
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            DeleteEmployee(Employee.EmployeeId);
        }

        private void kryptonTextBox3_TextChanged(object sender, EventArgs e)
        {
            this.Employee.FullName = kryptonTextBox3.Text;
        }

        private void kryptonComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Employee.EmployeeId = kryptonComboBox1.Text;
        }

        private void kryptonDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.Employee.Birth = kryptonDateTimePicker1.Value;
        }
        private void kryptonTextBox6_TextChanged(object sender, EventArgs e)
        {
            this.Employee.Phone = kryptonTextBox6.Text;
        }
        private void kryptonTextBox7_TextChanged(object sender, EventArgs e)
        {
            this.Employee.Username = kryptonTextBox7.Text;
        }

        private void kryptonTextBox8_TextChanged(object sender, EventArgs e)
        {
            this.Employee.Password = kryptonTextBox8.Text;
        }
    }
}

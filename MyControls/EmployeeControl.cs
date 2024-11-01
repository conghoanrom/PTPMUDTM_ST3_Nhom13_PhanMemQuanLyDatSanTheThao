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
        public EmployeeControl(Employee Employee)
        {
            InitializeComponent();
            this.Employee = Employee;
        }

        private void EmployeeControl_Load(object sender, EventArgs e)
        {
            List<Role> roles = new List<Role>();
            roles = db.Roles.ToList();
            kryptonComboBox2.DataSource = roles;
            kryptonComboBox2.DisplayMember = "RoleName";
            kryptonComboBox2.ValueMember = "RoleId";

            kryptonTextBox1.Text = Employee.EmployeeId;
            kryptonTextBox3.Text = Employee.FullName;
            kryptonComboBox1.Text = Employee.Gender;
            kryptonDateTimePicker1.Value = Employee.Birth.Value;
            kryptonTextBox6.Text = Employee.Phone;
            kryptonTextBox7.Text = Employee.Username;
            kryptonTextBox8.Text = Employee.Password;
            kryptonComboBox2.SelectedValue = Employee.RoleId;
        }

        private void kryptonComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (kryptonComboBox2.SelectedValue.ToString().Equals("LINQ"))
            //{
            //    KryptonMessageBox.Show("Bạn vừa chọn vào: " + kryptonComboBox2.SelectedValue.ToString());
            //}
            // Thay đổi vai trò của nhân viên
        }
    }
}

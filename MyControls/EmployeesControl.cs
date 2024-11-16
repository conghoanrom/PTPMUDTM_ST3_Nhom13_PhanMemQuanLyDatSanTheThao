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
    public partial class EmployeesControl : UserControl
    {
        private SportsFieldManagementContextDataContext db = new SportsFieldManagementContextDataContext();
        private string selectedEmployeeId = "";
        public string EmployeeId { get; set; }
        public EmployeesControl()
        {
            InitializeComponent();
        }

        private void EmployeesControl_Load(object sender, EventArgs e)
        {
            loadEmployees();
            loadEmployeeControl("add");
        }
        private void loadEmployees()
        {
            var employees = db.Employees.Where(x => x.EmployeeId != this.EmployeeId).ToList();
            kryptonDataGridView1.DataSource = employees;
            //loại bỏ cột không cần thiết
            kryptonDataGridView1.Columns["Password"].Visible = false;
            kryptonDataGridView1.Columns["Role"].Visible = false;
            kryptonDataGridView1.Columns["Username"].Visible = false;
            kryptonDataGridView1.Columns["SalaryPayment"].Visible = false;
            kryptonDataGridView1.Columns["TimeKeeping"].Visible = false;
        }
        private void loadEmployeeControl(string mode)
        {
            EmployeeControl employeeControl = new EmployeeControl();
            employeeControl.AddEmployee += new EmployeeControl.handleAddEmployee(addEmployee);
            employeeControl.DeleteEmployee += new EmployeeControl.handleDeleteEmployee(deleteEmployee);
            employeeControl.EditEmployee += new EmployeeControl.handleEditEmployee(editEmployee);
            employeeControl.Dock = DockStyle.Fill;
            employeeControl.Mode = mode;
            if (mode == "edit")
            {
                employeeControl.Employee = db.Employees.SingleOrDefault(x => x.EmployeeId == selectedEmployeeId);
            }
            this.kryptonPanel2.Controls.Clear();
            this.kryptonPanel2.Controls.Add(employeeControl);
        }

        private void kryptonDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // lấy id của nhân viên được chọn
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.kryptonDataGridView1.Rows[e.RowIndex];
                selectedEmployeeId = row.Cells["EmployeeId"].Value.ToString();
                loadEmployeeControl("edit");
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.loadEmployeeControl("add");
        }

        private void addEmployee(Employee employee)
        {
            try
            {
                db.Employees.InsertOnSubmit(employee);
                db.SubmitChanges();
                loadEmployees();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Thêm nhân viên thất bại(" + ex.Message + ")", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void editEmployee(string EmployeeId, string roleId)
        {
            try
            {
                //Tìm và sửa
                Employee currentEmployee = db.Employees.SingleOrDefault(x => x.EmployeeId == EmployeeId);
                // Tìm role
                Role role = db.Roles.SingleOrDefault(x => x.RoleId == roleId);
                currentEmployee.Role = role;
                db.SubmitChanges();
                loadEmployees();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Cập nhật thất bại(" + ex.Message + ")", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void deleteEmployee(string employeeId)
        {
            try
            {
                // Tìm và xóa
                Employee currentEmployee = db.Employees.SingleOrDefault(x => x.EmployeeId == employeeId);
                db.Employees.DeleteOnSubmit(currentEmployee);
                db.SubmitChanges();
                loadEmployees();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Xóa nhân viên thất bại(" + ex.Message + ")", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

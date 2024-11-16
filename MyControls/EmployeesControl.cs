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
        public List<Employee> Employees { get; set; }
        public EmployeesControl(List<Employee> Employees)
        {
            InitializeComponent();
            this.Employees = Employees;
        }

        private void EmployeesControl_Load(object sender, EventArgs e)
        {
            // Edit kryptonDataGridView1
            kryptonDataGridView1.DataSource = Employees;
            editView();

            EmployeeControl employeeControl = new EmployeeControl();
            //this.kryptonPanel2.Controls.Clear();
            this.kryptonPanel2.Controls.Add(employeeControl);
        }

        private void editView()
        {
            kryptonDataGridView1.Columns["TimeKeeping"].Visible = false;
            kryptonDataGridView1.Columns["SalaryPayment"].Visible = false;
            kryptonDataGridView1.Columns["Password"].Visible = false;
            kryptonDataGridView1.Columns["RoleId"].Visible = false;
            kryptonDataGridView1.Columns["Role"].Visible = false;
            kryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void kryptonDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy thông tin của hàng đã nhấp
                DataGridViewRow row = kryptonDataGridView1.Rows[e.RowIndex];

                // Ví dụ: Lấy giá trị của cột đầu tiên trong hàng đã nhấp
                string employeeId = row.Cells[0].Value.ToString();
                //KryptonMessageBox.Show("Bạn vừa nhấn vào nhân viên: " + employeeId);

                // Hiển thị thông tin chi tiết của nhân viên
                Employee employee = db.Employees.Where(x => x.EmployeeId == employeeId).FirstOrDefault();
                EmployeeControl employeeControl = new EmployeeControl(employee);

                this.kryptonPanel2.Controls.Clear();
                this.kryptonPanel2.Controls.Add(employeeControl);
            }
        }

        //load lại view
        public void Reload()
        {
            kryptonDataGridView1.DataSource = null;
            kryptonDataGridView1.DataSource = db.Employees.ToList();
            editView();
        }
        // làm mới chưa đúng định dạng của view, chưa test xoá sửa
        private void kryptonLabel1_Click(object sender, EventArgs e)
        {
            Reload();
        }
    }
}

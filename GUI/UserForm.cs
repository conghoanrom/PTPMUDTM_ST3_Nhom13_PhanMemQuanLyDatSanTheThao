using ComponentFactory.Krypton.Toolkit;
using LINQ;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class UserForm : Form
    {
        private Employee employee;
        private SportsFieldManagementContextDataContext db = new SportsFieldManagementContextDataContext();
        private bool isPasswordVisible = false;

        public UserForm(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            this.txt_IDNhanVien.Enabled = false;
            this.txt_Role.Enabled = false;
            this.txt_Username.Enabled = false;
            this.txt_Password.Enabled = false;

            this.txt_Password.PasswordChar = '•';
            this.txt_MatKhauCu.PasswordChar = '•';
            this.txt_MatKhauMoi.PasswordChar = '•';
            this.txt_NhapLaiMatKhau.PasswordChar = '•';
            comboBox_GioiTinh.Items.Add("Nam");
            comboBox_GioiTinh.Items.Add("Nữ");
            comboBox_GioiTinh.Items.Add("Khác");
            comboBox_GioiTinh.SelectedItem = employee.Gender;

            this.txt_IDNhanVien.Text = employee.EmployeeId;
            var role = db.Roles.SingleOrDefault(r => r.RoleId == employee.RoleId);
            this.txt_Role.Text = role != null ? role.RoleName : "N/A";
            this.txt_Username.Text = employee.Username;
            this.txt_Password.Text = employee.Password;
            this.txt_HovaTen.Text = employee.FullName;

            this.dtp_NgaySinh.Format = DateTimePickerFormat.Custom;
            this.dtp_NgaySinh.CustomFormat = "dd/MM/yyyy";
            this.dtp_NgaySinh.Value = employee.Birth.HasValue ? employee.Birth.Value : DateTime.Now;

            this.txt_SDT.Text = employee.Phone.ToString();
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            var existingEmployee = db.Employees.SingleOrDefault(emp => emp.EmployeeId == employee.EmployeeId);
            if (existingEmployee != null)
            {
                existingEmployee.FullName = this.txt_HovaTen.Text;
                existingEmployee.Gender = comboBox_GioiTinh.SelectedItem.ToString();
                existingEmployee.Birth = this.dtp_NgaySinh.Value;
                existingEmployee.Phone = this.txt_SDT.Text;

                db.SubmitChanges();
                this.employee = existingEmployee;
                MessageBox.Show("Thông tin đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không tìm thấy nhân viên để cập nhật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {
            this.txt_HovaTen.Text = employee.FullName;
            comboBox_GioiTinh.SelectedItem = employee.Gender;
            this.dtp_NgaySinh.Value = employee.Birth.HasValue ? employee.Birth.Value : DateTime.Now;
            this.txt_SDT.Text = employee.Phone.ToString();

            MessageBox.Show("Thông tin đã được khôi phục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_LuuDoiMK_Click(object sender, EventArgs e)
        {
            if (txt_MatKhauMoi.Text != txt_NhapLaiMatKhau.Text)
            {
                MessageBox.Show("Mật khẩu mới và nhập lại không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string encryptedPassword = employee.Password;
            // Giải mã mật khẩu
            string decryptedPassword = EncryptionHelper.Decrypt(encryptedPassword);

            if (txt_MatKhauCu.Text == decryptedPassword)
            {
                var existingEmployee = db.Employees.SingleOrDefault(emp => emp.EmployeeId == employee.EmployeeId);
                if (existingEmployee != null)
                {
                    existingEmployee.Password = EncryptionHelper.Encrypt(txt_MatKhauMoi.Text);
                    db.SubmitChanges();
                    txt_Password.Text = existingEmployee.Password;

                    // Đăng xuất và yêu cầu người dùng đăng nhập lại với mật khẩu mới
                    MessageBox.Show("Mật khẩu đã được đổi thành công! Vui lòng đăng nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Visible = false;  
                    Form1 loginForm = new Form1();
                    loginForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên để cập nhật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_HuyDoiMK_Click(object sender, EventArgs e)
        {
            txt_MatKhauCu.Clear();
            txt_MatKhauMoi.Clear();
            txt_NhapLaiMatKhau.Clear();
            MessageBox.Show("Thông tin đã được xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void TogglePasswordVisibility(KryptonTextBox textBox)
        {
            if (textBox.PasswordChar == '•')
            {
                textBox.PasswordChar = '\0';
            }
            else
            {
                textBox.PasswordChar = '•';
            }
        }
        private void btn_HienMatKhauCu_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility(txt_MatKhauCu);
        }

        private void btn_HienMatKhauMoi_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility(txt_MatKhauMoi);
        }

        private void btn_HienMatKhauNhapLai_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility(txt_NhapLaiMatKhau);
        }

        private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
        }
    }
}

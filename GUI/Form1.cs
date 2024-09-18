using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using LINQ;
using System.Data.Linq;
using Services;

namespace GUI
{
    public partial class Form1 : KryptonForm
    {
        SportsFieldManagementContextDataContext db = new SportsFieldManagementContextDataContext();
        public Form1()
        {
            InitializeComponent();
        }

        private void loginControl1_CancelClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = KryptonMessageBox.Show("Bạn có chắc chắn muốn thoát không?",
                                              "Xác nhận thoát",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

            // Nếu người dùng chọn "No", hủy sự kiện đóng form
            if (result == DialogResult.No)
            {
                e.Cancel = true;  // Hủy việc đóng form
            }
        }

        private void loginControl1_SubmitClicked(object sender, EventArgs e)
        {
            //KryptonMessageBox.Show("Username: " + this.loginControl1.Username + "\n" + "Password: " + this.loginControl1.Password, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Employee employee = db.Employees.Where(em => em.Username == this.loginControl1.Username).FirstOrDefault();
            if (employee == null)
            {
                KryptonMessageBox.Show("Tên đăng nhập không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                string encryptedPassword = employee.Password;
                // Giải mã mật khẩu
                string decryptedPassword = EncryptionHelper.Decrypt(encryptedPassword);

                if (this.loginControl1.Password != decryptedPassword)
                {
                    KryptonMessageBox.Show("Mật khẩu không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    KryptonMessageBox.Show("Đăng nhập thành công, vai trò của bạn là: " + employee.Role.RoleName, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.loginControl1.resetText();
                    return;
                }
            }
        }
    }
}

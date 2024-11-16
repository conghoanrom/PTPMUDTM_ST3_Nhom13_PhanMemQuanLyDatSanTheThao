using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using ComponentFactory.Krypton.Toolkit;

namespace MyControls
{
    public partial class ChangePasswordControl : UserControl
    {
        public ChangePassword changePassword = new ChangePassword();
        public delegate void handleChangePassword(ChangePassword changePassword);
        public handleChangePassword handleChangePasswordEvent;
        public ChangePasswordControl()
        {
            InitializeComponent();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (this.validateForm().Count > 0)
            {
                KryptonMessageBox.Show(string.Join("\n", this.validateForm()), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.handleChangePasswordEvent(this.changePassword);
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.changePassword.OldPassword = kryptonTextBox1.Text;
        }

        private void kryptonTextBox2_TextChanged(object sender, EventArgs e)
        {
            this.changePassword.NewPassword = kryptonTextBox2.Text;
        }

        private void kryptonTextBox3_TextChanged(object sender, EventArgs e)
        {
            this.changePassword.ConfirmPassword = kryptonTextBox3.Text;
        }

        private List<string> validateForm()
        {
            List<string> result = new List<string>();
            if (this.changePassword.OldPassword.Length < 1)
            {
                result.Add("Mật khẩu cũ không được để trống");
            }
            if (this.changePassword.NewPassword.Length < 1)
            {
                result.Add("Mật khẩu mới không được để trống");
            }
            if (this.changePassword.ConfirmPassword.Length < 1)
            {
                result.Add("Xác nhận mật khẩu không được để trống");
            }

            // Mật khẩu mới tối thiểu 8 kí tự
            if (this.changePassword.NewPassword.Length < 8)
            {
                result.Add("Mật khẩu mới tối thiểu 8 kí tự");
            }

            // Mật khẩu mới và xác nhận mật khẩu không trùng nhau
            if (!this.changePassword.NewPassword.Equals(this.changePassword.ConfirmPassword))
            {
                result.Add("Mật khẩu mới và xác nhận mật khẩu không trùng nhau");
            }

            // Mật khẩu mới không trùng với mật khẩu cũ
            if (this.changePassword.NewPassword.Equals(this.changePassword.OldPassword))
            {
                result.Add("Mật khẩu mới không được trùng với mật khẩu cũ");
            }

            return result;
        }
    }
}

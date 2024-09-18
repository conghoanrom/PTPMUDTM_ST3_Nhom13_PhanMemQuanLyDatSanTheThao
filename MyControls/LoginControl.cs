using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComponentFactory.Krypton.Toolkit;

namespace MyControls
{
    public partial class LoginControl : UserControl
    {
        public string Username { get; set; }
        public string Password { get; set; }

        // Sự kiện khi click vào nút Submit
        public delegate void SubmitClickedHandler(object sender, EventArgs e);
        public event SubmitClickedHandler SubmitClicked;
        protected virtual void OnSubmitClicked(object sender, EventArgs e)
        {
            if (SubmitClicked != null)
            {
                SubmitClicked(sender, e);
            }
        }
        // Sự kiện khi click vào nút Cancel
        public delegate void CancelClickedHandler(object sender, EventArgs e);
        public event CancelClickedHandler CancelClicked;
        protected virtual void OnCancelClicked(object sender, EventArgs e)
        {
            if (CancelClicked != null)
            {
                CancelClicked(sender, e);
            }
        }
        public LoginControl()
        {
            InitializeComponent();
        }

        private void kryptonCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            kryptonTextBox2.PasswordChar = (kryptonCheckBox1.Checked) ? (char)0 : '●';
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text.Length == 0)
            {
                KryptonMessageBox.Show("Vui lòng nhập tên đăng nhập", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (kryptonTextBox2.Text.Length == 0)
            {
                KryptonMessageBox.Show("Vui lòng nhập mật khẩu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OnSubmitClicked(sender, e);
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            OnCancelClicked(sender, e);
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.Username = kryptonTextBox1.Text;
        }

        private void kryptonTextBox2_TextChanged(object sender, EventArgs e)
        {
            this.Password = kryptonTextBox2.Text;
        }
        public void resetText()
        {
            kryptonTextBox1.Text = "";
            kryptonTextBox2.Text = "";
            kryptonCheckBox1.Checked = false;
        }
    }
}

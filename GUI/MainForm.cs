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

namespace GUI
{
    public partial class MainForm : KryptonForm
    {
        private TimeSpan timeElapsed;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            kryptonLabel1.Text = "Hi, Lê Nguyễn Công Hoan!";
            kryptonLabel2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            kryptonLabel3.Text = "Thời gian làm việc của bạn: 00:00:00";
            // Khởi tạo thời gian đã trôi qua
            timeElapsed = TimeSpan.Zero;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Cập nhật thời gian đã trôi qua
            timeElapsed = timeElapsed.Add(TimeSpan.FromSeconds(1));
            UpdateDateTime();
        }
        private void UpdateDateTime()
        {
            // Cập nhật nội dung của Label
            kryptonLabel2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            kryptonLabel3.Text = "Thời gian làm việc của bạn: " + timeElapsed.ToString(@"hh\:mm\:ss");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = KryptonMessageBox.Show("Thời gian làm việc của bạn là " + timeElapsed.ToString(@"hh\:mm\:ss")+ ", bạn có chắc chắn muốn thoát không?",
                                              "Xác nhận thoát",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

            // Nếu người dùng chọn "No", hủy sự kiện đóng form
            if (result == DialogResult.No)
            {
                e.Cancel = true;  // Hủy việc đóng form
            }
            else
            {
                // Chấm công nhân viên và thoát chương trình
            }
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyControls
{
    public partial class DatabaseControl : UserControl
    {
        public DatabaseControl()
        {
            InitializeComponent();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=MSI;Initial Catalog=DBSPORTSFIELDBOOKING;User ID=sa;Password=Hoan123321;Encrypt=False";  // Cập nhật chuỗi kết nối của bạn
            string backupFilePath = @"D:\backupsqlserver\DBSPORTSFIELDBOOKING.bak";

            string query = $"BACKUP DATABASE DBSPORTSFIELDBOOKING TO DISK = '{backupFilePath}'";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    KryptonMessageBox.Show("Sao lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show($"Lỗi sao lưu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=MSI;Initial Catalog=DBSPORTSFIELDBOOKING;User ID=sa;Password=Hoan123321;Encrypt=False";  // Cập nhật chuỗi kết nối của bạn
            string backupFilePath = @"D:\backupsqlserver\DBSPORTSFIELDBOOKING.bak";

            string query = $"RESTORE DATABASE DBSPORTSFIELDBOOKING FROM DISK = '{backupFilePath}'";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    KryptonMessageBox.Show("Phục hồi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show($"Lỗi phục hồi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

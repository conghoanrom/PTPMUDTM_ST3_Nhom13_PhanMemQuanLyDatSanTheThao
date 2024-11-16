namespace GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.quảnLíSânToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bóngĐáToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cầuLôngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tennisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bóngBànToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nướcNgọtToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mãGiảmGiáToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nhânViênToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.báoCáoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nướcNgọtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tàiKhoảnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trangCáNhânToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đổiMậtKhẩuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đăngXuấtToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quảnLíSânToolStripMenuItem,
            this.quảnLýToolStripMenuItem,
            this.tàiKhoảnToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1582, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // quảnLíSânToolStripMenuItem
            // 
            this.quảnLíSânToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bóngĐáToolStripMenuItem,
            this.cầuLôngToolStripMenuItem,
            this.tennisToolStripMenuItem,
            this.bóngBànToolStripMenuItem,
            this.nướcNgọtToolStripMenuItem1});
            this.quảnLíSânToolStripMenuItem.Image = global::GUI.Properties.Resources.clipboard;
            this.quảnLíSânToolStripMenuItem.Name = "quảnLíSânToolStripMenuItem";
            this.quảnLíSânToolStripMenuItem.Size = new System.Drawing.Size(116, 24);
            this.quảnLíSânToolStripMenuItem.Text = "Quản lí sân";
            // 
            // bóngĐáToolStripMenuItem
            // 
            this.bóngĐáToolStripMenuItem.Image = global::GUI.Properties.Resources.football_field;
            this.bóngĐáToolStripMenuItem.Name = "bóngĐáToolStripMenuItem";
            this.bóngĐáToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.bóngĐáToolStripMenuItem.Text = "Bóng đá";
            this.bóngĐáToolStripMenuItem.Click += new System.EventHandler(this.bóngĐáToolStripMenuItem_Click);
            // 
            // cầuLôngToolStripMenuItem
            // 
            this.cầuLôngToolStripMenuItem.Image = global::GUI.Properties.Resources.sport;
            this.cầuLôngToolStripMenuItem.Name = "cầuLôngToolStripMenuItem";
            this.cầuLôngToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.cầuLôngToolStripMenuItem.Text = "Cầu lông";
            this.cầuLôngToolStripMenuItem.Click += new System.EventHandler(this.cầuLôngToolStripMenuItem_Click);
            // 
            // tennisToolStripMenuItem
            // 
            this.tennisToolStripMenuItem.Image = global::GUI.Properties.Resources.court;
            this.tennisToolStripMenuItem.Name = "tennisToolStripMenuItem";
            this.tennisToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.tennisToolStripMenuItem.Text = "Tennis";
            this.tennisToolStripMenuItem.Click += new System.EventHandler(this.tennisToolStripMenuItem_Click);
            // 
            // bóngBànToolStripMenuItem
            // 
            this.bóngBànToolStripMenuItem.Image = global::GUI.Properties.Resources.table_tennis;
            this.bóngBànToolStripMenuItem.Name = "bóngBànToolStripMenuItem";
            this.bóngBànToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.bóngBànToolStripMenuItem.Text = "Bóng bàn";
            this.bóngBànToolStripMenuItem.Click += new System.EventHandler(this.bóngBànToolStripMenuItem_Click);
            // 
            // nướcNgọtToolStripMenuItem1
            // 
            this.nướcNgọtToolStripMenuItem1.Image = global::GUI.Properties.Resources.food;
            this.nướcNgọtToolStripMenuItem1.Name = "nướcNgọtToolStripMenuItem1";
            this.nướcNgọtToolStripMenuItem1.Size = new System.Drawing.Size(163, 26);
            this.nướcNgọtToolStripMenuItem1.Text = "Nước ngọt";
            this.nướcNgọtToolStripMenuItem1.Click += new System.EventHandler(this.nướcNgọtToolStripMenuItem1_Click);
            // 
            // quảnLýToolStripMenuItem
            // 
            this.quảnLýToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mãGiảmGiáToolStripMenuItem,
            this.nhânViênToolStripMenuItem,
            this.báoCáoToolStripMenuItem,
            this.dToolStripMenuItem,
            this.nướcNgọtToolStripMenuItem});
            this.quảnLýToolStripMenuItem.Image = global::GUI.Properties.Resources.project_management;
            this.quảnLýToolStripMenuItem.Name = "quảnLýToolStripMenuItem";
            this.quảnLýToolStripMenuItem.Size = new System.Drawing.Size(93, 24);
            this.quảnLýToolStripMenuItem.Text = "Quản lý";
            // 
            // mãGiảmGiáToolStripMenuItem
            // 
            this.mãGiảmGiáToolStripMenuItem.Image = global::GUI.Properties.Resources.coupon;
            this.mãGiảmGiáToolStripMenuItem.Name = "mãGiảmGiáToolStripMenuItem";
            this.mãGiảmGiáToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.mãGiảmGiáToolStripMenuItem.Text = "Mã giảm giá";
            this.mãGiảmGiáToolStripMenuItem.Click += new System.EventHandler(this.mãGiảmGiáToolStripMenuItem_Click);
            // 
            // nhânViênToolStripMenuItem
            // 
            this.nhânViênToolStripMenuItem.Image = global::GUI.Properties.Resources.employee;
            this.nhânViênToolStripMenuItem.Name = "nhânViênToolStripMenuItem";
            this.nhânViênToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.nhânViênToolStripMenuItem.Text = "Nhân viên";
            this.nhânViênToolStripMenuItem.Click += new System.EventHandler(this.nhânViênToolStripMenuItem_Click);
            // 
            // báoCáoToolStripMenuItem
            // 
            this.báoCáoToolStripMenuItem.Image = global::GUI.Properties.Resources.dashboard;
            this.báoCáoToolStripMenuItem.Name = "báoCáoToolStripMenuItem";
            this.báoCáoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.báoCáoToolStripMenuItem.Text = "Báo cáo";
            this.báoCáoToolStripMenuItem.Click += new System.EventHandler(this.báoCáoToolStripMenuItem_Click);
            // 
            // dToolStripMenuItem
            // 
            this.dToolStripMenuItem.Image = global::GUI.Properties.Resources.database;
            this.dToolStripMenuItem.Name = "dToolStripMenuItem";
            this.dToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.dToolStripMenuItem.Text = "Database";
            this.dToolStripMenuItem.Click += new System.EventHandler(this.dToolStripMenuItem_Click);
            // 
            // nướcNgọtToolStripMenuItem
            // 
            this.nướcNgọtToolStripMenuItem.Image = global::GUI.Properties.Resources.food;
            this.nướcNgọtToolStripMenuItem.Name = "nướcNgọtToolStripMenuItem";
            this.nướcNgọtToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.nướcNgọtToolStripMenuItem.Text = "Nước ngọt";
            this.nướcNgọtToolStripMenuItem.Click += new System.EventHandler(this.nướcNgọtToolStripMenuItem_Click);
            // 
            // tàiKhoảnToolStripMenuItem
            // 
            this.tàiKhoảnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trangCáNhânToolStripMenuItem,
            this.đổiMậtKhẩuToolStripMenuItem,
            this.đăngXuấtToolStripMenuItem1});
            this.tàiKhoảnToolStripMenuItem.Image = global::GUI.Properties.Resources.profile;
            this.tàiKhoảnToolStripMenuItem.Name = "tàiKhoảnToolStripMenuItem";
            this.tàiKhoảnToolStripMenuItem.Size = new System.Drawing.Size(105, 24);
            this.tàiKhoảnToolStripMenuItem.Text = "Tài khoản";
            // 
            // trangCáNhânToolStripMenuItem
            // 
            this.trangCáNhânToolStripMenuItem.Image = global::GUI.Properties.Resources.profile;
            this.trangCáNhânToolStripMenuItem.Name = "trangCáNhânToolStripMenuItem";
            this.trangCáNhânToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.trangCáNhânToolStripMenuItem.Text = "Trang cá nhân";
            this.trangCáNhânToolStripMenuItem.Click += new System.EventHandler(this.trangCáNhânToolStripMenuItem_Click);
            // 
            // đổiMậtKhẩuToolStripMenuItem
            // 
            this.đổiMậtKhẩuToolStripMenuItem.Image = global::GUI.Properties.Resources.shield;
            this.đổiMậtKhẩuToolStripMenuItem.Name = "đổiMậtKhẩuToolStripMenuItem";
            this.đổiMậtKhẩuToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.đổiMậtKhẩuToolStripMenuItem.Text = "Đổi mật khẩu";
            this.đổiMậtKhẩuToolStripMenuItem.Click += new System.EventHandler(this.đổiMậtKhẩuToolStripMenuItem_Click);
            // 
            // đăngXuấtToolStripMenuItem1
            // 
            this.đăngXuấtToolStripMenuItem1.Image = global::GUI.Properties.Resources.check_out;
            this.đăngXuấtToolStripMenuItem1.Name = "đăngXuấtToolStripMenuItem1";
            this.đăngXuấtToolStripMenuItem1.Size = new System.Drawing.Size(184, 26);
            this.đăngXuấtToolStripMenuItem1.Text = "Đăng xuất";
            this.đăngXuấtToolStripMenuItem1.Click += new System.EventHandler(this.đăngXuấtToolStripMenuItem1_Click);
            // 
            // kryptonPalette1
            // 
            this.kryptonPalette1.BasePaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.kryptonPalette1.ButtonSpecs.FormClose.Image = global::GUI.Properties.Resources.round;
            this.kryptonPalette1.ButtonSpecs.FormClose.Orientation = ComponentFactory.Krypton.Toolkit.PaletteButtonOrientation.Inherit;
            this.kryptonPalette1.ButtonSpecs.FormClose.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.kryptonPalette1.ButtonSpecs.FormMax.Image = global::GUI.Properties.Resources.circle;
            this.kryptonPalette1.ButtonSpecs.FormMax.Orientation = ComponentFactory.Krypton.Toolkit.PaletteButtonOrientation.Inherit;
            this.kryptonPalette1.ButtonSpecs.FormMax.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.kryptonPalette1.ButtonSpecs.FormMin.Image = global::GUI.Properties.Resources.round__1_;
            this.kryptonPalette1.ButtonSpecs.FormMin.Orientation = ComponentFactory.Krypton.Toolkit.PaletteButtonOrientation.Inherit;
            this.kryptonPalette1.ButtonSpecs.FormMin.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.kryptonPalette1.ButtonSpecs.FormRestore.Image = global::GUI.Properties.Resources.circle;
            this.kryptonPalette1.ButtonSpecs.FormRestore.Orientation = ComponentFactory.Krypton.Toolkit.PaletteButtonOrientation.Inherit;
            this.kryptonPalette1.ButtonSpecs.FormRestore.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color2 = System.Drawing.Color.White;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Rounding = 16;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color2 = System.Drawing.Color.White;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kryptonPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.kryptonPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1582, 825);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(3, 3);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1576, 777);
            this.kryptonPanel1.StateCommon.Color1 = System.Drawing.Color.White;
            this.kryptonPanel1.StateCommon.Color2 = System.Drawing.Color.White;
            this.kryptonPanel1.StateCommon.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(3, 786);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(1576, 36);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(215, 9);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(109, 24);
            this.kryptonLabel3.TabIndex = 2;
            this.kryptonLabel3.Values.Text = "kryptonLabel3";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(1422, 9);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(109, 24);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "kryptonLabel2";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(9, 9);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(109, 24);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "kryptonLabel1";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phần mềm quản lý đặt sân thể thao";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem quảnLíSânToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bóngĐáToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cầuLôngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tennisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bóngBànToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tàiKhoảnToolStripMenuItem;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Windows.Forms.Timer timer1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private System.Windows.Forms.ToolStripMenuItem quảnLýToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mãGiảmGiáToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nhânViênToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem báoCáoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nướcNgọtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trangCáNhânToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đổiMậtKhẩuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đăngXuấtToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nướcNgọtToolStripMenuItem1;
    }
}
namespace MyControls
{
    partial class ChangePasswordControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonTextBox1 = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonTextBox2 = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonTextBox3 = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(125, 118);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(96, 24);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Mật khẩu cũ";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(125, 178);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(106, 24);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "Mật khẩu mới";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(125, 238);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(142, 24);
            this.kryptonLabel3.TabIndex = 1;
            this.kryptonLabel3.Values.Text = "Xác nhận mật khẩu";
            // 
            // kryptonTextBox1
            // 
            this.kryptonTextBox1.Location = new System.Drawing.Point(291, 118);
            this.kryptonTextBox1.Name = "kryptonTextBox1";
            this.kryptonTextBox1.PasswordChar = '●';
            this.kryptonTextBox1.Size = new System.Drawing.Size(185, 27);
            this.kryptonTextBox1.TabIndex = 1;
            this.kryptonTextBox1.TextChanged += new System.EventHandler(this.kryptonTextBox1_TextChanged);
            // 
            // kryptonTextBox2
            // 
            this.kryptonTextBox2.Location = new System.Drawing.Point(291, 175);
            this.kryptonTextBox2.Name = "kryptonTextBox2";
            this.kryptonTextBox2.PasswordChar = '●';
            this.kryptonTextBox2.Size = new System.Drawing.Size(185, 27);
            this.kryptonTextBox2.TabIndex = 2;
            this.kryptonTextBox2.TextChanged += new System.EventHandler(this.kryptonTextBox2_TextChanged);
            // 
            // kryptonTextBox3
            // 
            this.kryptonTextBox3.Location = new System.Drawing.Point(291, 235);
            this.kryptonTextBox3.Name = "kryptonTextBox3";
            this.kryptonTextBox3.PasswordChar = '●';
            this.kryptonTextBox3.Size = new System.Drawing.Size(185, 27);
            this.kryptonTextBox3.TabIndex = 3;
            this.kryptonTextBox3.TextChanged += new System.EventHandler(this.kryptonTextBox3_TextChanged);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(211, 55);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(193, 40);
            this.kryptonLabel4.StateCommon.ShortText.Font = new System.Drawing.Font("Quicksand SemiBold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel4.StateCommon.ShortText.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.kryptonLabel4.StateCommon.ShortText.Trim = ComponentFactory.Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.kryptonLabel4.TabIndex = 3;
            this.kryptonLabel4.Values.Text = "Đổi mật khẩu";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(288, 292);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(188, 50);
            this.kryptonButton1.TabIndex = 4;
            this.kryptonButton1.Values.Text = "Đổi mật khẩu";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // ChangePasswordControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.kryptonLabel4);
            this.Controls.Add(this.kryptonTextBox3);
            this.Controls.Add(this.kryptonTextBox2);
            this.Controls.Add(this.kryptonTextBox1);
            this.Controls.Add(this.kryptonLabel3);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.kryptonLabel1);
            this.Name = "ChangePasswordControl";
            this.Size = new System.Drawing.Size(590, 372);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox kryptonTextBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox kryptonTextBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox kryptonTextBox3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
    }
}

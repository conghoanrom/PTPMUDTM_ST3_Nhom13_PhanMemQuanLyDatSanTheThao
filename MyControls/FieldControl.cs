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
    public partial class FieldControl : UserControl
    {
        public Field Field { get; set; }
        public FieldControl(Field field)
        {
            InitializeComponent();
            this.Field = field;
        }

        private void FieldControl_Load(object sender, EventArgs e)
        {

            kryptonLabel1.Text = Field.FieldName;
            kryptonLabel2.Text = "Vị trí: " + Field.Location;
            setFieldImage();
        }
        private void setFieldImage()
        {
            if (this.Field.FieldType.TypeId == 1 || this.Field.FieldType.TypeId == 2)
            {
                pictureBox1.Image = global::MyControls.Properties.Resources.pitch;
            }else if (this.Field.FieldType.TypeId == 3)
            {
                pictureBox1.Image = global::MyControls.Properties.Resources.field;
            }
            else if (this.Field.FieldType.TypeId == 4)
            {
                pictureBox1.Image = global::MyControls.Properties.Resources.tennis_court;
            }
            else
            {
                pictureBox1.Image = global::MyControls.Properties.Resources.ping_pong;
            }
        }
    }
}

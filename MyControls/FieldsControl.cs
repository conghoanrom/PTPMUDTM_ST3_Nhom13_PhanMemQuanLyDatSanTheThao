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
    public partial class FieldsControl : UserControl
    {
        public List<Field> fields { get; set; }
        public FieldsControl(List<Field> fields)
        {
            InitializeComponent();
            this.fields = fields;
        }

        private void FieldsControl_Load(object sender, EventArgs e)
        {
            int y = 0;
            foreach (var item in fields)
            {
                FieldControl field = new FieldControl(item);
                field.Field = item;
                field.Location = new Point(0, y);
                kryptonPanel1.Controls.Add(field);
                y += field.Height + 5;
            }
        }
    }
}

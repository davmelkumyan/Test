using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.success
{
    public partial class succ : Form
    {
        public succ()
        {
            InitializeComponent();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(255, 116, 0);
            button1.FlatAppearance.BorderSize = 0;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(41, 44, 51);
            button1.FlatAppearance.BorderSize = 2;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}

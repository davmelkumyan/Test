using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.success;

namespace WindowsFormsApp1
{
    public partial class LoadBar : Form
    {
        private byte loadValue = 0;
        public LoadBar()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Checker();
            if (loadValue == 100)
            Close();
        }

        private void Checker()
        {
            if (loadValue != 100)
            {
                loadValue++;
                progressBar1.Value = loadValue;
                label2.Text = loadValue.ToString() + "%";
            }
        }
    }
}

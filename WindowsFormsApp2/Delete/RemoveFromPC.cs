using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WindowsFormsApp1.success;
using System.Diagnostics;
using System.Media;
using System.Security.AccessControl;
using System.Security.Principal;

namespace WindowsFormsApp1.Delete
{
    public partial class RemoveFromPC : Form
    {
        string filePath;
        public RemoveFromPC()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                filePath = textBox1.Text;
                if (File.Exists(filePath))
                {
                    MessageBox.Show("File Choosed!");
                }
                else
                {
                    MessageBox.Show("File Not Exist!");
                }
            }
            else
            {
                filePath = textBox1.Text;
                if (Directory.Exists(filePath))
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Directory Choosed!");
                }
                else
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Directory Not Exist!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new OptimTool().Show();
            Close();
        }


        private static void AddFileSecurity(string fileName, string account,
         FileSystemRights rights, AccessControlType controlType)
        {
            // Get a FileSecurity object that represents the
            // current security settings.
            FileSecurity fSecurity = File.GetAccessControl(fileName);
            // Add the FileSystemAccessRule to the security settings.
            fSecurity.AddAccessRule(new FileSystemAccessRule(account,
                rights, controlType));
            // Set the new access settings.
            File.SetAccessControl(fileName, fSecurity);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filePath) && !checkBox1.Checked)
            {
                Hide();
                new LoadBar().ShowDialog();
                string processName = textBox1.Text.Substring(textBox1.Text.LastIndexOf(@"\") + 1);
                processName = processName.Remove(processName.LastIndexOf('.'));
                try
                {
                    foreach (Process Proc in Process.GetProcesses())
                    {
                        if ((Proc.ProcessName.Equals(processName)))
                        {
                            Proc.Kill();
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"No needed to close Process\r\n{ex}");
                }
                
                try
                {
                    WindowsIdentity wi = WindowsIdentity.GetCurrent();
                    string usr = wi.Name;
                    AddFileSecurity(filePath, usr, FileSystemRights.FullControl, AccessControlType.Allow);
                    File.SetAttributes(filePath, FileAttributes.Normal);
                    File.Delete(filePath);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                new LoadBar().Close();
                SystemSounds.Beep.Play();
                new succ().ShowDialog();
                new succ().Close();
                Show();
            }
            else if(!string.IsNullOrEmpty(filePath) && checkBox1.Checked)
            {
                Hide();
                new LoadBar().ShowDialog();
                try
                {
                    Directory.Delete(filePath, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                new LoadBar().Close();
                SystemSounds.Beep.Play();
                new succ().ShowDialog();
                new succ().Close();
                Show();
            }
            else
            {
                MessageBox.Show("Enter Path!");
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(255, 116, 0);
            button1.FlatAppearance.BorderSize = 0;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(41, 44, 51);
            button1.FlatAppearance.BorderSize = 2;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(255, 116, 0);
            button2.FlatAppearance.BorderSize = 0;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(41, 44, 51);
            button2.FlatAppearance.BorderSize = 2;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(255, 116, 0);
            button3.FlatAppearance.BorderSize = 0;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(41, 44, 51);
            button3.FlatAppearance.BorderSize = 2;
        }
    }
}

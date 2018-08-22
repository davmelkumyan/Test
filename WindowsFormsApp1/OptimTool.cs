using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CreateProcList;
using WindowsFormsApp1.Delete;
using WindowsFormsApp1.success;

namespace WindowsFormsApp1
{
    public partial class OptimTool : Form
    {
        NotifyIcon ni = new NotifyIcon();
        ProcList processList = new ProcList();
        LoadBar Lb = new LoadBar();

        //List<string> processListForAdd = new List<string>();
        int countProcess = 0, countFiles = 0, countFolders = 0;

        public OptimTool()
        {
            InitializeComponent();
        }

        private void informationToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Hide();
            Aboutus AUS = new Aboutus();
            AUS.ShowDialog();
        }

        //get Access to file and folder
        

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            var Lb = new LoadBar();
            Lb.ShowDialog();
            string deletePath1 = Path.GetTempPath();
            //string deletePath2 = @"C:\Windows\Prefetch";

            //Temp Delete
            var di = new DirectoryInfo(deletePath1);
            //var di2 = new DirectoryInfo(deletePath2);

            foreach (var file in di.EnumerateFileSystemInfos())
            {
                try
                {
                    file.Delete();
                    if (file.Attributes == FileAttributes.Directory)
                        countFolders++;
                    else
                        countFiles++;
                }
                catch
                {
                    textBox2.Text += "\r\nTemp File or Folder is Using...";
                }
            }

            //Prefetch Delete
            //bool a = false;
            //try
            //{
            //    Directory.GetAccessControl(deletePath2);
            //    DirectorySecurity ds = new DirectorySecurity(deletePath2, AccessControlSections.Access);
            //    Directory.SetAccessControl(deletePath2, ds);
            //    WindowsIdentity wi = WindowsIdentity.GetCurrent();
            //    string user = wi.Name;
            //    AddFileSecurity(deletePath2, @user,
            //            FileSystemRights.FullControl, AccessControlType.Allow);
            //    a = true;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //if (a)
            //{
            //    foreach (var file in Directory.GetFiles(deletePath2))
            //    {
            //        try
            //        {
            //            File.SetAttributes(file, FileAttributes.Normal);
            //            File.Delete(file);
            //            countFiles++;
            //        }
            //        catch
            //        {
            //            textBox2.Text += "\r\nPrefetch File is Using...";
            //        }
            //    }
            //}
            Process.Start(@"C:\Users\davom\Desktop\Building project (OPTIMTOOL)\WindowsFormApp1\WindowsFormsApp1\bin\bats\PrefetchDelete.bat");


            Lb.Close();
            SystemSounds.Beep.Play();
            new succ().ShowDialog();
            Show();
            textBox2.Text += $"\r\nFile Deleted: {countFiles} \r\nFolder Deleted: {countFolders}";
            countFiles = 0;
            countFolders = 0;
        }

        //High Performance
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            var Lb = new LoadBar();
            Lb.ShowDialog();
            if (checkBox1.Checked)
            {
                foreach (var Proc in Process.GetProcesses())
                {
                    if (processList.PList.Count > 0)
                    {
                        for (int i = 0; i < processList.PList.Count; i++)
                        {
                            if (Proc.ProcessName.Equals(processList.PList[i]))
                            {
                                try
                                {
                                    Proc.Kill();
                                    countProcess++;
                                }
                                catch
                                {
                                    textBox2.Text += "\r\nAccess denied!";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Process Proc in Process.GetProcesses())
                {
                    bool isDefault = false;
                    foreach (var proc1 in processList.defProcList)
                    {
                        if (Proc.ProcessName.Equals(proc1))
                        {
                            isDefault = true;
                        }
                    }
                    if (!isDefault)
                    {
                        try
                        {
                            Proc.Kill();
                            countProcess++;
                        }
                        catch
                        {
                            textBox2.Text += "\r\nAccess denied!";
                        }
                    }

                }
            }
            Lb.Close();
            SystemSounds.Beep.Play();
            new succ().ShowDialog();
            Show();
            textBox2.Text += $"\r\nProcess Killed : {countProcess}";
        }

        private void OptimTool_Load(object sender, EventArgs e)
        {
            textBox2.Text = "Console out put";
            notifyIcon1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || textBox1.Text.Equals("Process (.exe)"))
            {
                textBox2.Text += "\r\nType anything to Confirm...";
            }
            else
            {
                processList.PList.Add(textBox1.Text);
                SystemSounds.Beep.Play();
                textBox2.Text += "\r\nProcess Saved successfully!\r\nPress High Performance!";
            }
        }

        private void backgroundModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            notifyIcon1.Visible = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            ni.Visible = false;
        }

        private void fontModeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Show();
            ni.Visible = false;
        }

        private void informationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            informationToolStripMenuItem_Click_1(sender, e);
        }

        private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            var rf = new RemoveFromPC();
            rf.ShowDialog();
        }

        private void closeToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Process (.exe)")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Process (.exe)";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            panel4.Height = button1.Height;
            panel4.Top = button1.Top;
            panel4.BackColor = Color.FromArgb(255, 116, 0);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            panel4.BackColor = Color.FromArgb(41, 44, 51);
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            panel4.Height = button2.Height;
            panel4.Top = button2.Top;
            panel4.BackColor = Color.FromArgb(255, 116, 0);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            panel4.Height = button1.Height;
            panel4.Top = button1.Top;
            panel4.BackColor = Color.FromArgb(41, 44, 51);
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            panel4.Height = button4.Height;
            panel4.Top = button4.Top;
            panel4.BackColor = Color.FromArgb(255, 116, 0);
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            panel4.Height = button1.Height;
            panel4.Top = button1.Top;
            panel4.BackColor = Color.FromArgb(41, 44, 51);
        }

        private void close_butt_MouseEnter(object sender, EventArgs e)
        {
            panel4.Height = close_butt.Height;
            panel4.Top = close_butt.Top;
            panel4.BackColor = Color.FromArgb(255, 116, 0);
        }

        private void close_butt_MouseLeave(object sender, EventArgs e)
        {
            panel4.Height = button1.Height;
            panel4.Top = button1.Top;
            panel4.BackColor = Color.FromArgb(41, 44, 51);
        }

        private void close_butt_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button8_MouseEnter(object sender, EventArgs e)
        {
            panel5.Width = button8.Width;
            panel5.Top = button8.Bottom;
            panel5.BackColor = Color.FromArgb(255, 116, 0);
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            panel5.BackColor = Color.FromArgb(41, 44, 51);
        }

        private void button7_MouseEnter(object sender, EventArgs e)
        {
            panel7.BackColor = Color.FromArgb(255, 116, 0);
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            panel7.BackColor = Color.FromArgb(41, 44, 51);
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            panel8.BackColor = Color.FromArgb(255, 116, 0);
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            panel8.BackColor = Color.FromArgb(41, 44, 51);
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            panel9.BackColor = Color.FromArgb(255, 116, 0);
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            panel9.BackColor = Color.FromArgb(41, 44, 51);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            informationToolStripMenuItem_Click_1(sender, e);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Process.Start("http://google.com");
        }

        private bool mouseDown;
        private Point lastLocation;

        private void panel6_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void panel6_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void panel6_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Users\davom\Desktop\Building project (OPTIMTOOL)\WindowsFormApp1\WindowsFormsApp1\ReadMe.txt");
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

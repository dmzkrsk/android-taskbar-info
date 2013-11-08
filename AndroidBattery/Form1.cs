using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace AndroidBattery
{
    public partial class Form1 : Form
    {
        private readonly Timer timer;
        private int level = 0;
        private readonly ProcessStartInfo startInfo;
        private readonly TaskbarManager shellProgressBar;
        private bool TaskBarAvailable = false;

#if DEBUG
        private const int TIMER_INTERVAL = 5000;
#else
        private const int TIMER_INTERVAL = 1000 * 60 * 2;
#endif

        public Form1()
        {
            startInfo = new ProcessStartInfo("adb", "shell cat /sys/class/power_supply/battery/capacity")
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
            };

            InitializeComponent();

            shellProgressBar = TaskbarManager.Instance;
            shellProgressBar.ApplicationId = Application.ProductName;

            timer = new Timer();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            string output, errors;

            Process proc = null;

            // @todo ADB socket

            try
            {
                proc = Process.Start(startInfo);

                errors = proc.StandardError.ReadToEnd();
                if (!String.IsNullOrEmpty(errors)) errors = errors.Trim();
                
                output = proc.StandardOutput.ReadToEnd();
                if (!String.IsNullOrEmpty(output)) output = output.Trim();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return;
            }
            finally
            {
                if (proc != null) proc.Dispose();
            }

            if (Int32.TryParse(output, out level))
            {
                formProgressBar.Style = ProgressBarStyle.Blocks;
                formProgressBar.Value = level;

                Text = String.Format("[ {0}% ]", level);

                UpdateTaskbar(true);
            }
            else
            {
                formProgressBar.Style = ProgressBarStyle.Marquee;
                Text = errors;

                UpdateTaskbar(false);
            }

        }
        private void UpdateTaskbar(bool HasValue) {

            if (!TaskbarManager.IsPlatformSupported) return;
            if (!TaskBarAvailable) return;

            if (HasValue)
                shellProgressBar.SetProgressValue(level, 100);

            if (!HasValue)
                shellProgressBar.SetProgressState(TaskbarProgressBarState.Indeterminate);
            else if (level < 15)
                shellProgressBar.SetProgressState(TaskbarProgressBarState.Error);
            else if (level < 90)
                shellProgressBar.SetProgressState(TaskbarProgressBarState.Paused);
            else
                shellProgressBar.SetProgressState(TaskbarProgressBarState.Normal);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            Visible = false;

            formProgressBar.Style = ProgressBarStyle.Marquee;
            formProgressBar.Maximum = 100;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            shellProgressBar.SetProgressState(TaskbarProgressBarState.Indeterminate);
            shellProgressBar.SetApplicationIdForSpecificWindow(Handle, "MainWnd");

            TaskBarAvailable = true;

            timer_Tick(null, null);

            timer.Interval = TIMER_INTERVAL;
            timer.Tick += timer_Tick;
            timer.Enabled = true;
        }
    }

    internal class ValueProgressBar : ProgressBar
    {
        /*
        public ValueProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = e.ClipRectangle;
            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;

            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            rec.Height = rec.Height - 4;
            e.Graphics.FillRectangle(Brushes.Red, 2, 2, rec.Width, rec.Height);
        }
         * */
    }
}

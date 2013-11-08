using System.Windows.Forms;

namespace AndroidBattery
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.formProgressBar = new ProgressBar();
            this.SuspendLayout();
            // 
            // formProgressBar
            // 
            this.formProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formProgressBar.Location = new System.Drawing.Point(0, 0);
            this.formProgressBar.Name = "formProgressBar";
            this.formProgressBar.Size = new System.Drawing.Size(384, 61);
            this.formProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.formProgressBar.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 61);
            this.Controls.Add(this.formProgressBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 100);
            this.MinimumSize = new System.Drawing.Size(400, 100);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Android Battery Level";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private ProgressBar formProgressBar;

    }
}


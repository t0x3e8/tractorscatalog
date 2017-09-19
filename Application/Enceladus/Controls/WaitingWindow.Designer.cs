namespace Enceladus
{
    partial class WaitingWindow
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
            this.controlLabel1 = new Enceladus.UIToolbox.ControlLabel();
            this.waitingBar1 = new Enceladus.UIToolbox.WaitingBar();
            this.SuspendLayout();
            // 
            // controlLabel1
            // 
            this.controlLabel1.AutoSize = true;
            this.controlLabel1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.controlLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.controlLabel1.Location = new System.Drawing.Point(57, 11);
            this.controlLabel1.Name = "controlLabel1";
            this.controlLabel1.Size = new System.Drawing.Size(89, 18);
            this.controlLabel1.TabIndex = 0;
            this.controlLabel1.Text = "Please wait ...";
            // 
            // waitingBar1
            // 
            this.waitingBar1.BackColor = System.Drawing.Color.Transparent;
            this.waitingBar1.DelayInSeconds = 70;
            this.waitingBar1.DisplayStickNumber = 10;
            this.waitingBar1.EndStick = 8;
            this.waitingBar1.FirstColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(175)))), ((int)(((byte)(161)))));
            this.waitingBar1.LineStyle = System.Drawing.Drawing2D.LineCap.Round;
            this.waitingBar1.Location = new System.Drawing.Point(10, 9);
            this.waitingBar1.Name = "waitingBar1";
            this.waitingBar1.PenStyle = Enceladus.UIToolbox.PenStyles.Gradient;
            this.waitingBar1.Run = false;
            this.waitingBar1.SecondColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.waitingBar1.Size = new System.Drawing.Size(23, 22);
            this.waitingBar1.Smoothing = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.waitingBar1.StartStick = 6;
            this.waitingBar1.StickNumber = 10;
            this.waitingBar1.StickThickness = 2;
            this.waitingBar1.TabIndex = 28;
            this.waitingBar1.Text = "waitingBar1";
            // 
            // WaitingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(175)))), ((int)(((byte)(168)))));
            this.BackgroundImage = global::Enceladus.Properties.Resources.WaitingWindowBackground;
            this.ClientSize = new System.Drawing.Size(200, 40);
            this.Controls.Add(this.waitingBar1);
            this.Controls.Add(this.controlLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WaitingWindow";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "WaitingWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UIToolbox.ControlLabel controlLabel1;
        protected internal UIToolbox.WaitingBar waitingBar1;
    }
}
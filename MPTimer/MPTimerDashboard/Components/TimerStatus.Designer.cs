namespace MPTimerDashboard.Components
{
    partial class TimerStatus
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
            this.runningTimeLabel = new System.Windows.Forms.Label();
            this.currentTimeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // runningTimeLabel
            // 
            this.runningTimeLabel.AutoSize = true;
            this.runningTimeLabel.Location = new System.Drawing.Point(3, 15);
            this.runningTimeLabel.Name = "runningTimeLabel";
            this.runningTimeLabel.Size = new System.Drawing.Size(82, 15);
            this.runningTimeLabel.TabIndex = 0;
            this.runningTimeLabel.Text = "Running time:";
            // 
            // currentTimeLabel
            // 
            this.currentTimeLabel.AutoSize = true;
            this.currentTimeLabel.Location = new System.Drawing.Point(3, 0);
            this.currentTimeLabel.Name = "currentTimeLabel";
            this.currentTimeLabel.Size = new System.Drawing.Size(77, 15);
            this.currentTimeLabel.TabIndex = 1;
            this.currentTimeLabel.Text = "Current time:";
            // 
            // TimerStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.currentTimeLabel);
            this.Controls.Add(this.runningTimeLabel);
            this.Name = "TimerStatus";
            this.Size = new System.Drawing.Size(237, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label runningTimeLabel;
        private Label currentTimeLabel;
    }
}

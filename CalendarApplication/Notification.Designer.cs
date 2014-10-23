namespace Calendar
{
    partial class Notification
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
            this.apt_Label = new System.Windows.Forms.Label();
            this.snooze_Button = new System.Windows.Forms.Button();
            this.dismiss_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // apt_Label
            // 
            this.apt_Label.AutoSize = true;
            this.apt_Label.Location = new System.Drawing.Point(17, 16);
            this.apt_Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.apt_Label.Name = "apt_Label";
            this.apt_Label.Size = new System.Drawing.Size(0, 17);
            this.apt_Label.TabIndex = 0;
            // 
            // snooze_Button
            // 
            this.snooze_Button.Location = new System.Drawing.Point(409, 108);
            this.snooze_Button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.snooze_Button.Name = "snooze_Button";
            this.snooze_Button.Size = new System.Drawing.Size(100, 28);
            this.snooze_Button.TabIndex = 1;
            this.snooze_Button.Text = "Snooze";
            this.snooze_Button.UseVisualStyleBackColor = true;
            this.snooze_Button.Click += new System.EventHandler(this.snooze_Button_Click);
            // 
            // dismiss_Button
            // 
            this.dismiss_Button.Location = new System.Drawing.Point(529, 108);
            this.dismiss_Button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dismiss_Button.Name = "dismiss_Button";
            this.dismiss_Button.Size = new System.Drawing.Size(100, 28);
            this.dismiss_Button.TabIndex = 2;
            this.dismiss_Button.Text = "Dismiss";
            this.dismiss_Button.UseVisualStyleBackColor = true;
            this.dismiss_Button.Click += new System.EventHandler(this.dismiss_Button_Click);
            // 
            // Notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 145);
            this.Controls.Add(this.dismiss_Button);
            this.Controls.Add(this.snooze_Button);
            this.Controls.Add(this.apt_Label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Notification";
            this.Text = "Notification of Appointment";
            this.Load += new System.EventHandler(this.Notification_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label apt_Label;
        private System.Windows.Forms.Button snooze_Button;
        private System.Windows.Forms.Button dismiss_Button;
    }
}
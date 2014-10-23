namespace Calendar
{
    partial class RecurringForm
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
            this.reccur_Label = new System.Windows.Forms.Label();
            this.amountRecurring_Label = new System.Windows.Forms.Label();
            this.freq_ComboBox = new System.Windows.Forms.ComboBox();
            this.amount_TextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // type_ComboBox
            // 
            this.type_ComboBox.DataSource = new string[] {
        "Other",
        "Meeting",
        "Interview",
        "HR"};
            this.type_ComboBox.SelectedIndexChanged += new System.EventHandler(this.type_ComboBox_SelectedIndexChanged);
            // 
            // reccur_Label
            // 
            this.reccur_Label.AutoSize = true;
            this.reccur_Label.Location = new System.Drawing.Point(12, 141);
            this.reccur_Label.Name = "reccur_Label";
            this.reccur_Label.Size = new System.Drawing.Size(61, 13);
            this.reccur_Label.TabIndex = 17;
            this.reccur_Label.Text = "Frequency*";
            // 
            // amountRecurring_Label
            // 
            this.amountRecurring_Label.AutoSize = true;
            this.amountRecurring_Label.Location = new System.Drawing.Point(205, 141);
            this.amountRecurring_Label.Name = "amountRecurring_Label";
            this.amountRecurring_Label.Size = new System.Drawing.Size(47, 13);
            this.amountRecurring_Label.TabIndex = 18;
            this.amountRecurring_Label.Text = "Amount*";
            // 
            // freq_ComboBox
            // 
            this.freq_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.freq_ComboBox.FormattingEnabled = true;
            this.freq_ComboBox.Location = new System.Drawing.Point(79, 138);
            this.freq_ComboBox.Name = "freq_ComboBox";
            this.freq_ComboBox.Size = new System.Drawing.Size(120, 21);
            this.freq_ComboBox.TabIndex = 19;
            // 
            // amount_TextBox
            // 
            this.amount_TextBox.Location = new System.Drawing.Point(263, 138);
            this.amount_TextBox.Name = "amount_TextBox";
            this.amount_TextBox.Size = new System.Drawing.Size(100, 20);
            this.amount_TextBox.TabIndex = 20;
            this.amount_TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.amount_TextBox_KeyPress);
            // 
            // RecurringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 232);
            this.Controls.Add(this.amount_TextBox);
            this.Controls.Add(this.freq_ComboBox);
            this.Controls.Add(this.amountRecurring_Label);
            this.Controls.Add(this.reccur_Label);
            this.Name = "RecurringForm";
            this.Text = "Add/Edit Recurring Appointment";
            this.Load += new System.EventHandler(this.RecurringForm_Load);
            this.Controls.SetChildIndex(this.starttime_ComboBox, 0);
            this.Controls.SetChildIndex(this.save_Button, 0);
            this.Controls.SetChildIndex(this.subj_Label, 0);
            this.Controls.SetChildIndex(this.type_ComboBox, 0);
            this.Controls.SetChildIndex(this.reccur_Label, 0);
            this.Controls.SetChildIndex(this.amountRecurring_Label, 0);
            this.Controls.SetChildIndex(this.freq_ComboBox, 0);
            this.Controls.SetChildIndex(this.amount_TextBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label reccur_Label;
        private System.Windows.Forms.Label amountRecurring_Label;
        private System.Windows.Forms.ComboBox freq_ComboBox;
        private System.Windows.Forms.TextBox amount_TextBox;
    }
}
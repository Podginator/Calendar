namespace Calendar
{
    partial class AppointmentForm
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
            this.components = new System.ComponentModel.Container();
            this.subj_Label = new System.Windows.Forms.Label();
            this.variable_Label = new System.Windows.Forms.Label();
            this.date_Label = new System.Windows.Forms.Label();
            this.start_Label = new System.Windows.Forms.Label();
            this.length_Label = new System.Windows.Forms.Label();
            this.subj_TextBox = new System.Windows.Forms.TextBox();
            this.variable_TextBox = new System.Windows.Forms.TextBox();
            this.DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.names_Tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.starttime_ComboBox = new System.Windows.Forms.ComboBox();
            this.length_ComboBox = new System.Windows.Forms.ComboBox();
            this.save_Button = new System.Windows.Forms.Button();
            this.close_Button = new System.Windows.Forms.Button();
            this.type_Label = new System.Windows.Forms.Label();
            this.type_ComboBox = new System.Windows.Forms.ComboBox();
            this.Explan_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // subj_Label
            // 
            this.subj_Label.AutoSize = true;
            this.subj_Label.Location = new System.Drawing.Point(183, 34);
            this.subj_Label.Name = "subj_Label";
            this.subj_Label.Size = new System.Drawing.Size(47, 13);
            this.subj_Label.TabIndex = 0;
            this.subj_Label.Text = "Subject*";
            // 
            // variable_Label
            // 
            this.variable_Label.AutoSize = true;
            this.variable_Label.Location = new System.Drawing.Point(12, 68);
            this.variable_Label.Name = "variable_Label";
            this.variable_Label.Size = new System.Drawing.Size(52, 13);
            this.variable_Label.TabIndex = 1;
            this.variable_Label.Text = "Location*";
            // 
            // date_Label
            // 
            this.date_Label.AutoSize = true;
            this.date_Label.Location = new System.Drawing.Point(12, 103);
            this.date_Label.Name = "date_Label";
            this.date_Label.Size = new System.Drawing.Size(34, 13);
            this.date_Label.TabIndex = 3;
            this.date_Label.Text = "Date*";
            // 
            // start_Label
            // 
            this.start_Label.AutoSize = true;
            this.start_Label.Location = new System.Drawing.Point(205, 103);
            this.start_Label.Name = "start_Label";
            this.start_Label.Size = new System.Drawing.Size(56, 13);
            this.start_Label.TabIndex = 4;
            this.start_Label.Text = "StartTime*";
            // 
            // length_Label
            // 
            this.length_Label.AutoSize = true;
            this.length_Label.Location = new System.Drawing.Point(370, 103);
            this.length_Label.Name = "length_Label";
            this.length_Label.Size = new System.Drawing.Size(44, 13);
            this.length_Label.TabIndex = 5;
            this.length_Label.Text = "Length*";
            // 
            // subj_TextBox
            // 
            this.subj_TextBox.Location = new System.Drawing.Point(232, 31);
            this.subj_TextBox.Name = "subj_TextBox";
            this.subj_TextBox.Size = new System.Drawing.Size(269, 20);
            this.subj_TextBox.TabIndex = 7;
            // 
            // variable_TextBox
            // 
            this.variable_TextBox.Location = new System.Drawing.Point(79, 65);
            this.variable_TextBox.Name = "variable_TextBox";
            this.variable_TextBox.Size = new System.Drawing.Size(422, 20);
            this.variable_TextBox.TabIndex = 8;
            // 
            // DateTimePicker
            // 
            this.DateTimePicker.Location = new System.Drawing.Point(79, 102);
            this.DateTimePicker.Name = "DateTimePicker";
            this.DateTimePicker.Size = new System.Drawing.Size(120, 20);
            this.DateTimePicker.TabIndex = 10;
            this.DateTimePicker.ValueChanged += new System.EventHandler(this.DateTimePicker_ValueChanged);
            // 
            // starttime_ComboBox
            // 
            this.starttime_ComboBox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.starttime_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.starttime_ComboBox.FormattingEnabled = true;
            this.starttime_ComboBox.Location = new System.Drawing.Point(263, 100);
            this.starttime_ComboBox.Name = "starttime_ComboBox";
            this.starttime_ComboBox.Size = new System.Drawing.Size(100, 21);
            this.starttime_ComboBox.TabIndex = 11;
            this.starttime_ComboBox.SelectedIndexChanged += new System.EventHandler(this.starttime_ComboBox_SelectedIndexChanged);
            // 
            // length_ComboBox
            // 
            this.length_ComboBox.FormattingEnabled = true;
            this.length_ComboBox.Items.AddRange(new object[] {
            "30",
            "60",
            "90",
            "120",
            "150",
            "180",
            "210",
            "240",
            "300"});
            this.length_ComboBox.Location = new System.Drawing.Point(416, 100);
            this.length_ComboBox.Name = "length_ComboBox";
            this.length_ComboBox.Size = new System.Drawing.Size(85, 21);
            this.length_ComboBox.TabIndex = 12;
            this.length_ComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.length_ComboBox_KeyPress);
            // 
            // save_Button
            // 
            this.save_Button.Location = new System.Drawing.Point(345, 197);
            this.save_Button.Name = "save_Button";
            this.save_Button.Size = new System.Drawing.Size(75, 23);
            this.save_Button.TabIndex = 13;
            this.save_Button.Text = "Save";
            this.save_Button.UseVisualStyleBackColor = true;
            this.save_Button.Click += new System.EventHandler(this.save_Button_Click);
            // 
            // close_Button
            // 
            this.close_Button.Location = new System.Drawing.Point(426, 197);
            this.close_Button.Name = "close_Button";
            this.close_Button.Size = new System.Drawing.Size(75, 23);
            this.close_Button.TabIndex = 14;
            this.close_Button.Text = "Close";
            this.close_Button.UseVisualStyleBackColor = true;
            this.close_Button.Click += new System.EventHandler(this.close_Button_Click);
            // 
            // type_Label
            // 
            this.type_Label.AutoSize = true;
            this.type_Label.Location = new System.Drawing.Point(12, 34);
            this.type_Label.Name = "type_Label";
            this.type_Label.Size = new System.Drawing.Size(35, 13);
            this.type_Label.TabIndex = 15;
            this.type_Label.Text = "Type*";
            // 
            // type_ComboBox
            // 
            this.type_ComboBox.FormattingEnabled = true;
            this.type_ComboBox.Items.AddRange(new object[] {
            "00:00",
            "00:30",
            "01:00",
            "01:30",
            "02:00",
            "02:30",
            "03:00",
            "03:30",
            "04:00",
            "05:30",
            "06:00",
            "06:30",
            "07:00",
            "07:30",
            "08:00",
            "08:30",
            "09:00",
            "09:30",
            "10:00",
            "10:30",
            "11:00",
            "11:30",
            "12:00",
            "12:30",
            "13:00",
            "13:30",
            "14:00",
            "14:30",
            "15:00",
            "15:30",
            "16:00",
            "16:30",
            "17:00",
            "17:30",
            "18:00",
            "18:30",
            "19:00",
            "19:30",
            "20:00",
            "20:30",
            "21:00",
            "21:30",
            "22:00",
            "22:30",
            "23:00",
            "23:30"});
            this.type_ComboBox.Location = new System.Drawing.Point(79, 30);
            this.type_ComboBox.Name = "type_ComboBox";
            this.type_ComboBox.Size = new System.Drawing.Size(98, 21);
            this.type_ComboBox.TabIndex = 16;
            this.type_ComboBox.SelectedIndexChanged += new System.EventHandler(this.type_ComboBox_SelectedIndexChanged);
            // 
            // Explan_Label
            // 
            this.Explan_Label.AutoSize = true;
            this.Explan_Label.Location = new System.Drawing.Point(443, 9);
            this.Explan_Label.Name = "Explan_Label";
            this.Explan_Label.Size = new System.Drawing.Size(61, 13);
            this.Explan_Label.TabIndex = 17;
            this.Explan_Label.Text = "* = required";
            // 
            // AppointmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 227);
            this.Controls.Add(this.Explan_Label);
            this.Controls.Add(this.type_ComboBox);
            this.Controls.Add(this.type_Label);
            this.Controls.Add(this.close_Button);
            this.Controls.Add(this.save_Button);
            this.Controls.Add(this.length_ComboBox);
            this.Controls.Add(this.starttime_ComboBox);
            this.Controls.Add(this.DateTimePicker);
            this.Controls.Add(this.variable_TextBox);
            this.Controls.Add(this.subj_TextBox);
            this.Controls.Add(this.length_Label);
            this.Controls.Add(this.start_Label);
            this.Controls.Add(this.date_Label);
            this.Controls.Add(this.variable_Label);
            this.Controls.Add(this.subj_Label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AppointmentForm";
            this.Text = "Add/Edit Appointment";
            this.Load += new System.EventHandler(this.AddEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label subj_Label;
        private System.Windows.Forms.Label variable_Label;
        private System.Windows.Forms.Label date_Label;
        private System.Windows.Forms.Label start_Label;
        private System.Windows.Forms.Label length_Label;
        private System.Windows.Forms.TextBox subj_TextBox;
        private System.Windows.Forms.TextBox variable_TextBox;
        private System.Windows.Forms.DateTimePicker DateTimePicker;
        private System.Windows.Forms.ToolTip names_Tooltip;
        public System.Windows.Forms.ComboBox starttime_ComboBox;
        private System.Windows.Forms.ComboBox length_ComboBox;
        public System.Windows.Forms.Button save_Button;
        private System.Windows.Forms.Button close_Button;
        private System.Windows.Forms.Label type_Label;
        public System.Windows.Forms.ComboBox type_ComboBox;
        private System.Windows.Forms.Label Explan_Label;
    }
}
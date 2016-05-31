namespace DegreeWork_01
{
    partial class RemindForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemindForm));
            this.RemindMessage = new System.Windows.Forms.RichTextBox();
            this.dateAndTime = new System.Windows.Forms.DateTimePicker();
            this.timeList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // RemindMessage
            // 
            this.RemindMessage.Location = new System.Drawing.Point(43, 91);
            this.RemindMessage.Name = "RemindMessage";
            this.RemindMessage.ReadOnly = true;
            this.RemindMessage.Size = new System.Drawing.Size(194, 141);
            this.RemindMessage.TabIndex = 0;
            this.RemindMessage.Text = "";
            // 
            // dateAndTime
            // 
            this.dateAndTime.Location = new System.Drawing.Point(43, 38);
            this.dateAndTime.Name = "dateAndTime";
            this.dateAndTime.Size = new System.Drawing.Size(194, 20);
            this.dateAndTime.TabIndex = 1;
            this.dateAndTime.ValueChanged += new System.EventHandler(this.dateAndTime_ValueChanged);
            // 
            // timeList
            // 
            this.timeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeList.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timeList.FormattingEnabled = true;
            this.timeList.Location = new System.Drawing.Point(279, 37);
            this.timeList.Name = "timeList";
            this.timeList.Size = new System.Drawing.Size(121, 23);
            this.timeList.TabIndex = 2;
            // 
            // Form3
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(444, 262);
            this.Controls.Add(this.timeList);
            this.Controls.Add(this.dateAndTime);
            this.Controls.Add(this.RemindMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AgedPeopleHelper_0.1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox RemindMessage;
        private System.Windows.Forms.DateTimePicker dateAndTime;
        private System.Windows.Forms.ComboBox timeList;

    }
}

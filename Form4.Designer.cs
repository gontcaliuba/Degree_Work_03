namespace DegreeWork_01
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.remindTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // remindTextBox
            // 
            this.remindTextBox.Location = new System.Drawing.Point(41, 53);
            this.remindTextBox.Name = "remindTextBox";
            this.remindTextBox.ReadOnly = true;
            this.remindTextBox.Size = new System.Drawing.Size(409, 197);
            this.remindTextBox.TabIndex = 0;
            this.remindTextBox.Text = "";
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(584, 262);
            this.Controls.Add(this.remindTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form4";
            this.Text = "AgedPeopleHelper_0.1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox remindTextBox;
    }
}
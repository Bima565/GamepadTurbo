namespace GamepadTurbo
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
            this.components = new System.ComponentModel.Container();
            this.comboInput1 = new System.Windows.Forms.ComboBox();
            this.comboInput2 = new System.Windows.Forms.ComboBox();
            this.comboOutput1 = new System.Windows.Forms.ComboBox();
            this.comboOutput2 = new System.Windows.Forms.ComboBox();
            this.txtRepeatDelay = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboInput1
            // 
            this.comboInput1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboInput1.FormattingEnabled = true;
            this.comboInput1.Location = new System.Drawing.Point(12, 12);
            this.comboInput1.Name = "comboInput1";
            this.comboInput1.Size = new System.Drawing.Size(121, 24);
            this.comboInput1.TabIndex = 0;
            // 
            // comboInput2
            // 
            this.comboInput2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboInput2.FormattingEnabled = true;
            this.comboInput2.Location = new System.Drawing.Point(12, 42);
            this.comboInput2.Name = "comboInput2";
            this.comboInput2.Size = new System.Drawing.Size(121, 24);
            this.comboInput2.TabIndex = 1;
            // 
            // comboOutput1
            // 
            this.comboOutput1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboOutput1.FormattingEnabled = true;
            this.comboOutput1.Location = new System.Drawing.Point(150, 12);
            this.comboOutput1.Name = "comboOutput1";
            this.comboOutput1.Size = new System.Drawing.Size(121, 24);
            this.comboOutput1.TabIndex = 2;
            // 
            // comboOutput2
            // 
            this.comboOutput2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboOutput2.FormattingEnabled = true;
            this.comboOutput2.Location = new System.Drawing.Point(150, 42);
            this.comboOutput2.Name = "comboOutput2";
            this.comboOutput2.Size = new System.Drawing.Size(121, 24);
            this.comboOutput2.TabIndex = 3;
            // 
            // txtRepeatDelay
            // 
            this.txtRepeatDelay.Location = new System.Drawing.Point(12, 80);
            this.txtRepeatDelay.Name = "txtRepeatDelay";
            this.txtRepeatDelay.Size = new System.Drawing.Size(100, 22);
            this.txtRepeatDelay.TabIndex = 4;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(150, 76);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(121, 30);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start Turbo";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtRepeatDelay);
            this.Controls.Add(this.comboOutput2);
            this.Controls.Add(this.comboOutput1);
            this.Controls.Add(this.comboInput2);
            this.Controls.Add(this.comboInput1);
            this.Name = "Form1";
            this.Text = "GamepadTurbo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboInput1;
        private System.Windows.Forms.ComboBox comboInput2;
        private System.Windows.Forms.ComboBox comboOutput1;
        private System.Windows.Forms.ComboBox comboOutput2;
        private System.Windows.Forms.TextBox txtRepeatDelay;
        private System.Windows.Forms.Button btnStart;
    }
}


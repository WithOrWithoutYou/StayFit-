
namespace StayFit___Proto_
{
    partial class AddColumns
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
            this.tbInput = new System.Windows.Forms.TextBox();
            this.btnFileNew = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblInput = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbInput
            // 
            this.tbInput.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tbInput.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbInput.Location = new System.Drawing.Point(12, 59);
            this.tbInput.Multiline = true;
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(345, 68);
            this.tbInput.TabIndex = 0;
            // 
            // btnFileNew
            // 
            this.btnFileNew.FlatAppearance.BorderSize = 0;
            this.btnFileNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFileNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFileNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnFileNew.Image = global::StayFit___Proto_.Properties.Resources.settings;
            this.btnFileNew.Location = new System.Drawing.Point(363, 59);
            this.btnFileNew.Name = "btnFileNew";
            this.btnFileNew.Size = new System.Drawing.Size(129, 68);
            this.btnFileNew.TabIndex = 5;
            this.btnFileNew.Text = "Creat";
            this.btnFileNew.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnFileNew.UseVisualStyleBackColor = true;
            this.btnFileNew.Click += new System.EventHandler(this.btnFileNew_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnClose.Image = global::StayFit___Proto_.Properties.Resources.Custom_Icon_Design_Flatastic_4_Close;
            this.btnClose.Location = new System.Drawing.Point(498, 59);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(57, 61);
            this.btnClose.TabIndex = 12;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInput.Location = new System.Drawing.Point(24, 24);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(56, 24);
            this.lblInput.TabIndex = 13;
            this.lblInput.Text = "Input";
            // 
            // AddColumns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(577, 139);
            this.Controls.Add(this.lblInput);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnFileNew);
            this.Controls.Add(this.tbInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddColumns";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Button btnFileNew;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblInput;
    }
}
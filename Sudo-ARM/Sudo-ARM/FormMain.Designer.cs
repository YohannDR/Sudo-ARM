
namespace Sudo_ARM
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDisassemble = new System.Windows.Forms.Button();
            this.btnDoc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDisassemble
            // 
            this.btnDisassemble.Location = new System.Drawing.Point(61, 120);
            this.btnDisassemble.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDisassemble.Name = "btnDisassemble";
            this.btnDisassemble.Size = new System.Drawing.Size(132, 48);
            this.btnDisassemble.TabIndex = 0;
            this.btnDisassemble.Text = "Disassemble";
            this.btnDisassemble.UseVisualStyleBackColor = true;
            this.btnDisassemble.Click += new System.EventHandler(this.btnDisassemble_Click);
            // 
            // btnDoc
            // 
            this.btnDoc.Location = new System.Drawing.Point(61, 236);
            this.btnDoc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDoc.Name = "btnDoc";
            this.btnDoc.Size = new System.Drawing.Size(132, 48);
            this.btnDoc.TabIndex = 1;
            this.btnDoc.Text = "Documentation";
            this.btnDoc.UseVisualStyleBackColor = true;
            this.btnDoc.Click += new System.EventHandler(this.btnDoc_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.btnDoc);
            this.Controls.Add(this.btnDisassemble);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDisassemble;
        private System.Windows.Forms.Button btnDoc;
    }
}


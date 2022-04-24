
namespace Sudo_ARM
{
    partial class FormDoc
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabOpcodes = new System.Windows.Forms.TabPage();
            this.tabCPU = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabOpcodes);
            this.tabControl.Controls.Add(this.tabCPU);
            this.tabControl.Location = new System.Drawing.Point(0, 1);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(802, 451);
            this.tabControl.TabIndex = 0;
            // 
            // tabOpcodes
            // 
            this.tabOpcodes.Location = new System.Drawing.Point(4, 24);
            this.tabOpcodes.Name = "tabOpcodes";
            this.tabOpcodes.Padding = new System.Windows.Forms.Padding(3);
            this.tabOpcodes.Size = new System.Drawing.Size(794, 423);
            this.tabOpcodes.TabIndex = 0;
            this.tabOpcodes.Text = "Opcodes";
            this.tabOpcodes.UseVisualStyleBackColor = true;
            // 
            // tabCPU
            // 
            this.tabCPU.Location = new System.Drawing.Point(4, 24);
            this.tabCPU.Name = "tabCPU";
            this.tabCPU.Padding = new System.Windows.Forms.Padding(3);
            this.tabCPU.Size = new System.Drawing.Size(794, 423);
            this.tabCPU.TabIndex = 1;
            this.tabCPU.Text = "CPU";
            this.tabCPU.UseVisualStyleBackColor = true;
            // 
            // FormDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl);
            this.Name = "FormDoc";
            this.Text = "FormDoc";
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabOpcodes;
        private System.Windows.Forms.TabPage tabCPU;
    }
}
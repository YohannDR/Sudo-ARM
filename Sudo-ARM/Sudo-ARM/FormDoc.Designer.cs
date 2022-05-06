
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
            this.cbOpcodes = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbOpcodeInfo = new System.Windows.Forms.GroupBox();
            this.tbOpcodeSize = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbOpcodeBinary = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbOpcodeType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbOpcodeString = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelOpcodeDesc = new System.Windows.Forms.Label();
            this.tabCPU = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabOpcodes.SuspendLayout();
            this.gbOpcodeInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabOpcodes);
            this.tabControl.Controls.Add(this.tabCPU);
            this.tabControl.Location = new System.Drawing.Point(0, 1);
            this.tabControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(917, 601);
            this.tabControl.TabIndex = 0;
            // 
            // tabOpcodes
            // 
            this.tabOpcodes.Controls.Add(this.cbOpcodes);
            this.tabOpcodes.Controls.Add(this.label2);
            this.tabOpcodes.Controls.Add(this.gbOpcodeInfo);
            this.tabOpcodes.Location = new System.Drawing.Point(4, 29);
            this.tabOpcodes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabOpcodes.Name = "tabOpcodes";
            this.tabOpcodes.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabOpcodes.Size = new System.Drawing.Size(909, 568);
            this.tabOpcodes.TabIndex = 0;
            this.tabOpcodes.Text = "Opcodes";
            this.tabOpcodes.UseVisualStyleBackColor = true;
            // 
            // cbOpcodes
            // 
            this.cbOpcodes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOpcodes.FormattingEnabled = true;
            this.cbOpcodes.Location = new System.Drawing.Point(604, 51);
            this.cbOpcodes.Name = "cbOpcodes";
            this.cbOpcodes.Size = new System.Drawing.Size(129, 28);
            this.cbOpcodes.TabIndex = 3;
            this.cbOpcodes.SelectedIndexChanged += new System.EventHandler(this.cbOpcodes_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(513, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Opcode :";
            // 
            // gbOpcodeInfo
            // 
            this.gbOpcodeInfo.Controls.Add(this.tbOpcodeSize);
            this.gbOpcodeInfo.Controls.Add(this.label6);
            this.gbOpcodeInfo.Controls.Add(this.tbOpcodeBinary);
            this.gbOpcodeInfo.Controls.Add(this.label5);
            this.gbOpcodeInfo.Controls.Add(this.tbOpcodeType);
            this.gbOpcodeInfo.Controls.Add(this.label4);
            this.gbOpcodeInfo.Controls.Add(this.label3);
            this.gbOpcodeInfo.Controls.Add(this.tbOpcodeString);
            this.gbOpcodeInfo.Controls.Add(this.label1);
            this.gbOpcodeInfo.Controls.Add(this.labelOpcodeDesc);
            this.gbOpcodeInfo.Location = new System.Drawing.Point(8, 16);
            this.gbOpcodeInfo.Name = "gbOpcodeInfo";
            this.gbOpcodeInfo.Size = new System.Drawing.Size(472, 444);
            this.gbOpcodeInfo.TabIndex = 1;
            this.gbOpcodeInfo.TabStop = false;
            this.gbOpcodeInfo.Text = "Info";
            // 
            // tbOpcodeSize
            // 
            this.tbOpcodeSize.Location = new System.Drawing.Point(147, 341);
            this.tbOpcodeSize.Name = "tbOpcodeSize";
            this.tbOpcodeSize.ReadOnly = true;
            this.tbOpcodeSize.Size = new System.Drawing.Size(212, 27);
            this.tbOpcodeSize.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 344);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Size :";
            // 
            // tbOpcodeBinary
            // 
            this.tbOpcodeBinary.Location = new System.Drawing.Point(147, 290);
            this.tbOpcodeBinary.Name = "tbOpcodeBinary";
            this.tbOpcodeBinary.ReadOnly = true;
            this.tbOpcodeBinary.Size = new System.Drawing.Size(212, 27);
            this.tbOpcodeBinary.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 290);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Binary Template :";
            // 
            // tbOpcodeType
            // 
            this.tbOpcodeType.Location = new System.Drawing.Point(116, 35);
            this.tbOpcodeType.Name = "tbOpcodeType";
            this.tbOpcodeType.ReadOnly = true;
            this.tbOpcodeType.Size = new System.Drawing.Size(203, 27);
            this.tbOpcodeType.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Type :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 396);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Example :";
            // 
            // tbOpcodeString
            // 
            this.tbOpcodeString.Location = new System.Drawing.Point(147, 393);
            this.tbOpcodeString.Name = "tbOpcodeString";
            this.tbOpcodeString.ReadOnly = true;
            this.tbOpcodeString.Size = new System.Drawing.Size(212, 27);
            this.tbOpcodeString.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Description :";
            // 
            // labelOpcodeDesc
            // 
            this.labelOpcodeDesc.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelOpcodeDesc.Location = new System.Drawing.Point(18, 123);
            this.labelOpcodeDesc.Name = "labelOpcodeDesc";
            this.labelOpcodeDesc.Size = new System.Drawing.Size(351, 131);
            this.labelOpcodeDesc.TabIndex = 0;
            this.labelOpcodeDesc.Text = "Description";
            // 
            // tabCPU
            // 
            this.tabCPU.Location = new System.Drawing.Point(4, 29);
            this.tabCPU.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabCPU.Name = "tabCPU";
            this.tabCPU.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabCPU.Size = new System.Drawing.Size(909, 568);
            this.tabCPU.TabIndex = 1;
            this.tabCPU.Text = "CPU";
            this.tabCPU.UseVisualStyleBackColor = true;
            // 
            // FormDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormDoc";
            this.Text = "FormDoc";
            this.tabControl.ResumeLayout(false);
            this.tabOpcodes.ResumeLayout(false);
            this.tabOpcodes.PerformLayout();
            this.gbOpcodeInfo.ResumeLayout(false);
            this.gbOpcodeInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabOpcodes;
        private System.Windows.Forms.TabPage tabCPU;
        private System.Windows.Forms.Label labelOpcodeDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbOpcodeInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbOpcodes;
        private System.Windows.Forms.TextBox tbOpcodeString;
        private System.Windows.Forms.TextBox tbOpcodeType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbOpcodeBinary;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbOpcodeSize;
        private System.Windows.Forms.Label label6;
    }
}
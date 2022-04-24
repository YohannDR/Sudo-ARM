using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudo_ARM
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Opcode opcode = new Opcode(0x00A4);
            Console.WriteLine(opcode);
            Console.WriteLine(Convert.ToString(opcode.Assemble(false), 16));
            Console.WriteLine(Convert.ToString(opcode.Assemble(true), 16));
        }

        private void btnDisassemble_Click(object sender, EventArgs e)
        {
            new FormDisassemble(this).Show();
        }

        private void btnDoc_Click(object sender, EventArgs e)
        {
            new FormDoc(this).Show();
        }
    }
}

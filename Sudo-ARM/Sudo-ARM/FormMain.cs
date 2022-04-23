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
            Opcode[] funcTest = new Opcode[]
            {
                new Opcode(0xF0B5),
                new Opcode(0x021C),
                new Opcode(0x3030),
                new Opcode(0x0170),
                new Opcode(0x0330),
                new Opcode(0x0078),
                new Opcode(0xD17F),
                new Opcode(0x4018),
                new Opcode(0x0F21),
                new Opcode(0x091A),
                new Opcode(0x101C),
                new Opcode(0x2030),
                new Opcode(0x0170),
                new Opcode(0xA020),
                new Opcode(0x4000),
                new Opcode(0xB2F701FA),
                new Opcode(0x01BC),
                new Opcode(0x0047),
            };

            StringBuilder text = new();
            foreach (Opcode O in funcTest)
            {
                text.Append(O.ToString());
                Console.WriteLine(text.ToString());
                text.Clear();
            }
        }
    }
}

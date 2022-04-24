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
    public partial class FormDoc : Form
    {
        public static readonly string[] OpcodeDescriptions = new string[]
        {
            "Undefined opcode",
            "Left bit shift on rS by I, result is stored in rD",
            "Right bit shift on rS by I, result is stored in rD",
            "Right arithmetic bit shift on rS by I, result is stored in rD",
            "Adds rR to rS, result is stored in rD",
            "Subtracts rR to rS, result is stored in rD",
            "Adds I to rS, result is stored in rD",
            "Subtracts I to rS, result is stored in rD",
            "Moves I to rD",
            "Compares rD and I (rD - I)",
            "Adds I to rD",
            "Subtracts I to rD",
            "Binary and on rD and rS, result is stored in rD",
            "Binary exclusive or on rD and rS, result is stored in rD",
            "Left bit shift on rD of rS, result is stored in rD",
            "Right bit shift on rD of rS, result is stored in rD",
            "Right arithmetic shift on rD of rS, result is stored in rD",
            "Adds rS to rD, with carry",
            "Subtracts rS to rD, with carry",
            "Rotate right rD by rS, result is stored in rD",
            "Tests rD and rS (rD & rS)",
            "Negates rS, result is stored in rD",
            "Compares rD and rS (rD - rS)",
            "Compares negative rD and rS (rD + rS)",
            "Binary or on rD and rS, result is stored in rD",
            "Multiplies rD and rS, result is stored in rD",
            "Bit clears rD and rS (rD & ~rS), result if stored in rD",
            "Moves not rS to rD",
            "Adds rS to rD",
            "Compares rD and rS (rD - rS)",
            "Moves rS to rD",
            "No process (translates to mov r8,r8)",
            "Branches to rS, can switch CPU mode",
            "Loads a 32-bit word from a pool by indexing from PC (PC + I)",
            "Stores the 32-bit word of rS in the address in rD, indexed by rR",
            "Stores the 16-bit halfword of rS in the address in rD, indexed by rR",
            "Stores the 8-bit byte of rS in the address in rD, indexed by rR",
            "Loads the signed 8-bit byte in rD, of rS indexed by rR",
            "Loads the 32-bit word in rD, of rS indexed by rR",
            "Loads the 16-bit halfword in rD, of rS indexed by rR",
            "Loads the 8-bit byte in rD, of rS indexed by rR",
            "Loads the signed 16-bit halfword in rD, of rS indexed by rR",
            "Stores the 32-bit word of rS in the address in rD, indexed by I",
            "Loads the 32-bit word in rD, of rS indexed by I",
            "Stores the 8-bit byte of rS in the address in rD, indexed by I",
            "Loads the 8-bit byte in rD, of rS indexed by I",
            "Stores the 16-bit halfword of rS in the address in rD, indexed by I",
            "Loads the 16-bit halfword in rD, of rS indexed by I",
            "Stores the 32-bit word of rS in the address in SP, indexed by I",
            "Loads the 32-bit word in rD, of SP indexed by I",
            "Adds I to PC and stores the result in rD",
            "Adds I to SP and stores the result in rD",
            "Adds I to SP",
            "Substracts I to SP",
            "Pushes every register specified to the stack, highest number to lowest",
            "Pushes every register specified and LR to the stack, highest number to lowest",
            "Pops every register specified from the stack, lowest number to highest",
            "Pops every register specified and PC from the stack, lowest number to highest",
            "Stores every value in the specified registers to rD while incrementing it",
            "Loads every value from rS to the specified registers while incrementing it",
            "Branches to I if flag Zero is set (equal)",
            "Branches to I if flag Zero is not set (not equal)",
            "Branches to I if flag Carry is set (unsigned higher or same)",
            "Branches to I if flag Carry is not set (unsigned lower)",
            "Branches to I if flag Negative is set (negative)",
        };

        FormMain Master;
        Opcode CurrentOpcode;

        public FormDoc(FormMain master)
        {
            Master = master;
            InitializeComponent();
            CurrentOpcode = new Opcode(0x040E);
        }
    }
}

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
        #region Opcode Data

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
            "Branches to I if flag Negative is not set (positive)",
            "Branches to I if flag Overflow is set (became positive)",
            "Branches to I if flag Overflow is not set (didn't became positive)",
            "Branches to I if flag Carry is set and flag Zero is not set (unsigned higher)",
            "Branches to I if flag Carry is not set or flag Zero is not set (unsigned lower or same)",
            "Branches to I if flag Carry equal flag Overflow (signed higher or same)",
            "Branches to I if flag Carry does not equal flag Overflow (signed lower)",
            "Branches to I if flag Zero is not set and flag Negative equal flag Overflow (signed higher)",
            "Branches to I if flag Zero is not set or flag Negative does not equal flag Overflow (signed lower or same)",
            "Jumps to 00000008, enters supervisor mode in ARM state, with IRQ Disable flag set, does the I BIOS call",
            "Branches to I",
            "Saves PC to LR accordingly and branches to I"
        };

        public static readonly string[] OpcodeTemplates = new string[]
        {
            "Undefined",
            "lsl rD,rS,#i",
            "lsr rD,rS,#i",
            "asr rD,rS,#i",
            "add rD,rS,rR",
            "sub rD,rS,rR",
            "add rD,rS,#i",
            "sub rD,rS,#i",
            "mov rD,#i",
            "cmp rD,#i",
            "add rD,#i",
            "sub rD,#i",
            "and rD,rS",
            "eor rD,rS",
            "lsl rD,rS",
            "lsr rD,rS",
            "asr rD,rS",
            "adc rD,rS",
            "sbc rD,rS",
            "ror rD,rS",
            "tst rD,rS",
            "neg rD,rS",
            "cmp rD,rS",
            "cmn rD,rS",
            "orR rD,rS",
            "mul rD,rS",
            "bic rD,rS",
            "mvn rD,rS",
            "add rD,rS",
            "cmp rD,rS",
            "mov rD,rS",
            "nop",
            "bx rS",
            "ldr rD,[pc,#i]",
            "str rS,[rD,rR]",
            "strh rS,[rD,rR]",
            "strb rS,[rD,rR]",
            "ldsb rD,[rS,rR]",
            "ldr rD,[rS,rR]",
            "ldrh rD,[rS,rR]",
            "ldrb rD,[rS,rR]",
            "ldsh rD,[rS,rR]",
            "str rS,[rD,#i]",
            "ldr rD,[rS,#i]",
            "strb rS,[rD,#i]",
            "ldrb rD,[rS,#i]",
            "strh rS,[rD,#i]",
            "ldrh rD,[rS,#i]",
            "str rS,[sp,#i]",
            "ldr rD,[sp,#i]",
            "add rD,pc,#i",
            "add rD,sp,#i",
            "add sp,#i",
            "sub sp,#i",
            "push {rD}",
            "push {rD,lr}",
            "pop {rD}",
            "pop {rD,pc}",
            "stmia rD!,{rS}",
            "ldmia rS!,{rD}",
            "beq i",
            "bne i",
            "bcs i",
            "bcc i",
            "bmi i",
            "bpl i",
            "bvs i",
            "bhi i",
            "bls i",
            "bge i",
            "blt i",
            "bgt i",
            "ble i",
            "swi #i",
            "b i",
            "bl i"
        };

        public static readonly uint[] OpcodeHexTemplates = new uint[]
        {
            /*0x0047, // Undefined
            0x0006, // lsl r0,r0,#0x18
            0x000E, // lsr r0,r0,#0x18
            0x0016, // asr r0,r0,#0x18
            0x8818, // add r0,r1,r2
            0x881A, // sub r0,r1,r2
            0x481D, // add r0,r1,#0x5
            0x481F, // sub r0,r1,#0x5
            0x5020, // mov r0,#0x50
            0x5030, // add r0,#0x50
            0x5038, // sub r0,#0x50
            0x5028, // cmp r0,#0x50
            0x0840, // and r0,r1
            0x4840, // eor r0,r1
            0x8840, // lsl r0,r1
            0xC840, // lsr r0,r1
            0x0841, // asr r0,r1
            0x4841, // adc r0,r1
            0x8841, // sbc r0,r1
            0xC841, // ror r0,r1
            0x0842, // tst r0,r1
            0x4842, // neg r0,r1
            0x8842, // cmp r0,r1
            0xC842, // cmn r0,r1
            0x0843, // orr r0,r1
            0x4843, // mul r0,r1
            0x8843, // bic r0,r1
            0xC843, // mvn r0,r1
            0xC844, // add r8,r9
            0xC845, // cmp r8,r9
            0xC846, // mov r8,r9
            0xC046, // nop
            0x0047, // bx r0
            0x1048, // ldr r0,[pc,#0x40]
            0x8850, // str r0,[r1,r2]
            0x8852, // strh r0,[r1,r2]
            0x8854, // strb r0,[r1,r2]
            0x8856, // ldsb r0,[r1,r2]
            0x8858, // ldr r0,[r1,r2]
            0x885A, // ldrh r0,[r1,r2]
            0x885C, // ldrb r0,[r1,r2]
            0x885E, // ldsh r0,[r1,r2]
            0x0861, // str r0,[r1,#0x10]
            0x0869, // ldr r0,[r1,#0x10]
            0x0874, // strb r0,[r1,#0x10]
            0x087C, // ldrb r0,[r1,#0x10]
            0x0882, // strh r0,[r1,#0x10]
            0x088A, // ldrh r0,[r1,#0x10]
            0x04A0, // add r0,pc,#0x10
            0x04A8, // add r0,sp,#0x10
            0x04B0, // add sp,#0x10
            0x84B0, // sub sp,#0x10
            0x30B4, // push r4,r5
            0x30B5, // push r4,r5,r14
            0x30BC, // pop r4,r5
            0x30BD, // pop r4,r5,pc
            0x303D, // stmia r0!,{r4,r5}
            0x30C0, // ldmia r0!,{r4,r5}
            0xC5D0, // beq ?
            0xC4D1, // bne ?
            0xC3D2, // bcs ?
            0xC2D3, // bcc ?
            0xC1D4, // bmi ?
            0xC0D5, // bpl ?
            0xBFD9, // bvs ?
            0xBED7, // bvc ?
            0xBDD8, // bhi ?
            0xBCD9, // bls ?
            0xBBDA, // bge ?
            0xBADB, // blt ?
            0xB9DA, // bgt ?
            0xB8DD, // ble ?
            0x00DF, // swi #0x0
            0xB6E7, // b ?
            0xFFF7B5FF // bl ?*/
        };

        #endregion

        FormMain Master;
        Opcode CurrentOpcode;

        public FormDoc(FormMain master)
        {
            Master = master;
            InitializeComponent();

            cbOpcodes.Items.AddRange(OpcodeTemplates);
            cbOpcodes.SelectedIndex = 0;
        }

        private void UpdateOpcodeInfo()
        {
            labelOpcodeDesc.Text = OpcodeDescriptions[(int)CurrentOpcode.SubType];
            tbOpcodeString.Text = CurrentOpcode.ToString();
            tbOpcodeType.Text = CurrentOpcode.Type.ToString();
        }

        private void cbOpcodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentOpcode = new Opcode(OpcodeHexTemplates[cbOpcodes.SelectedIndex]);
            UpdateOpcodeInfo();
        }
    }
}

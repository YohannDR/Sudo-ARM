using System;
using System.Windows.Forms;

namespace Sudo_ARM
{
    public partial class FormDoc : Form
    {
        #region Opcode Data

        public static readonly string[] OpcodeDescriptions = new string[]
        {
            "Undefined opcode (combinaison of bits that aren't mapped to any instruction), will crash the console",
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
            "orr rD,rS",
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
            "bvc i",
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

        public static readonly Opcode[] OpcodeObjectTemplates = new Opcode[]
        {
            new Opcode(0x00DE), // Undefined (cond branch with 1110 for sub type)
            new Opcode(0x0006), // lsl r0,r0,#0x18
            new Opcode(0x000E), // lsr r0,r0,#0x18
            new Opcode(0x0016), // asr r0,r0,#0x18
            new Opcode(0x8818), // add r0,r1,r2
            new Opcode(0x881A), // sub r0,r1,r2
            new Opcode(0x481D), // add r0,r1,#0x5
            new Opcode(0x481F), // sub r0,r1,#0x5
            new Opcode(0x5020), // mov r0,#0x50
            new Opcode(0x5028), // cmp r0,#0x50
            new Opcode(0x5030), // add r0,#0x50
            new Opcode(0x5038), // sub r0,#0x50
            new Opcode(0x0840), // and r0,r1
            new Opcode(0x4840), // eor r0,r1
            new Opcode(0x8840), // lsl r0,r1
            new Opcode(0xC840), // lsr r0,r1
            new Opcode(0x0841), // asr r0,r1
            new Opcode(0x4841), // adc r0,r1
            new Opcode(0x8841), // sbc r0,r1
            new Opcode(0xC841), // ror r0,r1
            new Opcode(0x0842), // tst r0,r1
            new Opcode(0x4842), // neg r0,r1
            new Opcode(0x8842), // cmp r0,r1
            new Opcode(0xC842), // cmn r0,r1
            new Opcode(0x0843), // orr r0,r1
            new Opcode(0x4843), // mul r0,r1
            new Opcode(0x8843), // bic r0,r1
            new Opcode(0xC843), // mvn r0,r1
            new Opcode(0xC844), // add r8,r9
            new Opcode(0xC845), // cmp r8,r9
            new Opcode(0xC846), // mov r8,r9
            new Opcode(0xC046), // nop
            new Opcode(0x0047), // bx r0
            new Opcode(0x1048), // ldr r0,[pc,#0x40]
            new Opcode(0x8850), // str r0,[r1,r2]
            new Opcode(0x8852), // strh r0,[r1,r2]
            new Opcode(0x8854), // strb r0,[r1,r2]
            new Opcode(0x8856), // ldsb r0,[r1,r2]
            new Opcode(0x8858), // ldr r0,[r1,r2]
            new Opcode(0x885A), // ldrh r0,[r1,r2]
            new Opcode(0x885C), // ldrb r0,[r1,r2]
            new Opcode(0x885E), // ldsh r0,[r1,r2]
            new Opcode(0x0861), // str r0,[r1,#0x10]
            new Opcode(0x0869), // ldr r0,[r1,#0x10]
            new Opcode(0x0874), // strb r0,[r1,#0x10]
            new Opcode(0x087C), // ldrb r0,[r1,#0x10]
            new Opcode(0x0882), // strh r0,[r1,#0x10]
            new Opcode(0x088A), // ldrh r0,[r1,#0x10]
            new Opcode(0x0490), // str r0,[sp,#0x10]
            new Opcode(0x0498), // ldr r0,[sp,#0x10]
            new Opcode(0x04A0), // add r0,pc,#0x10
            new Opcode(0x04A8), // add r0,sp,#0x10
            new Opcode(0x04B0), // add sp,#0x10
            new Opcode(0x84B0), // sub sp,#0x10
            new Opcode(0x30B4), // push r4,r5
            new Opcode(0x30B5), // push r4,r5,r14
            new Opcode(0x30BC), // pop r4,r5
            new Opcode(0x30BD), // pop r4,r5,pc
            new Opcode(0x30C0), // stmia r0!,{r4,r5}
            new Opcode(0x30C8), // ldmia r0!,{r4,r5}
            new Opcode(0xC3D0), // beq
            new Opcode(0xC2D1), // bne
            new Opcode(0xC1D2), // bcs
            new Opcode(0xC0D3), // bcc
            new Opcode(0xBFD4), // bmi
            new Opcode(0xBED5), // bpl
            new Opcode(0xBDD6), // bvs
            new Opcode(0xBCD7), // bvc
            new Opcode(0xBBD8), // bhi
            new Opcode(0xBAD9), // bls
            new Opcode(0xB9DA), // bge
            new Opcode(0xB8DB), // blt
            new Opcode(0xB7DC), // bgt
            new Opcode(0xB6DD), // ble
            new Opcode(0x00DF), // swi #0x0
            new Opcode(0xB6E7), // b
            new Opcode(0xFFF7B5FF) // bl
        };

        public static readonly string[] OpcodeTypeDescriptions = new string[]
        {
            "Undefined",
            "Shift",
            "Add/Substract",
            "Immediate",
            "Logical/Arithmetic",
            "Hi-register",
            "Branch register",
            "Load PC relative",
            "Load/Store with register",
            "Load/Store with immediate",
            "Load/Store halfword",
            "Load/Store stack",
            "Add PC/SP",
            "Add/Substract to stack",
            "Stack operation",
            "Pseudo DMA",
            "Conditional branch",
            "Interrupt",
            "Branch",
            "Branch link"
        };

        public static readonly string[] OpcodeBinaryTemplates = new string[]
        {
            "Undefined",
            "000 00 iiiii sss ddd",
            "000 01 iiiii sss ddd",
            "000 10 iiiii sss ddd",
            "00011 00 rrr sss ddd",
            "00011 01 rrr sss ddd",
            "00011 10 iii sss ddd",
            "00011 11 iii sss ddd",
            "001 00 ddd iiiiiiii",
            "001 01 ddd iiiiiiii",
            "001 10 ddd iiiiiiii",
            "001 11 ddd iiiiiiii",
            "010000 0000 sss ddd",
            "010000 0001 sss ddd",
            "010000 0010 sss ddd",
            "010000 0011 sss ddd",
            "010000 0100 sss ddd",
            "010000 0101 sss ddd",
            "010000 0110 sss ddd",
            "010000 0111 sss ddd",
            "010000 1000 sss ddd",
            "010000 1001 sss ddd",
            "010000 1010 sss ddd",
            "010000 1011 sss ddd",
            "010000 1100 sss ddd",
            "010000 1101 sss ddd",
            "010000 1110 sss ddd",
            "010000 1111 sss ddd",
            "010001 00 d s sss ddd",
            "010001 01 d s sss ddd",
            "010001 10 d s sss ddd",
            "010001 10 1 1 000 000",
            "010001110 ssss 000",
            "01001 ddd iiiiiiii",
            "0101 000 rrr ddd sss",
            "0101 001 rrr ddd sss",
            "0101 010 rrr ddd sss",
            "0101 011 rrr sss ddd",
            "0101 100 rrr sss ddd",
            "0101 101 rrr sss ddd",
            "0101 110 rrr sss ddd",
            "0101 111 rrr sss ddd",
            "011 00 iiiii ddd sss",
            "011 01 iiiii sss ddd",
            "011 10 iiiii ddd sss",
            "011 11 iiiii sss ddd",
            "1000 0 iiiii ddd sss",
            "1000 1 iiiii ddd sss",
            "1001 0 sss iiiiiiii",
            "1001 1 ddd iiiiiiii",
            "1010 0 ddd iiiiiiii",
            "1010 1 ddd iiiiiiii",
            "10110000 0 iiiiiii",
            "10110000 1 iiiiiii",
            "1011 01 00 dddddddd",
            "1011 01 01 dddddddd",
            "1011 11 00 dddddddd",
            "1011 11 01 dddddddd",
            "1100 0 ddd ssssssss",
            "1100 1 sss dddddddd",
            "1101 0000 iiiiiiii",
            "1101 0001 iiiiiiii",
            "1101 0010 iiiiiiii",
            "1101 0011 iiiiiiii",
            "1101 0100 iiiiiiii",
            "1101 0101 iiiiiiii",
            "1101 0110 iiiiiiii",
            "1101 0111 iiiiiiii",
            "1101 1000 iiiiiiii",
            "1101 1001 iiiiiiii",
            "1101 1010 iiiiiiii",
            "1101 1011 iiiiiiii",
            "1101 1100 iiiiiiii",
            "1101 1101 iiiiiiii",
            "11011111 iiiiiiii",
            "11100 iiiiiiiiiii",
            "11110 iiiiiiiiiii 11111 iiiiiiiiiii"
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
            tbOpcodeType.Text = OpcodeTypeDescriptions[(int)CurrentOpcode.Type];
            tbOpcodeBinary.Text = OpcodeBinaryTemplates[(int)CurrentOpcode.SubType];
            tbOpcodeSize.Text = CurrentOpcode.Is32Bits() ? "32 bits" : "16 bits";

            clbOpcodeFlags.SetItemChecked(0, (CurrentOpcode.AffectedFlags & CPUFlags.Overflow) != 0);
            clbOpcodeFlags.SetItemChecked(1, (CurrentOpcode.AffectedFlags & CPUFlags.Carry) != 0);
            clbOpcodeFlags.SetItemChecked(2, (CurrentOpcode.AffectedFlags & CPUFlags.Zero) != 0);
            clbOpcodeFlags.SetItemChecked(3, (CurrentOpcode.AffectedFlags & CPUFlags.Negative) != 0);
        }

        private void cbOpcodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentOpcode = OpcodeObjectTemplates[cbOpcodes.SelectedIndex];
            UpdateOpcodeInfo();
        }
    }
}

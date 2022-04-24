using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudo_ARM
{
    [Flags]
    public enum CPUFlags
    {
        None = 0,
        ThumbMode = 1 << 5,
        FIQDisable = 1 << 6,
        IRQDisable = 1 << 7,
        AbortDisable = 1 << 8,
        JazelleMode = 1 << 24,
        StickyOverflow = 1 << 27,
        Overflow = 1 << 28,
        Carry = 1 << 29,
        Zero = 1 << 30,
        Negative = 1 << 31,
    }

    public class CPU
    {
        public int[] Registers = new int[16];
        public bool Crash;
        public CPUFlags Flags;

        public void ExecuteOpcode(Opcode opcode)
        {
            switch (opcode.SubType)
            {
                case SubOpcodeType.lslRI:
                    Registers[(int)opcode.RegD] = Registers[(int)opcode.RegS] << (int)opcode.Immediate;
                    break;

                case SubOpcodeType.lsrRI:
                    Registers[(int)opcode.RegD] = Registers[(int)opcode.RegS] >> (int)opcode.Immediate;
                    break;

                case SubOpcodeType.asrRI: // TODO
                    break;

                case SubOpcodeType.addRR:
                    Registers[(int)opcode.RegD] = Registers[(int)opcode.RegS] + Registers[(int)opcode.RegR];
                    break;

                case SubOpcodeType.subRR:
                    Registers[(int)opcode.RegD] = Registers[(int)opcode.RegS] - Registers[(int)opcode.RegR];
                    break;

                case SubOpcodeType.addRI:
                    Registers[(int)opcode.RegD] = Registers[(int)opcode.RegS] + (int)opcode.Immediate;
                    break;

                case SubOpcodeType.subRI:
                    Registers[(int)opcode.RegD] = Registers[(int)opcode.RegS] - (int)opcode.Immediate;
                    break;

                case SubOpcodeType.Undefined:
                    Crash = true;
                    break;
            }
        }
    }
}

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
            }
        }
    }
}

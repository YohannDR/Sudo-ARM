using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudo_ARM
{
    [Flags]
    public enum Flags
    {
        None = 0,
        Negative = 1,
        Zero = 2,
        Carry = 4,
        Overflow = 8
    }

    public class CPU
    {

    }
}

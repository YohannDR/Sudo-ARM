using System.Runtime.CompilerServices;
using System;

namespace Sudo_ARM
{
    public static class Bit
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetBit(uint value, byte bit)
            => (value >> bit) & 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetBits(uint value, byte bit, byte size)
            => (value >> (bit - size)) & (uint)(Math.Pow(2, size) - 1);
    }
}

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

        public static uint SwitchEndian(uint value)
        {
            byte[] data = BitConverter.GetBytes(value);
            return (uint)(data[3] | (data[2] << 8) | (data[1] << 16) | (data[0] << 24));
        }

        public static uint RegListToBits(byte[] list)
        {
            uint result = 0;

            for (int i = 0; i < list.Length; i++)
                result |= (uint)Math.Pow(2, list[i]);

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudo_ARM
{
    public enum OpcodeType
    {
        Undefined,
        Shift,
        AddSub,
        Immediate,
        Arithmetic,
        HiRegister,
        BranchRegister,
        LoadPC,
        LoadStoreReg,
        LoadStoreImmediate,
        LoadStoreHalf,
        LoadStoreSP,
        AddPCSP,
        AddSP,
        Stack,
        PseudoDMA,
        CondBranch,
        Interrupt,
        Branch,
        BranchLink
    }

    public enum SubOpcodeType
    {
        Undefined,

        // Shift
        /// <summary> lsl rd,rs,#i </summary>
        lslRI,
        /// <summary> lsr rd,rs,#i </summary>
        lsrRI,
        /// <summary> asr rd,rs,#i </summary>
        asrRI,

        // AddSub
        /// <summary> add rd,rs,rr </summary>
        addRR,
        /// <summary> sub rd,rs,rr </summary>
        subRR,
        /// <summary> add rd,rs,#i </summary>
        addRI,
        /// <summary> sub rd,rs,#i </summary>
        subRI,

        // Immediate
        /// <summary> mov rd,#i </summary>
        movI,
        /// <summary> cmp rd,#i </summary>
        cmpI,
        /// <summary> add rd,#i </summary>
        addI,
        /// <summary> sub rd,#i </summary>
        subI,

        // Arithmetic
        /// <summary> and rd,rs </summary>
        and,
        /// <summary> eor rd,rs </summary>
        eor,
        /// <summary> lsl rd,rs,#i </summary>
        lslR, // lsl rd,rs
        /// <summary> lsl rd,rs,#i </summary>
        lsrR, // lsr rd,rs
        /// <summary> lsl rd,rs,#i </summary>
        asrR, // asr rd,rs
        /// <summary> lsl rd,rs,#i </summary>
        adc, // adc rd,rs
        /// <summary> lsl rd,rs,#i </summary>
        sbc, // sbc rd,rs
        /// <summary> lsl rd,rs,#i </summary>
        ror, // ror rd,rs
        /// <summary> lsl rd,rs,#i </summary>
        tst, // tst rd,rs
        /// <summary> lsl rd,rs,#i </summary>
        neg, // neg rd,rs
        /// <summary> lsl rd,rs,#i </summary>
        cmp, // cmp rd,rs
        /// <summary> lsl rd,rs,#i </summary>
        cmn, // cmp rd,rs
        /// <summary> lsl rd,rs,#i </summary>
        orr, // orr rd,rs
        /// <summary> lsl rd,rs,#i </summary>
        mul, // mul rd,rs
        /// <summary> lsl rd,rs,#i </summary>
        bic, // bic rd,rs
        /// <summary> lsl rd,rs,#i </summary>
        mvn, // mvn rd, rs

        // HiRegister
        /// <summary> lsl rd,rs,#i </summary>
        addR, // add rd,rs
        /// <summary> lsl rd,rs,#i </summary>
        cmpR, // cmp rd,rs
        /// <summary> lsl rd,rs,#i </summary>
        movR, // mov rd,rs
        /// <summary> lsl rd,rs,#i </summary>
        nop, // nop

        // BranchRegister
        /// <summary> lsl rd,rs,#i </summary>
        bx, // bx rs

        // LoadPC
        /// <summary> lsl rd,rs,#i </summary>
        ldrPC, // ldr rd,[pc,#i]

        // LoadStoreReg
        /// <summary> lsl rd,rs,#i </summary>
        strR, // str rs,[rd,rr]
        /// <summary> lsl rd,rs,#i </summary>
        strhR, // strh rs,[rd,rr]
        /// <summary> lsl rd,rs,#i </summary>
        strbR, // strb rs,[rd,rr]
        /// <summary> lsl rd,rs,#i </summary>
        ldsb, // ldsb rd,[rs,rr]
        /// <summary> lsl rd,rs,#i </summary>
        ldrR, // ldr rd,[rs,rr]
        /// <summary> lsl rd,rs,#i </summary>
        ldrhR, // ldrh rd,[rs,rr]
        /// <summary> lsl rd,rs,#i </summary>
        ldrbR, // ldrb rd,[rs,rr]
        /// <summary> lsl rd,rs,#i </summary>
        ldsh, // ldsh rd,[rs,rr]

        // LoadStoreImmediate
        /// <summary> lsl rd,rs,#i </summary>
        strI, // str rs,[rd,#i]
        /// <summary> lsl rd,rs,#i </summary>
        ldrI, // ldr rd,[rs,#i]
        /// <summary> lsl rd,rs,#i </summary>
        strbI, // strb rs,[rd,#i]
        /// <summary> lsl rd,rs,#i </summary>
        ldrbI, // ldrb rd,[rs,#i]

        // LoadStoreHalf
        /// <summary> lsl rd,rs,#i </summary>
        strhI, // strh rs,[rd,#i]
        /// <summary> lsl rd,rs,#i </summary>
        ldrhI, // ldrh rd,[rs,#i]

        // LoadStoreSP
        /// <summary> lsl rd,rs,#i </summary>
        strSP, // str rs,[sp,#i]
        /// <summary> lsl rd,rs,#i </summary>
        ldrSP, // ldr rd,[sp,#i]

        // AddPCSP
        /// <summary> lsl rd,rs,#i </summary>
        addPCR, // add rd,pc,#i
        /// <summary> lsl rd,rs,#i </summary>
        addSPR, // add rd,sp,#i

        // AddSP
        /// <summary> lsl rd,rs,#i </summary>
        addSP, // add sp,#i
        /// <summary> lsl rd,rs,#i </summary>
        subSP, // add sp,-#i

        // Stack
        /// <summary> lsl rd,rs,#i </summary>
        push, // push {rd}
        /// <summary> lsl rd,rs,#i </summary>
        pushLR, // push {rd,lr}
        /// <summary> lsl rd,rs,#i </summary>
        pop, // pop {rd}
        /// <summary> lsl rd,rs,#i </summary>
        popPC, // pop {rd,pc}

        // PseudoDMA
        /// <summary> lsl rd,rs,#i </summary>
        stmia, // stmia rd!,{rs}
        /// <summary> lsl rd,rs,#i </summary>
        ldmia, // ldmia rs!,{rd}

        // CondBranch
        /// <summary> lsl rd,rs,#i </summary>
        beq, // beq i
        /// <summary> lsl rd,rs,#i </summary>
        bne, // bne i
        /// <summary> lsl rd,rs,#i </summary>
        bcs, // bcs i
        /// <summary> lsl rd,rs,#i </summary>
        bcc, // bcc i
        /// <summary> lsl rd,rs,#i </summary>
        bmi, // bmi i
        /// <summary> lsl rd,rs,#i </summary>
        bpl, // bpl i
        /// <summary> lsl rd,rs,#i </summary>
        bvs, // bvs i
        /// <summary> lsl rd,rs,#i </summary>
        bvc, // bvc i
        /// <summary> lsl rd,rs,#i </summary>
        bhi, // bhi i
        /// <summary> lsl rd,rs,#i </summary>
        bls, // bls i
        /// <summary> lsl rd,rs,#i </summary>
        bge, // bge i
        /// <summary> lsl rd,rs,#i </summary>
        blt, // blt i
        /// <summary> bgt i </summary>
        bgt,
        /// <summary> ble i </summary>
        ble,

        // Interrupt
        /// <summary> swi #i </summary>
        swi,

        // Branch
        /// <summary> b i </summary>
        b,

        // BranchLink
        /// <summary> bl i </summary>
        bl,
    }

    public class Opcode
    {
        public uint Value;
        public OpcodeType Type;
        public SubOpcodeType SubType;
        public byte Cycles;
        public byte Size;
        public Flags AffectedFlags;
        public byte? RegD;
        public byte? RegS;
        public byte? RegR;
        public byte[] RegList;
        public ushort? Immediate;

        public Opcode(uint value)
        {
            byte[] data = BitConverter.GetBytes(value);
            Value = (uint)(data[3] | (data[2] << 8) | (data[1] << 16) | (data[0] << 24));
            Type = GetOpcodeType();
            Immediate = GetImmediate();
        }

        public OpcodeType GetOpcodeType()
        {
            switch (Bit.GetBits(Value, 32, 3))
            {
                case 0: // 0b000
                    if (Bit.GetBits(Value, 29, 2) == 3)
                        return OpcodeType.AddSub;
                    else
                        return OpcodeType.Shift;

                case 1: // 0b001
                    return OpcodeType.Immediate;

                case 2: // 0b010
                    if (Bit.GetBit(Value, 29) == 1)
                        return OpcodeType.LoadStoreReg;
                    else if (Bit.GetBit(Value, 28) == 1)
                        return OpcodeType.LoadPC;
                    else if (Bit.GetBit(Value, 27) == 0)
                    {
                        if (Bit.GetBits(Value, 26, 2) == 3)
                            return OpcodeType.BranchRegister;
                        else
                            return OpcodeType.Arithmetic;
                    }
                    else
                        return OpcodeType.HiRegister;

                case 3: // 0b011
                    return OpcodeType.LoadStoreImmediate;

                case 4: // 0b100
                    if (Bit.GetBit(Value, 29) == 1)
                        return OpcodeType.LoadStoreHalf;
                    else
                        return OpcodeType.LoadStoreSP;

                case 5: // 0b101
                    if (Bit.GetBit(Value, 29) == 0)
                        return OpcodeType.AddPCSP;
                    else
                    {
                        if (Bit.GetBits(Value, 28, 2) == 0)
                            return OpcodeType.AddSP;
                        else
                            return OpcodeType.Stack;
                    }

                case 6: // 0b110
                    if (Bit.GetBit(Value, 29) == 1)
                    {
                        if (Bit.GetBits(Value, 28, 4) == 7)
                            return OpcodeType.Interrupt;
                        else
                            return OpcodeType.CondBranch;
                    }
                    else
                        return OpcodeType.PseudoDMA;

                case 7: // 0b111
                    if (Bit.GetBit(Value, 28) == 1)
                        return OpcodeType.BranchLink;
                    else
                        return OpcodeType.Branch;

                default:
                    return OpcodeType.Undefined;
            }
        }
    
        public ushort? GetImmediate()
        {
            switch (Type)
            {
                case OpcodeType.AddSub:
                    return null; // TODO

                case OpcodeType.LoadStoreImmediate:
                case OpcodeType.LoadStoreHalf:
                case OpcodeType.Shift:
                    return (ushort?)Bit.GetBits(Value, 27, 5);
                case OpcodeType.Immediate:
                case OpcodeType.LoadPC:
                case OpcodeType.LoadStoreSP:
                case OpcodeType.AddSP:
                case OpcodeType.CondBranch:
                case OpcodeType.Interrupt:
                    return (ushort?)((Value & 0xFF0000) >> 16);
                case OpcodeType.Branch:
                case OpcodeType.BranchLink:
                    return (ushort?)((Value & 0x7FF0000) >> 16);
                default:
                    return null;
            }
        }

        public SubOpcodeType GetSubOpcodeType()
        {
            uint sub;

            switch (Type)
            {
                case OpcodeType.Shift:
                    sub = Bit.GetBits(Value, 29, 2);
                    if (sub == 0)
                        return SubOpcodeType.lslRI;
                    else if (sub == 1)
                        return SubOpcodeType.lsrRI;
                    else if (sub == 2)
                        return SubOpcodeType.asrRI;
                    else
                        return SubOpcodeType.Undefined;

                case OpcodeType.AddSub:
                    sub = Bit.GetBits(Value, 29, 2);
                    if (sub == 0)
                        return SubOpcodeType.addRR;
                    else if (sub == 1)
                        return SubOpcodeType.subRR;
                    else if (sub == 2)
                        return SubOpcodeType.addRI;
                    else
                        return SubOpcodeType.subRI;
            }
        }

        public byte? GetRegS()
        {
            switch (Type)
            {
                case OpcodeType.Shift:
                case OpcodeType.AddSub:
                case OpcodeType.Arithmetic:
                    return (byte?)Bit.GetBits(Value, 22, 3);
                case OpcodeType.HiRegister:
                case OpcodeType.BranchRegister:
                    return (SubType == SubOpcodeType.nop) ? null : (byte?)Bit.GetBits(Value, 21, 4);
                case OpcodeType.LoadStoreReg:
                    return (SubType != SubOpcodeType.strbR && SubType != SubOpcodeType.strbR && SubType != SubOpcodeType.strhR)
                        ? (byte?)Bit.GetBits(Value, 22, 3) : (byte?)Bit.GetBits(Value, 19, 3);
                case OpcodeType.LoadStoreImmediate:
                    return (SubType != SubOpcodeType.strbI && SubType != SubOpcodeType.strI)
                        ? (byte?)Bit.GetBits(Value, 19, 3) : (byte?)Bit.GetBits(Value, 22, 3);
                case OpcodeType.LoadStoreHalf:
                    return (SubType != SubOpcodeType.strhI)
                        ? (byte?)Bit.GetBits(Value, 22, 3) : (byte?)Bit.GetBits(Value, 19, 3);
                case OpcodeType.LoadStoreSP:
                    return (SubType != SubOpcodeType.ldrPC)
                        ? null : (byte?)Bit.GetBits(Value, 27, 3);
                case OpcodeType.PseudoDMA:
                    return (SubType != SubOpcodeType.stmia)? (byte?)Bit.GetBits(Value, 24, 10) : (byte?)Bit.GetBits(Value, 27, 3);
                default:
                    return null;
            }
        }
}

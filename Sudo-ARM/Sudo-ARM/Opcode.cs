using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using System.Runtime.CompilerServices;

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
        /// <summary> lsl rd,rs </summary>
        lslR,
        /// <summary> lsr rd,rs </summary>
        lsrR,
        /// <summary> asr rd,rs </summary>
        asrR,
        /// <summary> adc rd,rs </summary>
        adc,
        /// <summary> sbc rd,rs </summary>
        sbc,
        /// <summary> ror rd,rs </summary>
        ror,
        /// <summary> tst rd,rs </summary>
        tst,
        /// <summary> neg rd,rs </summary>
        neg,
        /// <summary> cmp rd,rs </summary>
        cmp,
        /// <summary> cmp rd,rs </summary>
        cmn,
        /// <summary> orr rd,rs </summary>
        orr,
        /// <summary> mul rd,rs </summary>
        mul,
        /// <summary> bic rd,rs </summary>
        bic,
        /// <summary> mvn rd, rs </summary>
        mvn,

        // HiRegister
        /// <summary> add rd,rs </summary>
        addR,
        /// <summary> cmp rd,rs </summary>
        cmpR,
        /// <summary> mov rd,rs </summary>
        movR,
        /// <summary> nop </summary>
        nop,

        // BranchRegister
        /// <summary> bx rs </summary>
        bx,

        // LoadPC
        /// <summary> ldr rd,[pc,#i] </summary>
        ldrPC,

        // LoadStoreReg
        /// <summary> str rs,[rd,rr] </summary>
        strR,
        /// <summary> strh rs,[rd,rr] </summary>
        strhR,
        /// <summary> strb rs,[rd,rr] </summary>
        strbR,
        /// <summary> ldsb rd,[rs,rr] </summary>
        ldsb,
        /// <summary> ldr rd,[rs,rr] </summary>
        ldrR,
        /// <summary> ldrh rd,[rs,rr] </summary>
        ldrhR,
        /// <summary> ldrb rd,[rs,rr] </summary>
        ldrbR,
        /// <summary> ldsh rd,[rs,rr] </summary>
        ldsh,

        // LoadStoreImmediate
        /// <summary> str rs,[rd,#i] </summary>
        strI,
        /// <summary> ldr rd,[rs,#i] </summary>
        ldrI,
        /// <summary> strb rs,[rd,#i] </summary>
        strbI,
        /// <summary> ldrb rd,[rs,#i] </summary>
        ldrbI,

        // LoadStoreHalf
        /// <summary> strh rs,[rd,#i] </summary>
        strhI,
        /// <summary> ldrh rd,[rs,#i] </summary>
        ldrhI,

        // LoadStoreSP
        /// <summary> str rs,[sp,#i] </summary>
        strSP,
        /// <summary> ldr rd,[sp,#i] </summary>
        ldrSP,

        // AddPCSP
        /// <summary> add rd,pc,#i</summary>
        addPCR,
        /// <summary> add rd,sp,#i </summary>
        addSPR,

        // AddSP
        /// <summary> add sp,#i </summary>
        addSP,
        /// <summary> add sp,-#i </summary>
        subSP,

        // Stack
        /// <summary> push {rd} </summary>
        push,
        /// <summary> push {rd,lr} </summary>
        pushLR,
        /// <summary> pop {rd} </summary>
        pop,
        /// <summary> pop {rd,pc} </summary>
        popPC,

        // PseudoDMA
        /// <summary> stmia rd!,{rs} </summary>
        stmia,
        /// <summary> ldmia rs!,{rd} </summary>
        ldmia,

        // CondBranch
        /// <summary> beq i </summary>
        beq,
        /// <summary> bne i </summary>
        bne,
        /// <summary> bcs i </summary>
        bcs,
        /// <summary> bcc i </summary>
        bcc,
        /// <summary> bmi i </summary>
        bmi,
        /// <summary> bpl i </summary>
        bpl,
        /// <summary> bvs i </summary>
        bvs,
        /// <summary> bvc i </summary>
        bvc,
        /// <summary> bhi i </summary>
        bhi,
        /// <summary> bls i </summary>
        bls,
        /// <summary> bge i </summary>
        bge,
        /// <summary> blt i </summary>
        blt,
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
        public CPUFlags AffectedFlags;
        public byte RegD;
        public byte RegS;
        public byte RegR;
        public byte[] RegList;
        public ushort Immediate;

        public Opcode(OpcodeType type, SubOpcodeType subType, byte regD, byte regS, byte regR, ushort immediate, byte[] regList)
        {
            Type = type;
            SubType = subType;
            RegD = regD;
            RegS = regS;
            RegR = regR;
            Immediate = immediate;
            RegList = regList;
            AffectedFlags = GetAffectedFlags();
            Value = Assemble(true);
            if (SubType != SubOpcodeType.bl) Value >>= 16;
        }

        public Opcode(uint value)
        {
            Value = Bit.SwitchEndian(value);
            Type = GetOpcodeType();
            SubType = GetSubOpcodeType();
            if (SubType == SubOpcodeType.Undefined)
            {
                Type = OpcodeType.Undefined;
                return;
            }
            Immediate = GetImmediate();
            ConvertImmediate();
            RegS = GetRegS();
            RegD = GetRegD();
            RegR = GetRegR();
            RegList = GetRegList();
            AffectedFlags = GetAffectedFlags();
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
                    if (Bit.GetBit(Value, 28) == 1)
                        return OpcodeType.LoadStoreReg;
                    else if (Bit.GetBit(Value, 27) == 1)
                        return OpcodeType.LoadPC;
                    else if (Bit.GetBit(Value, 26) == 1)
                    {
                        if (Bit.GetBits(Value, 27, 3) == 7)
                            return OpcodeType.BranchRegister;
                        else
                            return OpcodeType.HiRegister;
                    }
                    else
                        return OpcodeType.Arithmetic;

                case 3: // 0b011
                    return OpcodeType.LoadStoreImmediate;

                case 4: // 0b100
                    if (Bit.GetBit(Value, 28) == 0)
                        return OpcodeType.LoadStoreHalf;
                    else
                        return OpcodeType.LoadStoreSP;

                case 5: // 0b101
                    if (Bit.GetBit(Value, 28) == 0)
                        return OpcodeType.AddPCSP;
                    else
                    {
                        if (Bit.GetBits(Value, 28, 2) == 0)
                            return OpcodeType.AddSP;
                        else
                            return OpcodeType.Stack;
                    }

                case 6: // 0b110
                    if (Bit.GetBit(Value, 28) == 1)
                    {
                        if (Bit.GetBits(Value, 28, 4) == 15)
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
                    sub = Bit.GetBits(Value, 27, 2);
                    if (sub == 0)
                        return SubOpcodeType.addRR;
                    else if (sub == 1)
                        return SubOpcodeType.subRR;
                    else if (sub == 2)
                        return SubOpcodeType.addRI;
                    else
                        return SubOpcodeType.subRI;

                case OpcodeType.Immediate:
                    sub = Bit.GetBits(Value, 29, 2);
                    if (sub == 0)
                        return SubOpcodeType.movI;
                    else if (sub == 1)
                        return SubOpcodeType.cmpI;
                    else if (sub == 2)
                        return SubOpcodeType.addI;
                    else
                        return SubOpcodeType.subI;

                case OpcodeType.Arithmetic:
                    sub = Bit.GetBits(Value, 26, 4);
                    return sub switch
                    {
                        0 => SubOpcodeType.and,
                        1 => SubOpcodeType.eor,
                        2 => SubOpcodeType.lslR,
                        3 => SubOpcodeType.lsrR,
                        4 => SubOpcodeType.asrR,
                        5 => SubOpcodeType.adc,
                        6 => SubOpcodeType.sbc,
                        7 => SubOpcodeType.ror,
                        8 => SubOpcodeType.tst,
                        9 => SubOpcodeType.neg,
                        10 => SubOpcodeType.cmp,
                        11 => SubOpcodeType.cmn,
                        12 => SubOpcodeType.orr,
                        13 => SubOpcodeType.mul,
                        14 => SubOpcodeType.bic,
                        15 => SubOpcodeType.mvn,
                        _ => SubOpcodeType.Undefined,
                    };
                case OpcodeType.HiRegister:
                    sub = Bit.GetBits(Value, 26, 2);
                    if (sub == 0)
                        return SubOpcodeType.addR;
                    else if (sub == 1)
                        return SubOpcodeType.cmpR;
                    else if (sub == 2)
                    {
                        if (Value == 1186988032)
                            return SubOpcodeType.nop;
                        else
                            return SubOpcodeType.movR;
                    }
                    else
                        return SubOpcodeType.Undefined;

                case OpcodeType.BranchRegister:
                    return SubOpcodeType.bx;

                case OpcodeType.LoadPC:
                    return SubOpcodeType.ldrPC;

                case OpcodeType.LoadStoreReg:
                    sub = Bit.GetBits(Value, 28, 3);
                    return sub switch
                    {
                        0 => SubOpcodeType.strR,
                        1 => SubOpcodeType.strhR,
                        2 => SubOpcodeType.strbR,
                        3 => SubOpcodeType.ldsb,
                        4 => SubOpcodeType.ldrR,
                        5 => SubOpcodeType.ldrhR,
                        6 => SubOpcodeType.ldrbR,
                        7 => SubOpcodeType.ldsh,
                        _ => SubOpcodeType.Undefined,
                    };
                case OpcodeType.LoadStoreImmediate:
                    sub = Bit.GetBits(Value, 29, 2);
                    if (sub == 0)
                        return SubOpcodeType.strI;
                    else if (sub == 1)
                        return SubOpcodeType.ldrI;
                    else if (sub == 2)
                        return SubOpcodeType.strbI;
                    else
                        return SubOpcodeType.ldrbI;

                case OpcodeType.LoadStoreHalf:
                    sub = Bit.GetBit(Value, 27);
                    if (sub == 0)
                        return SubOpcodeType.strhI;
                    else
                        return SubOpcodeType.ldrhI;

                case OpcodeType.LoadStoreSP:
                    sub = Bit.GetBit(Value, 27);
                    if (sub == 0)
                        return SubOpcodeType.strSP;
                    else
                        return SubOpcodeType.ldrSP;

                case OpcodeType.AddPCSP:
                    sub = Bit.GetBit(Value, 27);
                    if (sub == 0)
                        return SubOpcodeType.addPCR;
                    else
                        return SubOpcodeType.addSPR;

                case OpcodeType.AddSP:
                    sub = Bit.GetBit(Value, 23);
                    if (sub == 0)
                        return SubOpcodeType.addSP;
                    else
                        return SubOpcodeType.subSP;

                case OpcodeType.Stack:
                    sub = Bit.GetBits(Value, 28, 2);
                    if (sub == 1)
                    {
                        if (Bit.GetBit(Value, 24) == 1)
                            return SubOpcodeType.pushLR;
                        else
                            return SubOpcodeType.push;
                    }
                    else if (sub == 3)
                    {
                        if (Bit.GetBit(Value, 24) == 1)
                            return SubOpcodeType.popPC;
                        else
                            return SubOpcodeType.pop;
                    }
                    else
                        return SubOpcodeType.Undefined;

                case OpcodeType.PseudoDMA:
                    sub = Bit.GetBit(Value, 27);
                    if (sub == 0)
                        return SubOpcodeType.stmia;
                    else
                        return SubOpcodeType.ldmia;

                case OpcodeType.CondBranch:
                    sub = Bit.GetBits(Value, 28, 4);
                    return sub switch
                    {
                        0 => SubOpcodeType.beq,
                        1 => SubOpcodeType.bne,
                        2 => SubOpcodeType.bcs,
                        3 => SubOpcodeType.bcc,
                        4 => SubOpcodeType.bmi,
                        5 => SubOpcodeType.bpl,
                        6 => SubOpcodeType.bvs,
                        7 => SubOpcodeType.bvc,
                        8 => SubOpcodeType.bhi,
                        9 => SubOpcodeType.bls,
                        10 => SubOpcodeType.bge,
                        11 => SubOpcodeType.blt,
                        12 => SubOpcodeType.bgt,
                        13 => SubOpcodeType.ble,
                        _ => SubOpcodeType.Undefined
                    };

                case OpcodeType.Interrupt:
                    return SubOpcodeType.swi;

                case OpcodeType.Branch:
                    return SubOpcodeType.b;

                case OpcodeType.BranchLink:
                    return SubOpcodeType.bl;

                default:
                    return SubOpcodeType.Undefined;
            }
        }

        public ushort GetImmediate()
        {
            switch (Type)
            {
                case OpcodeType.AddSub:
                    if (SubType == SubOpcodeType.addRR || SubType == SubOpcodeType.subRR)
                        return 0;
                    else
                        return (ushort)Bit.GetBits(Value, 25, 3);

                case OpcodeType.LoadStoreImmediate:
                case OpcodeType.LoadStoreHalf:
                case OpcodeType.Shift:
                    return (ushort)Bit.GetBits(Value, 27, 5);
                case OpcodeType.Immediate:
                case OpcodeType.LoadPC:
                case OpcodeType.LoadStoreSP:
                case OpcodeType.CondBranch:
                case OpcodeType.Interrupt:
                case OpcodeType.AddPCSP:
                    return (ushort)((Value & 0xFF0000) >> 16);
                case OpcodeType.AddSP:
                    return (ushort)Bit.GetBits(Value, 23, 7);
                case OpcodeType.Branch:
                case OpcodeType.BranchLink:
                    return (ushort)((Value & 0x7FF0000) >> 16);
                default:
                    return 0;
            }
        }

        public void ConvertImmediate()
        {
            switch (SubType)
            {
                case SubOpcodeType.beq:
                case SubOpcodeType.bne:
                case SubOpcodeType.bcs:
                case SubOpcodeType.bcc:
                case SubOpcodeType.bmi:
                case SubOpcodeType.bpl:
                case SubOpcodeType.bvs:
                case SubOpcodeType.bhi:
                case SubOpcodeType.bls:
                case SubOpcodeType.bge:
                case SubOpcodeType.blt:
                case SubOpcodeType.bgt:
                case SubOpcodeType.ble:
                case SubOpcodeType.b:
                case SubOpcodeType.bl:
                case SubOpcodeType.strhI:
                case SubOpcodeType.ldrhI:
                    Immediate <<= 1;
                    break;

                case SubOpcodeType.strI:
                case SubOpcodeType.ldrI:
                case SubOpcodeType.strSP:
                case SubOpcodeType.ldrSP:
                case SubOpcodeType.addPCR:
                case SubOpcodeType.addSPR:
                case SubOpcodeType.addSP:
                case SubOpcodeType.subSP:
                case SubOpcodeType.ldrPC:
                    Immediate <<= 2;
                    break;
            }
        }

        public byte GetRegS()
        {
            switch (Type)
            {
                case OpcodeType.Shift:
                case OpcodeType.AddSub:
                case OpcodeType.Arithmetic:
                    return (byte)Bit.GetBits(Value, 22, 3);
                case OpcodeType.HiRegister:
                case OpcodeType.BranchRegister:
                    return (byte)Bit.GetBits(Value, 23, 4);
                case OpcodeType.LoadStoreReg:
                case OpcodeType.LoadStoreImmediate:
                case OpcodeType.LoadStoreHalf:
                    return IsStore() ? (byte)Bit.GetBits(Value, 19, 3) : (byte)Bit.GetBits(Value, 22, 3);
                case OpcodeType.LoadStoreSP:
                    return IsStore() ? (byte)0 : (byte)Bit.GetBits(Value, 27, 3);
                default:
                    return 0;
            }
        }

        public byte GetRegD()
        {
            switch (Type)
            {
                case OpcodeType.Shift:
                case OpcodeType.AddSub:
                case OpcodeType.Arithmetic:
                    return (byte)Bit.GetBits(Value, 19, 3);

                case OpcodeType.Immediate:
                    return (byte)Bit.GetBits(Value, 27, 3);

                case OpcodeType.HiRegister:
                    return (byte)(Bit.GetBit(Value, 23) << 3 | Bit.GetBits(Value, 19, 3));

                case OpcodeType.LoadStoreReg:
                case OpcodeType.LoadStoreImmediate:
                case OpcodeType.LoadStoreHalf:
                    return IsStore() ? (byte)Bit.GetBits(Value, 22, 3) : (byte)Bit.GetBits(Value, 19, 3);

                case OpcodeType.LoadStoreSP:
                    return IsStore() ? (byte)0 : (byte)Bit.GetBits(Value, 27, 3);

                case OpcodeType.AddPCSP:
                    return (byte)Bit.GetBits(Value, 27, 3);

                default:
                    return 0;
            }
        }

        public byte GetRegR()
        {
            switch (Type)
            {
                case OpcodeType.AddSub:
                    if (SubType == SubOpcodeType.addRR || SubType == SubOpcodeType.subRR)
                        return (byte)Bit.GetBits(Value, 25, 3);
                    else
                        return 0;

                case OpcodeType.LoadStoreReg:
                    return (byte)Bit.GetBits(Value, 25, 3);

                default:
                    return 0;
            }
        }

        public byte[] GetRegList()
        {
            switch (Type)
            {
                case OpcodeType.Stack:
                case OpcodeType.PseudoDMA:
                    List<byte> regs = new(8);
                    for (byte i = 0; i < 8; i++)
                        if (Bit.GetBit(Value, (byte)(16 + i)) == 1)
                            regs.Add(i);
                    return regs.ToArray();

                default:
                    return null;
            }
        }

        public CPUFlags GetAffectedFlags()
        {
            return SubType switch
            {
                SubOpcodeType.movI or SubOpcodeType.mvn or SubOpcodeType.and or SubOpcodeType.tst or SubOpcodeType.bic or SubOpcodeType.orr or SubOpcodeType.eor => CPUFlags.Negative | CPUFlags.Zero,
                SubOpcodeType.lslRI or SubOpcodeType.lslR or SubOpcodeType.lsrRI or SubOpcodeType.lsrR or SubOpcodeType.asrRI or SubOpcodeType.asrR or SubOpcodeType.ror or SubOpcodeType.mul => CPUFlags.Negative | CPUFlags.Zero | CPUFlags.Carry,
                SubOpcodeType.addRI or SubOpcodeType.addI or SubOpcodeType.addRR or SubOpcodeType.adc or SubOpcodeType.subRI or SubOpcodeType.subI or SubOpcodeType.subRR or SubOpcodeType.sbc or SubOpcodeType.neg or SubOpcodeType.cmp or SubOpcodeType.cmpI or SubOpcodeType.cmpR or SubOpcodeType.cmn or SubOpcodeType.movR => CPUFlags.Negative | CPUFlags.Zero | CPUFlags.Carry | CPUFlags.Overflow,
                _ => CPUFlags.None,
            };
        }

        public uint Assemble(bool littleEndian)
        {
            uint result = 0;

            switch (Type)
            {
                case OpcodeType.Shift:
                    result = (uint)(RegD | RegS << 3 | Immediate << 6);
                    break;

                case OpcodeType.AddSub:
                    result = (uint)(RegD | RegS << 3);
                    if (SubType == SubOpcodeType.addRR || SubType == SubOpcodeType.subRR)
                        result |= (uint)(RegR << 6);
                    else
                        result |= (uint)(Immediate << 6);
                    break;

                case OpcodeType.Immediate:
                    result = (uint)(Immediate | RegD << 8);
                    break;

                case OpcodeType.Arithmetic:
                    result = (uint)(RegD | RegS << 3);
                    break;

                case OpcodeType.HiRegister:
                    result = (uint)((RegD & 7) | RegS << 3 | (RegD & 8));
                    break;

                case OpcodeType.BranchRegister:
                    result = (uint)(RegS << 3);
                    break;

                case OpcodeType.LoadPC:
                    result = (uint)(Immediate | RegD << 8);
                    break;

                case OpcodeType.LoadStoreReg:
                    if (IsStore())
                        result = (uint)(RegS | RegD << 3);
                    else
                        result = (uint)(RegD | RegS << 3);
                    result |= (uint)(RegR << 6);
                    break;

                case OpcodeType.LoadStoreImmediate:
                case OpcodeType.LoadStoreHalf:
                    if (IsStore())
                        result = (uint)(RegS | RegD << 3);
                    else
                        result = (uint)(RegD | RegS << 3);
                    result |= (uint)(Immediate << 6);
                    break;

                case OpcodeType.LoadStoreSP:
                    if (SubType == SubOpcodeType.strSP)
                        result = (uint)(Immediate | RegS << 8);
                    else
                        result = (uint)(Immediate | RegD << 8);
                    break;

                case OpcodeType.AddPCSP:
                    result = (uint)(Immediate | RegD << 8);
                    break;

                case OpcodeType.AddSP:
                    result = (uint)Immediate;
                    break;

                case OpcodeType.Stack:
                    result = Bit.RegListToBits(RegList);
                    break;

                case OpcodeType.PseudoDMA:
                    result = Bit.RegListToBits(RegList);
                    if (SubType == SubOpcodeType.stmia)
                        result |= (uint)(RegD << 8);
                    else
                        result |= (uint)(RegS << 8);
                    break;

                case OpcodeType.CondBranch:
                    result = (uint)Immediate;
                    break;

                case OpcodeType.Interrupt:
                    result = (uint)Immediate;
                    break;

                case OpcodeType.Branch:
                    result = (uint)Immediate;
                    break;

                // case OpcodeType.BranchLink: TODO
            }

            result = result | GetTypeBits() | GetSubTypeBits();

            if (littleEndian)
                result = Bit.SwitchEndian(result);
            return result;
        }

        public uint GetTypeBits()
        {
            return Type switch
            {
                OpcodeType.Shift => 0,
                OpcodeType.AddSub => 3 << 11,
                OpcodeType.Immediate => 1 << 13,
                OpcodeType.Arithmetic => 16 << 10,
                OpcodeType.HiRegister => 17 << 10,
                OpcodeType.BranchRegister => 142 << 7,
                OpcodeType.LoadPC => 9 << 11,
                OpcodeType.LoadStoreReg => 5 << 12,
                OpcodeType.LoadStoreImmediate => 3 << 13,
                OpcodeType.LoadStoreHalf => 8 << 12,
                OpcodeType.LoadStoreSP => 9 << 12,
                OpcodeType.AddPCSP => 10 << 12,
                OpcodeType.AddSP => 176 << 8,
                OpcodeType.Stack => 11 << 12,
                OpcodeType.PseudoDMA => 12 << 12,
                OpcodeType.CondBranch => 13 << 12,
                OpcodeType.Interrupt => 223 << 8,
                OpcodeType.Branch => 28 << 11,
                // case OpcodeType.BranchLink TODO
                _ => 0,
            };
        }

        public uint GetSubTypeBits()
        {
            return SubType switch
            {
                SubOpcodeType.lsrRI or SubOpcodeType.cmpI or SubOpcodeType.ldrI => 1 << 11,
                SubOpcodeType.asrRI or SubOpcodeType.addI or SubOpcodeType.strbI => 2 << 11,
                SubOpcodeType.subI or SubOpcodeType.ldrbI => 3 << 11,
                SubOpcodeType.subRR => 1 << 9,
                SubOpcodeType.addRI => 2 << 9,
                SubOpcodeType.subRI => 3 << 9,
                SubOpcodeType.eor => 1 << 6,
                SubOpcodeType.lslR => 2 << 6,
                SubOpcodeType.lsrR => 3 << 6,
                SubOpcodeType.asrR => 4 << 6,
                SubOpcodeType.adc => 5 << 6,
                SubOpcodeType.sbc => 6 << 6,
                SubOpcodeType.ror => 7 << 6,
                SubOpcodeType.tst => 8 << 6,
                SubOpcodeType.neg => 9 << 6,
                SubOpcodeType.cmp => 10 << 6,
                SubOpcodeType.cmn => 11 << 6,
                SubOpcodeType.orr => 12 << 6,
                SubOpcodeType.mul => 13 << 6,
                SubOpcodeType.bic => 14 << 6,
                SubOpcodeType.mvn => 15 << 6,
                SubOpcodeType.cmpR => 1 << 8,
                SubOpcodeType.nop or SubOpcodeType.movR => 2 << 8,
                SubOpcodeType.strhR => 1 << 9,
                SubOpcodeType.strbR => 2 << 9,
                SubOpcodeType.ldsb => 3 << 9,
                SubOpcodeType.ldrR => 4 << 9,
                SubOpcodeType.ldrhR => 5 << 9,
                SubOpcodeType.ldrbR => 6 << 9,
                SubOpcodeType.ldsh => 7 << 9,
                SubOpcodeType.ldrhI or SubOpcodeType.addSPR or SubOpcodeType.ldmia => 1 << 10,
                SubOpcodeType.subSP => 1 << 8,
                SubOpcodeType.push => 4 << 8,
                SubOpcodeType.pushLR => 5 << 8,
                SubOpcodeType.pop => 12 << 8,
                SubOpcodeType.popPC => 13 << 8,
                SubOpcodeType.bne => 1 << 8,
                SubOpcodeType.bcs => 2 << 8,
                SubOpcodeType.bcc => 3 << 8,
                SubOpcodeType.bmi => 4 << 8,
                SubOpcodeType.bpl => 5 << 8,
                SubOpcodeType.bvs => 6 << 8,
                SubOpcodeType.bvc => 7 << 8,
                SubOpcodeType.bhi => 8 << 8,
                SubOpcodeType.bls => 9 << 8,
                SubOpcodeType.bge => 10 << 8,
                SubOpcodeType.blt => 11 << 8,
                SubOpcodeType.bgt => 12 << 8,
                SubOpcodeType.ble => 13 << 8,
                _ => 0,
            };
        }

        public override string ToString()
        {
            switch (SubType)
            {
                case SubOpcodeType.lslRI:
                    return $"lsl r{RegD},r{RegS},#0x{Immediate:X}";
                case SubOpcodeType.lsrRI:
                    return $"lsr r{RegD},r{RegS},#0x{Immediate:X}";
                case SubOpcodeType.asrRI:
                    return $"asr r{RegD},r{RegS},#0x{Immediate:X}";

                case SubOpcodeType.addRR:
                    return $"add r{RegD},r{RegS},r{RegR}";
                case SubOpcodeType.subRR:
                    return $"sub r{RegD},r{RegS},r{RegR}";
                case SubOpcodeType.addRI:
                    return $"add r{RegD},r{RegS},#0x{Immediate:X}";
                case SubOpcodeType.subRI:
                    return $"sub r{RegD},r{RegS},#0x{Immediate:X}";

                case SubOpcodeType.movI:
                    return $"mov r{RegD},#0x{Immediate:X}";
                case SubOpcodeType.cmpI:
                    return $"cmp r{RegD},#0x{Immediate:X}";
                case SubOpcodeType.addI:
                    return $"add r{RegD},#0x{Immediate:X}";
                case SubOpcodeType.subI:
                    return $"sub r{RegD},#0x{Immediate:X}";

                case SubOpcodeType.and:
                    return $"and r{RegD},r{RegS}";
                case SubOpcodeType.eor:
                    return $"eor r{RegD},r{RegS}";
                case SubOpcodeType.lslR:
                    return $"lsl r{RegD},r{RegS}";
                case SubOpcodeType.lsrR:
                    return $"lsr r{RegD},r{RegS}";
                case SubOpcodeType.asrR:
                    return $"asr r{RegD},r{RegS}";
                case SubOpcodeType.adc:
                    return $"adc r{RegD},r{RegS}";
                case SubOpcodeType.sbc:
                    return $"sbc r{RegD},r{RegS}";
                case SubOpcodeType.ror:
                    return $"ror r{RegD},r{RegS}";
                case SubOpcodeType.tst:
                    return $"tst r{RegD},r{RegS}";
                case SubOpcodeType.neg:
                    return $"neg r{RegD},r{RegS}";
                case SubOpcodeType.cmp:
                    return $"cmp r{RegD},r{RegS}";
                case SubOpcodeType.cmn:
                    return $"cmn r{RegD},r{RegS}";
                case SubOpcodeType.orr:
                    return $"orr r{RegD},r{RegS}";
                case SubOpcodeType.mul:
                    return $"mul r{RegD},r{RegS}";
                case SubOpcodeType.bic:
                    return $"bic r{RegD},r{RegS}";
                case SubOpcodeType.mvn:
                    return $"mvn r{RegD},r{RegS}";

                case SubOpcodeType.addR:
                    return $"add r{RegD},r{RegS}";
                case SubOpcodeType.cmpR:
                    return $"cmp r{RegD},r{RegS}";
                case SubOpcodeType.movR:
                    return $"mov r{RegD},r{RegS}";
                case SubOpcodeType.nop:
                    return "nop";

                case SubOpcodeType.bx:
                    return $"bx r{RegS}";

                case SubOpcodeType.ldrPC:
                    return $"ldr r{RegD},[pc,#0x{Immediate:X}]";

                case SubOpcodeType.strR:
                    return $"str r{RegS},[r{RegD},r{RegR}]";
                case SubOpcodeType.strhR:
                    return $"strh r{RegS},[r{RegD},r{RegR}]";
                case SubOpcodeType.strbR:
                    return $"strb r{RegS},[r{RegD},r{RegR}]";
                case SubOpcodeType.ldsb:
                    return $"ldsb r{RegD},[r{RegS},r{RegR}]";
                case SubOpcodeType.ldrR:
                    return $"ldr r{RegD},[r{RegS},r{RegR}]";
                case SubOpcodeType.ldrhR:
                    return $"ldrh r{RegD},[r{RegS},r{RegR}]";
                case SubOpcodeType.ldrbR:
                    return $"ldrb r{RegD},[r{RegS},r{RegR}]";
                case SubOpcodeType.ldsh:
                    return $"ldsh r{RegD},[r{RegS},r{RegR}]";

                case SubOpcodeType.strI:
                    return $"str r{RegS},[r{RegD},#0x{Immediate:X}]";
                case SubOpcodeType.ldrI:
                    return $"ldr r{RegD},[r{RegS},#0x{Immediate:X}]";
                case SubOpcodeType.strbI:
                    return $"strb r{RegS},[r{RegD},#0x{Immediate:X}]";
                case SubOpcodeType.ldrbI:
                    return $"ldrb r{RegD},[r{RegS},#0x{Immediate:X}]";

                case SubOpcodeType.strhI:
                    return $"strh r{RegS},[r{RegD},#0x{Immediate:X}]";
                case SubOpcodeType.ldrhI:
                    return $"ldrh r{RegD},[r{RegS},#0x{Immediate:X}]";

                case SubOpcodeType.strSP:
                    return $"str r{RegS},[sp,#0x{Immediate:X}]";
                case SubOpcodeType.ldrSP:
                    return $"ldr r{RegD},[sp,#0x{Immediate:X}]";

                case SubOpcodeType.addPCR:
                    return $"add r{RegD},pc,#0x{Immediate:X}";
                case SubOpcodeType.addSPR:
                    return $"add r{RegD},sp,#0x{Immediate:X}";

                case SubOpcodeType.addSP:
                    return $"add sp,#0x{Immediate:X}";
                case SubOpcodeType.subSP:
                    return $"sub sp,#0x{Immediate:X}";

                case SubOpcodeType.push:
                    StringBuilder push = new("push {");
                    for (int i = 0; i < RegList.Length; i++)
                    {
                        push.Append('r').Append(RegList[i]);
                        if (i != RegList.Length - 1)
                            push.Append(',');
                    }

                    return push.Append('}').ToString();
                case SubOpcodeType.pushLR:
                    StringBuilder pushLR = new("push {");
                    for (int i = 0; i < RegList.Length; i++)
                        pushLR.Append('r').Append(RegList[i]).Append(',');
                    pushLR.Append("r14}");
                    return pushLR.ToString();
                case SubOpcodeType.pop:
                    StringBuilder pop = new("pop {");
                    for (int i = 0; i < RegList.Length; i++)
                    {
                        pop.Append('r').Append(RegList[i]);
                        if (i != RegList.Length - 1)
                            pop.Append(',');
                    }

                    return pop.Append('}').ToString();
                case SubOpcodeType.popPC:
                    StringBuilder popPC = new("pop {");
                    for (int i = 0; i < RegList.Length; i++)
                        popPC.Append('r').Append(RegList[i]).Append(',');
                    popPC.Append("pc}");
                    return popPC.ToString();

                case SubOpcodeType.stmia:
                    StringBuilder stmia = new StringBuilder("stmia r").Append(RegD).Append("!,{");
                    for (int i = 0; i < RegList.Length; i++)
                    {
                        stmia.Append('r').Append(RegList[i]);
                        if (i != RegList.Length - 1)
                            stmia.Append(',');
                    }
                    stmia.Append('}');
                    return stmia.ToString();
                case SubOpcodeType.ldmia:
                    StringBuilder ldmia = new StringBuilder("ldmia r").Append(RegD).Append("!,{");
                    for (int i = 0; i < RegList.Length; i++)
                    {
                        ldmia.Append('r').Append(RegList[i]);
                        if (i != RegList.Length - 1)
                            ldmia.Append(',');
                    }
                    ldmia.Append('}');
                    return ldmia.ToString();

                case SubOpcodeType.beq:
                    return $"beq {Immediate:X}";
                case SubOpcodeType.bne:
                    return $"bne {Immediate:X}";
                case SubOpcodeType.bcs:
                    return $"bcs {Immediate:X}";
                case SubOpcodeType.bcc:
                    return $"bcc {Immediate:X}";
                case SubOpcodeType.bmi:
                    return $"bmi {Immediate:X}";
                case SubOpcodeType.bpl:
                    return $"bpl {Immediate:X}";
                case SubOpcodeType.bvs:
                    return $"bvs {Immediate:X}";
                case SubOpcodeType.bvc:
                    return $"bvc {Immediate:X}";
                case SubOpcodeType.bhi:
                    return $"bhi {Immediate:X}";
                case SubOpcodeType.bls:
                    return $"bls {Immediate:X}";
                case SubOpcodeType.bge:
                    return $"bge {Immediate:X}";
                case SubOpcodeType.blt:
                    return $"blt {Immediate:X}";
                case SubOpcodeType.bgt:
                    return $"bgt {Immediate:X}";
                case SubOpcodeType.ble:
                    return $"ble {Immediate:X}";

                case SubOpcodeType.swi:
                    return $"swi #0x{Immediate:X}";

                case SubOpcodeType.b:
                    return $"b {Immediate:X}";

                case SubOpcodeType.bl:
                    return $"bl {Immediate:X}";

                default:
                    return "Undefined";
            }
        }

        public bool IsStore()
        {
            return SubType switch
            {
                SubOpcodeType.strR => true,
                SubOpcodeType.strhR => true,
                SubOpcodeType.strbR => true,
                SubOpcodeType.strI => true,
                SubOpcodeType.strbI => true,
                SubOpcodeType.strhI => true,
                SubOpcodeType.stmia => true,
                SubOpcodeType.strSP => true,
                _ => false
            };
        }

        public bool IsLoad()
        {
            return SubType switch
            {
                SubOpcodeType.ldrR => true,
                SubOpcodeType.ldrhR => true,
                SubOpcodeType.ldrbR => true,
                SubOpcodeType.ldsb => true,
                SubOpcodeType.ldsh => true,
                SubOpcodeType.ldrI => true,
                SubOpcodeType.ldrbI => true,
                SubOpcodeType.ldrhI => true,
                SubOpcodeType.ldmia => true,
                SubOpcodeType.ldrSP => true,
                _ => false
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Is32Bits() => SubType == SubOpcodeType.bl;

        public static Opcode Parse(string text)
        {
            OpcodeType type = OpcodeType.Undefined;
            SubOpcodeType subType = SubOpcodeType.Undefined;
            byte regS = 0;
            byte regD = 0;
            byte regR = 0;
            byte[] regList = null;
            ushort immediate = 0;

            string[] data = text.Split(new char[] { ' ', ',' } );
            int count = data.Length;

            switch (data[0])
            {
                case "lsl":
                    if (count == 3)
                    {
                        type = OpcodeType.Arithmetic;
                        subType = SubOpcodeType.lslR;
                    }
                    else
                    {
                        type = OpcodeType.Shift;
                        subType = SubOpcodeType.lslRI;
                    }
                    break;

                case "lsr":
                    if (count == 3)
                    {
                        type = OpcodeType.Arithmetic;
                        subType = SubOpcodeType.lsrR;
                    }
                    else
                    {
                        type = OpcodeType.Shift;
                        subType = SubOpcodeType.lsrRI;
                    }
                    break;

                case "asr":
                    if (count == 3)
                    {
                        type = OpcodeType.Arithmetic;
                        subType = SubOpcodeType.asrR;
                    }
                    else
                    {
                        type = OpcodeType.Shift;
                        subType = SubOpcodeType.asrRI;
                    }
                    break;

                case "add":
                    if (count < 2)
                        throw new ParsingException(ParsingErrorID.IncompleteOpcode, "Incomplete opcode");
                    if (data[1].Contains('r'))
                    {
                        regD = ParseReg(data[1], 3);
                        if(count == 3)
                        {
                            if(data[2].Contains('r'))
                            {
                                type = OpcodeType.HiRegister;
                                subType = SubOpcodeType.addR;
                                regS = ParseReg(data[2], 3);
                            }
                            else
                            {
                                type = OpcodeType.Immediate;
                                subType = SubOpcodeType.addI;
                                immediate = ParseImmediate(data[2], 8, 0);
                            }
                        }
                        else
                        {
                            CheckIsOpcodeComplete(data, 4);
                            if (data[2].Contains('r'))
                            {
                                type = OpcodeType.AddSub;
                                regS = ParseReg(data[2], 3);
                                if (data[3].Contains('r'))
                                {
                                    subType = SubOpcodeType.addRR;
                                    regR = ParseReg(data[3], 3);
                                }
                                else
                                {
                                    subType = SubOpcodeType.addRI;
                                    immediate = ParseImmediate(data[3], 3, 0);
                                }
                            }
                            else
                            {
                                type = OpcodeType.AddPCSP;
                                immediate = ParseImmediate(data[3], 8, 4);
                                if (data[2] == "pc")
                                    subType = SubOpcodeType.addPCR;
                                else if (data[2] == "sp")
                                    subType = SubOpcodeType.addSPR;
                            }
                        }
                    }
                    else if (data[1] == "sp")
                    {
                        CheckIsOpcodeComplete(data, 3);
                        type = OpcodeType.AddSP;
                        subType = SubOpcodeType.addSP;
                        immediate = ParseImmediate(data[2], 7, 4);
                    }
                    break;

                case "sub":
                    if (count == 4)
                    {
                        type = OpcodeType.AddSub;
                        regD = ParseReg(data[1], 3);
                        regS = ParseReg(data[2], 3);
                        if (data[3].Contains('r'))
                        {
                            subType = SubOpcodeType.subRR;
                            regR = ParseReg(data[3], 3);
                        }
                        else
                        {
                            subType = SubOpcodeType.subRI;
                            immediate = ParseImmediate(data[3], 3, 0);
                        }
                    }
                    else
                    {
                        CheckIsOpcodeComplete(data, 3);
                        
                        if (data[1] == "sp")
                        {
                            type = OpcodeType.AddSP;
                            subType = SubOpcodeType.subSP;
                            immediate = ParseImmediate(data[2], 8, 4);
                        }
                        else
                        {
                            type = OpcodeType.Immediate;
                            subType = SubOpcodeType.subI;
                            regD = ParseReg(data[1], 3);
                            immediate = ParseImmediate(data[2], 8, 0);
                        }
                    }
                    break;

                case "mov":
                    CheckIsOpcodeComplete(data, 3);
                    if (data[2].Contains('r'))
                    {
                        type = OpcodeType.HiRegister;
                        subType = SubOpcodeType.movR;
                        regD = ParseReg(data[1], 4);
                        regS = ParseReg(data[2], 4);
                    }
                    else
                    {
                        type = OpcodeType.Immediate;
                        subType = SubOpcodeType.movI;
                        regD = ParseReg(data[1], 3);
                        immediate = ParseImmediate(data[2], 8, 0);
                    }
                    break;

                case "cmp":
                    CheckIsOpcodeComplete(data, 3);
                    regD = ParseReg(data[1], 3);
                    if (data[2].Contains('r'))
                    {
                        type = OpcodeType.Immediate;
                        subType = SubOpcodeType.cmpI;
                        regS = ParseReg(data[2], 3);
                    }
                    else
                    {
                        type = OpcodeType.HiRegister;
                        subType = SubOpcodeType.cmpR;
                        immediate = ParseImmediate(data[2], 8, 0);
                    }
                    break;

                case "and":
                    CheckIsOpcodeComplete(data, 3);
                    type = OpcodeType.Arithmetic;
                    subType = SubOpcodeType.and;
                    regD = ParseReg(data[1], 3);
                    regS = ParseReg(data[2], 3);
                    break;
                case "eor":
                    CheckIsOpcodeComplete(data, 3);
                    type = OpcodeType.Arithmetic;
                    subType = SubOpcodeType.eor;
                    regD = ParseReg(data[1], 3);
                    regS = ParseReg(data[2], 3);
                    break;

                case "adc":
                    CheckIsOpcodeComplete(data, 3);
                    type = OpcodeType.Arithmetic;
                    subType = SubOpcodeType.adc;
                    regD = ParseReg(data[1], 3);
                    regS = ParseReg(data[2], 3);
                    break;

                case "sbc":
                    CheckIsOpcodeComplete(data, 3);
                    type = OpcodeType.Arithmetic;
                    subType = SubOpcodeType.sbc;
                    regD = ParseReg(data[1], 3);
                    regS = ParseReg(data[2], 3);
                    break;

                case "ror":
                    CheckIsOpcodeComplete(data, 3);
                    type = OpcodeType.Arithmetic;
                    subType = SubOpcodeType.ror;
                    regD = ParseReg(data[1], 3);
                    regS = ParseReg(data[2], 3);
                    break;

                case "tst":
                    CheckIsOpcodeComplete(data, 3);
                    type = OpcodeType.Arithmetic;
                    subType = SubOpcodeType.tst;
                    regD = ParseReg(data[1], 3);
                    regS = ParseReg(data[2], 3);
                    break;

                case "neg":
                    CheckIsOpcodeComplete(data, 3);
                    type = OpcodeType.Arithmetic;
                    subType = SubOpcodeType.neg;
                    regD = ParseReg(data[1], 3);
                    regS = ParseReg(data[2], 3);
                    break;

                case "cmn":
                    CheckIsOpcodeComplete(data, 3);
                    type = OpcodeType.Arithmetic;
                    subType = SubOpcodeType.cmn;
                    regD = ParseReg(data[1], 3);
                    regS = ParseReg(data[2], 3);
                    break;

                case "orr":
                    CheckIsOpcodeComplete(data, 3);
                    type = OpcodeType.Arithmetic;
                    subType = SubOpcodeType.orr;
                    regD = ParseReg(data[1], 3);
                    regS = ParseReg(data[2], 3);
                    break;

                case "mul":
                    CheckIsOpcodeComplete(data, 3);
                    type = OpcodeType.Arithmetic;
                    subType = SubOpcodeType.mul;
                    regD = ParseReg(data[1], 3);
                    regS = ParseReg(data[2], 3);
                    break;

                case "bic":
                    CheckIsOpcodeComplete(data, 3);
                    type = OpcodeType.Arithmetic;
                    subType = SubOpcodeType.bic;
                    regD = ParseReg(data[1], 3);
                    regS = ParseReg(data[2], 3);
                    break;

                case "mvn":
                    CheckIsOpcodeComplete(data, 3);
                    type = OpcodeType.Arithmetic;
                    subType = SubOpcodeType.mvn;
                    regD = ParseReg(data[1], 3);
                    regS = ParseReg(data[2], 3);
                    break;

                case "nop":
                    type = OpcodeType.HiRegister;
                    subType = SubOpcodeType.nop;
                    break;

                case "bx":
                    CheckIsOpcodeComplete(data, 2);
                    regS = ParseReg(data[1], 4);
                    type = OpcodeType.BranchRegister;
                    subType = SubOpcodeType.bx;
                    break;

                case "str":
                    // TODO
                    break;

                case "strh":
                    // TODO
                    break;

                case "strb":
                    // TODO
                    break;

                case "ldr":
                    // TODO
                    break;

                case "ldrh":
                    // TODO
                    break;

                case "ldrb":
                    // TODO
                    break;

                case "ldsh":
                case "ldrsh":
                    // TODO
                    break;

                case "ldsb":
                case "ldrsb":
                    // TODO
                    break;

                case "push":
                    type = OpcodeType.Stack;
                    if (data.Contains("lr") || data.Contains("r14"))
                        subType = SubOpcodeType.pushLR;
                    else
                        subType = SubOpcodeType.push;
                    break;

                case "pop":
                    type = OpcodeType.Stack;
                    if (data.Contains("pc") || data.Contains("r15"))
                        subType = SubOpcodeType.popPC;
                    else
                        subType = SubOpcodeType.pop;
                    regList = ParseRegList(data.Select(D => D).Where(D => D != "pop").ToArray(), 3);
                    break;

                case "stmia":
                    type = OpcodeType.PseudoDMA;
                    subType = SubOpcodeType.stmia;
                    break;

                case "ldmia":
                    type = OpcodeType.PseudoDMA;
                    subType = SubOpcodeType.ldmia;
                    break;

                // TODO cond branch

                case "swi":
                    CheckIsOpcodeComplete(data, 2);
                    immediate = ParseImmediate(data[1], 8, 0);
                    type = OpcodeType.Interrupt;
                    subType = SubOpcodeType.swi;
                    break;

                case "b":
                    type = OpcodeType.Branch;
                    subType = SubOpcodeType.b;
                    break;

                case "bl":
                    type = OpcodeType.BranchLink;
                    subType = SubOpcodeType.bl;
                    break;
            }

            if (type != OpcodeType.Undefined && subType != SubOpcodeType.Undefined)
                return new Opcode(type, subType, regD, regS, regR, immediate, regList);
            else
                throw new ParsingException(ParsingErrorID.ParseFail, "Could not parse Opcode");
        }

        private static void CheckIsOpcodeComplete(string[] array, int length)
        {
            if (array.Length != length)
                throw new ParsingException(ParsingErrorID.IncompleteOpcode, "Opcode isn't complete");
        }

        private static byte ParseReg(string text, byte size)
        {
            if (!Regex.Match(text, @"r\d\d?").Success)
                throw new ParsingException(ParsingErrorID.InvalidRegisterFormat, "Couldn't parse register, regex failed");

            if (!byte.TryParse(text.Remove(0, 1), out byte value))
                throw new ParsingException(ParsingErrorID.SyntaxError, "Invalid syntax for the register");

            if (!Bit.CheckFits(value, size))
                throw new ParsingException(ParsingErrorID.RegisterNumberTooHigh, "Couldn't parse register, doesn't fit in the opcode (too high)");

            return value;
        }

        private static byte[] ParseRegList(string[] array, byte size)
        {
            List<byte> data = new List<byte>(array.Length);
            foreach (string S in array)
                if (S != "lr" && S != "r14" && S != "pc" && S != "r15")
                    data.Add(ParseReg(S, size));
            return data.ToArray();
        }

        private static ushort ParseImmediate(string text, byte size, byte multiple)
        {
            if (!Regex.Match(text, "#?(0[xX])?[0-9a-fA-F]+h?").Success)
                throw new ParsingException(ParsingErrorID.InvalidImmediateFormat, "Couldn't parse immediate, regex failed");

            if (!ushort.TryParse(text.Trim(), System.Globalization.NumberStyles.AllowHexSpecifier, null, out ushort value))
                throw new ParsingException(ParsingErrorID.SyntaxError, "Invalid syntax for the immediate");

            if (multiple != 0 && value % multiple != 0)
                throw new ParsingException(ParsingErrorID.ImmediateMultipleError, $"Immediate value must be a multiple of {multiple}");

            if (!Bit.CheckFits(value, size))
                throw new ParsingException(ParsingErrorID.ImmediateTooHigh, $"Immediate value doesn't fit in the opcode (must be below #0x{Convert.ToString((uint)(Math.Pow(2, size) - 1), 16).ToUpper()})");

            return value;
        }

        public static implicit operator bool(Opcode O) => O is not null;
    }
}

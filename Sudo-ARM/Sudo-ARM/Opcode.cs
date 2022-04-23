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
            SubType = GetSubOpcodeType();
            Immediate = GetImmediate();
            RegS = GetRegS();
            RegD = GetRegD();
            RegR = GetRegR();
            RegList = GetRegList();
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
                    else if (Bit.GetBit(Value, 27) == 1)
                        return OpcodeType.LoadPC;
                    else if (Bit.GetBit(Value, 26) == 1)
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
                    if (Bit.GetBit(Value, 28) == 1)
                    {
                        if (Bit.GetBits(Value, 27, 4) == 7)
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
                        if (Value == 3225812992)
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
                    sub = Bit.GetBit(Value, 28);
                    if (sub == 0)
                        return SubOpcodeType.strhI;
                    else
                        return SubOpcodeType.ldrhI;

                case OpcodeType.LoadStoreSP:
                    sub = Bit.GetBit(Value, 28);
                    if (sub == 0)
                        return SubOpcodeType.strSP;
                    else
                        return SubOpcodeType.ldrSP;

                case OpcodeType.AddPCSP:
                    sub = Bit.GetBit(Value, 28);
                    if (sub == 0)
                        return SubOpcodeType.addPCR;
                    else
                        return SubOpcodeType.addSPR;

                case OpcodeType.AddSP:
                    sub = Bit.GetBit(Value, 24);
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

        public ushort? GetImmediate()
        {
            switch (Type)
            {
                case OpcodeType.AddSub:
                    if (SubType == SubOpcodeType.addRR || SubType == SubOpcodeType.subRR)
                        return null;
                    else
                        return (ushort?)Bit.GetBits(Value, 24, 3);

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
                    return (byte?)Bit.GetBits(Value, 23, 4);
                case OpcodeType.LoadStoreReg:
                    return IsStore() ? (byte?)Bit.GetBits(Value, 22, 3) : (byte?)Bit.GetBits(Value, 19, 3);
                case OpcodeType.LoadStoreImmediate:
                    return IsStore() ? (byte?)Bit.GetBits(Value, 19, 3) : (byte?)Bit.GetBits(Value, 22, 3);
                case OpcodeType.LoadStoreHalf:
                    return IsStore() ? (byte?)Bit.GetBits(Value, 22, 3) : (byte?)Bit.GetBits(Value, 19, 3);
                case OpcodeType.LoadStoreSP:
                    return IsStore() ? null : (byte?)Bit.GetBits(Value, 27, 3);
                default:
                    return null;
            }
        }

        public byte? GetRegD()
        {
            switch (Type)
            {
                case OpcodeType.Shift:
                case OpcodeType.AddSub:
                case OpcodeType.Arithmetic:
                    return (byte?)Bit.GetBits(Value, 19, 3);

                case OpcodeType.Immediate:
                    return (byte?)Bit.GetBits(Value, 27, 3);

                case OpcodeType.HiRegister:
                    return (byte?)(Bit.GetBit(Value, 23) << 3 | Bit.GetBits(Value, 19, 3));

                case OpcodeType.LoadStoreReg:
                case OpcodeType.LoadStoreImmediate:
                case OpcodeType.LoadStoreHalf:
                    return IsStore() ? (byte?)Bit.GetBits(Value, 22, 3) : (byte?)Bit.GetBits(Value, 19, 3);

                case OpcodeType.LoadStoreSP:
                    return IsStore() ? null : (byte?)Bit.GetBits(Value, 27, 3);

                case OpcodeType.AddPCSP:
                    return (byte?)Bit.GetBits(Value, 27, 3);

                default:
                    return null;
            }
        }

        public byte? GetRegR()
        {
            switch (Type)
            {
                case OpcodeType.AddSub:
                    if (SubType == SubOpcodeType.addRR || SubType == SubOpcodeType.subRR)
                        return (byte?)Bit.GetBits(Value, 24, 3);
                    else
                        return null;

                case OpcodeType.LoadStoreReg:
                    return (byte?)Bit.GetBits(Value, 24, 3);

                default:
                    return null;
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

        public override string ToString()
        {
            switch (SubType)
            {
                case SubOpcodeType.lslRI:
                    return $"lsl r{RegD},r{RegS},#0x{Immediate:X}";
                case SubOpcodeType.lsrRI:
                    return $"lsr r{RegD},r{RegS},#0x{Immediate:X}";
                case SubOpcodeType.asrRI:
                    return $"r{RegD},r{RegS},#0x{Immediate:X}";

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
                    return $"ldr r{RegD},[pc,#0x{Immediate:X}";

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
                    return $"strh r{RegS},[r{RegD},#0x{Immediate}]";
                case SubOpcodeType.ldrhI:
                    return $"ldrh r{RegD},[r{RegS},#0x{Immediate}]";

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
                    StringBuilder push = new("push ");
                    for (int i = 0; i < RegList.Length; i++)
                    {
                        push.Append('r').Append(RegList[i]);
                        if (i != RegList.Length - 1)
                            push.Append(',');
                    }

                    return push.ToString();
                case SubOpcodeType.pushLR:
                    StringBuilder pushLR = new("push ");
                    for (int i = 0; i < RegList.Length; i++)
                        pushLR.Append('r').Append(RegList[i]).Append(',');
                    pushLR.Append("r14");
                    return pushLR.ToString();
                case SubOpcodeType.pop:
                    StringBuilder pop = new("pop ");
                    for (int i = 0; i < RegList.Length; i++)
                    {
                        pop.Append('r').Append(RegList[i]);
                        if (i != RegList.Length - 1)
                            pop.Append(',');
                    }

                    return pop.ToString();
                case SubOpcodeType.popPC:
                    StringBuilder popPC = new("push ");
                    for (int i = 0; i < RegList.Length; i++)
                        popPC.Append('r').Append(RegList[i]).Append(',');
                    popPC.Append("pc");
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
    }
}

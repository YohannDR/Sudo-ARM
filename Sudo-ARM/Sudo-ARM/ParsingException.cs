using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Sudo_ARM
{
    public enum ParsingErrorID
    {
        ParseFail,
        RegexFail,
        SyntaxError,
        IncompleteOpcode,
        InvalidRegisterFormat,
        RegisterNumberTooHigh,
        InvalidImmediateFormat,
        ImmediateTooHigh,
        ImmediateMultipleError
    }

    public class ParsingException : Exception
    {
        public ParsingErrorID ErrorID;

        public ParsingException(ParsingErrorID errorID, string message) : base(message)
            => ErrorID = errorID;

        public void ToMessageBox()
            => MessageBox.Show(Message, $"Parsing Exception N°{(uint)ErrorID}", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public override string ToString()
            => $"Parsing Exception {(uint)ErrorID} | {Regex.Replace(ErrorID.ToString(), "(?<=[A-Za-z])(?=[A-Z][a-z])|(?<=[a-z0-9])(?=[0-9]?[A-Z])", " ")} : {Message}";
    }
}

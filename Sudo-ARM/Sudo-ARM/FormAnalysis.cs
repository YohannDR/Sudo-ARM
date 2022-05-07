using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace Sudo_ARM
{
    public partial class FormAnalysis : Form
    {
        FormMain Master;

        public FormAnalysis(FormMain master)
        {
            Master = master;
            InitializeComponent();
        }

        private void btnAnalyse_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch S = Stopwatch.StartNew();
                Opcode O = Opcode.Parse(tbCode.Text);
                S.Stop();
                rtbLog.AppendText(O.ToString());
                rtbLog.AppendText("\n");
            }
            catch (ParsingException error)
            {
                StringBuilder text = new StringBuilder('\n').Append(error.ToString()).Append('\n');
                rtbLog.AppendText(text.ToString());
            }
        }
    }
}

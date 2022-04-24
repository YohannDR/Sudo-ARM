using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudo_ARM
{
    public partial class FormDisassemble : Form
    {
        FormMain Master;

        public FormDisassemble(FormMain master)
        {
            Master = master;
            InitializeComponent();
        }

        private void FormDisassemble_Load(object sender, EventArgs e)
        {

        }
    }
}

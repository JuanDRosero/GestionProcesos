using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionProcesos
{
    public partial class FormRR : Form
    {
        private Form padre;
        public FormRR(Form padre)
        {
            InitializeComponent();
            this.padre = padre;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void FormRR_FormClosed(object sender, FormClosedEventArgs e)
        {
            padre.Show();
        }
    }
}

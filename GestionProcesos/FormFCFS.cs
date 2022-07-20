using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProcesosLib;

namespace GestionProcesos
{
    public partial class FormFCFS : Form
    {
        private Graphics myGraphics;
        int x=0;
        private IAcciones alg = new FCFS();
        private Form padre;
        public FormFCFS(Form padre)
        {
            InitializeComponent();
            timer1.Start();
            this.padre = padre;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            Pen pen2 = new Pen(Color.FromArgb(229, 229, 229, 229));
            e.Graphics.DrawLine(pen, 20, 10, 20, 600);//x
            e.Graphics.DrawLine(pen, 20, 600, 800, 600);//y

            //Lineas 
            for (int i = 0; i < 53; i++)
            {
                e.Graphics.DrawLine(pen2, 20+i*15, 10, 20+i*15, 600);
            }
        }

        private void Dibujar(Dictionary<string, Estado> procesos)
        {
            
            myGraphics = panel1.CreateGraphics();

            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();

            int cont = 1;
            foreach (var item in procesos)
            {
                switch (item.Value)
                {
                    case Estado.Activo:
                        myBrush.Color = System.Drawing.Color.Green;
                        break;

                    case Estado.Interrumpido:
                        myBrush.Color = System.Drawing.Color.Red;
                        break;

                    case Estado.Espera:
                        myBrush.Color = System.Drawing.Color.Gray;
                        break;

                    default:
                        myBrush.Color = System.Drawing.Color.Gray;
                        break;
                }

                switch (item.Key)
                {
                    case "Notepad":
                        myGraphics.FillRectangle(myBrush, new Rectangle(20 + x, 600 - 73 * 1, 15, 61));
                        break;

                    case "Word":
                        myGraphics.FillRectangle(myBrush, new Rectangle(20 + x, 600 - 73 * 2, 15, 61));
                        break;

                    case "Excel":
                        myGraphics.FillRectangle(myBrush, new Rectangle(20 + x, 600 - 73 * 3, 15, 61));
                        break;

                    case "AutoCAD":
                        myGraphics.FillRectangle(myBrush, new Rectangle(20 + x, 600 - 73 * 4, 15, 61));
                        break;

                    case "Calculadora":
                        myGraphics.FillRectangle(myBrush, new Rectangle(20 + x, 600 - 73 * 5, 15, 61));
                        break;

                    case "Windows Defender":
                        myGraphics.FillRectangle(myBrush, new Rectangle(20 + x, 600 - 73 * 6, 15, 61));
                        break;

                    default:
                        break;
                }
                cont++;
            }
            myBrush.Dispose();
            formGraphics.Dispose();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            alg.Ejecutar();
            Dibujar(alg.GetProcesos());
            x += 15;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            alg.AgregarProceso(1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            alg.AgregarProceso(2);

            //Estado.
        }

        private void button9_Click(object sender, EventArgs e)
        {
            alg.AgregarProceso(3);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            alg.AgregarProceso(4);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            alg.AgregarProceso(5);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            alg.AgregarProceso(6);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            alg.TerminarProceso(1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            alg.TerminarProceso(2);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            alg.TerminarProceso(3);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            alg.TerminarProceso(4);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            alg.TerminarProceso(5);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            alg.TerminarProceso(6);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            alg.InterumpirProceso(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            alg.InterumpirProceso(2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            alg.InterumpirProceso(3);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            alg.InterumpirProceso(4);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            alg.InterumpirProceso(5);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            alg.InterumpirProceso(6);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            alg.ReanudarProceso(1);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            alg.ReanudarProceso(2);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            alg.ReanudarProceso(3);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            alg.ReanudarProceso(4);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            alg.ReanudarProceso(5);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            alg.ReanudarProceso(6);
        }

        private void FormFCFS_FormClosed(object sender, FormClosedEventArgs e)
        {
            padre.Show();
        }
    }
}

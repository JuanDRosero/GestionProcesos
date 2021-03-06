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
using Microsoft.VisualBasic;

namespace GestionProcesos
{
    public partial class FormSJF : Form
    {
        private Graphics myGraphics;
        int x = 0;
        private IAcciones alg = new SJF();
        Dictionary<string, int> dict = new Dictionary<string, int>();
        private Form padre;
        public FormSJF(Form padre)
        {
            InitializeComponent();
            timer1.Start();
            this.padre = padre;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int valor = Int32.Parse(Interaction.InputBox("Inserte la duración: ", "Inserte valor: "));
                if (valor<=1)
                {
                    throw new Exception("Valor no valido");
                }
                alg.AgregarProceso(1,valor);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Valor no valido", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }  
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
                e.Graphics.DrawLine(pen2, 20 + i * 15, 10, 20 + i * 15, 600);
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
                        myGraphics.FillRectangle(myBrush, new Rectangle(20 + x, panel1.Size.Height - 73 * 1, 15, 61));
                        break;

                    case "Word":
                        myGraphics.FillRectangle(myBrush, new Rectangle(20 + x, panel1.Size.Height - 73 * 2, 15, 61));
                        break;

                    case "Excel":
                        myGraphics.FillRectangle(myBrush, new Rectangle(20 + x, panel1.Size.Height - 73 * 3, 15, 61));
                        break;

                    case "AutoCAD":
                        myGraphics.FillRectangle(myBrush, new Rectangle(20 + x, panel1.Size.Height - 73 * 4, 15, 61));
                        break;

                    case "Calculadora":
                        myGraphics.FillRectangle(myBrush, new Rectangle(20 + x, panel1.Size.Height - 73 * 5, 15, 61));
                        break;

                    case "Windows Defender":
                        myGraphics.FillRectangle(myBrush, new Rectangle(20 + x, panel1.Size.Height - 73 * 6, 15, 61));
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

        private void FormSJF_FormClosed(object sender, FormClosedEventArgs e)
        {
            padre.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int valor = Int32.Parse(Interaction.InputBox("Inserte la duración: ", "Inserte valor: "));
                if (valor <= 1)
                {
                    throw new Exception("Valor no valido");
                }
                alg.AgregarProceso(2, valor);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Valor no valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                int valor = Int32.Parse(Interaction.InputBox("Inserte la duración: ", "Inserte valor: "));
                if (valor <= 1)
                {
                    throw new Exception("Valor no valido");
                }
                alg.AgregarProceso(3, valor);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Valor no valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                int valor = Int32.Parse(Interaction.InputBox("Inserte la duración: ", "Inserte valor: "));
                if (valor <= 1)
                {
                    throw new Exception("Valor no valido");
                }
                alg.AgregarProceso(4, valor);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Valor no valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                int valor = Int32.Parse(Interaction.InputBox("Inserte la duración: ", "Inserte valor: "));
                if (valor <= 1)
                {
                    throw new Exception("Valor no valido");
                }
                alg.AgregarProceso(5, valor);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Valor no valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                int valor = Int32.Parse(Interaction.InputBox("Inserte la duración: ", "Inserte valor: "));
                if (valor <= 1)
                {
                    throw new Exception("Valor no valido");
                }
                alg.AgregarProceso(6, valor);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Valor no valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            alg.InterumpirProceso(1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            alg.InterumpirProceso(2);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            alg.InterumpirProceso(3);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            alg.InterumpirProceso(4);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            alg.InterumpirProceso(5);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            alg.InterumpirProceso(6);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            alg.ReanudarProceso(1);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            alg.ReanudarProceso(2);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            alg.ReanudarProceso(3);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            alg.ReanudarProceso(4);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            alg.ReanudarProceso(5);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            alg.ReanudarProceso(6);
        }
    }
}

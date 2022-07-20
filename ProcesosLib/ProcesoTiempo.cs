using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesosLib
{
    internal class ProcesoTiempo: Proceso
    {
        public int tiempoRestante { get; set; }
        public int duracion { get; set; }      //Momento de finalizacion del proceso
        public ProcesoTiempo(int IDProceso, string nombre, int duracion, Estado estado) : base(IDProceso, nombre)
        {
            this.tiempoRestante = duracion;
            switch (estado)
            {
                case Estado.Activo:
                    Reanudar();
                    break;
                case Estado.Espera:
                    Suspender();
                    break;
                default:
                    MessageBox.Show("No ha seleccionado un estado valido", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
            }
        }

        public override void Tick() //Este método se ejecuta cada vez que hay un tick;
        {
            if(Accion != null)
            {
                Accion();
            }
        }
    }
}

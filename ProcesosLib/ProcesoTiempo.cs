using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesosLib
{
    internal class ProcesoTiempo: Proceso
    {
        public int tiempoTotal { get; set; }
        public int tiempoRestante { get; set; }
        public int tiempoInicio { get; set; }   //Momento de llegada del proceso 
        public int tiempoFin { get; set; }      //Momento de finalizacion del proceso
        public ProcesoTiempo(int IDProceso, string nombre, int tiempoInicio, int tiempoFin, Estado estado) : base(IDProceso, nombre)
        {
            this.tiempoTotal = tiempoFin - tiempoInicio;
            this.tiempoRestante = tiempoFin;
            this.tiempoInicio = tiempoInicio;
            this.tiempoFin = tiempoFin;
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

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
        public ProcesoTiempo(int IDProceso, string nombre, int tiempoTotal, int tiemporestante) : base(IDProceso, nombre)
        {
            this.tiempoTotal = tiempoTotal;
            this.tiempoRestante = tiemporestante;
        }

        public override void Tick() //Este método se ejecuta cada vez que hay un tick;
        {
            throw new NotImplementedException();
        }
    }
}

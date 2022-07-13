using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesosLib
{
    internal class ProcesoCola: Proceso
    {
        public ProcesoCola(int IDProceso, string nombre, Estado estado) : base(IDProceso, nombre)
        {
            switch (estado)
            {
                case Estado.Activo:
                    Reanudar();
                    break;
                case Estado.Espera:
                    Suspender();
                    break;
                default:
                    Console.WriteLine("No ha seleccionado un estado valido");
                    break;
            }
        }

        public override void Tick()
        {
            if (Accion != null)
            {
                Accion();
            }

        }
    }
}

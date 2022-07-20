using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesosLib
{
    public interface IAcciones
    {
        public void AgregarProceso(params int[] valores);
        public void InterumpirProceso(int id);
        public void ReanudarProceso(int id);
        public void TerminarProceso(int id);
        public void Ejecutar();
        public string GetProceso();
        public Dictionary<string, Estado> GetProcesos();
    }
}

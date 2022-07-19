using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesosLib
{
    public class SJF: IAcciones
    {
        private List<ProcesoTiempo> procesos;
        private List<ProcesoTiempo> interrumpidos;
        private List<ProcesoTiempo> procesosEliminar;

        public SJF()
        {
            procesos = new List<ProcesoTiempo>();
            interrumpidos = new List<ProcesoTiempo>();
            procesosEliminar = new List<ProcesoTiempo>();
        }

        public void AgregarProceso(params int[] valores)
        {
            var cond = procesos.Count == 0;
            int id = valores[0];
            var cond2 = procesos.Union(interrumpidos).Where(x => x.IDProceso == id).Count() == 0;
            if (cond2)
            {
                switch (id)
                {
                    case 1:
                        if (cond)
                        {
                            procesos.Add(new ProcesoTiempo(id, "Notepad", valores[1], valores[2], Estado.Activo));
                        }
                        else
                        {
                            procesos.Add(new ProcesoTiempo(id, "Notepad", valores[1], valores[2], Estado.Espera));
                        }
                        break;
                    case 2:
                        if (cond)
                        {
                            procesos.Add(new ProcesoTiempo(id, "Word", valores[1], valores[2], Estado.Activo));
                        }
                        else
                        {
                            procesos.Add(new ProcesoTiempo(id, "Word", valores[1], valores[2], Estado.Espera));
                        }
                        break;
                    case 3:
                        if (cond)
                        {
                            procesos.Add(new ProcesoTiempo(id, "Excel", valores[1], valores[2], Estado.Activo));
                        }
                        else
                        {
                            procesos.Add(new ProcesoTiempo(id, "Excel", valores[1], valores[2], Estado.Espera));
                        }
                        break;
                    case 4:
                        if (cond)
                        {
                            procesos.Add(new ProcesoTiempo(id, "AutoCAD", valores[1], valores[2], Estado.Activo));
                        }
                        else
                        {
                            procesos.Add(new ProcesoTiempo(id, "AutoCAD", valores[1], valores[2], Estado.Espera));
                        }
                        break;
                    case 5:
                        if (cond)
                        {
                            procesos.Add(new ProcesoTiempo(id, "Calculadora", valores[1], valores[2], Estado.Activo));
                        }
                        else
                        {
                            procesos.Add(new ProcesoTiempo(id, "Calculadora", valores[1], valores[2], Estado.Espera));
                        }
                        break;
                    case 6:
                        if (cond)
                        {
                            procesos.Add(new ProcesoTiempo(id, "Windows Defender", valores[1], valores[2], Estado.Activo));
                        }
                        else
                        {
                            procesos.Add(new ProcesoTiempo(id, "Windows Defender", valores[1], valores[2], Estado.Espera));
                        }
                        break;
                    default:
                        MessageBox.Show("No ha seleccionado un id valido", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show("El proceso ya se encuentra agregado", "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void InterumpirProceso(int id)   //Interrumpe el procesoss con el id suministrado (Si lo encuentra)
        {
            foreach (var item in procesos)
            {
                if (item.IDProceso == id)
                {
                    item.Accion = item.Interumpir;
                }
            }
        }
        public void ReanudarProceso(int id) //El proceso pasa de estar interrumpido a pausa (Si lo encuentra)
        {
            foreach (var item in interrumpidos)
            {
                if (item.IDProceso == id)
                {
                    item.Accion = item.Suspender;
                }
            }
        }
        private void IniciarProceso(int id)     //Activa el proceso
        {
            foreach (var item in procesos)
            {
                if (item.IDProceso == id)
                {
                    item.Accion = item.Reanudar;
                }
            }
        }
        public void TerminarProceso(int id)
        {
            foreach (var item in procesos)
            {
                if (item.IDProceso == id)
                {
                    item.Accion = item.Terminar;
                }
            }
        }
        public void Ejecutar()
        {

            foreach (var item in interrumpidos)     //Ejecuta los ticks de todos los procesos interrumpidos
            {
                item.Tick();
            }
            foreach (var item in procesos)          //Ejecuta todos los ticks de los procesos en espera
            {
                item.Tick();
            }
            if (interrumpidos.Count != 0)   //Revisa si se retomaron procesos interumpidos
            {

                foreach (var item in interrumpidos)
                {
                    if (item.Estado == Estado.Espera)
                    {
                        procesosEliminar.Add(item);
                        procesos.Add(item);

                    }
                }
                procesosEliminar.ForEach(x => interrumpidos.Remove(x));
                procesosEliminar.Clear();
            }
            if (procesos.Count != 0)
            {
                IniciarProceso(procesos.ElementAt(0).IDProceso);
                var actual = procesos.ElementAt(0);
                switch (actual.Estado)
                {
                    case Estado.Activo:
                        actual.tiempoRestante--;
                        break;
                    case Estado.Interrumpido:
                        interrumpidos.Add(actual);
                        procesos.Remove(actual);
                        procesos = procesos.OrderBy(x => x.tiempoTotal).ToList();   //Ordena la lista de procesos en orden ascendente
                        break;
                    case Estado.Terminado:
                        procesos.Remove(actual);
                        procesos = procesos.OrderBy(x => x.tiempoTotal).ToList();   //Ordena la lista de procesos en orden ascendente
                        break;
                }
                if (procesos.Count > 0 && procesos.ElementAt(0).tiempoRestante <= 0)
                {
                    TerminarProceso(procesos.ElementAt(0).IDProceso);
                }
            }
            
        }

        public string GetProceso()
        {
            if (procesos.Count != 0 && procesos.ElementAt(0).Estado == Estado.Activo)
            {
                return procesos.ElementAt(0).Nombre;
            }
            return "CPU Libre";

        }
        public Dictionary<string, Estado> GetProcesos()
        {
            return procesos.Union(interrumpidos).ToDictionary(p => p.Nombre, p => p.Estado);

        }
    }
}

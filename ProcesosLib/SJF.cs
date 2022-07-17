using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesosLib
{
    public class SJF
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

        public void agregarProceso(int id, int tiempoInicio, int tiempoFin)
        {
            var cond = procesos.Count == 0;
            switch (id)
            {
                case 1:
                    if (cond)
                    {
                        procesos.Add(new ProcesoTiempo(id, "Notepad", tiempoInicio, tiempoFin, Estado.Activo));
                    }
                    else
                    {
                        procesos.Add(new ProcesoTiempo(id, "Notepad", tiempoInicio, tiempoFin, Estado.Espera));
                    }
                    break;
                case 2:
                    if (cond)
                    {
                        procesos.Add(new ProcesoTiempo(id, "Word", tiempoInicio, tiempoFin, Estado.Activo));
                    }
                    else
                    {
                        procesos.Add(new ProcesoTiempo(id, "Word", tiempoInicio, tiempoFin, Estado.Espera));
                    }
                    break;
                case 3:
                    if (cond)
                    {
                        procesos.Add(new ProcesoTiempo(id, "Excel", tiempoInicio, tiempoFin, Estado.Activo));
                    }
                    else
                    {
                        procesos.Add(new ProcesoTiempo(id, "Excel", tiempoInicio, tiempoFin, Estado.Espera));
                    }
                    break;
                case 4:
                    if (cond)
                    {
                        procesos.Add(new ProcesoTiempo(id, "AutoCAD", tiempoInicio, tiempoFin, Estado.Activo));
                    }
                    else
                    {
                        procesos.Add(new ProcesoTiempo(id, "AutoCAD", tiempoInicio, tiempoFin, Estado.Espera));
                    }
                    break;
                case 5:
                    if (cond)
                    {
                        procesos.Add(new ProcesoTiempo(id, "Calculadora", tiempoInicio, tiempoFin, Estado.Activo));
                    }
                    else
                    {
                        procesos.Add(new ProcesoTiempo(id, "Calculadora", tiempoInicio, tiempoFin, Estado.Espera));
                    }
                    break;
                case 6:
                    if (cond)
                    {
                        procesos.Add(new ProcesoTiempo(id, "Windows Defender", tiempoInicio, tiempoFin, Estado.Activo));
                    }
                    else
                    {
                        procesos.Add(new ProcesoTiempo(id, "Windows Defender", tiempoInicio, tiempoFin, Estado.Espera));
                    }
                    break;
            }
        }

        public void interumpirProceso(int id)   //Interrumpe el procesoss con el id suministrado (Si lo encuentra)
        {
            foreach (var item in procesos)
            {
                if (item.IDProceso == id)
                {
                    item.Accion = item.Interumpir;
                }
            }
        }
        public void reanudarProceos(int id) //El proceso pasa de estar interrumpido a pausa (Si lo encuentra)
        {
            foreach (var item in interrumpidos)
            {
                if (item.IDProceso == id)
                {
                    item.Accion = item.Suspender;
                }
            }
        }
        private void iniciarProceso(int id)     //Activa el proceso
        {
            foreach (var item in procesos)
            {
                if (item.IDProceso == id)
                {
                    item.Accion = item.Reanudar;
                }
            }
        }
        public void terminarProceso(int id)
        {
            foreach (var item in procesos)
            {
                if (item.IDProceso == id)
                {
                    item.Accion = item.Terminar;
                }
            }
        }
        public void ejecutar()
        {

            foreach (var item in interrumpidos)     //Ejecuta los ticks de todos los procesos interrumpidos
            {
                item.Tick();
            }
            foreach (var item in procesos)          //Ejecuta todos los ticks de los procesos en espera
            {
                item.Tick();
            }
            if (procesos.Count != 0)
            {
                iniciarProceso(procesos.ElementAt(0).IDProceso);
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
                    terminarProceso(procesos.ElementAt(0).IDProceso);
                }
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
        }

        public string getProceso()
        {
            if (procesos.Count != 0 && procesos.ElementAt(0).Estado == Estado.Activo)
            {
                return procesos.ElementAt(0).Nombre;
            }
            return "CPU Libre";

        }
    }
}

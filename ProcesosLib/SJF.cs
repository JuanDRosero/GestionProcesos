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

        public SJF()
        {
            procesos = new List<ProcesoTiempo>();
            interrumpidos = new List<ProcesoTiempo>();
        }

        public void agregarP(int id, int tiempoInicio, int tiempoFin)
        {
            var cond = procesos.Count == 0;
            switch(id)
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

        public void interumpirprocesos(int id)   //Interrumpe el procesoss con el id suministrado (Si lo encuentra)
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
            foreach (var item in interrumpidos)
            {
                if (item.IDProceso == id)
                {
                    item.Accion = item.Reanudar;
                }
            }
        }
        public void terminarProceso(int id)
        {
            foreach(var item in procesos)
            {
                if(item.IDProceso == id)
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
            procesos = procesos.OrderBy(x => x.tiempoTotal).ToList();
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
                        break;
                    case Estado.Terminado:
                        procesos.Remove(actual);
                        break;
                }
                if (procesos.Count > 1)
                {
                    for (int i = 0; i < procesos.Count; i++)
                    {
                        procesos.ElementAt(i).tiempoFin++;
                        procesos.ElementAt(i).tiempoRestante++;
                    }
                }
                if (interrumpidos.Count != 0)
                {
                    for (int i = 0; i < interrumpidos.Count; i++)
                    {
                        interrumpidos.ElementAt(i).tiempoFin++;
                        interrumpidos.ElementAt(i).tiempoRestante++;
                    }
                }
            }
        }

        public string getProceso()
        {
            if (procesos.Count != 0)
            {
                return procesos.ElementAt(0).Nombre;
            }
            return "CPU Libre";

        }


    }
}

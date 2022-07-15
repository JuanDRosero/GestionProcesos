//Hay que agregar el despachador y reiniciarlo cada vez que se lance una interrupción.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesosLib
{
    public class RoundRobin
    {
        private List<ProcesoCola> procesos;
        private List<ProcesoCola> interrumpidos;
        private int quantum;
        private int tRestante;
        public RoundRobin(int quuantum=3)
        {
            procesos = new List<ProcesoCola>();
            interrumpidos = new List<ProcesoCola>();
            this.quantum = quantum;
            tRestante = quuantum;
        }

        public void agregarProceso(int id)  //Agregarle un bool por si se intenta agregar un elemento que ya existe
        {
            var cond = procesos.Count == 0;
            switch (id)
            {
                case 0:
                    procesos.Insert(0, new ProcesoCola(id, "Despachador", Estado.Activo));
                    break;
                case 1:
                    if (cond)
                    {
                        procesos.Add(new ProcesoCola(id, "Notepad", Estado.Activo));
                    }
                    else
                    {
                        procesos.Add(new ProcesoCola(id, "Notepad", Estado.Espera));
                    }
                    break;
                case 2:
                    if (cond)
                    {
                        procesos.Add(new ProcesoCola(id, "Word", Estado.Activo));
                    }
                    else
                    {
                        procesos.Add(new ProcesoCola(id, "Word", Estado.Espera));
                    }
                    break;

                case 3:
                    if (cond)
                    {
                        procesos.Add(new ProcesoCola(id, "Excel", Estado.Activo));
                    }
                    else
                    {
                        procesos.Add(new ProcesoCola(id, "Excel", Estado.Espera));
                    }
                    break;

                case 4:
                    if (cond)
                    {
                        procesos.Add(new ProcesoCola(id, "AutoCAD", Estado.Activo));
                    }
                    else
                    {
                        procesos.Add(new ProcesoCola(id, "AutoCAD", Estado.Espera));
                    }
                    break;
                case 5:
                    if (cond)
                    {
                        procesos.Add(new ProcesoCola(id, "Calculadora", Estado.Activo));
                    }
                    else
                    {
                        procesos.Add(new ProcesoCola(id, "Calculadora", Estado.Espera));
                    }
                    break;

                case 6:
                    if (cond)
                    {
                        procesos.Add(new ProcesoCola(id, "Windows Defender", Estado.Activo));
                    }
                    else
                    {
                        procesos.Add(new ProcesoCola(id, "Windows Defender", Estado.Espera));
                    }
                    break;
                default:
                    MessageBox.Show("No ha seleccionado un id valido", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
        public void interumpirProceso(int id)   //Interrumpe el procesos con el id suministrado (Si lo encuentra)
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
            if (procesos.Where(x => x.IDProceso == id).ToList().Count == 1) //Busca el proceso en la cola de espera
            {
                procesos.Where(x => x.IDProceso == id).First().Accion = procesos.Where(x => x.IDProceso == id).
                    First().Terminar;
            }
            else if (interrumpidos.Where(x => x.IDProceso == id).ToList().Count == 1)   //Busca el proceso en la cola de interrumpidos
            {
                interrumpidos.Where(x => x.IDProceso == id).First().Accion = interrumpidos.Where(x => x.IDProceso == id).
                    First().Terminar;
            }
            else
            {
                MessageBox.Show("No se encontro el proceso con el ID indicado", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void ejecutar()  //Esta función es la que se ejecuta en cada TICK
        {
            if (procesos.Count!=0 && procesos.ElementAt(0).IDProceso==0)
            {
                procesos.RemoveAt(0);
            }

            if (tRestante == quantum)
            {
                agregarDes();
                
            }
            else
            {
                tRestante++;
            }
            foreach (var item in interrumpidos)     //Ejecuta los ticks de todos los procesos interrumpidos
            {
                item.Tick();
            }
            foreach (var item in procesos)          //Ejecuta todos los ticks de los procesos en espera
            {
                item.Tick();
            }
            if (procesos.Count != 0 && procesos.ElementAt(0).IDProceso!=0)
            {
                iniciarProceso(procesos.ElementAt(0).IDProceso);  //Le cede la CPU al primer elemento

                switch (procesos.ElementAt(0).Estado)
                {
                    case Estado.Terminado:
                        procesos.RemoveAt(0);         //Termina el proceso actual si es que se solicitó
                        break;
                    case Estado.Interrumpido:
                        interrumpidos.Add(procesos.ElementAt(0));  //Termina el proceso actual si es que se solicitó
                        procesos.RemoveAt(0);
                        agregarDes();
                        break;
                }
            }
                //Revisa si se reanudaron proceso

                if (interrumpidos.Count != 0)
                {
                    ProcesoCola p = null;
                    foreach (var item in interrumpidos)
                    {
                        if (item.Estado == Estado.Espera)
                        {
                            p = item;
                            procesos.Add(item);

                        }
                    }
                    interrumpidos.Remove(p);
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
        private void agregarDes()
        {
            agregarProceso(0);
            tRestante = 0;
        }
    }
}

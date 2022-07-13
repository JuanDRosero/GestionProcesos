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
        private Queue<ProcesoCola> procesos;
        private List<ProcesoCola> interrumpidos;
        public RoundRobin()
        {
            procesos = new Queue<ProcesoCola>();
            interrumpidos = new List<ProcesoCola>();
        }

        public void agregarProceso(int id)  //Agregarle un bool por si se intenta agregar un elemento que ya existe
        {
            var cond = procesos.Count == 0;
            switch (id)
            {
                case 1:
                    if (cond)
                    {
                        procesos.Enqueue(new ProcesoCola(id, "Notepad", Estado.Activo));
                    }
                    else
                    {
                        procesos.Enqueue(new ProcesoCola(id, "Notepad", Estado.Espera));
                    }
                    break;
                case 2:
                    if (cond)
                    {
                        procesos.Enqueue(new ProcesoCola(id, "Word", Estado.Activo));
                    }
                    else
                    {
                        procesos.Enqueue(new ProcesoCola(id, "Word", Estado.Espera));
                    }
                    break;

                case 3:
                    if (cond)
                    {
                        procesos.Enqueue(new ProcesoCola(id, "Excel", Estado.Activo));
                    }
                    else
                    {
                        procesos.Enqueue(new ProcesoCola(id, "Excel", Estado.Espera));
                    }
                    break;

                case 4:
                    if (cond)
                    {
                        procesos.Enqueue(new ProcesoCola(id, "AutoCAD", Estado.Activo));
                    }
                    else
                    {
                        procesos.Enqueue(new ProcesoCola(id, "AutoCAD", Estado.Espera));
                    }
                    break;
                case 5:
                    if (cond)
                    {
                        procesos.Enqueue(new ProcesoCola(id, "Calculadora", Estado.Activo));
                    }
                    else
                    {
                        procesos.Enqueue(new ProcesoCola(id, "Calculadora", Estado.Espera));
                    }
                    break;

                case 6:
                    if (cond)
                    {
                        procesos.Enqueue(new ProcesoCola(id, "Windows Defender", Estado.Activo));
                    }
                    else
                    {
                        procesos.Enqueue(new ProcesoCola(id, "Windows Defender", Estado.Espera));
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
                iniciarProceso(procesos.Peek().IDProceso);  //Le cede la CPU al primer elemento

                switch (procesos.Peek().Estado)
                {
                    case Estado.Terminado:
                        procesos.Dequeue();         //Termina el proceso actual si es que se solicitó
                        break;
                    case Estado.Interrumpido:
                        interrumpidos.Add(procesos.Dequeue());  //Termina el proceso actual si es que se solicitó
                        break;
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
                            procesos.Enqueue(item);

                        }
                    }
                    interrumpidos.Remove(p);
                }
            }

        }
        public string getProceso()
        {
            if (procesos.Count != 0)
            {
                return procesos.Peek().Nombre;
            }
            return "CPU Libre";

        }
    }
}

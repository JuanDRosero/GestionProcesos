using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesosLib
{
    public class FCFS
    {
        private Queue<ProcesoCola> procesos;
        private List<ProcesoCola> interrumpidos;
        private int contador { get; set; }
        private bool libre;
        public FCFS()
        {
            procesos = new Queue<ProcesoCola>();
            interrumpidos = new List<ProcesoCola>();
            contador = 0;
            libre = false;
        }

        public void agregarProceso(int id)  //Agregarle un bool por si se intenta agregar un elemento que ya existe
        {
            var cond = procesos.Count == 0;
            switch (id)
            {
                case 0:
                    if (cond)
                    {
                        procesos.Enqueue(new ProcesoCola(id, "Nottepad", Estado.Activo));
                    }
                    else
                    {
                        procesos.Enqueue(new ProcesoCola(id, "Nottepad", Estado.Espera));
                    }
                    break;
                case 1:
                    if (cond)
                    {
                        procesos.Enqueue(new ProcesoCola(id, "Word", Estado.Activo));
                    }
                    else
                    {
                        procesos.Enqueue(new ProcesoCola(id, "Word", Estado.Espera));
                    }
                    break;

                case 2:
                    if (cond)
                    {
                        procesos.Enqueue(new ProcesoCola(id, "Excel", Estado.Activo));
                    }
                    else
                    {
                        procesos.Enqueue(new ProcesoCola(id, "Excel", Estado.Espera));
                    }
                    break;

                default:
                    Console.WriteLine("No ha seleccionado un id valido");
                    break;
            }
        }
        public void interumpirProceso(int id)
        {
            foreach (var item in procesos)
            {
                if (item.IDProceso == id)
                {
                    item.Accion = item.Interumpir;
                }
            }
        }
        public void reanudarProceos(int id) //El proceso pasa de estar interrumpido a pausa
        {
            foreach (var item in interrumpidos)
            {
                if (item.IDProceso == id)
                {
                    item.Accion = item.Suspender;
                }
            }
        }
        private void iniciarProceso(int id)
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
            if (procesos.Where(x => x.IDProceso == id).ToList().Count == 1)
            {
                procesos.Where(x => x.IDProceso == id).First().Accion = procesos.Where(x => x.IDProceso == id).First().Terminar;
            }
            else if (interrumpidos.Where(x => x.IDProceso == id).ToList().Count == 1)
            {
                interrumpidos.Where(x => x.IDProceso == id).First().Accion = interrumpidos.Where(x => x.IDProceso == id).First().Terminar;
            }
            else
            {
                Console.WriteLine("No se encontro el proceso con el ID indicado");
            }
        }

        public void ejecutar()  //Esta función es la que se ejecuta en cada TICK
        {

            foreach (var item in interrumpidos)
            {
                item.Tick();
            }
            foreach (var item in procesos)
            {
                item.Tick();
            }
            if (procesos.Count != 0)
            {
                iniciarProceso(procesos.Peek().IDProceso);  //Le cede la CPU al primer elemento
            }
            if (procesos.Count != 0)
            {
                switch (procesos.Peek().Estado)
                {
                    case Estado.Terminado:
                        procesos.Dequeue();
                        break;
                    case Estado.Interrumpido:
                        interrumpidos.Add(procesos.Dequeue());
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

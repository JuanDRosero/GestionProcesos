//Hay que agregar el despachador y reiniciarlo cada vez que se lance una interrupción.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesosLib
{
    public class RoundRobin: Acciones
    {
        private List<ProcesoCola> procesos;
        private List<ProcesoCola> interrumpidos;
        private List<ProcesoCola> procesosEliminar;
        private int quantum;
        private int tRestante;
        private int contador;
        public RoundRobin(int quantum=3)
        {
            procesos = new List<ProcesoCola>();
            interrumpidos = new List<ProcesoCola>();
            procesosEliminar = new List<ProcesoCola>();
            this.quantum = quantum;
            tRestante = quantum;
            contador = 0;
        }

        public void AgregarProceso(params int[] valores)  //Agregarle un bool por si se intenta agregar un elemento que ya existe
        {
            var cond = procesos.Count == 0;
            int id = valores[0];
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
        public void InterumpirProceso(int id)   //Interrumpe el procesos con el id suministrado (Si lo encuentra)
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

        public void Ejecutar()  //Esta función es la que se ejecuta en cada TICK
        {
            if (procesos.Count != 0 && procesos.ElementAt(0).IDProceso == 0)
            {
                procesos.RemoveAt(0);
            }

            if (tRestante == quantum)
            {
                if (procesos.Count != 0)
                {
                    var temp = procesos.ElementAt(0);
                    procesos.Add(temp);
                    procesos.RemoveAt(0);
                }
                AgregarDes();
            }
            else
            {
                foreach (var item in interrumpidos)     //Ejecuta los ticks de todos los procesos interrumpidos
                {
                    item.Tick();
                }
                foreach (var item in procesos)          //Ejecuta todos los ticks de los procesos en espera
                {
                    item.Tick();
                }
                //Revisa si se reanudaron proceso

                if (interrumpidos.Count != 0)
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
                if (procesos.Count != 0 && procesos.ElementAt(0).IDProceso != 0)
                {
                    IniciarProceso(procesos.ElementAt(0).IDProceso);  //Le cede la CPU al primer elemento
                    procesos.ElementAt(0).Accion();

                    switch (procesos.ElementAt(0).Estado)
                    {
                        case Estado.Terminado:
                            procesos.RemoveAt(0);         //Termina el proceso actual si es que se solicitó
                            break;
                        case Estado.Interrumpido:
                            interrumpidos.Add(procesos.ElementAt(0));  //Termina el proceso actual si es que se solicitó
                            procesos.RemoveAt(0);
                            AgregarDes();
                            break;
                    }
                }
                tRestante++;
                contador++;
            }

        }
        public string GetProceso()
        {
            if (procesos.Count != 0)
            {
                return procesos.ElementAt(0).Nombre;
            }
            return "CPU Libre";

        }
        private void AgregarDes()
        {
            AgregarProceso(0);
            tRestante = 0;
            if (procesos.Count > 1)
            {
                foreach (var item in procesos)
                {
                    if (item.IDProceso != 0 && item.Accion != item.Interumpir && item.Accion != item.Terminar)
                        item.Accion = item.Suspender;
                }
            }
        }
    }
}

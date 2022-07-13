namespace ProcesosLib
{
    public enum Estado
    {
        Activo,
        Interrumpido,
        Espera,
        Terminado
    }

    internal abstract class Proceso
    {
        public int IDProceso { get; set; }
        public string Nombre { get; set; }
        public Estado Estado { get; private set; }
        public delegate void Funcion();
        public Funcion Accion = null;

        public Proceso(int IDProceso, string Nombre)
        {
            this.IDProceso = IDProceso;
            this.Nombre = Nombre;
            //Estado = Estado.Activo;
        }

        public void Interumpir()
        {
            if (Estado == Estado.Activo || Estado == Estado.Espera)
                Estado = Estado.Interrumpido;
        }
        public void Reanudar()
        {
            if (Estado == Estado.Interrumpido)
                Estado = Estado.Activo;
        }
        public void Suspender()
        {
            if (Estado == Estado.Activo || Estado == Estado.Interrumpido)
                Estado = Estado.Espera;
        }
        public void Terminar()
        {
            Estado = Estado.Terminado;
        }

        public abstract void Tick();

    }
}
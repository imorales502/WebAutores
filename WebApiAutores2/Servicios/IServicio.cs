namespace WebApiAutores2.Servicios
{
    public interface IServicio
    {
        void RealizarTarea();
    }

    public class ServicioA : IServicio
    {
        public ServicioA(ILogger<ServicioA> logger)
        {

        }
        public void RealizarTarea()
        {
            throw new NotImplementedException();
        }
    }

    public class ServicioB : IServicio
    {
        public void RealizarTarea()
        {
            throw new NotImplementedException();
        }
    }
}

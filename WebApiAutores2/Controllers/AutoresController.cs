using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores2.Entidades;
using WebApiAutores2.Servicios;

namespace WebApiAutores2.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IServicio servicio;
        private readonly ServicioTransient servicioTransient;
        private readonly ServicioScoped servicioScoped;
        private readonly ServicioSingleton servicioSingleton;
        private readonly ILogger<AutoresController> logger;

        public AutoresController(ApplicationDbContext context, IServicio servicio,
            ServicioTransient servicioTransient, ServicioScoped servicioScoped,
            ServicioSingleton servicioSingleton, ILogger<AutoresController> logger)
        {
            this.context = context;
            this.servicio = servicio;
            this.servicioTransient = servicioTransient;
            this.servicioScoped = servicioScoped;
            this.servicioSingleton = servicioSingleton;
            this.logger = logger;
        }

        [HttpGet("GUID")]
        public ActionResult ObtenerGuids()
        {
            return Ok(new
            {
                AutoresController_Transient = servicioTransient.Guid,
                AutoresController_Scoped = servicioScoped.Guid,
                AutoresController_Singleton = servicioSingleton.Guid,
                ServicioA_Transient = servicio.ObtenerTransient(),
                ServicioA_Scoped = servicio.ObtenerScoped(),
                ServicioA_Singleton = servicio.ObtenerSingleton()
            });
        }

        //[HttpGet]  // api/autores
        //[HttpGet("listado")]  //listado
        //[HttpGet("/listado")]
        //public async Task<ActionResult<List<Autor>>> Get()
        //{
        //    return await context.Autores.Include(x => x.Libros).ToListAsync();
        //}

        [HttpGet]
        public List<Autor> Get()
        {
            logger.LogInformation("Estamos obteniendo el listado de autores");
            logger.LogWarning("Este es un mensaje de prueba");
            servicio.RealizarTarea();
            return context.Autores.Include(x => x.Libros).ToList();
        }

        [HttpGet("primero")]
        public async Task<ActionResult<Autor>> PrimerAutor([FromHeader] int miValor, [FromQuery] string nombre)
        {
            var autor = await context.Autores.FirstOrDefaultAsync();

            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }

        //[HttpGet("{id:int}/{param2?}")]   //Prueba ActionResult
        //public ActionResult<Autor> Get(int id, string param2)
        //{
        //    var autor = context.Autores.FirstOrDefault(x => x.Id == id);

        //    if (autor == null)
        //    {
        //        return NotFound();
        //    }

        //    return autor;
        //}

        [HttpGet("{id:int}/{param2}")]   //Prueba ActionResult
        public IActionResult GetListAutores(int id)
        {
            var autor = context.Autores.FirstOrDefault(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return Ok(autor);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Autor>> Get(int id)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if(autor == null)
            {
                return NotFound();
            }

            return autor;
        }


        [HttpGet("primero/{id:int}")]
        public async Task<ActionResult<Autor>> AutoresLibros(int id)
        {

            var autor = await context.Autores.Include(x => x.Libros).FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            //Validaremos que no exista un autor con el mismo nombre
            var ExisteAutor = await context.Autores.AnyAsync(x => x.Nombre == autor.Nombre);

            if (ExisteAutor)
            {
                return BadRequest($"Ya existe un autor con el nombre {autor.Nombre}");
            }

            context.Add(autor);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id:int}")] //api/autores/1
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (autor.Id != id)
            {
                return BadRequest("El autor no existe");
            }

            if (!existe)
            {
                return NotFound();
            }

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }

        //public bool ValidarAutor(int id)
        //{
        //    var existe = context.Autores.Any(x => x.Id == id);

        //    if (existe)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        [HttpGet("nombre/{nombre}")]
        public async Task<ActionResult<Autor>> AutorNombre(string nombre)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }
    }
}

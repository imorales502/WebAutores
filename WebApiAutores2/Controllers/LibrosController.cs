using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores2.Entidades;

namespace WebApiAutores2.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            return await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);

        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var exiteAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);

            if (!exiteAutor)
            {
                return BadRequest($"No existe el autor de ID: { libro.AutorId}");

            }

            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiAutores2.Validaciones;

namespace WebApiAutores2.Entidades
{
    public class Autor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 10, ErrorMessage = "El campo {0} no debe superar los {1} carácteres")]
        [PrimeraLetraMayuscula]
        public string? Nombre { get; set; }
        [Range(15,25)]
        [NotMapped]
        public int Edad { get; set; }
        //[CreditCard]
        [NotMapped] 
        public string TarjetaDeCredito { get; set; }
        [Url]
        [NotMapped]
        public string URL { get; set; }
        public List<Libro>? Libros { get; set; }

    }
}

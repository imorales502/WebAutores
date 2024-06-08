using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiAutores2.Validaciones;

namespace WebApiAutores2.Entidades
{
    public class Autor:IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50, ErrorMessage = "El campo {0} no debe superar los {1} carácteres")]
        //[PrimeraLetraMayuscula]
        public string? Nombre { get; set; }
        [Range(15,25)]
        [NotMapped]
        public int Edad { get; set; }
        //[CreditCard]
        //[NotMapped] 
        //public string TarjetaDeCredito { get; set; }
        //[Url]
        //[NotMapped]
        //public string URL { get; set; }
        //[NotMapped]
        //public int Menor { get; set; }
        //[NotMapped]
        //public int Mayor { get; set; }
        public List<Libro>? Libros { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validation)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();

                if(primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúscula", 
                        new string[] { nameof(Nombre) });
                }
            }

            //if(Menor > Mayor)
            //{
            //    yield return new ValidationResult("Este valor no puede ser más grande que el campo Mayor",
            //        new string[] { nameof(Menor) });
            //}
        }
    }
}

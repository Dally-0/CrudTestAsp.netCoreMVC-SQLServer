using System.ComponentModel.DataAnnotations;

namespace EmbutidosTarea3.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; }

        public string? Descripcion { get; set; }

        // Propiedad de Navegación (Relación 1 a muchos)
        public virtual ICollection<Producto>? Productos { get; set; }
    }
}

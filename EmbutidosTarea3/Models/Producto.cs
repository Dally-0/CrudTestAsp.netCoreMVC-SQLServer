using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmbutidosTarea3.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")] // Mapeo exacto a SQL
        [Range(0.01, 10000.00, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Required]
        [Range(0, 10000, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }

        [DataType(DataType.Date)] // Para que el input HTML sea tipo calendario
        [Display(Name = "Fecha de Vencimiento")]
        public DateTime? FechaVencimiento { get; set; }

        // Llave Foránea
        [Display(Name = "Categoría")]
        public int IdCategoria { get; set; }

        // Propiedad de Navegación (Relación con Categoria)
        [ForeignKey("IdCategoria")]
        public virtual Categoria? Categoria { get; set; }
    }
}

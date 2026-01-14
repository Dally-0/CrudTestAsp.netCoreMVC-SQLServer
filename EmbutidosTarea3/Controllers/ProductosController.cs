using EmbutidosTarea3.Data;
using EmbutidosTarea3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace EmbutidosTarea3.Controllers
{
    public class ProductosController : Controller
    {
        private readonly AppDbContext _context;

        // CONSTRUCTOR: Aquí inyectamos la conexión a la Base de Datos
        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        // ---------------------------------------------------------
        // 1. LISTAR (READ)
        // ---------------------------------------------------------
        public async Task<IActionResult> Index()
        {
            // Usamos .Include() para traer también los datos de la Categoría (JOIN)
            var productos = _context.Productos.Include(p => p.Categoria);
            return View(await productos.ToListAsync());
        }

        // ---------------------------------------------------------
        // 2. INSERTAR (CREATE)
        // ---------------------------------------------------------

        // GET: Muestra el formulario vacío
        public IActionResult Create()
        {
            // Cargamos la lista de categorías para el DropDown (Select)
            // Parametros: Fuente de datos, Campo de Valor (ID), Campo de Texto (Nombre)
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "Nombre");
            return View();
        }

        // POST: Recibe los datos del formulario y guarda
        [HttpPost]
        [ValidateAntiForgeryToken] // Seguridad contra ataques CSRF
        public async Task<IActionResult> Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Home");
            }

            // Si algo falla, recargamos la lista de categorías para que no salga vacía
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "Nombre", producto.IdCategoria);
            return View(producto);
        }

        // ---------------------------------------------------------
        // 3. MODIFICAR (UPDATE)
        // ---------------------------------------------------------

        // GET: Busca el producto y rellena el formulario
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            // Enviamos la lista de categorías, seleccionando la que ya tiene el producto
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "Nombre", producto.IdCategoria);
            return View(producto);
        }

        // POST: Guarda los cambios
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.IdProducto) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto)) return NotFound();
                    else throw;
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Home");
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "Nombre", producto.IdCategoria);
            return View(producto);
        }

        // ---------------------------------------------------------
        // 4. ELIMINAR (DELETE)
        // ---------------------------------------------------------

        // GET: Muestra la pantalla de confirmación "¿Seguro que desea borrar?"
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.IdProducto == id);

            if (producto == null) return NotFound();

            return View(producto);
        }

        // POST: Ejecuta el borrado real
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "Home");
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }
    }
}

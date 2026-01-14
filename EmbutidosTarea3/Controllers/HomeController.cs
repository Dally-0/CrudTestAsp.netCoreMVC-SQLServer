
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Necesario para Include y ToListAsync
using EmbutidosTarea3.Data;
using EmbutidosTarea3.Models;

namespace EmbutidosTarea3.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Creamos el ViewModel y llenamos las dos listas
            var dashboard = new DashboardViewModel
            {
                Categorias = await _context.Categorias.ToListAsync(),
                // Incluimos la categoría para que se vea el nombre en la tabla
                Productos = await _context.Productos.Include(p => p.Categoria).ToListAsync()
            };

            return View(dashboard);
        }
    }
}

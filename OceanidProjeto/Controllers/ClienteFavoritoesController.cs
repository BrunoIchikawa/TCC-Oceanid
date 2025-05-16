
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OceanidProjeto.Data;
using OceanidProjeto.Models;

namespace OceanidProjeto.Controllers
{
    public class ClienteFavoritoesController : Controller
    {
        private readonly AppDbContext _context;

        public ClienteFavoritoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ClienteFavoritoes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ClienteFavoritos.Include(c => c.cliente).Include(c => c.produto);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ClienteFavoritoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteFavorito = await _context.ClienteFavoritos
                .Include(c => c.cliente)
                .Include(c => c.produto)
                .FirstOrDefaultAsync(m => m.idClienteFav == id);
            if (clienteFavorito == null)
            {
                return NotFound();
            }

            return View(clienteFavorito);
        }

        // GET: ClienteFavoritoes/Create
        public IActionResult Create()
        {
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "emailCliente");
            ViewData["idProd"] = new SelectList(_context.Produtos, "idProd", "descricaoProd");
            return View();
        }

        // POST: ClienteFavoritoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idClienteFav,idCliente,idProd,ativo")] ClienteFavorito clienteFavorito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteFavorito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "emailCliente", clienteFavorito.idCliente);
            ViewData["idProd"] = new SelectList(_context.Produtos, "idProd", "descricaoProd", clienteFavorito.idProd);
            return View(clienteFavorito);
        }

        // GET: ClienteFavoritoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteFavorito = await _context.ClienteFavoritos.FindAsync(id);
            if (clienteFavorito == null)
            {
                return NotFound();
            }
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "emailCliente", clienteFavorito.idCliente);
            ViewData["idProd"] = new SelectList(_context.Produtos, "idProd", "descricaoProd", clienteFavorito.idProd);
            return View(clienteFavorito);
        }

        // POST: ClienteFavoritoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idClienteFav,idCliente,idProd,ativo")] ClienteFavorito clienteFavorito)
        {
            if (id != clienteFavorito.idClienteFav)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteFavorito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteFavoritoExists(clienteFavorito.idClienteFav))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "emailCliente", clienteFavorito.idCliente);
            ViewData["idProd"] = new SelectList(_context.Produtos, "idProd", "descricaoProd", clienteFavorito.idProd);
            return View(clienteFavorito);
        }

        // GET: ClienteFavoritoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteFavorito = await _context.ClienteFavoritos
                .Include(c => c.cliente)
                .Include(c => c.produto)
                .FirstOrDefaultAsync(m => m.idClienteFav == id);
            if (clienteFavorito == null)
            {
                return NotFound();
            }

            return View(clienteFavorito);
        }

        // POST: ClienteFavoritoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clienteFavorito = await _context.ClienteFavoritos.FindAsync(id);
            if (clienteFavorito != null)
            {
                _context.ClienteFavoritos.Remove(clienteFavorito);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteFavoritoExists(int id)
        {
            return _context.ClienteFavoritos.Any(e => e.idClienteFav == id);
        }
    }
}

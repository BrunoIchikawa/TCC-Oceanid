using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OceanidProjeto.Models;
using OceanidProjeto.Data;


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
            var clienteFavoritos = _context.ClienteFavoritos.Include(cl => cl.produto).ToList();
            ViewBag.ClienteFavoritos = clienteFavoritos;

            var idClienteStr = HttpContext.Session.GetString("idCliente");
            if (string.IsNullOrEmpty(idClienteStr))
            {
                return RedirectToAction("Logins", "Login");
            }

            int idCliente = int.Parse(idClienteStr);

            var favoritos = await _context.ClienteFavoritos
                .Include(cl => cl.produto)
                .Where(cl => cl.idCliente == idCliente && cl.ativo)
                .ToListAsync();

            return View(favoritos);
        }



        // GET: ClienteFavoritoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var clienteFavoritos = _context.ClienteFavoritos.Include(cl => cl.produto).ToList();
            ViewBag.ClienteFavoritos = clienteFavoritos;
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
            var clienteFavoritos = _context.ClienteFavoritos.Include(cl => cl.produto).ToList();
            ViewBag.ClienteFavoritos = clienteFavoritos;
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente");
            ViewData["IdProd"] = new SelectList(_context.Produtos, "idProd", "idProd");
            return View();
        }

        // POST: ClienteFavoritoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdClienteFav,IdCliente,IdProd,Ativo")] ClienteFavorito clienteFavorito)
        {
            var clienteFavoritos = _context.ClienteFavoritos.Include(cl => cl.produto).ToList();
            ViewBag.ClienteFavoritos = clienteFavoritos;
            if (ModelState.IsValid)
            {
                _context.Add(clienteFavorito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", clienteFavorito.idCliente);
            ViewData["IdProd"] = new SelectList(_context.Produtos, "idProd", "idProd", clienteFavorito.idProd);
            return View(clienteFavorito);
        }

        // GET: ClienteFavoritoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var clienteFavoritos = _context.ClienteFavoritos.Include(cl => cl.produto).ToList();
            ViewBag.ClienteFavoritos = clienteFavoritos;
            if (id == null)
            {
                return NotFound();
            }

            var clienteFavorito = await _context.ClienteFavoritos.FindAsync(id);
            if (clienteFavorito == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", clienteFavorito.idCliente);
            ViewData["IdProd"] = new SelectList(_context.Produtos, "idProd", "idProd", clienteFavorito.idProd);
            return View(clienteFavorito);
        }

        // POST: ClienteFavoritoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdClienteFav,IdCliente,IdProd,Ativo")] ClienteFavorito clienteFavorito)
        {
            var clienteFavoritos = _context.ClienteFavoritos.Include(cl => cl.produto).ToList();
            ViewBag.ClienteFavoritos = clienteFavoritos;
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", clienteFavorito.idCliente);
            ViewData["IdProd"] = new SelectList(_context.Produtos, "idProd", "idProd", clienteFavorito.idProd);
            return View(clienteFavorito);
        }

        // GET: ClienteFavoritoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var clienteFavoritos = _context.ClienteFavoritos.Include(cl => cl.produto).ToList();
            ViewBag.ClienteFavoritos = clienteFavoritos;
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
            var clienteFavoritos = _context.ClienteFavoritos.Include(cl => cl.produto).ToList();
            ViewBag.ClienteFavoritos = clienteFavoritos;
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


        //FAVORITOS

        [HttpGet]
        public IActionResult Favoritar()
        {
            var idCliente = HttpContext.Session.GetInt32("idCliente");

            if (idCliente == null)
            {
                TempData["Login"] = "Primeiro faça o login";
                return RedirectToAction("Index", "Home");
            }

            var clienteFavoritos = _context.ClienteFavoritos
       .Include(cf => cf.produto)
       .Where(cf => cf.idCliente == idCliente.Value)
       .ToList();

            ViewBag.ClienteFavoritos = clienteFavoritos;




            // Recuperar idCliente da sessão

            return View();

        }


        [HttpPost]
        public IActionResult Favoritar(int idProd)
        {
            // Recuperar idCliente da sessão corretamente
            int? idCliente = HttpContext.Session.GetInt32("idCliente");

            if (!idCliente.HasValue)
            {
                TempData["Login"] = "Primeiro faça o login";
                return RedirectToAction("Index", "Home");
            }

            // Verifica se já existe esse produto nos favoritos do cliente
            var favoritoExistente = _context.ClienteFavoritos
                .FirstOrDefault(f => f.idCliente == idCliente.Value && f.idProd == idProd);

            if (favoritoExistente != null)
            {
                // Se já estiver favoritado, remove
                _context.ClienteFavoritos.Remove(favoritoExistente);
                _context.SaveChanges();
            }
            else
            {
                // Caso contrário, adiciona aos favoritos
                var produto = _context.Produtos.FirstOrDefault(p => p.idProd == idProd);
                var cliente = _context.Clientes.FirstOrDefault(c => c.idCliente == idCliente.Value);

                if (produto == null || cliente == null)
                {
                    TempData["Error"] = "Produto ou cliente não encontrado.";
                    return RedirectToAction("Index", "Home");
                }

                var novoFavorito = new ClienteFavorito
                {
                    idCliente = idCliente.Value,
                    idProd = idProd,
                    cliente = cliente,
                    produto = produto
                };
                _context.ClienteFavoritos.Add(novoFavorito);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Desfavoritar(int idProd)
        {

            // Recuperar idCliente da sessão corretamente
            int? idCliente = HttpContext.Session.GetInt32("idCliente");

            if (!idCliente.HasValue)
            {
                TempData["Login"] = "Primeiro faça o login";
                return RedirectToAction("Index", "Home");
            }


            // Verifica se já existe esse produto nos favoritos do cliente
            var favoritoExistente = _context.ClienteFavoritos
                .FirstOrDefault(f => f.idCliente == idCliente.Value && f.idProd == idProd);


            if (favoritoExistente != null)
            {
                // Se já estiver favoritado, remove
                _context.ClienteFavoritos.Remove(favoritoExistente);
                _context.SaveChanges();
            }

            return RedirectToAction("Favoritar", "ClienteFavoritoes");

        }

    }
}

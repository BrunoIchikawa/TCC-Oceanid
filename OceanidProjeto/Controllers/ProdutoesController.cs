using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OceanidProjeto.Data;
using OceanidProjeto.Models;

namespace OceanidProjeto.Controllers
{
    public class ProdutoesController : Controller
    {
        private readonly AppDbContext _context;

        public ProdutoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Produtoes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Produtos.Include(p => p.categoria);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Produtoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.categoria)
                .FirstOrDefaultAsync(m => m.idProd == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtoes/Create
        public IActionResult Create()
        {
            ViewData["idCategoria"] = new SelectList(_context.Categorias, "idCategoria", "nomeCategoria");
            return View();
        }

        // POST: Produtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idProd,codBar,nomeProd,precoProd,qtdProd,marcaProd,descricaoProd,idCategoria")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idCategoria"] = new SelectList(_context.Categorias, "idCategoria", "nomeCategoria", produto.idCategoria);
            return View(produto);
        }

        // GET: Produtoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["idCategoria"] = new SelectList(_context.Categorias, "idCategoria", "nomeCategoria", produto.idCategoria);
            return View(produto);
        }

        // POST: Produtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idProd,codBar,nomeProd,precoProd,qtdProd,marcaProd,descricaoProd,idCategoria")] Produto produto)
        {
            if (id != produto.idProd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.idProd))
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
            ViewData["idCategoria"] = new SelectList(_context.Categorias, "idCategoria", "nomeCategoria", produto.idCategoria);
            return View(produto);
        }

        // GET: Produtoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.categoria)
                .FirstOrDefaultAsync(m => m.idProd == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.idProd == id);
        }

        public IActionResult Pesquisar(string searchTerm)
        {
            // Verifica se o termo de pesquisa foi fornecido
            if (string.IsNullOrEmpty(searchTerm))
            {
                return View();
            }
            var produtos = _context.Produtos
                   .Where(p => p.nomeProd.Contains(searchTerm))
                   .ToList();

            return View(produtos);
        }

        /* VIEWS */

        //CATEGORIAS
        public IActionResult Maquiagem()
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.idCategoria == 1);

            if (categoria != null)
            {
                categoria.Produtos = _context.Produtos
                    .Where(p => p.idCategoria == categoria.idCategoria)
                    .ToList();

                ViewBag.CategoriaProdutos = new List<Categoria> { categoria };
            }
            else
            {
                ViewBag.CategoriaProdutos = new List<Categoria>();
            }

            return View();
        }


        public IActionResult Skincare()
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.idCategoria == 2);

            if (categoria != null)
            {
                categoria.Produtos = _context.Produtos
                    .Where(p => p.idCategoria == categoria.idCategoria)
                    .ToList();

                ViewBag.CategoriaProdutos = new List<Categoria> { categoria };
            }
            else
            {
                ViewBag.CategoriaProdutos = new List<Categoria>();
            }

            return View();
        }


        public IActionResult Cabelo()
        {
            var categoria = _context.Categorias
                .Where(c => c.idCategoria == 3)
                .FirstOrDefault();

            if (categoria != null)
            {
                categoria.Produtos = _context.Produtos
                    .Where(p => p.idCategoria == categoria.idCategoria)
                    .ToList();

                ViewBag.CategoriaProdutos = new List<Categoria> { categoria };
            }
            else
            {
                ViewBag.CategoriaProdutos = new List<Categoria>();
            }

            return View();
        }


        public IActionResult Perfume()
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.idCategoria == 4);

            if (categoria != null)
            {
                categoria.Produtos = _context.Produtos
                    .Where(p => p.idCategoria == categoria.idCategoria)
                    .ToList();

                ViewBag.CategoriaProdutos = new List<Categoria> { categoria };
            }
            else
            {
                ViewBag.CategoriaProdutos = new List<Categoria>();
            }

            return View();
        }

    }
}
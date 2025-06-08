using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OceanidProjeto.Data;
using OceanidProjeto.Models;

namespace OceanidProjeto.Controllers
{
    public class PromocaosController : Controller
    {
        private readonly AppDbContext _context;

        public PromocaosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Promocaos
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Promocao.Include(p => p.categoria).Include(p => p.produto);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Promocaos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Promocao = await _context.Promocao
                .Include(p => p.categoria)
                .Include(p => p.produto)
                .FirstOrDefaultAsync(m => m.idPromocao == id);
            if (Promocao == null)
            {
                return NotFound();
            }

            return View(Promocao);
        }

        // GET: Promocaos/Create
        public IActionResult Create()
        {
            // Carrega listas para os dropdowns
            ViewBag.Produtos = new SelectList(_context.Produtos.ToList(), "idProd", "nomeProd");
            ViewBag.Categorias = new SelectList(_context.Categorias.ToList(), "idCategoria", "nomeCategoria");

            // Valores padrão
            var model = new Promocao
            {
                dataInicio = DateTime.Now,
                dataFim = DateTime.Now.AddDays(7),
                ativa = true,
                tipoDesconto = "Percentual" // Valor padrão
            };

            return View();
        }

        // POST: Promocaos/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idPromocao,nomePromocao,tipoDesconto,valorDesconto,precoPromocional,dataInicio,dataFim,ativa,limitePorCliente,idProd,idCategoria")] Promocao promocao)
        {
            // Validação customizada
            if (promocao.idProd == 0 && promocao.idCategoria == 0)
            {
                ModelState.AddModelError("", "Você deve selecionar um produto OU uma categoria para a promoção.");
            }
            else if (promocao.idProd != 0 && promocao.idCategoria != 0)
            {
                ModelState.AddModelError("", "Selecione apenas um produto OU uma categoria, não ambos.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(promocao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recarrega as listas
            ViewBag.Produtos = new SelectList(_context.Produtos, "idProd", "nomeProd");
            ViewBag.Categorias = new SelectList(_context.Categorias, "idCategoria", "nomeCategoria");

            return View(promocao);
        }

        // GET: Promocaos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Promocao = await _context.Promocao.FindAsync(id);
            if (Promocao == null)
            {
                return NotFound();
            }
            ViewData["idCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "nomeCategoria", Promocao.idCategoria);
            ViewData["idPromocao"] = new SelectList(_context.Produtos, "IdProd", "DescricaoProd", Promocao.idProd);
            return View(Promocao);
        }

        // POST: Promocaos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nomePromocao,DescontoPercentual,precoPromocional,dataInicio,dataFim,ativa,idPromocao,idCategoria")] Promocao Promocao)
        {
            if (id != Promocao.idPromocao)
            {
                return NotFound();
            }

            // Removed unreachable code below
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Promocao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromocaoExists(Promocao.idPromocao))
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

            ViewData["idCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "nomeCategoria", Promocao.idCategoria);
            ViewData["idPromocao"] = new SelectList(_context.Produtos, "IdProd", "DescricaoProd", Promocao.idProd);
            return View(Promocao);
        }

        // GET: Promocaos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Promocao = await _context.Promocao
                .Include(p => p.idCategoria)
                .Include(p => p.produto)
                .FirstOrDefaultAsync(m => m.idPromocao == id);
            if (Promocao == null)
            {
                return NotFound();
            }

            return View(Promocao);
        }

        // POST: Promocaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Promocao = await _context.Promocao.FindAsync(id);
            if (Promocao != null)
            {
                _context.Promocao.Remove(Promocao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromocaoExists(int id)
        {
            return _context.Promocao.Any(e => e.idPromocao == id);
        }

        [HttpPost]
        public async Task<IActionResult> Toggleativa(int id, bool ativa)
        {
            var Promocao = await _context.Promocao.FindAsync(id);
            if (Promocao == null)
            {
                return NotFound();
            }

            Promocao.ativa = ativa;
            _context.Update(Promocao);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Ofertas(string tipoDesconto = null)
        {
            var hoje = DateTime.Now;

            var promocoes = _context.Promocao
                .Include(p => p.produto)
                    .ThenInclude(prod => prod.categoria)
                .Where(p => p.ativa &&
                            p.dataInicio <= hoje &&
                            p.dataFim >= hoje &&
                            (tipoDesconto == null || p.tipoDesconto == tipoDesconto))
                .ToList();

            ViewBag.TiposDesconto = new List<string> { "Percentual", "Valor Fixo" };
            ViewBag.TipoSelecionado = tipoDesconto;

            return View(promocoes);
        }
    }
}
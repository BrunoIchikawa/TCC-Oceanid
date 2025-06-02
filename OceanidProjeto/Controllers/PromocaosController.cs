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
        public async Task<IActionResult> Details(int? id)
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
            ViewData["idCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "nomeCategoria");
            ViewData["idPromocao"] = new SelectList(_context.Produtos, "IdProd", "DescricaoProd");
            return View();
        }

        // POST: Promocaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nomePromocao,DescontoPercentual,precoPromocional,dataInicio,dataFim,ativa,idPromocao,idCategoria")] Promocao Promocao)
        {
            // Validação para garantir que ou idPromocao ou idCategoria está preenchido, mas não ambos
            if (Promocao.idProd == null && Promocao.idCategoria == null)
            {
                ModelState.AddModelError(string.Empty, "Você deve selecionar um produto OU uma categoria para a promoção.");
            }
            else if (Promocao.idProd != null && Promocao.idCategoria!= null)
            {
                ModelState.AddModelError(string.Empty, "Selecione apenas um produto OU uma categoria, não ambos.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(Promocao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["idCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "nomeCategoria", Promocao.idCategoria);
            ViewData["idPromocao"] = new SelectList(_context.Produtos, "IdProd", "DescricaoProd", Promocao.idPromocao);
            return View(Promocao);
        }

        // GET: Promocaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nomePromocao,DescontoPercentual,precoPromocional,dataInicio,dataFim,ativa,idPromocao,idCategoria")] Promocao Promocao)
        {
            if (id != Promocao.idPromocao)
            {
                return NotFound();
            }


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

            ViewData["idCategoriaPromocao"] = new SelectList(_context.Categorias, "idPromocaoCategoria", "nomeCategoria", Promocao.categoria);
            ViewData["idPromocaoPromocao"] = new SelectList(_context.Produtos, "idPromocaoProd", "DescricaoProd", Promocao.idPromocao);
            return View(Promocao);
        }

        // GET: Promocaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OceanidProjeto.Data;
using OceanidProjeto.Models;

namespace OceanidProjeto.Controllers
{
    public class PromocoessController : Controller
    {
        private readonly AppDbContext _context;

        public PromocoessController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Promocoess
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Promocoes.Include(p => p.categoria).Include(p => p.produto);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Promocoess/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Promocoes = await _context.Promocoes
                .Include(p => p.categoria)
                .Include(p => p.produto)
                .FirstOrDefaultAsync(m => m.idPromocoes == id);
            if (Promocoes == null)
            {
                return NotFound();
            }

            return View(Promocoes);
        }

        // GET: Promocoess/Create
        public IActionResult Create()
        {
            ViewData["idCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "NomeCategoria");
            ViewData["idPromocoes"] = new SelectList(_context.Produtos, "IdProd", "DescricaoProd");
            return View();
        }

        // POST: Promocoess/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,DescontoPercentual,PrecoPromocional,DataInicio,DataFim,Ativa,idPromocoes,idCategoria")] Promocoes Promocoes)
        {
            // Validação para garantir que ou idPromocoes ou idCategoria está preenchido, mas não ambos
            if (Promocoes.idProd == null && Promocoes.idCategoria == null)
            {
                ModelState.AddModelError(string.Empty, "Você deve selecionar um produto OU uma categoria para a promoção.");
            }
            else if (Promocoes.idProd != null && Promocoes.idCategoria!= null)
            {
                ModelState.AddModelError(string.Empty, "Selecione apenas um produto OU uma categoria, não ambos.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(Promocoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["idCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "NomeCategoria", Promocoes.idCategoria);
            ViewData["idPromocoes"] = new SelectList(_context.Produtos, "IdProd", "DescricaoProd", Promocoes.idPromocoes);
            return View(Promocoes);
        }

        // GET: Promocoess/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Promocoes = await _context.Promocoes.FindAsync(id);
            if (Promocoes == null)
            {
                return NotFound();
            }
            ViewData["idCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "NomeCategoria", Promocoes.idCategoria);
            ViewData["idPromocoes"] = new SelectList(_context.Produtos, "IdProd", "DescricaoProd", Promocoes.idProd);
            return View(Promocoes);
        }

        // POST: Promocoess/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DescontoPercentual,PrecoPromocional,DataInicio,DataFim,Ativa,idPromocoes,idCategoria")] Promocoes Promocoes)
        {
            if (id != Promocoes.idPromocoes)
            {
                return NotFound();
            }


            try
            {
                _context.Update(Promocoes);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromocoesExists(Promocoes.idPromocoes))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            ViewData["idCategoriaPromocoes"] = new SelectList(_context.Categorias, "idPromocoesCategoria", "NomeCategoria", Promocoes.categoria);
            ViewData["idPromocoesPromocoes"] = new SelectList(_context.Produtos, "idPromocoesProd", "DescricaoProd", Promocoes.idPromocoes);
            return View(Promocoes);
        }

        // GET: Promocoess/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Promocoes = await _context.Promocoes
                .Include(p => p.idCategoria)
                .Include(p => p.produto)
                .FirstOrDefaultAsync(m => m.idPromocoes == id);
            if (Promocoes == null)
            {
                return NotFound();
            }

            return View(Promocoes);
        }

        // POST: Promocoess/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Promocoes = await _context.Promocoes.FindAsync(id);
            if (Promocoes != null)
            {
                _context.Promocoes.Remove(Promocoes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromocoesExists(int id)
        {
            return _context.Promocoes.Any(e => e.idPromocoes == id);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleAtiva(int id, bool ativa)
        {
            var Promocoes = await _context.Promocoes.FindAsync(id);
            if (Promocoes == null)
            {
                return NotFound();
            }

            Promocoes.Ativa = ativa;
            _context.Update(Promocoes);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

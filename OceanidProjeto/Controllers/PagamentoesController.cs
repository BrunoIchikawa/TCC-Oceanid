using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OceanidProjeto.Data;
using OceanidProjeto.Models;

namespace OceanidProjeto.Controllers
{
    public class PagamentoesController : Controller
    {
        private readonly AppDbContext _context;

        public PagamentoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Pagamentoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pagamentos.ToListAsync());
        }

        // GET: Pagamentoes/Details/5
<<<<<<< HEAD
        public async Task<IActionResult> Details(int? id)
=======
        public async Task<IActionResult> Details(int id)
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagamento = await _context.Pagamentos
                .FirstOrDefaultAsync(m => m.idPag == id);
            if (pagamento == null)
            {
                return NotFound();
            }

            return View(pagamento);
        }

        // GET: Pagamentoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pagamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idPag,status,metodo")] Pagamento pagamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pagamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pagamento);
        }

        // GET: Pagamentoes/Edit/5
<<<<<<< HEAD
        public async Task<IActionResult> Edit(int? id)
=======
        public async Task<IActionResult> Edit(int id)
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagamento = await _context.Pagamentos.FindAsync(id);
            if (pagamento == null)
            {
                return NotFound();
            }
            return View(pagamento);
        }

        // POST: Pagamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idPag,status,metodo")] Pagamento pagamento)
        {
            if (id != pagamento.idPag)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pagamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagamentoExists(pagamento.idPag))
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
            return View(pagamento);
        }

        // GET: Pagamentoes/Delete/5
<<<<<<< HEAD
        public async Task<IActionResult> Delete(int? id)
=======
        public async Task<IActionResult> Delete(int id)
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagamento = await _context.Pagamentos
                .FirstOrDefaultAsync(m => m.idPag == id);
            if (pagamento == null)
            {
                return NotFound();
            }

            return View(pagamento);
        }

        // POST: Pagamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);
            if (pagamento != null)
            {
                _context.Pagamentos.Remove(pagamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagamentoExists(int id)
        {
            return _context.Pagamentos.Any(e => e.idPag == id);
        }
    }
}

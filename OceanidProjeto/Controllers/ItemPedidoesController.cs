
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OceanidProjeto.Data;
using OceanidProjeto.Models;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Mvc.ViewFeatures;
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

namespace OceanidProjeto.Controllers
{
    public class ItemPedidoesController : Controller
    {
        private readonly AppDbContext _context;

        public ItemPedidoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ItemPedidoes
        public async Task<IActionResult> Index()
        {
            var AppDbContext = _context.ItemPedido.Include(i => i.pedido).Include(i => i.produto);
            return View(await AppDbContext.ToListAsync());
        }

        // GET: ItemPedidoes/Details/5
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

            var itemPedido = await _context.ItemPedido
                .Include(i => i.pedido)
                .Include(i => i.produto)
                .FirstOrDefaultAsync(m => m.idItemPedido == id);
            if (itemPedido == null)
            {
                return NotFound();
            }

            return View(itemPedido);
        }

        // GET: ItemPedidoes/Create
        public IActionResult Create()
        {
            ViewData["idPedido"] = new SelectList(_context.Pedidos, "idPed", "idPed");
            ViewData["idProd"] = new SelectList(_context.Produtos, "idProd", "idProd");
            return View();
        }

        // POST: ItemPedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idProdutoPedido,idPedido,idProd,quantidade,precoUnitario")] ItemPedido itemPedido)
        {

            _context.Add(itemPedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

<<<<<<< HEAD
=======
            #pragma warning disable CS0162 // Código inacessível detectado
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
            ViewData["idPedido"] = new SelectList(_context.Pedidos, "idPed", "idPed", itemPedido.idPedido);
            ViewData["idProd"] = new SelectList(_context.Produtos, "idProd", "idProd", itemPedido.idProd);
            return View(itemPedido);
        }

        // GET: ItemPedidoes/Edit/5
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

            var itemPedido = await _context.ItemPedido.FindAsync(id);
            if (itemPedido == null)
            {
                return NotFound();
            }
            ViewData["idPedido"] = new SelectList(_context.Pedidos, "idPed", "idPed", itemPedido.idPedido);
            ViewData["idProd"] = new SelectList(_context.Produtos, "idProd", "idProd", itemPedido.idProd);
            return View(itemPedido);
        }

        // POST: ItemPedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idProdutoPedido,idPedido,idProd,quantidade,precoUnitario")] ItemPedido itemPedido)
        {
            if (id != itemPedido.idItemPedido)
            {
                return NotFound();
            }


            try
            {
                _context.Update(itemPedido);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemPedidoExists(itemPedido.idItemPedido))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

<<<<<<< HEAD
=======
            #pragma warning disable CS0162 // Código inacessível detectado
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
            ViewData["idPedido"] = new SelectList(_context.Pedidos, "idPed", "idPed", itemPedido.idPedido);
            ViewData["idProd"] = new SelectList(_context.Produtos, "idProd", "idProd", itemPedido.idProd);
            return View(itemPedido);
        }

        // GET: ItemPedidoes/Delete/5
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

            var itemPedido = await _context.ItemPedido
                .Include(i => i.pedido)
                .Include(i => i.produto)
                .FirstOrDefaultAsync(m => m.idItemPedido == id);
            if (itemPedido == null)
            {
                return NotFound();
            }

            return View(itemPedido);
        }

        // POST: ItemPedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemPedido = await _context.ItemPedido.FindAsync(id);
            if (itemPedido != null)
            {
                _context.ItemPedido.Remove(itemPedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemPedidoExists(int id)
        {
            return _context.ItemPedido.Any(e => e.idItemPedido == id);
        }
    }
}

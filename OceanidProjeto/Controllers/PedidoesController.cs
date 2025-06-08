using OceanidProjeto.CarrinhoCompra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OceanidProjeto.Data;
using OceanidProjeto.Models;

namespace OceanidProjeto.Controllers
{
    public class PedidoesController : Controller
    {
        private readonly AppDbContext _context;
        private CookieCarrinhoCompra _cookieCarrinhoCompra;
        public PedidoesController(AppDbContext context, CookieCarrinhoCompra cookieCarrinhoCompra)
        {
            _context = context;
            _cookieCarrinhoCompra = cookieCarrinhoCompra;
        }

        // GET: Pedidoes
        public async Task<IActionResult> Index()
        {
            var oceanidDBContext = _context.Pedidos.Include(p => p.cliente).Include(p => p.endereco).Include(p => p.pagamento);
            return View(await oceanidDBContext.ToListAsync());
        }




        // GET: Pedidoes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.cliente)
                .Include(p => p.endereco)
                .Include(p => p.pagamento)
                .FirstOrDefaultAsync(m => m.idPed == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidoes/Create
        public IActionResult Create()
        {
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente");
            ViewData["idEnd"] = new SelectList(_context.Enderecos, "idEnd", "idEnd");
            ViewData["idPag"] = new SelectList(_context.Pagamentos, "idPag", "idPag");
            return View();
        }

        // POST: Pedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idPed,idCliente,idEnd,idPag,dataPed,totalPed")] Pedido pedido)
        {

            _context.Add(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

#pragma warning disable CS0162 // Código inacessível detectado
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", pedido.idCliente);
            ViewData["idEnd"] = new SelectList(_context.Enderecos, "idEnd", "idEnd", pedido.idEnd);
            ViewData["idPag"] = new SelectList(_context.Pagamentos, "idPag", "idPag", pedido.idPag);
            return View(pedido);
        }

        // GET: Pedidoes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", pedido.idCliente);
            ViewData["idEnd"] = new SelectList(_context.Enderecos, "idEnd", "idEnd", pedido.idEnd);
            ViewData["idPag"] = new SelectList(_context.Pagamentos, "idPag", "idPag", pedido.idPag);
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idPed,idCliente,idEnd,idPag,dataPed,totalPed")] Pedido pedido)
        {
            if (id != pedido.idPed)
            {
                return NotFound();
            }


            try
            {
                _context.Update(pedido);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(pedido.idPed))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

#pragma warning disable CS0162 // Código inacessível detectado
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", pedido.idCliente);
            ViewData["idEnd"] = new SelectList(_context.Enderecos, "idEnd", "idEnd", pedido.idEnd);
            ViewData["idPag"] = new SelectList(_context.Pagamentos, "idPag", "idPag", pedido.idPag);
            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.cliente)
                .Include(p => p.endereco)
                .Include(p => p.pagamento)
                .FirstOrDefaultAsync(m => m.idPed == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.idPed == id);
        }

        //-----------------  PEDIIDOOOOO     -------------------------------------
        // PAGINA CARRINHO
        [HttpGet]
        public IActionResult Carrinho()
        {
            return View(_cookieCarrinhoCompra.Consultar()); // EXIBE os itens salvos
        }

        [HttpPost]
        public IActionResult AdicionarItem(int id)
        {
            Produto produto = _context.Produtos.Find(id);

            if (produto == null)
            {
                return View("NaoExisteItem");
            }
            else
            {
                var item = new Produto()
                {
                    idProd = id,
                    qtdProd = 1, // Sempre adiciona 1 unidade ao carrinho
                    nomeProd = produto.nomeProd,
                    precoProd = produto.precoProd,
                };

                _cookieCarrinhoCompra.Cadastrar(item);

                return RedirectToAction(nameof(Carrinho));
            }
        }

        // DIMINUIR ITEM
        [HttpPost]
        public IActionResult DiminuirItem(int id)
        {
            // Verifica se o produto existe no banco de dados
            Produto produto = _context.Produtos.Find(id);
            if (produto == null)
            {
                return View("NaoExisteItem");
            }

            // Obtém o carrinho atual
            var carrinho = _cookieCarrinhoCompra.Consultar();
            var itemNoCarrinho = carrinho.FirstOrDefault(p => p.idProd == id);

            if (itemNoCarrinho == null)
            {
                return View("NaoExisteItem");
            }

            // Diminui a quantidade ou remove se for 1
            if (itemNoCarrinho.qtdProd > 1)
            {
                var item = new Produto()
                {
                    idProd = id,
                    qtdProd = -1, // Diminui 1 unidade
                    nomeProd = produto.nomeProd,
                    precoProd = produto.precoProd,
                };
                _cookieCarrinhoCompra.DiminuirProduto(item);
            }
            else
            {
                _cookieCarrinhoCompra.Remover(new Produto() { idProd = id });
            }

            return RedirectToAction(nameof(Carrinho));
        }

        // REMOVER ITEM
        [HttpPost]
        public IActionResult RemoverItem(int id)
        {
            _cookieCarrinhoCompra.Remover(new Produto() { idProd = id });
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult SalvarCarrinho()
        {
            TempData["Login"] = "Compra finalizada com sucesso!";
            return RedirectToAction("Index", "Home");
        }
    }
}
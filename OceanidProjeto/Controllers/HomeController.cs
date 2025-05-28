using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OceanidProjeto.Models;
using OceanidProjeto.Data;
using System.Diagnostics;

namespace OceanidProjeto.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Carrega os produtos do banco de dados incluindo as promoções ativas e categorias
            var produtosDoBanco = _context.Produtos
                .Include(p => p.Promocao.Where(promo => promo.ativa && promo.dataInicio <= DateTime.Now && promo.dataFim >= DateTime.Now))
                .Include(p => p.categoria)
                .Where(p => p.qtdProd > 0) // Apenas produtos com estoque disponível
                .ToList();

            ViewBag.ProdutosDoBanco = produtosDoBanco;
            return View();
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

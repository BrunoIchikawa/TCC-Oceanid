using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OceanidProjeto.Data;
using OceanidProjeto.Models;
using OceanidProjeto.Repositorios.Interface;

namespace OceanidProjeto.Controllers
{
    public class LoginsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILoginRepositorio _loginRepositorio;

        public LoginsController(AppDbContext context, ILoginRepositorio loginRepositorio)
        {
            _context = context;
            _loginRepositorio = loginRepositorio;
        }

        // GET: Logins
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Login.Include(l => l.Adm).Include(l => l.cliente);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Logins/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login
                .Include(l => l.Adm)
                .Include(l => l.cliente)
                .FirstOrDefaultAsync(m => m.idLogin == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // GET: Logins/Create
        public IActionResult Create()
        {
            ViewData["idAdm"] = new SelectList(_context.Adms, "idAdm", "Email");
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "emailCliente");
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idLogin,idCliente,idAdm")] Login login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idAdm"] = new SelectList(_context.Adms, "idAdm", "Email", login.idAdm);
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "emailCliente", login.idCliente);
            return View(login);
        }

        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            ViewData["idAdm"] = new SelectList(_context.Adms, "idAdm", "Email", login.idAdm);
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "emailCliente", login.idCliente);
            return View(login);
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idLogin,idCliente,idAdm")] Login login)
        {
            if (id != login.idLogin)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.idLogin))
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
            ViewData["idAdm"] = new SelectList(_context.Adms, "idAdm", "Email", login.idAdm);
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "emailCliente", login.idCliente);
            return View(login);
        }

        // GET: Logins/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login
                .Include(l => l.Adm)
                .Include(l => l.cliente)
                .FirstOrDefaultAsync(m => m.idLogin == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var login = await _context.Login.FindAsync(id);
            if (login != null)
            {
                _context.Login.Remove(login);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginExists(int id)
        {
            return _context.Login.Any(e => e.idLogin == id);
        }


        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            // Verifica se já existe um usuário logado
            if (HttpContext.Session.GetString("emailCliente") != null ||
                HttpContext.Session.GetString("emailAdm") != null)
            {
                TempData["Login"] = "Faça o logout antes de trocar de conta.";
                return RedirectToAction("Index", "Home");
            }

            var login = _loginRepositorio.Login(email, senha);

            if (login is Cliente cliente)
            {
                HttpContext.Session.SetString("emailCliente", email);
                HttpContext.Session.SetInt32("idCliente", cliente.idCliente);
                TempData["Login"] = "Bem-vindo, cliente!";
                return RedirectToAction("Index", "Home");
            }
            else if (login is Adm)
            {
                HttpContext.Session.SetString("emailAdm", email);
                TempData["Login"] = "Bem-vindo, admin!";
                return RedirectToAction("Index", "Adms");
            }

            TempData["Login"] = "E-mail ou senha inválidos.";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            TempData["Login"] = "Faça o login antes de adicionar ao carrinho";
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Login"] = "Você foi desconectado.";
            return RedirectToAction("Index", "Home");
        }
    }
}
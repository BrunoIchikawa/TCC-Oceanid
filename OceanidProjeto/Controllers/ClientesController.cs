using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OceanidProjeto.Data;
using OceanidProjeto.Models;

namespace OceanidProjeto.Controllers
{
    public class ClientesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientesController(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Clientes.Include(c => c.enderecoCli );      
            return View(await appDbContext.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.enderecoCli)
                .FirstOrDefaultAsync(m => m.idCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["endereco"] = new SelectList(_context.Enderecos, "idEnd", "Bairro");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idCliente,Cpf,nomeCompleto,Senha,Email,DataNascimento,endereco")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["endereco"] = new SelectList(_context.Enderecos, "idEnd", "Bairro", cliente.enderecoCli);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["endereco"] = new SelectList(_context.Enderecos, "idEnd", "Bairro", cliente.enderecoCli);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idCliente,Cpf,nomeCompleto,Senha,Email,DataNascimento,endereco")] Cliente cliente)
        {
            if (id != cliente.idCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.idCliente))
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
            ViewData["endereco"] = new SelectList(_context.Enderecos, "idEnd", "Bairro", cliente.enderecoCli);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.enderecoCli)
                .FirstOrDefaultAsync(m => m.idCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.idCliente == id);
        }
        public async Task<IActionResult> Cadastro(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            // Criando a lista de claims
            //Claims são um tipo de identificadores do usuario
            var claims = new List<Claim> // guarda os dados dos usuarios
            {
                new Claim(ClaimTypes.Name, cliente.emailCliente),
                new Claim(ClaimTypes.SerialNumber, Convert.ToString(cliente.idCliente)),
                // Convert.ToInt32(User.FindFirst(ClaimTypes.SerialNumber)?.Value)
                new Claim(ClaimTypes.Role, "Cliente")
            };
            //[Authorize(Roles = "Usuario")] para tipos especificos
            //[Authorize] logado

            //Criando o Claim de identidade do usuario, juntamente de coockies
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //Permite que o usuario continue logado mesmo se fechar o navegador
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Mantém o cookie ao fechar o navegador
            };
            //Vai logar o usuario com o HTTP usando tanto os coockies quanto a identidade do usuario
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            TempData["Login"] = "Cadastro efetuado com sucesso!!!";
            return RedirectToAction("Index", "Home");



        }
    }
}

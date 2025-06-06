namespace OceanidProjeto.Cookie
{
    public class Cookie
    {

        private IHttpContextAccessor _context;
        private IConfiguration _configuration;

        public Cookie(IHttpContextAccessor context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public void Cadastrar(string Key, string Valor)
        {
            CookieOptions Options = new CookieOptions();
            Options.Expires = DateTime.Now.AddDays(7);
            Options.IsEssential = true;

            _context.HttpContext.Response.Cookies.Append(Key, Valor, Options);
        }
        public void Remover(string Key)
        {
            _context.HttpContext.Response.Cookies.Delete(Key);
        }
<<<<<<< HEAD
        public string Consultar(string Key)
        {
            var valor = _context.HttpContext.Request.Cookies[Key];
            return valor;
=======

        public IHttpContextAccessor Get_context()
        {
            return _context;
        }

        public string Consultar(string Key, IHttpContextAccessor _context)
        {
        #pragma warning disable CS8603 // Possível retorno de referência nula.
            return _context.HttpContext.Request.Cookies[Key];
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
        }
        public bool Existe(string Key)
        {
            if (_context.HttpContext.Request.Cookies[Key] == null)
            {
                return false;
            }
            return true;
        }
        public void Atualizar(string Key, string Valor)
        {
            if (Existe(Key))
            {
                Remover(Key);
            }
            Cadastrar(Key, Valor);
        }
    }
}


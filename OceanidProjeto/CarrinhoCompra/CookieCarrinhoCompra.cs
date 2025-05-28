using Newtonsoft.Json;
using OceanidProjeto.Models;
using System.Net;

namespace OceanidProjeto.CarrinhoCompra
{
    public class CookieCarrinhoCompra
    {

        private string Key = "Carrinho.Compras";
        private Cookie.Cookie _cookie;

        public CookieCarrinhoCompra(Cookie.Cookie cookie)
        {
            _cookie = cookie;
        }

        public void Salvar(List<Produto> lista)
        {
            string Valor = JsonConvert.SerializeObject(lista);
            _cookie.Cadastrar(Key, Valor);
        }
        public List<Produto> Consultar()
        {
            if (_cookie.Existe(Key))
            {
                string valor = _cookie.Consultar(Key, _cookie.Get_context());
                #pragma warning disable CS8603 
                return JsonConvert.DeserializeObject<List<Produto>>(valor);
            }
            else
            {
                return new List<Produto>();
            }
        }
        public void Cadastrar(Produto item)
        {
            List<Produto> Lista;
            if (_cookie.Existe(Key))
            {
                Lista = Consultar();
                var ItemLocalizado = Lista.SingleOrDefault(a => a.idProd == item.idProd);

                if (ItemLocalizado == null)
                {
                    item.qtdProd = 1;
                    Lista.Add(item);
                }
                else
                {
                    ItemLocalizado.qtdProd += 1;
                }
            }
            else
            {
                Lista = new List<Produto>();
                item.qtdProd = 1;
                Lista.Add(item);
            }

            Salvar(Lista);
        }

        public void Atualizar(Produto item)
        {
            var Lista = Consultar();
            var ItemLocalizado = Lista.SingleOrDefault(a => a.idProd == item.idProd);

            if (ItemLocalizado != null)
            {
                ItemLocalizado.qtdProd = item.qtdProd + 1;
                Salvar(Lista);
            }
        }

        public void Remover(Produto item)
        {
            var Lista = Consultar();
            var ItemLocalizado = Lista.SingleOrDefault(a => a.idProd == item.idProd);

            if (ItemLocalizado != null)
            {
                Lista.Remove(ItemLocalizado);
                Salvar(Lista);
            }
        }
        public void DiminuirProduto(Produto item)
        {
            var Lista = Consultar();
            var ItemLocalizado = Lista.SingleOrDefault(a => a.idProd == item.idProd);

            if (ItemLocalizado != null && ItemLocalizado.qtdProd > 1)
            {

                ItemLocalizado.qtdProd = ItemLocalizado.qtdProd - 1;
                ItemLocalizado.precoProd = Convert.ToDecimal(ItemLocalizado.precoProd) * ItemLocalizado.qtdProd;
                Salvar(Lista);
            }
        }
        public bool Existe(string Key)
        {
            if (_cookie.Existe(Key))
            {
                return false;
            }
            return true;
        }
        public void RemoverTodos()
        {
            _cookie.Remover(Key);
        }
    }
}


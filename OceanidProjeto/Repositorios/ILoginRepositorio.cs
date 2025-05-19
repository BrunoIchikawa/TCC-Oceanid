using OceanidProjeto.Models;

namespace OceanidProjeto.Repositorios.Interface
{
    public interface ILoginRepositorio
    {
        object Login(string email, string senha);

    }
}
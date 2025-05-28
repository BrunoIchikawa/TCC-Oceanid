using Microsoft.EntityFrameworkCore;
using OceanidProjeto.Models;
using MySql.Data.MySqlClient;
using OceanidProjeto.Data;
using OceanidProjeto.Repositorios.Interface;

namespace OceanidProjeto.Repositorios
{
    #pragma warning disable CS9113 // O parâmetro não está lido.
    public class LoginRepositorio(AppDbContext context, IConfiguration configuration) : ILoginRepositorio
    {
        #pragma warning disable CS8601
        private readonly string _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");

        #pragma warning disable CS8766 // A nulidade de tipos de referência no tipo de retorno não corresponde ao membro implementado implicitamente (possivelmente devido a atributos de nulidade).
        public object? Login(string email, string senha)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                // parte de cleinte
                var cmdCliente = new MySqlCommand("SELECT * FROM tbCliente WHERE emailCliente = @Email AND senhaCliente = @Senha", conexao);
                cmdCliente.Parameters.AddWithValue("@Email", email);
                cmdCliente.Parameters.AddWithValue("@Senha", senha);

                using (var dr = cmdCliente.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        var cliente = new Cliente
                        {
                            idCliente = Convert.ToInt32(dr["idCliente"]),
                            nomeCompleto = dr["nomeCompleto"].ToString(),
                            emailCliente = dr["emailCliente"].ToString(),
                            senhaCliente = dr["senhaCliente"].ToString()
                        };
                        return cliente;
                    }
                }

                // parte de adm
                var cmdAdm = new MySqlCommand("SELECT * FROM tbAdm WHERE emailAdm = @Email AND senhaAdm = @Senha", conexao);
                cmdAdm.Parameters.AddWithValue("@Email", email);
                cmdAdm.Parameters.AddWithValue("@Senha", senha);

                using (var dr = cmdAdm.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        var adm = new Adm
                        {
                            idAdm = Convert.ToInt32(dr["idAdm"]),
                            nomeAdm = dr["nomeAdm"].ToString(),
                            emailAdm = dr["emailAdm"].ToString(),
                            senhaAdm = dr["senhaAdm"].ToString()
                        };
                        return adm;
                    }
                }

                return null;
            }
        }

    }

}
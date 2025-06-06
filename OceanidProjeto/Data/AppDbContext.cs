using Microsoft.EntityFrameworkCore;
using OceanidProjeto.Models;



namespace OceanidProjeto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets para todas as entidades do sistema
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Adm> Adms { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Promocao> Promocao { get; set; }
        public DbSet<ClienteFavorito> ClienteFavoritos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<ItemPedido> ItemPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração dos nomePromocaos das tabelas no banco de dados
            modelBuilder.Entity<Endereco>().ToTable("tbEndereco");
            modelBuilder.Entity<Adm>().ToTable("tbAdm");
            modelBuilder.Entity<Cliente>().ToTable("tbCliente");
            modelBuilder.Entity<Login>().ToTable("tbLogin");
            modelBuilder.Entity<Produto>().ToTable("tbProduto");
            modelBuilder.Entity<Categoria>().ToTable("tbCategoria");
            modelBuilder.Entity<Promocao>().ToTable("tbPromocao");
            modelBuilder.Entity<ClienteFavorito>().ToTable("tbClienteFavoritos");
            modelBuilder.Entity<Pedido>().ToTable("tbPedido");
            modelBuilder.Entity<ItemPedido>().ToTable("tbItemPedido");
            modelBuilder.Entity<Pagamento>().ToTable("tbPagamento");

            // Configuração das chaves primárias
            modelBuilder.Entity<Endereco>().HasKey(e => e.idEnd);
            modelBuilder.Entity<Adm>().HasKey(a => a.idAdm);
            modelBuilder.Entity<Cliente>().HasKey(c => c.idCliente);
            modelBuilder.Entity<Login>().HasKey(l => l.idLogin);
            modelBuilder.Entity<Produto>().HasKey(p => p.idProd);
            modelBuilder.Entity<Categoria>().HasKey(c => c.idCategoria);
            modelBuilder.Entity<Promocao>().HasKey(p => p.idPromocao);
            modelBuilder.Entity<ClienteFavorito>().HasKey(cf => cf.idClienteFav);
            modelBuilder.Entity<Pedido>().HasKey(p => p.idPed);
            modelBuilder.Entity<ItemPedido>().HasKey(ip => ip.idItemPedido);
            modelBuilder.Entity<Pagamento>().HasKey(p => p.idPag);

            /*
            RELACIONAMENTOS DO SISTEMA:
            
            1. Cliente <-> Endereço (N:1)
               - Um cliente tem um endereço (optional)
               - Um endereço pode pertencer a vários clientes
               - DeleteBehavior.Restrict para evitar exclusão acidental de endereço vinculado
            */
            modelBuilder.Entity<Cliente>()
                .HasOne(e => e.enderecoCli)
                .WithMany(c => c.Clientes)
                .HasForeignKey(ie => ie.idEnd)
                .OnDelete(DeleteBehavior.Restrict);

            /*
            2. Produto <-> Categoria (N:1)
<<<<<<< HEAD
               - Um produto pertence a uma categoria (required)
=======
               - Um produto pertence a uma categoria ()
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
               - Uma categoria pode ter muitos produtos
            */
            modelBuilder.Entity<Produto>()
                .HasOne(c => c.categoria)
                .WithMany(p => p.Produtos)
                .HasForeignKey(ic => ic.idCategoria)
                .OnDelete(DeleteBehavior.Restrict);

            /*
            3. Promoção -> Categoria (1:1 opcional)
               - Uma promoção pode estar vinculada a uma categoria (optional)
               - Uma categoria pode ter várias promoções
            */
            modelBuilder.Entity<Promocao>()
                .HasOne(p => p.categoria)
                .WithMany(c => c.Promocao)
                .HasForeignKey(p => p.idCategoria)
                .OnDelete(DeleteBehavior.Restrict);

            /*
            4. Promoção -> Produto (1:1 opcional)
               - Uma promoção pode estar vinculada a um produto (optional)
               - Um produto pode ter várias promoções
               - Validação customizada garante que ou produto ou categoria está preenchido
            */
            modelBuilder.Entity<Promocao>()
                .HasOne(p => p.produto)
                .WithMany(pr => pr.Promocao)
                .HasForeignKey(p => p.idProd)
                .OnDelete(DeleteBehavior.Restrict);

            /*
            5. ClienteFavorito (Tabela de junção N:N entre Cliente e Produto)
               - Um cliente pode ter vários produtos favoritos
               - Um produto pode ser favorito de vários clientes
               - Relacionamento configurado como Restrict para manter integridade
            */
            modelBuilder.Entity<ClienteFavorito>()
                .HasOne(cf => cf.cliente)
                .WithMany(f => f.ClienteFavoritos)
                .HasForeignKey(c => c.idCliente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClienteFavorito>()
                .HasOne(p => p.produto)
                .WithMany(cf => cf.ClienteFavoritos)
                .HasForeignKey(cf => cf.idProd)
                .OnDelete(DeleteBehavior.Restrict);

            /*
            6. Pedido -> Cliente (N:1)
<<<<<<< HEAD
               - Um pedido pertence a um cliente (required)
=======
               - Um pedido pertence a um cliente ()
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
               - Um cliente pode ter vários pedidos
            */
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.idCliente)
                .OnDelete(DeleteBehavior.Restrict);

            /*
            7. Pedido -> Endereço (N:1)
<<<<<<< HEAD
               - Um pedido está vinculado a um endereço de entrega (required)
=======
               - Um pedido está vinculado a um endereço de entrega ()
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
               - Um endereço pode estar em vários pedidos
            */
            modelBuilder.Entity<Pedido>()
                .HasOne(e => e.endereco)
                .WithMany()
                .HasForeignKey(p => p.idEnd)
                .OnDelete(DeleteBehavior.Restrict);

            /*
            8. Pedido -> Pagamento (N:1)
<<<<<<< HEAD
               - Um pedido tem um método de pagamento (required)
=======
               - Um pedido tem um método de pagamento ()
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
               - Um pagamento pode estar em vários pedidos
            */
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.pagamento)
                .WithMany(pa => pa.Pedidos)
                .HasForeignKey(p => p.idPag)
                .OnDelete(DeleteBehavior.Restrict);

            /*
            9. ItemPedido -> Pedido (N:1)
<<<<<<< HEAD
               - Um item de pedido pertence a um pedido (required)
=======
               - Um item de pedido pertence a um pedido ()
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
               - Um pedido pode ter vários itens
            */
            modelBuilder.Entity<ItemPedido>()
                .HasOne(ip => ip.pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(ip => ip.idPedido)
                .OnDelete(DeleteBehavior.Restrict);

            /*
            10. ItemPedido -> Produto (N:1)
<<<<<<< HEAD
                - Um item de pedido referencia um produto (required)
=======
                - Um item de pedido referencia um produto ()
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
                - Um produto pode estar em vários itens de pedido
            */
            modelBuilder.Entity<ItemPedido>()
                .HasOne(ip => ip.produto)
                .WithMany(p => p.ItensPedidos)
                .HasForeignKey(ip => ip.idPedido)
                .OnDelete(DeleteBehavior.Restrict);
            /*
            11. Login -> Cliente(1:1)
<<<<<<< HEAD
                - um login por usuario (required)
=======
                - um login por usuario ()
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
                - um cliente por login
            */
            modelBuilder.Entity<Login>()
                .HasOne(c => c.cliente)
                .WithMany(l => l.Login)
                .HasForeignKey(log => log.idCliente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Login>()
                .HasOne(a => a.Adm)
                .WithMany(a => a.Login)
                .HasForeignKey(log => log.idAdm)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração de índice único para email de cliente
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.emailCliente)
                .IsUnique();

            // Configuração de índice único para email de administrador
            modelBuilder.Entity<Adm>()
                .HasIndex(a => a.emailAdm)
                .IsUnique();

            // Configuração de índice único para CPF de cliente
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.cpf)
                .IsUnique();
        }
    }
}
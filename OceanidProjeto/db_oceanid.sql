DROP DATABASE IF EXISTS db_oceanid;
create database db_oceanid;
use db_oceanid;


create table tbEndereco(
	idEnd int primary key auto_increment,
	cepEnd varchar(10) not null,
	numeroEnd int unsigned not null,
	logradouro varchar(250) not null,
	complemento varchar(100),
	bairro varchar(100) not null,
    estado varchar(100) not null,
	cidade varchar(100) not null
);

create table tbAdm(
    idAdm int primary key auto_increment,
	nomePromocaoAdm varchar(70) not null,
    senhaAdm varchar(30) not null unique,
    emailAdm enum ('adm1@gmail.com','adm2@gmail.com','adm3@gmail.com','adm4@gmail.com','adm5@gmail.com') unique not null
);

  create table tbCategoria(
	idCategoria int primary key auto_increment,
	nomeCategoria varchar(50) not null
);

create table tbPagamento(
	idPag int primary key auto_increment,
	statusPag enum('Pago', 'Pendente', 'Não Realizado') not null default 'Pendente',
	metodoPag varchar(50) not null
);



create table tbCliente (
	idCliente int primary key auto_increment,
    cpf varchar(11) unique,
    nomeCompleto varchar(200) not null,
    senhaCliente varchar(30) not null ,
    emailCliente varchar(50) not null unique,
    dataNasc date not null,
    idEnd int,
    foreign key (idEnd) references tbEndereco(idEnd)
);

create table tblogin(
  idLogin  int primary key auto_increment,
  idCliente int, 
  foreign key (idCliente) references tbCliente(idCliente),
  idAdm int, 
  foreign key (idAdm) references tbAdm(idAdm)
);
  

create table tbProduto (
	idProd int primary key auto_increment,
	codBar varchar(15),
	nomeProd varchar(200) not null,
	precoProd decimal(10,2) not null,
	qtdProd int unsigned not null,
	marcaProd varchar(50) not null,
	descricaoProd varchar(200) not null,
	idCategoria int not null,
	foreign key (idCategoria) references tbCategoria(idCategoria)
);

CREATE TABLE tbPromocao (
    idPromocao INT PRIMARY KEY AUTO_INCREMENT,
    nomePromocao VARCHAR(100) NOT NULL,
    tipoDesconto ENUM('Percentual', 'Valor Fixo', 'Leve X Pague Y', 'Brinde') NOT NULL,
    valorDesconto DECIMAL(10,2), -- Pode ser percentual ou valor fixo
    precoPromocional DECIMAL(10,2), -- Preço final promocional
    dataInicio DATETIME NOT NULL,
    dataFim DATETIME NOT NULL,
    ativa BOOLEAN NOT NULL DEFAULT true,
    limitePorCliente INT,
    idProd INT,
    FOREIGN KEY (idProd) REFERENCES tbProduto(idProd) ON DELETE CASCADE,
    idCategoria INT,
    FOREIGN KEY (idCategoria) REFERENCES tbCategoria(idCategoria) ON DELETE CASCADE,
    check (dataFim > dataInicio)
);



create table tbClienteFavoritos(
    idClienteFav int primary key auto_increment,
    idCliente int not null,
    foreign key (idCliente) references tbCliente(idCliente) on delete cascade,
    idProd int not null,
    foreign key (idProd) references tbProduto(idProd) on delete cascade,
    ativo boolean not null default true,
    constraint unique_client_product unique (idCliente, idProd)
);




/*Esta é uma restrição de unicidade composta que garante que cada combinação de idCliente e idProd seja única na tabela tbClienteFavoritos.*/

create table tbPedido(
    idPed int primary key auto_increment,
    idEnd int not null,
	foreign key (idEnd) references tbEndereco(idEnd),
    idPag int not null,
	foreign key (idPag) references tbPagamento(idPag),
    idCliente int not null,
    foreign key (idCliente) references tbCliente(idCliente),
    dataPed datetime not null,
    totalPed decimal(10,2) not null
);

select * from tbCliente;

create table tbItemPedido (
    idItemPedido int primary key auto_increment,
    idPedido int not null,
	foreign key(idPedido) references tbPedido(idPed),
    idProd int not null,
    foreign key (idProd) references tbProduto(idProd),
    quantidade int not null,
    precoUnitario DECIMAL(10, 2)  not null
);
 
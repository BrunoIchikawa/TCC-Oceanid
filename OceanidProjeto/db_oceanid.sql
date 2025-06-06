DROP DATABASE IF EXISTS db_oceanid;
create database db_oceanid;
use db_oceanid;

<<<<<<< HEAD

=======
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
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

<<<<<<< HEAD
create table tbAdm(
    idAdm int primary key auto_increment,
	nomePromocaoAdm varchar(70) not null,
    senhaAdm varchar(30) not null unique,
    emailAdm enum ('adm1@gmail.com','adm2@gmail.com','adm3@gmail.com','adm4@gmail.com','adm5@gmail.com') unique not null
);

=======
insert into tbEndereco (
    cepEnd, numeroEnd, logradouro, complemento, bairro, estado, cidade
) values (
    '05102090',
    200,
    'Rua Domingos de Braga',
    'Bloco 4 apto 151',
    'Vila dos Remedios',
    'São paulo',
    'São paulo'
);


create table tbAdm(
    idAdm int primary key auto_increment,
	nomeAdm varchar(70) not null,
    senhaAdm varchar(30) not null,
    emailAdm enum ('adm1@gmail.com','adm2@gmail.com','adm3@gmail.com','adm4@gmail.com','adm5@gmail.com','adm6@gmail.com','adm7@gmail.com') unique not null);

insert into tbadm (nomeAdm, senhaAdm, emailAdm) values 
('Bruno Santos Ichikawa', 'orv', 'adm1@gmail.com'),
('Emilly Sodré', 'admin', 'adm2@gmail.com'),
('Caroline Ferreira', 'admin', 'adm3@gmail.com'),
('Gabriela Gomes', 'admin', 'adm4@gmail.com'),
('Gabriela Moreira', 'admin', 'adm5@gmail.com'),
('Gabriely Lima', 'admin', 'adm6@gmail.com'),
('Giovanna Althemann', 'admin', 'adm7@gmail.com');


select * from tbAdm;
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
  create table tbCategoria(
	idCategoria int primary key auto_increment,
	nomeCategoria varchar(50) not null
);

create table tbPagamento(
	idPag int primary key auto_increment,
	statusPag enum('Pago', 'Pendente', 'Não Realizado') not null default 'Pendente',
	metodoPag varchar(50) not null
);


<<<<<<< HEAD

=======
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
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

<<<<<<< HEAD
=======
insert into tbCliente (cpf, nomeCompleto, senhaCliente, emailCliente, dataNasc,idEnd)
values (
    '54520929865',
    'Bruno Ichuikawa',
    'orv',
    'b123@gmail.com',
    '2008-06-09',
    '1'
);
select * from tbCliente;



>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
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
<<<<<<< HEAD
    idPag int not null,
=======
    idPag int not null, 
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
	foreign key (idPag) references tbPagamento(idPag),
    idCliente int not null,
    foreign key (idCliente) references tbCliente(idCliente),
    dataPed datetime not null,
    totalPed decimal(10,2) not null
);

<<<<<<< HEAD
select * from tbCliente;

=======
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9
create table tbItemPedido (
    idItemPedido int primary key auto_increment,
    idPedido int not null,
	foreign key(idPedido) references tbPedido(idPed),
<<<<<<< HEAD
    idProd int not null,
    foreign key (idProd) references tbProduto(idProd),
    quantidade int not null,
    precoUnitario DECIMAL(10, 2)  not null
);
 
=======
    batata int not null,
    foreign key (batata) references tbProduto(idProd),
    quantidade int not null,
    precoUnitario DECIMAL(10, 2)  not null
);
 


 
 -- Inserindo categorias de produtos de beleza
INSERT INTO tbCategoria (nomeCategoria) VALUES
('Maquiagem'),
('Cuidados com a Pele'),
('Cabelos'),
('Perfumaria'),
('Unhas'),
('Corpo e Banho'),
('Barbearia'),
('Acessórios de Beleza'),
('Produtos Naturais'),
('Tratamentos Capilares');

-- Inserindo produtos de maquiagem
INSERT INTO tbProduto (codBar, nomeProd, precoProd, qtdProd, marcaProd, descricaoProd, idCategoria) VALUES
('7891234567890', 'Batom Matte Vermelho', 29.90, 50, 'Ruby Rose', 'Batom matte de longa duração na cor vermelho intenso', 1),
('7891234567891', 'Paleta de Sombras Nude', 89.90, 30, 'Natural Beauty', 'Paleta com 12 tons neutros para olhar marcante', 1),
('7891234567892', 'Base Líquida Alta Cobertura', 59.90, 40, 'Skin Perfect', 'Base com acabamento natural e cobertura buildable', 1),
('7891234567893', 'Máscara de Cílios Volume Extremo', 39.90, 60, 'Lash Queen', 'Máscara que alonga e dá volume aos cílios', 1),
('7891234567894', 'Delineador Líquido Preto', 34.90, 45, 'Black Line', 'Delineador de alta precisão com ponta fina', 1);

-- Inserindo produtos para cuidados com a pele
INSERT INTO tbProduto (codBar, nomeProd, precoProd, qtdProd, marcaProd, descricaoProd, idCategoria) VALUES
('7891234567895', 'Creme Facial Hidratante', 79.90, 35, 'Hydra Plus', 'Hidrata profundamente por 24 horas', 2),
('7891234567896', 'Protetor Solar FPS 50', 69.90, 50, 'Sun Safe', 'Proteção UVA/UVB com textura leve', 2),
('7891234567897', 'Sérum Antienvelhecimento', 129.90, 25, 'Age Defy', 'Reduz linhas de expressão em 4 semanas', 2),
('7891234567898', 'Água Micelar 400ml', 49.90, 40, 'Pure Skin', 'Remove maquiagem e impurezas sem ressecar', 2),
('7891234567899', 'Esfoliante Facial Suave', 39.90, 30, 'Gentle Touch', 'Esfoliação delicada para pele sensível', 2);

-- Inserindo produtos para cabelos
INSERT INTO tbProduto (codBar, nomeProd, precoProd, qtdProd, marcaProd, descricaoProd, idCategoria) VALUES
('7891234567800', 'Shampoo Reparador', 45.90, 40, 'Hair Therapy', 'Repara danos e fortalece os fios', 3),
('7891234567801', 'Condicionador Nutritivo', 49.90, 35, 'Hair Therapy', 'Nutrição intensiva para cabelos secos', 3),
('7891234567802', 'Máscara de Reconstrução', 89.90, 20, 'Keratin Power', 'Tratamento com queratina para fios danificados', 3),
('7891234567803', 'Óleo Capilar 50ml', 39.90, 50, 'Silky Hair', 'Óleo multifuncional para pontas duplas', 3),
('7891234567804', 'Spray Termoprotetor', 59.90, 30, 'Heat Guard', 'Protege até 230°C de calor', 3);

-- Inserindo perfumes
INSERT INTO tbProduto (codBar, nomeProd, precoProd, qtdProd, marcaProd, descricaoProd, idCategoria) VALUES
('7891234567805', 'Perfume Florais EDP 100ml', 199.90, 15, 'Floral Essence', 'Fragrância floral com notas de jasmim', 4),
('7891234567806', 'Colônia Fresh 200ml', 129.90, 20, 'Citrus Splash', 'Toque refrescante para o dia a dia', 4),
('7891234567807', 'Perfume Oriental 50ml', 179.90, 10, 'Mystic Night', 'Fragrância intensa e marcante', 4),
('7891234567808', 'Deo Colônia Sport', 79.90, 25, 'Active Man', 'Ideal para homens ativos', 4),
('7891234567809', 'Perfume Doce Vício 30ml', 149.90, 18, 'Vanilla Dream', 'Notas doces de baunilha e caramelo', 4);

-- Inserindo produtos para unhas
INSERT INTO tbProduto (codBar, nomeProd, precoProd, qtdProd, marcaProd, descricaoProd, idCategoria) VALUES
('7891234567810', 'Esmalte Vermelho Clássico', 9.90, 100, 'Nail Color', 'Vermelho vibrante com alta duração', 5),
('7891234567811', 'Kit Manicure Completo', 49.90, 20, 'Nail Pro', 'Alicate, cortador, lixa e palito', 5),
('7891234567812', 'Base Fortalecedora', 19.90, 40, 'Strong Nails', 'Prepara e fortalece as unhas', 5),
('7891234567813', 'Top Coat Brilho Intenso', 24.90, 35, 'Super Shine', 'Sela a cor e prolonga a duração', 5),
('7891234567814', 'Esmalte Removedor Rápido', 14.90, 50, 'Quick Off', 'Remove esmalte sem acetona', 5);

-- Inserindo produtos para corpo e banho
INSERT INTO tbProduto (codBar, nomeProd, precoProd, qtdProd, marcaProd, descricaoProd, idCategoria) VALUES
('7891234567815', 'Sabonete Líquido Hidratante', 24.90, 60, 'Soft Touch', 'Limpeza suave com extrato de aveia', 6),
('7891234567816', 'Creme Corporal Nutritivo', 39.90, 40, 'Body Care', 'Hidratação 24h para pele seca', 6),
('7891234567817', 'Óleo Corporal Relaxante', 59.90, 25, 'Aroma Bliss', 'Com óleos essenciais de lavanda', 6),
('7891234567818', 'Esfoliante Corporal Energizante', 34.90, 30, 'Invigorate', 'Remove células mortas e revitaliza', 6),
('7891234567819', 'Gel de Banho Aromático', 29.90, 45, 'Fresh Start', 'Fragrância revigorante', 6);

-- Inserindo produtos para barbearia
INSERT INTO tbProduto (codBar, nomeProd, precoProd, qtdProd, marcaProd, descricaoProd, idCategoria) VALUES
('7891234567820', 'Gel de Barbear Sensitive', 34.90, 40, 'Barber Pro', 'Para peles sensíveis, reduz irritações', 7),
('7891234567821', 'Lâmina de Barbear 5 Lâminas', 49.90, 50, 'Smooth Shave', 'Lâminas com tecnologia anti-irritação', 7),
('7891234567822', 'Loção Pós-Barba Hidratante', 44.90, 35, 'After Shave', 'Acalma e hidrata a pele após a barba', 7),
('7891234567823', 'Kit Barba Completo', 129.90, 15, 'Beard Care', 'Óleo, balm e escova para barba', 7),
('7891234567824', 'Tesoura para Barba Profissional', 59.90, 20, 'Barber Tools', 'Precisão no corte e acabamento', 7);

-- Inserindo acessórios de beleza
INSERT INTO tbProduto (codBar, nomeProd, precoProd, qtdProd, marcaProd, descricaoProd, idCategoria) VALUES
('7891234567825', 'Kit Pincéis de Maquiagem', 89.90, 25, 'Brush Set', '7 pincéis profissionais', 8),
('7891234567826', 'Espelho de Aumento Iluminado', 79.90, 20, 'Beauty Mirror', 'Luz LED e aumento 5x', 8),
('7891234567827', 'Necessaire Grande', 49.90, 30, 'Organize', 'Com divisórias e espelho interno', 8),
('7891234567828', 'Secador de Cabelo Profissional', 199.90, 15, 'Hair Dry', '2200W com 3 velocidades', 8),
('7891234567829', 'Chapinha Cerâmica', 159.90, 18, 'Straight Pro', 'Temperatura ajustável até 230°C', 8);

-- Inserindo produtos naturais
INSERT INTO tbProduto (codBar, nomeProd, precoProd, qtdProd, marcaProd, descricaoProd, idCategoria) VALUES
('7891234567830', 'Óleo de Coco Orgânico', 39.90, 40, 'Nature Pure', '100% puro para cabelo e pele', 9),
('7891234567831', 'Sabonete Natural de Argila', 19.90, 60, 'Earth Elements', 'Limpeza profunda sem químicos', 9),
('7891234567832', 'Hidratante Corporal Vegano', 49.90, 30, 'Green Care', 'Com manteiga de karité e óleo de amêndoas', 9),
('7891234567833', 'Desodorante Natural Roll-on', 29.90, 50, 'Pure Fresh', 'Sem alumínio e parabenos', 9),
('7891234567834', 'Óleo Essencial de Lavanda', 34.90, 25, 'Aroma Therapy', 'Para relaxamento e bem-estar', 9);

-- Inserindo tratamentos capilares
INSERT INTO tbProduto (codBar, nomeProd, precoProd, qtdProd, marcaProd, descricaoProd, idCategoria) VALUES
('7891234567835', 'Kit Progressiva de Queratina', 149.90, 10, 'Hair Magic', 'Tratamento em casa por 3 meses', 10),
('7891234567836', 'Ampola de Reconstrução', 24.90, 40, 'Hair Rescue', 'Recuperação instantânea para fios', 10),
('7891234567837', 'Tônico Antiqueda', 89.90, 20, 'Root Strengthen', 'Redução da queda em 30 dias', 10),
('7891234567838', 'Máscara de Hidratação Intensa', 59.90, 25, 'Moisture Boost', 'Para cabelos extremamente ressecados', 10),
('7891234567839', 'Óleo de Argan Puro', 69.90, 30, 'Moroccan Gold', 'Tratamento premium para fios', 10);
 
>>>>>>> 68c1ac3c63b82c5528ee599e81a2cef6e92e80a9

CREATE TABLE tblPerfil (
PerfilId int IDENTITY primary key,
Perfil varchar (100),
)
create table tblUsuario (
UsuarioId int identity primary key,
Nome varchar (100),
Usuario varchar (100),
Senha varchar (50),
Email varchar (100),
Ativo bit,
PerfilId int foreign key (PerfilId) references tblPerfil
) 
CREATE TABLE tblEstado (
EstadoId int IDENTITY primary key,
Estado varchar (100)
)
create table tblCidade (
CidadeId int identity primary key,
Cidade varchar (100),
EstadoId int foreign key (EstadoId) references tblEstado
)
create table tblCliente (
ClienteId int identity primary key,
Nome varchar(100),
CPF varchar(11),
DataNascimento date,
Endereco varchar(100),
Numero varchar(5),
Complemento varchar (100),
Bairro varchar(50),
CEP varchar(8),
Telefone varchar (15),
CidadeId int foreign key (CidadeId) references tblCidade,
UsuarioId int foreign key (UsuarioId) references tblUsuario
)
CREATE TABLE tblmarca(
marcaId int IDENTITY primary key,
marca varchar(50)
)
CREATE TABLE tblmodelo(
modeloId int IDENTITY primary key,
modelo varchar(50),
marcaId int foreign key (MarcaId) references tblMarca
)
create table tblCombustivel (
CombustivelId int identity primary key,
Combustivel varchar (20),
)
create table tblVeiculo (
VeiculoId int identity primary key,
Placa varchar (7),
AnoFabricacao varchar (4),
Cor varchar (100),
CombustivelId int foreign key (CombustivelId) references tblCombustivel,
modeloId int foreign key (modeloId) references tblmodelo,
ClienteId int foreign key (ClienteId) references tblCliente 
)


CREATE TABLE tblRecuperarSenha (
RecuperSenhaId int IDENTITY primary key,
Codigo varchar (100),
UsuarioId int foreign key (UsuarioId) references tblUsuario
)


create table tblSituacao (
SituacaoId int identity primary key,
Situacao varchar (100)
)
create table tblPedido (
PedidoId int identity primary key,
DataPedido date,
Ocorrencia varchar (100),
Imagem image,
Resposta varchar (4000),
VeiculoId int foreign key (VeiculoId) references tblVeiculo,
SituacaoId int foreign key (SituacaoId) references tblSituacao,
ClienteId int foreign key (ClienteId) references tblCliente
)

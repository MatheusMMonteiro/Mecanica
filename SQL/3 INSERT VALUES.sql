insert into tblUsuario 
(Nome, Usuario, Senha, Email, Ativo, PerfilId)
values
('João Silva', 'joao.silva', '12345', 'joaosilva@gmail.com',1,3)

insert into tblCliente 
(Nome, CPF, DataNascimento, Endereco, Numero,
Complemento, Bairro, CEP, Telefone, CidadeId, UsuarioId )
values
('João Pedro', '12345678901', '1980-02-03', 'Rua Tito', '1327',
'', 'Lapa', '99999190', '11999999999' ,177 , 1)

insert into tblVeiculo
(Placa, AnoFabricacao, Cor, CombustivelId, modeloId, ClienteId)
values
('DVL6E03', '2008', 'Prata', 3, 118, 1)

insert into tblPedido 
(DataPedido, Ocorrencia,VeiculoId, SituacaoId, ClienteId)
values
('2021-06-06','Teste', 1, 2, 1)


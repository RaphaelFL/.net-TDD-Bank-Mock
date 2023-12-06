using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Alura.ByteBank.Infraestrutura.Testes;
using Alura.ByteBank.Infraestrutura.Testes.DTO;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ByteBank.Infraestrutura.Teste
{
    public class ContaCorrenteRepositorioTestes
    {
        private readonly IContaCorrenteRepositorio _repositorio;

        public ContaCorrenteRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddTransient<IContaCorrenteRepositorio, ContaCorrenteRepositorio>();

            var provedor = servico.BuildServiceProvider();

            _repositorio = provedor.GetService<IContaCorrenteRepositorio>();
        }

        [Fact]
        public void TesteObterTodasContaCorrentes()
        {
            //Arrange
            //var _repositorio = new ClienteRepositorio();
            //Act
            List<ContaCorrente> lista = _repositorio.ObterTodos();
            //Assert
            Assert.NotNull(lista);
        }
        [Fact]
        public void TestaObterContaCorrente()
        {
            //Arrange
            //Act
            var cliente = _repositorio.ObterPorId(1);
            //Assert
            Assert.NotNull(cliente);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaContaCorrentePorVariosId(int id)
        {
            var cliente = _repositorio.ObterPorId(id);

            Assert.NotNull(cliente);
        }

        [Fact]
        public void TestaAtualizacaoSaldoDeterminadoConta()
        {
            var conta = _repositorio.ObterPorId(1);
            double saldoNovo = 15;
            conta.Saldo = saldoNovo;

            var atualizado = _repositorio.Atualizar(1, conta);

            Assert.True(atualizado);
        }

        [Fact]
        public void TestaInsertNovaContaCorrenteNoBancoDeDados()
        {
            var conta = new ContaCorrente()
            {
                Saldo = 10,
                Identificador = Guid.NewGuid(),
                Cliente = new Cliente()
                {
                    Nome = "Tomas",
                    CPF = "486.074.980-45",
                    Identificador = Guid.NewGuid(),
                    Profissao = "Batedor",
                    Id = 1
                },
                Agencia = new Agencia()
                {
                    Nome = "Uma",
                    Identificador = Guid.NewGuid(),
                    Id = 1,
                    Endereco = "Geladaaaaaaaaaaaaaaaaaaaaaaaaaa",
                    Numero = 24,
                }
            };
            var retorno = _repositorio.Adicionar(conta);
            Assert.True(retorno);
        }

        [Fact]
        public void TestConsultaPix()
        {
            //Arange
            var guid  = new Guid("a0b80d53-c0dd-4897-ab90-c0615ad80d5a");
            var pix = new PixDTO() { Chave = guid, Saldo = 10 };

            var pixRepositorioMock = new Mock<IPixRepositorio>();
            pixRepositorioMock.Setup(x => x.consultaPix(It.IsAny<Guid>())).Returns(pix);

            //Act
            var mock = pixRepositorioMock.Object;

            var saldo = mock.consultaPix(guid).Saldo;

            //Assert
            Assert.Equal(10, saldo);

        }
    }
}


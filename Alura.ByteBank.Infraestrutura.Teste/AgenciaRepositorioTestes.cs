using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Alura.ByteBank.Infraestrutura.Testes;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ByteBank.Infraestrutura.Teste
{
    public class AgenciaRepositorioTestes
    {
        private readonly IAgenciaRepositorio _repositorio;

        public AgenciaRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddTransient<IAgenciaRepositorio, AgenciaRepositorio>();
            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IAgenciaRepositorio>();
        }

        [Fact]
        public void TestaTodasAgencias()
        {
            List<Agencia> lista = _repositorio.ObterTodos();
            Assert.NotNull(lista);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterAgenciasPorId(int id)
        {
            var agencia = _repositorio.ObterPorId(id);
            Assert.NotNull(agencia);
        }
        [Fact]
        public void TestesRemoverInformacaoDetemidaAgencia()
        {

            var atualizado = _repositorio.Excluir(3);

            Assert.False(atualizado);
        }

        [Fact]
        public void TestExcecaoConstaAgenciaPorId()
        {
            Assert.Throws<Exception>(
                () => _repositorio.ObterPorId(33)
             );
        }
        [Fact]
        public void TestaObjetoAgenciaMock()
        {
            //Arange
            var bytebankRepositorioMock = new Mock<IByteBankRepositorio>();
            var Mock = bytebankRepositorioMock.Object;
            //Act
            var lista = Mock.BuscarAgencias();
            //Assert
            bytebankRepositorioMock.Verify(b => b.BuscarAgencias());

        }

        [Fact]
        public void TestAdicionarAgencia()
        {
            // Arrange
            var agencia = new Agencia()
            {
                Nome = "Agência Amaral",
                Identificador = Guid.NewGuid(),
                Id = 4,
                Endereco = "Rua Arthur Costa",
                Numero = 6497
            };

            var repositorioMock = new ByteBankRepositorio();
            //Act
            var adcionar = repositorioMock.AdicionarAgencia(agencia);
            //Assert
            Assert.True(adcionar);
        }
    }
}

using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ByteBank.Infraestrutura.Teste
{
    public class ClienteRepositoriTestes
    {

        private readonly IClienteRepositorio _repositorio;

        public ClienteRepositoriTestes()
        {
            var servico = new ServiceCollection();
            servico.AddTransient<IClienteRepositorio, ClienteRepositorio>();
            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IClienteRepositorio>();
        }

        [Fact]
        public void TesteObterTodosClientes()
        {
            //Arrange
            //var _repositorio = new ClienteRepositorio();
            //Act
            List<Cliente> lista = _repositorio.ObterTodos();
            //Assert
            Assert.NotNull(lista);
            Assert.Equal(3, lista.Count());
        }
        [Fact]
        public void TestaObterCliente()
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
        public void TestaClientePorVariosId(int id)
        {
            var cliente = _repositorio.ObterPorId(id);

            Assert.NotNull(cliente);
        }
    }
}

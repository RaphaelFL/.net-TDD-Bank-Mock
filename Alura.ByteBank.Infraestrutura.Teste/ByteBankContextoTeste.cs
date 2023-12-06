using Alura.ByteBank.Dados.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ByteBank.Infraestrutura.Teste
{
    public class ByteBankContextoTeste
    {
        [Fact]
        public void TestaConexaoContextoCmDBMYSQL()
        {
            //Arrange
            //dotnet add package Microsoft.EntityFrameworkCore.Design // dotnet tool update --global dotnet-ef
            //dotnet ef migrations add PopularBancoByteBank //
            //dotnet ef database update 
            var contexto = new ByteBankContexto();
            bool conectado;

            //Act
            try
            {
                conectado = contexto.Database.CanConnect();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel conectar a base de dados");
            }
            //Assert
            Assert.True(conectado);
        }
    }
}

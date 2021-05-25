using MontaPalavras.Servicos;
using NUnit.Framework;
using System.Text;

namespace Testes
{
    public class MontadorPalavraTeste
    {
        public MontadorPalavraServico montadorPalavraServico;

        [SetUp]
        public void Setup()
        {
            montadorPalavraServico = new MontadorPalavraServico();
        }

        [Test]
        public void MontarPalavra_OK()
        {
            var resultado = montadorPalavraServico.MontarPalavra("Hbxdríiqqcjko", "0");

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("HIDRICO, palavra de 14 pontos");
            stringBuilder.AppendLine("Sobraram: B, X, Q, Q, J, K");

            Assert.AreEqual(resultado, stringBuilder.ToString());
        }

        [Test]
        public void MontarPalavra_Inexistente()
        {
            var resultado = montadorPalavraServico.MontarPalavra("ccccccc", "0");

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Nenhuma palavra encontrada");
            stringBuilder.AppendLine("Sobraram: 0");

            Assert.AreEqual(resultado, stringBuilder.ToString());
        }

        [Test]
        public void MontarPalavra_Sem_Bonus()
        {
            var resultado = montadorPalavraServico.MontarPalavra("folgauv", "0");

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("FOLGA, palavra de 10 pontos");
            stringBuilder.AppendLine("Sobraram: U, V");

            Assert.AreEqual(resultado, stringBuilder.ToString());
        }

        [Test]
        public void MontarPalavra_Com_Bonus()
        {
            var resultado = montadorPalavraServico.MontarPalavra("folgauv", "2");

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("UVA, palavra de 12 pontos");
            stringBuilder.AppendLine("Sobraram: F, O, L, G");

            Assert.AreEqual(resultado, stringBuilder.ToString());
        }
    }
}

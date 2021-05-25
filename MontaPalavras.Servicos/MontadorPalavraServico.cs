using MontaPalavras.Comum;
using MontaPalavras.Dados;
using System.Linq;
using System.Text;

namespace MontaPalavras.Servicos
{

    public class MontadorPalavraServico
    {
        private PalavraServico _palavraServico;
        private PontuacaoServico _pontuacaoServico;

        public MontadorPalavraServico()
        {
            _palavraServico = new PalavraServico();
            _pontuacaoServico = new PontuacaoServico();
        }

        /// <summary>
        /// Realiza a chamada do calculo de palavras válidas e do calculo de pontos e exibe a palavra vencedora na tela inicial
        /// </summary>
        /// <param name="letrasInformadas">cadeia de letras informadas pelo usuário na tela inicial</param>
        /// <param name="posicaoBonus">Posição bonus a ser considera no cálculo de pontuação</param>
        /// <returns></returns>
        public string MontarPalavra(string letrasInformadas, string posicaoBonus)
        {
            var palavraVencedora = new ResultadoValido();

            //valida se entradas estão no padrão esperado
            ValidarEntradas(posicaoBonus);

            //Obtém palavras formadas a partir da lista de caracteres passada
            var resultadosValidos = _palavraServico.ObterPalavrasValidas(letrasInformadas);

            //Somente realiza o calculo de pontuação caso exista alguma palavra válida nos resultados obtidos
            if (resultadosValidos.Any())
                palavraVencedora = _pontuacaoServico.CalcularPalavraValidaVencedora(resultadosValidos, int.Parse(posicaoBonus));

            return FormatarResposta(palavraVencedora);
        }



        /// <summary>
        /// Formata o objeto ResultadoValido no texto padrão para exibição na tela inicial
        /// </summary>
        /// <param name="palavraVencedora">Objeto a ser formatado</param>
        /// <returns>texto no padrão da tela inicial </returns>
        private string FormatarResposta(ResultadoValido palavraVencedora)
        {
            var stringBuilder = new StringBuilder();

            if(palavraVencedora.Palavra == null)
                stringBuilder.AppendLine("Nenhuma palavra encontrada");
            else
            stringBuilder.AppendLine($"{palavraVencedora.Palavra.ToUpper()}, palavra de {palavraVencedora.Pontuacao} pontos");

            if(palavraVencedora.LetrasRestantes.Any())
                stringBuilder.AppendLine($"Sobraram: {string.Join(", ", palavraVencedora.LetrasRestantes)}");
            else
                stringBuilder.AppendLine("Sobraram: 0");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Realiza validação da entrada de dados realizada pelo usuário e retorna erro caso não estejam no padrão
        /// </summary>
        /// <param name="posicaoBonus">Posição bonus a ser considera no cálculo de pontuação</param>
        private void ValidarEntradas(string posicaoBonus)
        {
            if (!int.TryParse(posicaoBonus, out _))
                throw new RegraException("Posição Bonus inválida, somente números são permitidos.");
        }

    }
}

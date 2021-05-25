using MontaPalavras.Comum;
using MontaPalavras.Dados;
using System.Collections.Generic;
using System.Linq;

namespace MontaPalavras.Servicos
{
    public class PontuacaoServico
    {
        /// <summary>
        /// Obtém todas as pontuação validas da configuração.
        /// </summary>
        /// <returns>Lista de pontuações definidas</returns>
        public List<Pontuacao> ObterPontuacoes()
        {
            return ConfiguracaoHandler
                 .ObterPontuacoesConfiguracao();
        }

        /// <summary>
        /// Calcula a pontuação de cada palavra e retorna a palavra vencedora em meio a lista.
        /// </summary>
        /// <param name="resultadosValidos">Lista de palavras validadas presentes na cadeia informada pelo usuário</param>
        /// <param name="bonusPosicao">Posição bonus a ser considera no cálculo de pontuação</param>
        /// <returns>ResultadoValido da palavra com a pontuação vencedora</returns>
        public ResultadoValido CalcularPalavraValidaVencedora(List<ResultadoValido> resultadosValidos, int bonusPosicao = 0)
        {

            //Obtém a lista de pontuações pré-configurada
            var pontuacoes = ObterPontuacoes();

            //Itera sobre cada palavra valida chamando o cálculo de pontos para as letras daquela palavra
            //passando as informações de pontuação pre definidas e o bonus de posição
            foreach (var resultado in resultadosValidos)
            {
                CalcularPontuacao(pontuacoes, resultado, bonusPosicao);
            }

            //Retorna o ResultadoValido de maior valor, caso empate, realiza o desempate seguindo as regras internas
            return ProcessarMaiorResultado(resultadosValidos);
        }

        /// <summary>
        /// Realiza o cálculo da pontuação da palavra definida através da lista de pontuações, 
        /// levando em conta o bonus de posição.
        /// </summary>
        /// <param name="pontuacoes">Lista de pontuação pré carregada nas configurações</param>
        /// <param name="resultadoValido">Palavra valida a calcular pontuação</param>
        /// <param name="bonusPosicao">Posição da palavra no qual o bonus deve ser considerado</param>
        private void CalcularPontuacao(List<Pontuacao> pontuacoes, ResultadoValido resultadoValido, int bonusPosicao = 0)
        {
            //Itera sobre cada letra da palavra para buscar a pontuação relativa a ela
            for (int i = 0; i < resultadoValido.Palavra.Length; i++)
            {
                //Obtém a pontuação referente a letra corrente e armazena em variável
                var pontuacaoEquivalente = pontuacoes.First(o => o.Letras.Contains(resultadoValido.Palavra[i]));

                var pontos = pontuacaoEquivalente.Valor;

                //Realiza a validação da posição da letra corrente em relação a posição bonus definida,
                //Caso a posição bonus seja a posição letra corrente a pontuação da letra é dobrada
                if (bonusPosicao != 0 && bonusPosicao == i + 1)
                    pontos *= 2;

                //Associa pontuação ao resultado da palavra
                resultadoValido.Pontuacao += pontos;
            }
        }

        /// <summary>
        /// Realiza processamento da maior pontuação entre a lista de resultados válidos e caso
        /// haja empate aplica a validação dos critérios de desempate e retorna o resultado vencedor.
        /// </summary>
        /// <param name="resultadosValidos">Lista de palavras validadas presentes na cadeia informada pelo usuário</param>
        /// <returns>Objeto de ResultadoValido com a maior pontuação</returns>
        private ResultadoValido ProcessarMaiorResultado(List<ResultadoValido> resultadosValidos)
        {
            //Obtém a maior pontuação da lista de resultados
            var maiorPontuacao = resultadosValidos.OrderByDescending(o => o.Pontuacao).First();

            //verifica se existem resultados empatados
            var resultadosMaiorPontuacao = resultadosValidos.Where(o => o.Pontuacao == maiorPontuacao.Pontuacao);

            //Caso não exista empate retorna o resultado de pontuação
            if (resultadosMaiorPontuacao.Count() == 1)
                return resultadosMaiorPontuacao.First();

            //Caso exista empate realiza a chamada do método de validação dos criterios de desempate
            return ProcessarResultadosEmpatados(resultadosMaiorPontuacao.ToList());

        }

        /// <summary>
        /// Aplica os critérios de desempate para retornar o resultado válido vencedor
        /// </summary>
        /// <param name="resultadosEmpatados">Lista de palavras validadas empatadas em pontos</param>
        /// <returns>Objeto de ResultadoValido que venceu nos critérios de desempate</returns>
        private ResultadoValido ProcessarResultadosEmpatados(List<ResultadoValido> resultadosEmpatados)
        {
            //Calcula a menor palavra encontrada na lista
            var menorPalavraResultado = CalcularMenorPalavra(resultadosEmpatados);

            //Caso não existam palavras com o mesmo numero de letras, retorna a menor palavra como válida
            if (menorPalavraResultado.Count == 1)
                return menorPalavraResultado.First();

            //Caso existam palavras com o mesmo numero de letras, realiza o calculo da ordem alfabética
            return CalcularPalavraOrdemAlfabetica(menorPalavraResultado);

        }

        /// <summary>
        /// Calcula a menor palavra entre a lista de resultados empatados informada
        /// </summary>
        /// <param name="resultadosEmpatados">Lista de palavras validadas empatadas em pontos</param>
        /// <returns>As palavras que possuem o menor número de letras</returns>
        private List<ResultadoValido> CalcularMenorPalavra(List<ResultadoValido> resultadosEmpatados)
        {
            var resultadoEscolhido = new ResultadoValido();

            //Itera sobre as palavras para validar o tamanho de cada uma 
            foreach (var resultado in resultadosEmpatados)
            {
                //Valida se a palavra corrente é menor do que a palavra definida como a menor
                //Se sim define a palavra corrente como menor.
                if (resultadoEscolhido.Palavra == null)
                    resultadoEscolhido = resultado;
                else if (resultado.Palavra.Count() < resultadoEscolhido.Palavra.Count())
                    resultadoEscolhido = resultado;
                else if (resultado.Palavra.Count() == resultadoEscolhido.Palavra.Count())
                    continue;
            }

            //Retorna lista de palavras que possuem o mesmo numero de palavras que a menor,
            //Pois ao fim da iteração é possivel ter duas palavras com a mesma pontuação e o 
            //mesmo número de letras
            return resultadosEmpatados
                .Where(o=> o.Palavra.Count() == resultadoEscolhido.Palavra.Count())
                .ToList();

        }

        /// <summary>
        /// Calcula a palavra vencedora através da ordenação alfabética das palavras da lista.
        /// </summary>
        /// <param name="resultadosEmpatados">Lista de palavras validadas empatadas em pontos e em quantidade de letras</param>
        /// <returns>O Resultado Valido da primeira palavra da ordem alfabética</returns>
        private ResultadoValido CalcularPalavraOrdemAlfabetica(List<ResultadoValido> resultadosEmpatados)
        {
            var resultadoEscolhido = new ResultadoValido();

            //Itera sobre lista de palavras para validar ordem das letras
            foreach (var resultado in resultadosEmpatados)
            {
                //Caso seja a primeira palavra da validação define ela como a escolhida a priori
                if (resultadoEscolhido.Palavra == null)
                {
                    resultadoEscolhido = resultado;
                    continue;
                }

                //Itera sobre cada letra da palavra para comparar a posição alfabética das letras
                for (int i = 0; i < resultado.Palavra.Length; i++)
                {
                    //Valida se a letra na posição i da palavra corrente é antecesora a letra na posição i da 
                    //palavra definida como escolhida.
                    if (resultado.Palavra[i].ObterPosicaoAlfabetica() >
                        resultadoEscolhido.Palavra[i].ObterPosicaoAlfabetica())
                    {
                        //Caso a letra atual seja predecessora a letra da palavra escolhida, já sabemos que
                        //essa palavra não será anterior a escolhida então paramos o laço e vamos para a proxima palavra
                        break;
                    }
                    else if (resultado.Palavra[i].ObterPosicaoAlfabetica() ==
                       resultadoEscolhido.Palavra[i].ObterPosicaoAlfabetica())
                    {
                        //Caso as letras possuam a mesma posição, segue para a validação da proxima letra
                        continue;
                    }

                    //Caso a letra atual seja antecesora a letra da palavra escolhida, sabemos que
                    //essa palavra será anterior a escolhida então paramos o laço e definimos ela como a nova escolhida.

                    resultadoEscolhido = resultado;
                    break;

                }
            }

            return resultadoEscolhido;

        }
    }
}

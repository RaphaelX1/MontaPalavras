using MontaPalavras.Comum;
using MontaPalavras.Dados;
using System.Collections.Generic;
using System.Linq;

namespace MontaPalavras.Servicos
{
    public class PalavraServico
    {
        /// <summary>
        /// Obtém todas as palavras validas da configuração.
        /// </summary>
        /// <returns>Lista de palavras válidas</returns>
        public List<string> ObterPalavras()
        {
            return ConfiguracaoHandler
                 .ObterPalavrasConfiguracao();
        }

        /// <summary>
        /// Realiza chamada da validação de palavras e preenche uma lista com as palavras válidas para retornar.
        /// </summary>
        /// <param name="letrasInformadas">Cadeia de letras informadas pelo usuário na tela inicial</param>
        /// <returns>Lista do objeto ResultadoValidos com as respectivas palavras inseridas</returns>
        public List<ResultadoValido> ObterPalavrasValidas(string letrasInformadas)
        {
            var resultadosValidos = new List<ResultadoValido>();

            //obtém palavras válidas da configuração
            var palavrasPossiveis = ObterPalavras();

            //Remove caracteres especiais e coloca em maiúsculo tanto a lista de palavras quanto a cadeia de letras
            //para realizar as devidas comparações
            palavrasPossiveis = ProcessarFormatoPalavrasPossiveis(palavrasPossiveis);
            letrasInformadas = ProcessarFormatoLetrasInformadas(letrasInformadas);

            //Itera sobre a lista de palavras chamando o método de verificação para validar a palavra na 
            //na cadeia de letras informada
            foreach (var palavra in palavrasPossiveis)
            {
                var resultado = ValidarPalavra(palavra, letrasInformadas);

                if (resultado != null)
                    resultadosValidos.Add(resultado);
            }

            return resultadosValidos;

        }

        /// <summary>
        /// Remove acentos e coloca cadeia de letras em maiúsculo
        /// </summary>
        /// <param name="letrasInformadas">cadeia de letras a ser convertida</param>
        /// <returns>Cadeia de letras sem acento e com letras maiúsculas </returns>
        public string ProcessarFormatoLetrasInformadas(string letrasInformadas)
        {
            letrasInformadas = letrasInformadas.RemoverAcentos();
            letrasInformadas = letrasInformadas.ToUpper();

            return letrasInformadas;
        }

        /// <summary>
        /// Remove acentos e coloca lista de palavras em maiúsculo
        /// </summary>
        /// <param name="palavrasPossiveis">Lista de palavras a serem convertidas</param>
        /// <returns>Lista de palavras sem acento e com letras maiúsculas</returns>
        public List<string> ProcessarFormatoPalavrasPossiveis(List<string> palavrasPossiveis) 
        {
            palavrasPossiveis = palavrasPossiveis.Select(o => o.RemoverAcentos()).ToList();
            palavrasPossiveis = palavrasPossiveis.Select(o => o.ToUpper()).ToList();

            return palavrasPossiveis;
        }

        /// <summary>
        /// Valida se as letras informadas são capazes de formar a palavra específica
        /// </summary>
        /// <param name="palavra">palavra a ser formada</param>
        /// <param name="letrasInformadas">Cadeia de letras informadas pelo usuário na tela inicial</param>
        /// <returns>Objeto ResultadoValido caso a palavra tenha sido formada e nulo caso não</returns>
        private ResultadoValido ValidarPalavra(string palavra, string letrasInformadas)
        {
            //Itera sobre cada letra da palavra a ser formada 
            foreach (var letra in palavra)
            {
                //Se existir uma letra da palavra que não está presente nas letras informadas pelo usuário a palavra
                //não será formada e é retornado vazio
                if (letrasInformadas.IndexOf(letra) == -1)
                    return null;

                //Para cada letra informada encontrada na palavra é feita a remoção da letra da lista informada pelo usuário
                letrasInformadas = letrasInformadas.Remove(letrasInformadas.IndexOf(letra), 1);
            }

            //Se a iteraração acima for completamente finalizada sem retornar nulo significa que a palavra existe dentro 
            //da lista de letras informadas pelo o usuário, então é retornado um resultado válido com ela e suas letras restantes
            return new ResultadoValido() 
            {
                Palavra = palavra,
                LetrasRestantes = letrasInformadas.Select(o=> o)
            };

        }
    }
}

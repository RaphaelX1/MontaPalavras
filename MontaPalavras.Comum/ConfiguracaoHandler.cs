using MontaPalavras.Dados;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MontaPalavras.Comum
{
    public static class ConfiguracaoHandler
    {
        /// <summary>
        /// Recupera objeto informado no arquivo de configuração e o devolve no formato do objeto especificado 
        /// </summary>
        /// <typeparam name="T">Objeto para o qual o arquivo deve ser convertido</typeparam>
        /// <param name="arquivo">nome do arquivo onde serão buscadas as configurações</param>
        /// <returns>Objeto do arquivo de configuração</returns>
        public static T ObterObjetoConfiguracao<T>(string arquivo) 
        {
            StreamReader sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), arquivo), Encoding.GetEncoding(28591));
            string arquivoStr = sr.ReadToEnd();

            return JsonConvert.DeserializeObject<T>(arquivoStr);

        }

        /// <summary>
        /// Recupera as informações configuráveis da lista de palavras disponível e converte para o objeto específico
        /// </summary>
        /// <returns>Objeto Palavras preenchido da configuração</returns>
        public static List<string> ObterPalavrasConfiguracao()
        {
            return ObterObjetoConfiguracao<Palavras>("palavrasConfiguracao.json").PalavrasPossiveis;
        }

        /// <summary>
        /// Recupera as informações configuráveis da lista de pontuação e converte para o objeto específico
        /// </summary>
        /// <returns>Lista de objetos Pontuacao preenchido da configuração</returns>
        public static List<Pontuacao> ObterPontuacoesConfiguracao()
        {
            return ObterObjetoConfiguracao<Pontuacoes>("pontuacaoConfiguracao.json").PontuacoesPossiveis;
        }
    }
}

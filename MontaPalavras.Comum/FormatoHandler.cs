using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MontaPalavras.Comum
{
    public static class FormatoHandler
    {
        /// <summary>
        /// Remove os acentos e caracteres especiais de uma palavra
        /// </summary>
        /// <param name="texto">palavra a ser convertida</param>
        /// <returns>palavra sem caracteres especiais</returns>
        public static string RemoverAcentos(this string texto)
        {
            return new string(texto
                .Normalize(NormalizationForm.FormD)
                    .Where(caracter => char.GetUnicodeCategory(caracter) != UnicodeCategory.NonSpacingMark)
                 .ToArray());
        }

        /// <summary>
        /// Obtém a posição na ordem alfabética de um dado caracter
        /// </summary>
        /// <param name="caracter">caracter a ser consultado</param>
        /// <returns>Posição numérica do caracter no alfabeto</returns>
        public static int ObterPosicaoAlfabetica(this char caracter)
        {
            return char.ToUpper(caracter) - 64;
        }

        /// <summary>
        /// Verifica se a cadeia de caracteres contém apenas letras válidas
        /// </summary>
        /// <param name="texto">cadeia de caracteres a ser validada</param>
        /// <returns>Verdadeiro caso contenha apenas letras e falso caso não</returns>
        public static bool SomenteLetras(this string texto)
        {
            return Regex.IsMatch(texto, @"^[a-zA-Z]+$");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MontaPalavras.Dados
{
    public class ResultadoValido
    {
        public ResultadoValido()
        {
            LetrasRestantes = new List<char>();
        }

        public string Palavra { get; set; }

        public int Pontuacao { get; set; }

        public IEnumerable<char> LetrasRestantes { get; set; }
    }
}

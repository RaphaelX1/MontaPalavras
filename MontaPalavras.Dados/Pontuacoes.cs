using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MontaPalavras.Dados
{

    public class Pontuacoes
    {
        [JsonProperty]
        public List<Pontuacao> PontuacoesPossiveis { get; set; }
    }

    public class Pontuacao
    {
        [JsonProperty]
        public int Valor { get; set; }

        [JsonProperty]
        public List<char> Letras { get; set; }

    }
}

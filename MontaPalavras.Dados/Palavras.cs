using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MontaPalavras.Dados
{
    public class Palavras
    {
        [JsonProperty]
        public List<string> PalavrasPossiveis { get; set; }
    }
}

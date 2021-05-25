using System;
using MontaPalavras.Comum;
using MontaPalavras.Servicos;

namespace MontaPalavras
{
    class Program
    {
        static void Main(string[] args)
        {
            //Inicializa serviço principal do projeto
            var montadorPalavra = new MontadorPalavraServico();

            while (true)
            {
                try
                {
                    Console.WriteLine("------------------------------ Monta Palavras ------------------------------");
                    Console.WriteLine("Digite as letras disponíveis nesta jogada:");
                    var valoresEntrada = Console.ReadLine();

                    Console.WriteLine("Digite a posição bônus:");
                    var posicaoBonus = Console.ReadLine();


                    //Realiza a chamada do método de cálculo do montador de palavras
                    var resultado = montadorPalavra.MontarPalavra(valoresEntrada, posicaoBonus);
                    Console.WriteLine(resultado);
                   
                }
                catch (RegraException e)
                {
                    //Exibe na tela erros relacionados a regra de negócio
                    Console.WriteLine(e.Message);
                }
                catch (Exception)
                {
                    //Exibe mensagem amigável para erros não mapeados
                    Console.WriteLine("Houve um erro inesperado ! :(");
                }

                //Aguarda entrada de tecla para retornar ao Loop
                Console.ReadKey();
            }
        }
    }
}

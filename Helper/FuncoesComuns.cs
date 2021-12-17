using System;
using System.Text;
namespace Car_Storage
{
    public static class FuncoesComuns
    {
        public static bool escrevePergunta(string msg = "", bool read = false)
        {
            criaLinha();
            Console.WriteLine(msg + (read ? "\nPressione qualquer tecla para continuar." : string.Empty));
            if (read)
            {
                Console.ReadKey();
                Console.Clear();
                return false;
            }
            string? resposta = Console.ReadLine();
            Console.Clear();
            if (!string.IsNullOrEmpty(resposta) &&
            resposta.ToLower() == "s")
                return true;
            else
                return false;
        }
        public static void criaLinha(int repeticoes = 30, char simbolo = '=')
        {
            var store = new StringBuilder(repeticoes);
            int index = 0;
            while (index < repeticoes)
            {
                store.Append(simbolo);
                index++;
            }
            Console.WriteLine(store.ToString());
        }
    }
}
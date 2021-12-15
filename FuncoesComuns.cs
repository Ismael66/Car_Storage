using System;
using System.Text;
namespace Car_Storage
{
    public class FuncoesComuns
    {
        public bool retornarMenu(string msg = "", bool read = false)
        {
            criaLinha();
            Console.WriteLine(msg + (read ? "\nPressione qualquer tecla para continuar." : string.Empty));
            if (read)
            {
                Console.ReadKey();
                return false;
            }
            string? resposta = Console.ReadLine();
            // Console.Clear();
            if (!string.IsNullOrEmpty(resposta) &&
            resposta.ToLower() == "s")
                return true;
            else
                return false;
        }
        public void criaLinha(int repeticoes = 30, char simbolo = '=')
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
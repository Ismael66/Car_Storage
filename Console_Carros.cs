using System;
using System.Text;

namespace Car_Storage
{
    public class Console_Carros
    {
        public static void Menu()
        {
            try
            {
                Console.Clear();
                criaLinha();
                Console.WriteLine("Menu Principal");
                criaLinha();
                Console.WriteLine("[1] Listar os veículos cadastrados\n" +
                "[2] Inserir um novo veículo\n" +
                "[3] Listar os veículos filtrando-se por ano de fabricação\n" +
                "[4] Listar os veículos com o ano de fabricação\n" +
                "[5] Listar os veículos filtrando-se pelo modelo");
                criaLinha();
                Console.Write("Digite a opção desejada: ");
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        static void criaLinha(int repeticoes = 30, char simbolo = '-')
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
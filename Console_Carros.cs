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
                Console.WriteLine("[1] Inserir um novo veículo\n" +
                "[2] Listar os veículos cadastrados\n" +
                "[3] Listar os veículos filtrando-se por ano de fabricação\n" +
                "[4] Listar os veículos com o ano de fabricação\n" +
                "[5] Listar os veículos filtrando-se pelo modelo");
                criaLinha();
                Console.Write("Digite a opção desejada: ");
                Escolha(Console.ReadLine());
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        static void Escolha(string? escolha)
        {
            try
            {
                Console.Clear();
                criaLinha();
                switch (escolha)
                {
                    case "1":
                        inserirNovoVeiculo();
                        break;
                    case "2":
                        Console.WriteLine("2");
                        break;
                    case "3":
                        Console.WriteLine("3");
                        break;
                    case "4":
                        Console.WriteLine("4");
                        break;
                    case "5":
                        Console.WriteLine("5");
                        break;
                    default:
                        Console.WriteLine("default");
                        Menu();
                        break;
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region Funções Switch
        static void inserirNovoVeiculo()
        {
            Console.Clear();
            StringBuilder carro = new StringBuilder();
            criaLinha();
            carro.Append("==============================\n");
            Console.WriteLine("Digite a marca do carro");
            carro.Append($"Marca: {Console.ReadLine()}\n");
            Console.WriteLine("Digite o modelo do carro");
            carro.Append($"Modelo: {Console.ReadLine()}\n");
            Console.WriteLine("Digite o ano de fabricação do carro");
            carro.Append($"Ano: {Console.ReadLine()}\n");
            Console.WriteLine("Digite a placa do carro");
            carro.Append($"Placa: {Console.ReadLine()}");
            Console.WriteLine(carro);
            criaLinha();
        }
        #endregion
        static void criaLinha(int repeticoes = 30, char simbolo = '=')
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
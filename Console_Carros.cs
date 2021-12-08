using System;
using System.Text;

namespace Car_Storage
{
    public class Console_Carros
    {
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @$"\Carros.txt";
        public static void Menu()
        {
            try
            {
                Console.Clear();
                criaLinha();
                Console.WriteLine("Menu Principal");
                criaLinha();
                Console.WriteLine("[1] Inserir um novo veículo\n" +
                "[2] Listar os veículos cadastrados");
                criaLinha();
                Console.Write("Digite a opção desejada: ");
                escolha(Console.ReadLine());
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        static void escolha(string? escolha)
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
                        listarVeiculos();
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
            if (File.Exists(path))
            {
                Console.Clear();
                var arquivo = new StreamWriter(path);
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
                arquivo.Write(carro);
                arquivo.Close();
                criaLinha();
            }
            else criaArquivo();
        }
        static void listarVeiculos()
        {
            if (File.Exists(path))
            {
                Console.Clear();
                criaLinha();
                Console.WriteLine("Como deseja listar?");
                criaLinha();
                Console.WriteLine("[1] Listar os veículos filtrando pela ordem de cadastro\n" +
                    "[2] Listar os veículos filtrando pelo ano de fabricação\n" +
                    "[3] Listar os veículos filtrando pelo modelo");
                criaLinha();
                Console.Write("Digite a opção desejada: ");
                escolhaListar(Console.ReadLine());
            }
            else criaArquivo();
        }
        #endregion
        static void escolhaListar(string escolha)
        {

            try
            {
                Console.Clear();
                criaLinha();
                switch (escolha)
                {
                    case "1":
                        listaOrdemCadastro();
                        break;
                    case "2":
                        Console.WriteLine("Lista ordem ano");
                        break;
                    case "3":
                        Console.WriteLine("Lista ordem modelo");
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
        #region Funções Switch Lista
        static void listaOrdemCadastro()
        {

        }
        #endregion
        #region Funções Normais
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
        static void criaArquivo()
        {
            Console.WriteLine("O arquivo com a lista de carros não existe.\nDeseja criar um novo? <s/n>");
            if (Console.ReadLine() == "s")
            {
                criaLinha();
                var arquivo = new StreamWriter(path, true);
                arquivo.Close();
                Console.WriteLine("Arquivo criado com sucesso.\nPressione qualquer tecla para continuar.");
                Console.ReadKey();
                Menu();
            }
            else Menu();
        }
        #endregion

    }
}
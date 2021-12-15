using System;
using Newtonsoft.Json;

namespace Car_Storage
{
    public class Console_Carros
    {
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @$"\Carros.txt";
        public static void Menu()
        {
            try
            {
                // Console.Clear();
                FuncoesComuns.criaLinha();
                Console.WriteLine("Menu Principal");
                FuncoesComuns.criaLinha();
                Console.WriteLine("[1] Inserir um novo veículo\n" +
                "[2] Listar os veículos cadastrados");
                FuncoesComuns.criaLinha();
                Console.Write("Digite a opção desejada: ");
                escolha(Console.ReadLine());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region Funções Switch Principal
        static void escolha(string? escolha)
        {
            try
            {
                // Console.Clear();
                FuncoesComuns.criaLinha();
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        static void inserirNovoVeiculo()
        {
            try
            {
                if (File.Exists(path))
                {
                    // Console.Clear();
                    var veiculo = new Carro();
                    var arquivo = new StreamWriter(path, append: true);
                    FuncoesComuns.criaLinha();
                    Console.WriteLine("Digite a marca do carro");
                    veiculo.marca = Console.ReadLine();
                    Console.WriteLine("Digite o modelo do carro");
                    veiculo.modelo = Console.ReadLine();
                    Console.WriteLine("Digite o ano de fabricação do carro");
                    veiculo.ano = int.Parse(Console.ReadLine());
                    Console.WriteLine("Digite a placa do carro");
                    veiculo.placa = Console.ReadLine();
                    Console.WriteLine(veiculo.marca);
                    Console.WriteLine(veiculo.modelo);
                    Console.WriteLine(veiculo.ano);
                    Console.WriteLine(veiculo.placa);
                    arquivo.WriteLine(JsonConvert.SerializeObject(veiculo) + ";");
                    arquivo.Close();
                    FuncoesComuns.escrevePergunta("Informações guardadas com sucesso.", true);
                    Menu();
                }
                else FuncoesArquivo.criaArquivo();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Função inserirNovoVeiculo, erro :{ex.Message}");
                throw new Exception(ex.Message);
            }
        }
        static void listarVeiculos()
        {
            if (File.Exists(path))
            {
                if (System.IO.File.ReadAllText(path) != "")
                {
                    // Console.Clear();
                    FuncoesComuns.criaLinha();
                    Console.WriteLine("Como deseja listar?");
                    FuncoesComuns.criaLinha();
                    Console.WriteLine("[1] Listar os veículos filtrando pela ordem de cadastro\n" +
                        "[2] Listar os veículos filtrando pelo ano de fabricação\n" +
                        "[3] Listar os veículos filtrando pelo modelo");
                    FuncoesComuns.criaLinha();
                    Console.Write("Digite a opção desejada: ");
                    escolhaListarVeiculos(Console.ReadLine());
                }
                else
                {
                    FuncoesComuns.escrevePergunta("O arquivo está vazio, insira dados.", true);
                    Menu();
                }
            }
            else FuncoesArquivo.criaArquivo();
        }
        #endregion
        #region Funções Switch Lista
        static void escolhaListarVeiculos(string? escolha)
        {
            try
            {
                // Console.Clear();
                FuncoesComuns.criaLinha();
                switch (escolha)
                {
                    case "1":
                        listaOrdemCadastro();
                        break;
                    case "2":
                        listaOrdemAno();
                        break;
                    case "3":
                        listaOrdemModelo();
                        break;
                    default:
                        Console.WriteLine("default");
                        Menu();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        static void listaOrdemCadastro()
        {
            // Console.Clear();
            List<Carro> carrosLista = FuncoesArquivo.insereValorListaCarros();
            imprimeCarros(carrosLista);
        }
        static void listaOrdemAno()
        {
            List<Carro> carrosLista = FuncoesArquivo.insereValorListaCarros();
            imprimeCarros(carrosLista.OrderBy(elemento => elemento.ano));
        }
        static void listaOrdemModelo()
        {
            List<Carro> carrosLista = FuncoesArquivo.insereValorListaCarros();
            imprimeCarros(carrosLista.OrderBy(elemento => elemento.modelo));
        }
        private static void imprimeCarros(IEnumerable<Carro> listaCarros)
        {
            if (listaCarros.Count<Carro>() == 0)
                FuncoesComuns.escrevePergunta("O arquivo está vazio, insira dados.", true);            
            else
            {
                foreach (Carro carro in listaCarros)
                {
                    Console.WriteLine($"=> Marca:{carro.marca}; Modelo:{carro.modelo}; Ano:{carro.ano}; Placa:{carro.placa}.");
                }
                FuncoesComuns.escrevePergunta("Arquivo aberto com sucesso.", true);
            }
            Menu();
        }
        #endregion
    }
}
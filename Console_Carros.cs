using System;
using Newtonsoft.Json;

namespace Car_Storage
{
    public class Console_Carros
    {
        public static FuncoesComuns FComuns = new FuncoesComuns();
        static Carro veiculo = new Carro();
        static FuncoesArquivo FArquivo = new FuncoesArquivo();
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @$"\Carros.txt";
        public static void Menu()
        {
            try
            {
                // Console.Clear();
                FComuns.criaLinha();
                Console.WriteLine("Menu Principal");
                FComuns.criaLinha();
                Console.WriteLine("[1] Inserir um novo veículo\n" +
                "[2] Listar os veículos cadastrados");
                FComuns.criaLinha();
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
                FComuns.criaLinha();
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
                    var arquivo = new StreamWriter(path, append: true);
                    FComuns.criaLinha();
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
                    FComuns.retornarMenu("Informações guardadas com sucesso.", true);
                    Menu();
                }
                else FArquivo.criaArquivo();
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
                    FComuns.criaLinha();
                    Console.WriteLine("Como deseja listar?");
                    FComuns.criaLinha();
                    Console.WriteLine("[1] Listar os veículos filtrando pela ordem de cadastro\n" +
                        "[2] Listar os veículos filtrando pelo ano de fabricação\n" +
                        "[3] Listar os veículos filtrando pelo modelo");
                    FComuns.criaLinha();
                    Console.Write("Digite a opção desejada: ");
                    escolhaListarVeiculos(Console.ReadLine());
                }
                else
                {
                    FComuns.retornarMenu("O arquivo está vazio, insira dados.", true);
                    Menu();
                }
            }
            else FArquivo.criaArquivo();
        }
        #endregion
        #region Funções Switch Lista
        static void escolhaListarVeiculos(string? escolha)
        {
            try
            {
                // Console.Clear();
                FComuns.criaLinha();
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
            List<Carro> carrosLista = FArquivo.insereValorListaCarros();
            foreach (Carro elemento in carrosLista)
            {
                Console.WriteLine($"=> Marca:{elemento.marca}; Modelo:{elemento.modelo}; Ano:{elemento.ano}; Placa:{elemento.placa}.");
            }
            carrosLista.Clear();
            FComuns.retornarMenu("Arquivo aberto com sucesso.", true);
            Menu();
        }
        static void listaOrdemAno()
        {
            List<Carro> carrosLista = FArquivo.insereValorListaCarros();
            IEnumerable<Carro> sequencia = carrosLista.OrderBy(elemento => elemento.ano);
            foreach (Carro elemento in sequencia)
            {
                Console.WriteLine($"=> Marca:{elemento.marca}; Modelo:{elemento.modelo}; Ano:{elemento.ano}; Placa:{elemento.placa}.");
            }
            carrosLista.Clear();
            FComuns.retornarMenu("Arquivo aberto com sucesso.", true);
            Menu();
        }
        static void listaOrdemModelo()
        {
            List<Carro> carrosLista = FArquivo.insereValorListaCarros();
            IEnumerable<Carro> sequencia = carrosLista.OrderBy(elemento => elemento.modelo);
            foreach (Carro elemento in sequencia)
            {
                Console.WriteLine($"=> Marca:{elemento.marca}; Modelo:{elemento.modelo}; Ano:{elemento.ano}; Placa:{elemento.placa}.");
            }
            carrosLista.Clear();
            FComuns.retornarMenu("Arquivo aberto com sucesso.", true);
            Menu();
        }
        #endregion

        #region Funções Arquivo

        #endregion
    }
}
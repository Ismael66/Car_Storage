using System;
using System.Text;
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
                Console.Clear();
                while (true)
                {
                    FuncoesComuns.criaLinha();
                    Console.WriteLine("Menu Principal");
                    FuncoesComuns.criaLinha();
                    Console.WriteLine("[1] Inserir um novo veículo\n" +
                    "[2] Listar os veículos cadastrados\n" +
                    "[3] Sair do programa");
                    FuncoesComuns.criaLinha();
                    Console.Write("Digite a opção desejada: ");
                    if (escolha() == "3")
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region Funções Switch Principal
        static string? escolha()
        {
            string? escolha = Console.ReadLine();
            try
            {
                Console.Clear();
                switch (escolha)
                {
                    case "1":
                        inserirNovoVeiculo();
                        break;
                    case "2":
                        listarVeiculos();
                        break;
                    case "3":
                        break;
                    case "testeMassa":
                        inserirMassaDados();
                        break;
                    default:
                        break;
                }
                return escolha;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        static void inserirMassaDados()
        {
            Console.WriteLine("Gravando...");
            var arquivo = new StreamWriter(path, append: true);
            for (int i = 0; i < 500; i++)
            {
                var veiculo = new Carro()
                {
                    marca = $"marca {i}",
                    ano = 2000 + i,
                    modelo = $"modelo {i}",
                    placa = $"placa {i}"
                };
                arquivo.WriteLine(JsonConvert.SerializeObject(veiculo) + ";");
            }
            arquivo.Close();
        }
        static void inserirNovoVeiculo()
        {
            try
            {
                if (File.Exists(path))
                {
                    Console.Clear();
                    var veiculo = new Carro();
                    var arquivo = new StreamWriter(path, append: true);
                    FuncoesComuns.criaLinha();
                    Console.WriteLine("Digite a marca do carro");
                    veiculo.marca = Console.ReadLine();
                    Console.WriteLine("Digite o modelo do carro");
                    veiculo.modelo = Console.ReadLine();
                    Console.WriteLine("Digite o ano de fabricação do carro");
                    veiculo.ano = validaAno();
                    Console.WriteLine("Digite a placa do carro");
                    veiculo.placa = validaPlaca();
                    Console.WriteLine(veiculo.marca);
                    Console.WriteLine(veiculo.modelo);
                    Console.WriteLine(veiculo.ano);
                    Console.WriteLine(veiculo.placa);
                    arquivo.WriteLine(JsonConvert.SerializeObject(veiculo) + ";");
                    arquivo.Close();
                    FuncoesComuns.escrevePergunta("Informações guardadas com sucesso.", true);
                }
                else
                    FuncoesArquivo.criaArquivo();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Função inserirNovoVeiculo, erro :{ex.Message}");
                throw new Exception(ex.Message);
            }
        }
        public static void listarVeiculos()
        {
            if (File.Exists(path))
            {
                if (System.IO.File.ReadAllText(path) != "")
                {
                    // Console.Clear();
                    while (true)
                    {
                        FuncoesComuns.criaLinha();
                        Console.WriteLine("Como deseja listar?");
                        FuncoesComuns.criaLinha();
                        Console.WriteLine("[1] Listar os veículos filtrando pela ordem de cadastro\n" +
                            "[2] Listar os veículos filtrando pelo ano de fabricação\n" +
                            "[3] Listar os veículos filtrando pelo modelo\n" +
                            "[4] Retornar ao menu principal.");
                        FuncoesComuns.criaLinha();    
                        Console.Write("Digite a opção desejada: ");
                        if (escolhaListarVeiculos() == "4")
                            break;
                    }
                }
                else
                {
                    FuncoesComuns.escrevePergunta("O arquivo está vazio, insira dados.", true);
                }
            }
            else FuncoesArquivo.criaArquivo();
        }
        #endregion
        #region Funções Validação Inserir novo carro
        static int validaAno()
        {
            int ano;
            while (true)
            {
                int.TryParse(Console.ReadLine(), out ano);
                int anoAtual = DateTime.Now.Year;
                if (ano > 1500 && ano <= anoAtual)
                    break;
                Console.WriteLine("Digite um ano de fabricação válido.\nApenas datas maiores que 1500 e menores que a data atual.");
            }
            return ano;
        }
        static string validaPlaca()
        {
            string? placa;
            while (true)
            {
                placa = Console.ReadLine();
                if (placa.Length == 7)
                {
                    int quantLetras = 0;
                    int quantNumeros = 0;
                    for (int i = 0; i < placa.Length; i++)
                    {
                        if (Char.IsNumber(placa[i]))
                            quantNumeros++;
                        else if (Char.IsLetter(placa[i]))
                            quantLetras++;
                    }
                    if (quantLetras == 3 && quantNumeros == 4)
                        break;
                }
                Console.WriteLine("Digite uma placa válida.\nNela deve conter um total de 7 caracteres, incluindo 3 letras e 4 numeros.");
            }
            return placa;
        }
        #endregion
        #region Funções Switch Lista
        static string? escolhaListarVeiculos()
        {
            try
            {
                // Console.Clear();
                string? escolha = Console.ReadLine();
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
                    case "4":
                        break;
                    default:
                        break;
                }
                return escolha;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        static void listaOrdemCadastro()
        {
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
            if (listaCarros.Count() == 0)
                FuncoesComuns.escrevePergunta("O arquivo está vazio, insira dados.", true);
            else
            {
                FuncoesComuns.criaLinha();
                var teste = new StringBuilder();
                foreach (Carro carro in listaCarros)
                {
                    teste.Append($"=> Marca:{carro.marca}; Modelo:{carro.modelo}; Ano:{carro.ano}; Placa:{carro.placa}.\n");
                }
                Console.WriteLine(teste.ToString());
                FuncoesComuns.escrevePergunta("Arquivo aberto com sucesso.", true);
            }
        }
        #endregion
    }
}
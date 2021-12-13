using System;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Car_Storage
{
    public class Console_Carros
    {
        static List<Carro> carrosLista = new List<Carro>();
        static Carro veiculo = new Carro();
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @$"\Carros.txt";
        public static void Menu()
        {
            try
            {
                // Console.Clear();
                criaLinha();
                Console.WriteLine("Menu Principal");
                criaLinha();
                Console.WriteLine("[1] Inserir um novo veículo\n" +
                "[2] Listar os veículos cadastrados");
                criaLinha();
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
                    criaLinha();
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
                    retornarMenu("Informações guardadas com sucesso.", true);
                    Menu();
                }
                else criaArquivo();
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
                    criaLinha();
                    Console.WriteLine("Como deseja listar?");
                    criaLinha();
                    Console.WriteLine("[1] Listar os veículos filtrando pela ordem de cadastro\n" +
                        "[2] Listar os veículos filtrando pelo ano de fabricação\n" +
                        "[3] Listar os veículos filtrando pelo modelo");
                    criaLinha();
                    Console.Write("Digite a opção desejada: ");
                    escolhaListarVeiculos(Console.ReadLine());
                }
                else
                {
                    retornarMenu("O arquivo está vazio, insira dados.", true);
                    Menu();
                }
            }
            else criaArquivo();
        }
        #endregion
        #region Funções Switch Lista
        static void escolhaListarVeiculos(string? escolha)
        {
            try
            {
                // Console.Clear();
                criaLinha();
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
            insereValorListaCarros();
            foreach (Carro elemento in carrosLista)
            {
                Console.WriteLine($"=> Marca:{elemento.marca}; Modelo:{elemento.modelo}; Ano:{elemento.ano}; Placa:{elemento.placa}.");
            }
            retornarMenu("Arquivo aberto com sucesso.", true);
            Menu();
        }
        static void listaOrdemAno()
        {
            insereValorListaCarros();
            IEnumerable<Carro> sequencia = carrosLista.OrderBy(elemento => elemento.ano);
            foreach (Carro elemento in sequencia)
            {
                Console.WriteLine($"=> Marca:{elemento.marca}; Modelo:{elemento.modelo}; Ano:{elemento.ano}; Placa:{elemento.placa}.");
            }
            retornarMenu("Arquivo aberto com sucesso.", true);
            Menu();
        }
        static void listaOrdemModelo()
        {
            insereValorListaCarros();
            IEnumerable<Carro> sequencia = carrosLista.OrderBy(elemento => elemento.modelo);
            foreach (Carro elemento in sequencia)
            {
                Console.WriteLine($"=> Marca:{elemento.marca}; Modelo:{elemento.modelo}; Ano:{elemento.ano}; Placa:{elemento.placa}.");
            }
            retornarMenu("Arquivo aberto com sucesso.", true);
            Menu();
        }
        #endregion
        #region Funções Comuns
        public static bool retornarMenu(string msg = "", bool read = false)
        {
            criaLinha();
            Console.WriteLine(msg + (read ? "\nPressione qualquer tecla para continuar." : string.Empty));
            if (read)
            {
                Console.ReadKey();
                carrosLista.Clear();
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
        #region Funções Arquivo
        static void insereValorListaCarros()
        {
            string arquivo = System.IO.File.ReadAllText(path);
            if (arquivo.Contains(";"))
            {
                string[] blocos = arquivo.Split(";");
                for (int i = 0; i < blocos.Length; i++)
                {
                    if (!string.IsNullOrEmpty(blocos[i]))
                    {
                        try
                        {
                            var itemConvertido = JsonConvert.DeserializeObject<Carro>(blocos[i]);
                            if (itemConvertido != null)
                                carrosLista.Add(itemConvertido);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Função insereValorListaCarros, erro :{ex.Message}");
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
            else
            {
                carrosLista.Add(JsonConvert.DeserializeObject<Carro>(arquivo));
            }
        }
        #endregion
    }
}
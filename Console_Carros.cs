using System;
using System.Text;
using Newtonsoft.Json;

namespace Car_Storage
{
    public class Console_Carros
    {
        static ArquivoFormato conteudo = new ArquivoFormato();
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
                var arquivo = new StreamWriter(path, append: true);
                criaLinha();
                Console.WriteLine("Digite a marca do carro");
                string? marca = Console.ReadLine();
                Console.WriteLine("Digite o modelo do carro");
                string modelo = Console.ReadLine();
                Console.WriteLine("Digite o ano de fabricação do carro");
                int ano = int.Parse(Console.ReadLine());
                Console.WriteLine("Digite a placa do carro");
                string placa = Console.ReadLine();
                string teste = "{" + $"marca : {marca}, modelo : {modelo}, ano : {ano}, placa : {placa}" + "}";
                Console.WriteLine(teste);
                conteudo = JsonConvert.DeserializeObject<ArquivoFormato>(teste);
                Console.WriteLine(conteudo.marca);
                Console.WriteLine(conteudo.modelo);
                Console.WriteLine(conteudo.ano);
                Console.WriteLine(conteudo.placa);
                arquivo.WriteLine(teste);
                arquivo.Close();
                retornarMenu("Informações guardadas com sucesso.", true);
                Menu();
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
                        separaArquivo();
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
            Console.Clear();
            string arquivo = System.IO.File.ReadAllText(path);
            Console.Write(arquivo);
            retornarMenu("Arquivo aberto com seucesso.", true);
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
        static void separaArquivo()
        {
            string arquivo = System.IO.File.ReadAllText(path);
            Console.WriteLine("Entrou");
            string[] blocos = arquivo.Split("}");
            Console.WriteLine(blocos);
            Console.WriteLine(blocos[0]);
            conteudo = JsonConvert.DeserializeObject<ArquivoFormato>(blocos[0]);
            Console.WriteLine("Entrou");
            Console.WriteLine(conteudo.marca);
            Console.WriteLine(blocos[1]);
        }
        #endregion
    }

}
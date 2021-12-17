using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Car_Storage
{
    public class FuncoesArquivo
    {
        static string[] blocos;
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @$"\Carros.txt";
        public static void criaArquivo()
        {
            Console.WriteLine("O arquivo com a lista de carros não existe.\nDeseja criar um novo? <s/n>");
            if (Console.ReadLine() == "s")
            {
                FuncoesComuns.criaLinha();
                var arquivo = new StreamWriter(path, true);
                arquivo.Close();
                FuncoesComuns.escrevePergunta("Arquivo criado com sucesso", true);
            }
        }
        public static List<Carro> insereValorListaCarros()
        {
            var carrosLista = new List<Carro>();
            string arquivo = System.IO.File.ReadAllText(path);
            if (arquivo.Contains(";"))
            {
                blocos = arquivo.Split(";");
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
                            Console.Clear();
                            string[] linhasArquivo = System.IO.File.ReadAllLines(path);
                            FuncoesComuns.criaLinha();
                            Console.WriteLine($"A linha {i} está com um ou mais caracteres indesejados.");
                            Console.WriteLine($"Linha {i} = {linhasArquivo[i - 1]}");
                            FuncoesComuns.criaLinha();
                            removeTextoIndesejadoArquivo(i);
                        }
                    }
                }
            }
            return carrosLista;
        }
        static void removeTextoIndesejadoArquivo(int linha)
        {
            Console.WriteLine("O que deseja fazer?");
            FuncoesComuns.criaLinha();
            Console.WriteLine("[1] Programa apaga linha\n" +
                "[2] Usuário apaga caracteres indesejados manualmente\n" +
                "[3] Retornar");
            FuncoesComuns.criaLinha();
            Console.Write("Digite a opção desejada: ");
            escolhaErroArquivo(linha);
        }
        static string escolhaErroArquivo(int linha)
        {
            string? escolha = Console.ReadLine();
            switch (escolha)
            {
                case "1":
                    apagarLinha(linha);
                    break;
                case "2":
                    abrirArquivo();
                    Environment.Exit(0);
                    break;
                case "3":
                    Console_Carros.listarVeiculos();
                    break;
                default:
                    break;
            }
            return escolha;
        }
        static void apagarLinha(int linha)
        {
            string[] linhasArquivo = System.IO.File.ReadAllLines(path);
            linhasArquivo[linha - 1] = "";
            File.WriteAllLines(path, linhasArquivo);
            FuncoesComuns.escrevePergunta("Arquivo alterado com sucesso.", true);
            Console_Carros.listarVeiculos();
        }
        static void abrirArquivo()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("carros.txt");
            startInfo.FileName = path;
            startInfo.UseShellExecute = true;
            Process.Start(startInfo);
        }
    }
}
using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Car_Storage
{
    public class FuncoesArquivo
    {
        static string[]? blocos;
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
                        catch (Exception)
                        {
                            Console.Clear();
                            string[] linhasArquivo = System.IO.File.ReadAllLines(path);
                            FuncoesComuns.criaLinha();
                            Console.WriteLine($"A linha {i} está com um ou mais caracteres indesejados.");
                            Console.WriteLine($"Linha {i} = {linhasArquivo[i - 1]}");
                            FuncoesComuns.criaLinha();
                            Console_Carros.menuArquivo();
                            Console_Carros.escolhaErroArquivo(i);
                        }
                    }
                }
            }
            return carrosLista;
        }
        public static void apagarLinha(int linha)
        {
            string[] linhasArquivo = System.IO.File.ReadAllLines(path);
            linhasArquivo[linha - 1] = "";
            File.WriteAllLines(path, linhasArquivo);
            FuncoesComuns.escrevePergunta("Arquivo alterado com sucesso.", true);
            Console_Carros.listarVeiculos();
        }
        public static void abrirArquivo()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("carros.txt");
            startInfo.FileName = path;
            startInfo.UseShellExecute = true;
            Process.Start(startInfo);
        }
    }
}
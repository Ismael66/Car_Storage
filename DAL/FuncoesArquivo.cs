using System;
using Newtonsoft.Json;

namespace Car_Storage
{
    public class FuncoesArquivo
    {       
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
            return carrosLista;
        }
    }
}
using System;
using Newtonsoft.Json;

namespace Car_Storage
{
    public class FuncoesArquivo
    {
        public static List<Carro> carrosLista = new List<Carro>();
        static Console_Carros menu = new Console_Carros();
        public static FuncoesComuns FComuns = new FuncoesComuns();
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @$"\Carros.txt";

        public void criaArquivo()
        {
            Console.WriteLine("O arquivo com a lista de carros não existe.\nDeseja criar um novo? <s/n>");
            if (Console.ReadLine() == "s")
            {
                FComuns.criaLinha();
                var arquivo = new StreamWriter(path, true);
                arquivo.Close();
                Console.WriteLine("Arquivo criado com sucesso.\nPressione qualquer tecla para continuar.");
                Console.ReadKey();
                return;
            }
            else return;
        }
        public List<Carro> insereValorListaCarros()
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
            return carrosLista;
        }
    }
}
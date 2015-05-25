using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace ContaTamanhoCSV
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                long count = 0;
                var tamanhos = new List<int>();
                var reader = new StreamReader(File.OpenRead(args[0]));

                Console.WriteLine("Processando {0}...", args[0]);

                while (!reader.EndOfStream)
                {
                    count++;
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    // Verifica se é necessário crescer a lista
                    if (values.Length > tamanhos.Count)
                    {
                        for (int i = tamanhos.Count; i < values.Length; i++)
                        {
                            tamanhos.Add(0);
                        }
                    }

                    // Atualiza o tamanho máximo das colunas
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (values[i].Length > tamanhos[i]) tamanhos[i] = values[i].Length;
                    }

                    // Exibe o contador.
                    if (count % 100000 == 0) Console.WriteLine("Processado arquivo {0} ...", count);
                }

                // Conclusão, exibe os tamanhos máximos das colunas:
                Console.WriteLine("Processadas {0:9,990} linhas.", count);
                Console.WriteLine("Tamanho máximo das colunas: ");
                for (int i = 0; i < tamanhos.Count; i++)
                {
                    Console.WriteLine("- Coluna {0} : {1} caracteres.", i, tamanhos[i]);
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine(@"Uso: ContaTamanhoCSV <caminho_arquivo_csv>");
                Console.WriteLine();
                Console.WriteLine(@"  Exemplos:");
                Console.WriteLine(@"     ContaTamanhoCSV arquivo.csv");
                Console.WriteLine(@"     ContaTamanhoCSV c:\pasta1\pasta2\arquivo.csv");
                Console.WriteLine(@"     ContaTamanhoCSV ..\pastaX\arquivo.csv");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(@"ERRO: O arquivo {0} não pode ser encontrado.", args[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(@"ERRO: Ocorreu um erro não esperado: {0}", e.Message);
            }

#if DEBUG
            Console.ReadKey();
#endif
        }
    }
}

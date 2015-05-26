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
                var colunas = new List<string>();
                var tamanhos = new List<int>();
                var reader = new StreamReader(File.OpenRead(args[0]));

                Console.WriteLine();
                Console.WriteLine("Processando {0}...", args[0]);
                Console.WriteLine();

                while (!reader.EndOfStream)
                {
                    count++;
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    // Verifica se é a primeira linha (cabeçalho) e guarda os nomes das colunas
                    if (count == 1)
                    {
                        colunas.AddRange(values);
                    }
                    else
                    {
                        // Verifica se a linha tem mais colunas do que deveria.
                        if (values.Length > colunas.Count) Console.WriteLine("AVISO: A linha {0:0,0} possui colunas excedentes.", count);
                        
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
                        if (count % 100000 == 0) Console.WriteLine("Processado linha {0:#,0} ...", count);
                    }
                }

                // Conclusão, exibe os tamanhos máximos das colunas:
                Console.WriteLine("Processadas {0:#,0} linhas.\n", count);
                Console.WriteLine("-- Tamanho máximo das colunas: ");
                for (int i = 0; i < colunas.Count; i++)
                {
                    Console.WriteLine("-- Coluna \"{0}\" --> {1:#,0} caracteres.", colunas[i], tamanhos[i]);
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine();
                Console.WriteLine(@"Uso: ContaTamanhoCSV <caminho_arquivo_csv>");
                Console.WriteLine();
                Console.WriteLine(@"  Exemplos:");
                Console.WriteLine(@"    ContaTamanhoCSV arquivo.csv");
                Console.WriteLine(@"    ContaTamanhoCSV c:\pasta1\pasta2\arquivo.csv");
                Console.WriteLine(@"    ContaTamanhoCSV ..\pastaX\arquivo.csv");
                Console.WriteLine();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine();
                Console.WriteLine(@"ERRO: O arquivo {0} não pode ser encontrado.", args[0]);
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(@"ERRO: Ocorreu um erro não esperado: {0}", e.Message);
                Console.WriteLine();
            }

#if DEBUG
            Console.ReadKey();
#endif
        }
    }
}

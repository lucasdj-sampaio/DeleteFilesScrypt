using System;
using System.IO;
using System.Threading.Tasks;

namespace DeleteCrimFile
{
    class Program
    {
        private readonly static string logFileName = $"{DateTime.Now:yyyy_MM_dd_hh_mm_ss}.txt";

        static void Main(string[] args)
        {
            try
            {
                foreach (var file in File.ReadAllLines(args[0]))
                {
                    if (File.Exists($"{args[2]}\\{file}"))
                    {
                        if (!Directory.Exists($"{args[1]}\\{file.Replace(new FileInfo(file).Name, "")}"))
                            Directory.CreateDirectory($"{args[1]}\\{file.Replace(new FileInfo(file).Name, "")}");

                        if (MoveAsync($"{args[2]}\\{file}", $"{args[1]}\\{file}").GetAwaiter().GetResult())
                            RegisterLog(args[0].Replace(new FileInfo(args[0]).Name, ""),
                                $"---------------------------\nArquivo ({new FileInfo($"{args[2]}\\{file}").Name}) movido " +
                                $"DE: {args[2]}/{file}" +
                                $"PARA: {args[1]}/{file}\n");
                        else
                            RegisterLog(args[0].Replace(new FileInfo(args[0]).Name, ""),
                                $"---------------------------\nErro ao mover arquivo - \n{args[2]}\\{file}\n");
                    }
                }

                Console.WriteLine("PROCESSO CONCLUÍDO");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO NA EXECUÇÃO: {ex.Message}");
                Console.ReadKey();
            }
        }

        private static async Task<bool> MoveAsync(string oldFile, string newFile)
        {
            try
            {
                using (FileStream dfStream = File.Open(oldFile, FileMode.Open))
                using (FileStream newStream = File.Create(newFile))
                    await dfStream.CopyToAsync(newStream);

                if (new FileInfo(oldFile).Length != new FileInfo(newFile).Length)
                {
                    try { File.Delete(newFile); } catch { }
                    return false;
                }
                else 
                {
                    try { File.Delete(oldFile); } catch { }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private static void RegisterLog(string path, string register)
        {
            if (!File.Exists($"{path}\\{logFileName}"))
                File.Create($"{path}\\{logFileName}").Close();

            File.AppendAllTextAsync($"{path}\\{logFileName}", register).Dispose();
        }
    }
}
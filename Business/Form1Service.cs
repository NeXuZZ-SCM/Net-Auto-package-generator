using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Form1Service
    {
        public void ZipAndBackoupFiles(string folderToZip, string zipFilePath, List<string> selectedDeleteFiles, typeOfZip type)
        {
            // Crear un archivo zip temporal
            using (ZipArchive archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
            {
                // Obtener todos los archivos en la carpeta
                DirectoryInfo infoCarpeta = new DirectoryInfo(folderToZip);
                var algo = infoCarpeta.GetFiles();
                foreach (FileInfo file in infoCarpeta.GetFiles())
                {
                    if (type is typeOfZip.Backup)
                    {
                        archive.CreateEntryFromFile(file.FullName, file.Name);
                    }
                    else
                    {
                        // Verificar si el archivo está en la lista de exclusión
                        if (!selectedDeleteFiles.Contains(file.Name))
                        {
                            if (!selectedDeleteFiles.Contains(file.Extension))
                            {
                                // Añadir el archivo al archivo zip
                                archive.CreateEntryFromFile(file.FullName, file.Name);
                            }
                        }
                    }
                }
            }
        }
        public void DeleteFiles(string folderToZip, List<string> selectedDeleteFiles)
        {
            foreach (var item in selectedDeleteFiles)
            {
                try
                {
                    // Obtener todos los archivos en la carpeta con la extensión deseada
                    string[] files = Directory.GetFiles(folderToZip, $"*{item}");

                    foreach (string file in files)
                    {
                        try
                        {
                            // Eliminar cada archivo
                            File.Delete(file);
                            Console.WriteLine($"Archivo eliminado: {file}");
                        }
                        catch (IOException ioEx)
                        {
                            // Manejar la excepción si el archivo no se puede eliminar
                            Console.WriteLine($"No se pudo eliminar el archivo {file}. Error: {ioEx.Message}");
                        }
                        catch (UnauthorizedAccessException unEx)
                        {
                            // Manejar excepciones relacionadas con permisos
                            Console.WriteLine($"No se tienen los permisos necesarios para eliminar el archivo {file}. Error: {unEx.Message}");
                        }
                    }
                    Console.WriteLine("Todos los archivos correspondientes han sido eliminados.");
                }
                catch (DirectoryNotFoundException dirEx)
                {
                    // Manejar la excepción si la carpeta no existe
                    Console.WriteLine($"La carpeta especificada no existe: {dirEx.Message}");
                }
            }
        }
        public enum typeOfZip
        {
            Backup,
            Deploy
        }
    }
}

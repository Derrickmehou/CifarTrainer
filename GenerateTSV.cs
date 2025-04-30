using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CifarTrainer
{
    public class GenerateTSV
    {
        public static void CreateTSV(string datasetFolder, string outputFile)
        {
            using (var writer = new StreamWriter(outputFile))
            {
                foreach (var labelFolder in Directory.GetDirectories(datasetFolder))
                {
                    string label = Path.GetFileName(labelFolder);
                    foreach (var imagePath in Directory.GetFiles(labelFolder, "*.png"))
                    {
                        string relativePath = Path.Combine(label, Path.GetFileName(imagePath))
                           .Replace("\\", "/");
                        writer.WriteLine($"{relativePath}\t{label}");
                    }
                }
            }

            Console.WriteLine($"Fichier {outputFile} généré avec succès !");
        }
    }
}

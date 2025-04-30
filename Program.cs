using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Vision;

namespace CifarTrainer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var mlContext = new MLContext();
            string datasetFolder = "cifar10";
            string tsvPath = "cifar10_train.tsv";

            // Générer le fichier TSV
            GenerateTSV.CreateTSV(datasetFolder, tsvPath);

            // Charger les données
            var data = mlContext.Data.LoadFromTextFile<ImageInputData>(
                path: tsvPath,
                //separatorChar: '\t',
                hasHeader: false);

            // Pipeline d'apprentissage

        var pipeline = mlContext.Transforms.Conversion
       .MapValueToKey(outputColumnName: "LabelAsKey", inputColumnName: "Label")
       .Append(mlContext.Transforms.LoadRawImageBytes(
           outputColumnName: "Image",
           imageFolder: datasetFolder,
           inputColumnName: "ImagePath"))
       .Append(mlContext.MulticlassClassification.Trainers.ImageClassification(new ImageClassificationTrainer.Options
       {
           FeatureColumnName = "Image",
           LabelColumnName = "LabelAsKey",
        
       }))
       .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            Console.WriteLine("Entraînement du modèle...");
            var model = pipeline.Fit(data);
            Console.WriteLine("Modèle entraîné !");

            // Sauvegarde
            mlContext.Model.Save(model, data.Schema, "cifar10_model.zip");
            Console.WriteLine("Modèle sauvegardé !");
        }
    }
}

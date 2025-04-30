using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CifarTrainer
{
        public class ImageInputData
        {
            [LoadColumn(0)]
            public string? ImagePath;
            [LoadColumn(1)]
            public string? Label;
        }

        public class ImagePrediction : ImageInputData
        {
            public string PredictedLabel;
            public float[] Score;
        } 
}

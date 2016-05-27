using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork_01
{
   public class JsonWorker
    {
        public static string Convert(string message)
        {
            var jsons = message.Split('\n');
            string text = null;

            foreach (var j in jsons)
            {
                dynamic jsonObject = JsonConvert.DeserializeObject(j);
                if (jsonObject == null || jsonObject.result.Count <= 0) continue;

                text = jsonObject.result[0].alternative[0].transcript;
            }
            return text;
        }
        public static int getNumber(string message)
        {
            var jsons = message.Split('\n');
            int number = -1;

            foreach (var j in jsons)
            {
                dynamic jsonObject = JsonConvert.DeserializeObject(j);
                if (jsonObject == null || jsonObject.result.Count <= 0) continue;

                var alternativesNumber = jsonObject.result[0].alternative.Count;
                for (int i = 0; i < alternativesNumber; i++)
                {
                    string text = jsonObject.result[0].alternative[i].transcript;
                    if (int.TryParse(text, out number) == true) break;
                }
            }
            return number;
        }
    }
}

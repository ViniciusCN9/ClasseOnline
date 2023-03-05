using System.Collections.Generic;
using System.IO;
using Models.Entities;
using Newtonsoft.Json;

namespace Utils
{
    public static class JsonUtil
    {
        public static List<T> CarregarEntidadeJson<T>(string caminhoJson)
        {
            using (StreamReader streamReader = new StreamReader(caminhoJson))
            {
                var json = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
        }

        public static void EscreverEntidadeJson<T>(List<T> entidades, string caminhoJson)
        {
            var json = JsonConvert.SerializeObject(entidades, Formatting.Indented);
            File.WriteAllText(caminhoJson, json);
        }
    }
}
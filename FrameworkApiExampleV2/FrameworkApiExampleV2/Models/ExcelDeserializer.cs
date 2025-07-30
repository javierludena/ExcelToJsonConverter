using Newtonsoft.Json;
using System.Collections.Generic;

namespace FrameworkApiExample.Models
{
    /// <summary>
    /// Clase helper para deserializar el JSON del Excel a objetos tipados
    /// </summary>
    public static class ExcelDeserializer
    {
        /// <summary>
        /// Deserializa el JSON completo del Excel y extrae los datos de una hoja específica
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a deserializar</typeparam>
        /// <param name="jsonResult">JSON completo del Excel</param>
        /// <param name="sheetName">Nombre de la hoja a extraer</param>
        /// <returns>Lista de objetos del tipo especificado</returns>
        public static List<T> DeserializeSheet<T>(string jsonResult, string sheetName)
        {
            // Deserializar el JSON principal
            var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResult);
            
            // Extraer la hoja específica
            if (jsonObject.ContainsKey(sheetName))
            {
                var sheetJson = jsonObject[sheetName].ToString();
                return JsonConvert.DeserializeObject<List<T>>(sheetJson);
            }
            
            return new List<T>();
        }

        /// <summary>
        /// Deserializa los datos del "Fichero de carga"
        /// </summary>
        /// <param name="jsonResult">JSON completo del Excel</param>
        /// <returns>Lista de objetos FicheroCarga</returns>
        public static List<FicheroCarga> DeserializeFicheroCarga(string jsonResult)
        {
            return DeserializeSheet<FicheroCarga>(jsonResult, "Fichero de carga");
        }

        /// <summary>
        /// Obtiene los nombres de todas las hojas disponibles en el JSON
        /// </summary>
        /// <param name="jsonResult">JSON completo del Excel</param>
        /// <returns>Lista con los nombres de las hojas</returns>
        public static List<string> GetSheetNames(string jsonResult)
        {
            var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResult);
            return new List<string>(jsonObject.Keys);
        }
    }
}
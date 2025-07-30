using System;
     using System.Net;
     using System.Net.Http;
     using System.Threading.Tasks;
     using System.Web.Http;
     using ExcelToJsonConverter; // Asegúrate de que esta línea esté presente
     using FrameworkApiExample.Models;

     namespace FrameworkApiExample.Controllers
     {
         public class ExcelUploadController : ApiController
         {
             [HttpPost]
             [Route("api/excel/convert")]
             public async Task<HttpResponseMessage> ConvertExcel()
             {
                 // Comprueba si la solicitud es de tipo multipart/form-data.
                 if (!Request.Content.IsMimeMultipartContent())
                 {
                     throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                 }

                 var provider = new MultipartMemoryStreamProvider();

                 try
                 {
                     await Request.Content.ReadAsMultipartAsync(provider);

                     // Busca el primer archivo en el formulario.
                     foreach (var file in provider.Contents)
                     {
                         var stream = await file.ReadAsStreamAsync();
                         if (stream.Length > 0)
                         {
                             // Usa la librería para convertir el stream a JSON.
                             // Nota: Usamos ConvertToJson síncrono aquí, ya que el async es para.NET 4.8+
                             string jsonResult = ExcelConverter.ConvertToJson(stream);

                             // EJEMPLO: Cómo deserializar el JSON a objetos tipados
                             // =====================================================
                             // 
                             // OPCIÓN 1: Usar la clase helper ExcelDeserializer
                             // var datosFichero = ExcelDeserializer.DeserializeFicheroCarga(jsonResult);
                             // 
                             // OPCIÓN 2: Para hojas con nombres específicos
                             // var clientes = ExcelDeserializer.DeserializeSheet<Cliente>(jsonResult, "Clientes");
                             // var pedidos = ExcelDeserializer.DeserializeSheet<Pedido>(jsonResult, "Pedidos");
                             //
                             // OPCIÓN 3: Ver qué hojas están disponibles
                             // var nombreHojas = ExcelDeserializer.GetSheetNames(jsonResult);
                             //
                             // EJEMPLO DE USO REAL:
                             // var datos = ExcelDeserializer.DeserializeFicheroCarga(jsonResult);
                             // foreach(var item in datos)
                             // {
                             //     Console.WriteLine($"Cliente: {item.NombreSolicitante}");
                             //     Console.WriteLine($"Material: {item.TextoBreveMaterial}");
                             //     Console.WriteLine($"Cantidad: {item.CantidadPedido}");
                             //     Console.WriteLine($"Importe: {item.Importe}");
                             // }

                             // Devuelve el JSON en la respuesta.
                             return new HttpResponseMessage(HttpStatusCode.OK)
                             {
                                 Content = new StringContent(jsonResult, System.Text.Encoding.UTF8, "application/json")
                             };
                      }
                     }

                     return Request.CreateResponse(HttpStatusCode.BadRequest, "No file uploaded.");
                 }
                 catch (Exception e)
                 {
                     return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
               }
          }
      }
     }
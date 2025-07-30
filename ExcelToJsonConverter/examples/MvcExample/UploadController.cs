using System.Web.Mvc;
using System.Web;
using ExcelToJsonConverter;
using System.IO;

namespace MvcExample.Controllers
{
    public class UploadController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    // Convert the uploaded file stream to JSON
                    string json = ExcelConverter.ConvertToJson(file.InputStream);

                    ViewBag.Json = json;
                }
                catch (System.Exception ex)
                {
                    ViewBag.Error = "Error: " + ex.Message;
                }
            }
            else
            {
                ViewBag.Error = "Please select a file.";
            }

            return View("Index");
        }
    }
}

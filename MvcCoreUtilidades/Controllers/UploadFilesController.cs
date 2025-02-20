using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Herlpers;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private HelperPathProvider helperPath;

        public UploadFilesController(HelperPathProvider helperPath)
        {
            this.helperPath = helperPath;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SubirFichero()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            string fileName = fichero.FileName;
  
            string path = this.helperPath.MapPath(fileName, Folders.Images);
            //string path = Path.Combine(this.helperPath.GetBasePath(), Folders.Images, fileName);

            //PARA SUBIR EL FICHERO SE UTILIZA STREAM CON IFORMFILE
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            string UrlPath = this.helperPath.MapUrlPath(fileName, Folders.Images);
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            ViewData["PATH"] = UrlPath;
            return View();
        }
    }
}

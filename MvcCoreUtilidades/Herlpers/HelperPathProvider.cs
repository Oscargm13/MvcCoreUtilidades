﻿using System.Globalization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;

namespace MvcCoreUtilidades.Herlpers
{
    //VAMOS A OFERECER EN PROGRMACION UNA ENUMERACION CON LAS CARPETAS DE NUESTRO SERVIDOR

    public enum Folders { Images, Facturas, Uploads, Temporal }
    public class HelperPathProvider
    {
        private IWebHostEnvironment hostEnvironment;
        private IHttpContextAccessor httpContextAccessor;
        private IServer server;

        public HelperPathProvider(IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor, IServer server)
        {
            this.hostEnvironment = hostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.server = server;
        }

        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if(folder == Folders.Images)
            {
                carpeta = "images";
            }else if (folder == Folders.Facturas)
            {
                carpeta = "facturas";
            }else if(folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "temp";
            }
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);
            return path;

        }
        public string MapUrlPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            else if (folder == Folders.Facturas)
            {
                carpeta = "facturas";
            }
            else if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "temp";
            }
            var adresses = this.server.Features.Get<IServerAddressesFeature>().Addresses;
            string serverUrl = adresses.FirstOrDefault();
            string urlPath = serverUrl + "/" + carpeta + "/" + fileName;
            return urlPath;
        }
    }
}

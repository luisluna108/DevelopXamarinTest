﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DevelopXamarinTestAPI.Helpers
{
    public class FilesHelper
    {
        public static string UploadPhoto(MemoryStream stream, string folder, string name)
        {
            try
            {
                stream.Position = 0;
                var path = Path.Combine(HttpContext.Current.Server.MapPath(folder), name);
                File.WriteAllBytes(path, stream.ToArray());
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

            return "succes";
        }
    }
}
namespace Site.Services
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Drawing;
    using Models;
    using Services;

    class ImageService: HttpPostedFileBase
    {
        [HttpPost]
        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string encodedImage = Convert.ToBase64String(imageBytes);
                return encodedImage;
            }
        }
    }
}
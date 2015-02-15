namespace Site.Storage
{
    using System;
    using System.Web;

    public interface IImageStore
    {
        Uri StoreImageAsync(HttpPostedFileBase imageFile);
    }
}
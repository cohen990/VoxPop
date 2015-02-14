namespace Site.Storage
{
    using System;
    using System.IO;

    public interface IImageStore
    {
        Uri StoreImageAsync(Stream stream);
    }
}
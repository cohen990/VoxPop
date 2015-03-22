namespace Site.Storage
{
    using System;
    using System.IO;
    using System.Net.Mime;
    using System.Web;
    using System.Web.UI.WebControls;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class BlobImageStore : IImageStore
    {
        private CloudBlobContainer _container;

        public BlobImageStore()
        {
            InitializeBlobImageStore();
        }

        private void InitializeBlobImageStore()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("voxpop.blobstorageaccount"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            _container = blobClient.GetContainerReference("voxpopuserimages");

            // Create the container if it doesn't already exist.
            _container.CreateIfNotExists();

            var permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Container
            };

            _container.SetPermissions(permissions);
        }

        public Uri StoreImageAsync(HttpPostedFileBase imageFile)
        {
            var reference = Guid.NewGuid().ToString("N");

            var stream = imageFile.InputStream;

            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(reference);
            stream.Seek(0, SeekOrigin.Begin);
            blockBlob.Properties.ContentType = "image";
            blockBlob.UploadFromStream(stream);

            return blockBlob.Uri;
        }
    }
}
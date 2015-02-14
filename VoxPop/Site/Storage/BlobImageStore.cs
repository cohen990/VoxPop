namespace Site.Storage
{
    using System;
    using System.IO;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class BlobImageStore : IImageStore
    {
        private readonly CloudBlobContainer _container;

        public BlobImageStore()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("voxpop.blobstorageaccount"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            _container = blobClient.GetContainerReference("VoxPop.UserImages");

            _container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Container
            });

            // Create the container if it doesn't already exist.
            _container.CreateIfNotExists();
        }

        public Uri StoreImageAsync(Stream stream)
        {
            var reference = Guid.NewGuid().ToString("N") + ".png";

            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(reference);
            stream.Seek(0, SeekOrigin.Begin);
            blockBlob.UploadFromStream(stream);

            return blockBlob.Uri;
        }
    }
}
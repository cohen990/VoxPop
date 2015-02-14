namespace Site.Services
{
    using System;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System.IO;

    public class BlobStore
    {
        private readonly CloudStorageAccount _storageAccount;

        private readonly CloudBlobClient _blobClient;

        private readonly CloudBlobContainer _container;

        public BlobStore()
        {
            // Retrieve storage account from connection string.
            _storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("voxpop.blobstorageaccount"));

            // Create the blob client.
            _blobClient = _storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            _container = _blobClient.GetContainerReference("userimagescontainer");

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
﻿namespace Site.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.UI.WebControls;
    using Microsoft.Data.Edm.Csdl;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
    using Microsoft.WindowsAzure.Storage.Auth;
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

            // Create the container if it doesn't already exist.
            _container.CreateIfNotExists();
        }

        public Uri StoreAsync(Stream stream)
        {
            var reference = Guid.NewGuid().ToString("N");

            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(reference);
            stream.Seek(0, SeekOrigin.Begin);
            blockBlob.UploadFromStream(stream);

            return blockBlob.Uri;
        }
    }
}
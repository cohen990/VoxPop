namespace Site.Services
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Net;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Auth;
    using Microsoft.WindowsAzure.Storage.Table;
    using Models;

    class BlogService : IBlogService
    {
        private readonly TableStore<BlogEntity> _blogStore;

        public BlogService()
        {
            _blogStore = new TableStore<BlogEntity>();
        }

        public void CreateAsync(BlogViewModel blog)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<BlogEntity>> GetAllAsync()
        {
            return await _blogStore.GetAllAsync();
        }
    }

    public class BlogEntity : ITableEntity
    {
        /// <summary>
        /// Populates the entity's properties from the
        /// <see cref="T:Microsoft.WindowsAzure.Storage.Table.EntityProperty"/> data values in the
        /// <paramref name="properties"/> dictionary.
        /// </summary>
        /// <param name="properties">
        /// The dictionary of string property names to
        /// <see cref="T:Microsoft.WindowsAzure.Storage.Table.EntityProperty"/> data values to deserialize and store in
        /// this table entity instance.
        /// </param>
        /// <param name="operationContext">
        /// An <see cref="T:Microsoft.WindowsAzure.Storage.OperationContext"/> object that represents the context for
        /// the current operation.
        /// </param>
        public void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Serializes the <see cref="T:System.Collections.Generic.IDictionary`2"/> of property names mapped to
        /// <see cref="T:Microsoft.WindowsAzure.Storage.Table.EntityProperty"/> data values from the entity instance.
        /// </summary>
        /// <param name="operationContext">
        /// An <see cref="T:Microsoft.WindowsAzure.Storage.OperationContext"/> object that represents the context for
        /// the current operation.
        /// </param>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IDictionary`2"/> object of property names to
        /// <see cref="T:Microsoft.WindowsAzure.Storage.Table.EntityProperty"/> data typed values created by
        /// serializing this table entity instance.
        /// </returns>
        public IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            throw new NotImplementedException();
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string ETag { get; set; }
    }
}
namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;

    public class BlogPostEntity : ITableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlogPostEntity"/> class.
        /// </summary>
        /// <param name="blogTitle">The title of the blog post.</param>
        /// <param name="blogContent">The content of the blog post.</param>
        /// <param name="pollOptions">The poll attached to the blog post.</param>
        public BlogPostEntity(string blogTitle, string blogContent, IEnumerable<string> pollOptions)
        {
            if (string.IsNullOrEmpty(blogTitle))
                throw new ArgumentNullException("blogTitle");
            if (string.IsNullOrEmpty(blogContent))
                throw new ArgumentNullException("blogContent");
            if (pollOptions == null || !pollOptions.Any())
                throw new ArgumentNullException("pollOptions");

            PartitionKey = blogTitle.Split(' ').First().ToLowerInvariant();
            RowKey = Guid.NewGuid().ToString();

            BlogTitle = blogTitle;
            BlogContent = blogContent;

            Poll = pollOptions.ToDictionary(key => key, value => 0);
        }

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
            BlogContent = properties["BlogContent"].StringValue;
            BlogTitle = properties["BlogTitle"].StringValue;
            Poll = properties["Poll"].PropertyAsObject as Dictionary<string, int>;

            RowKey = properties["RowKey"].StringValue;
            PartitionKey = properties["PartitionKey"].StringValue;
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

        /// <summary>
        /// Gets or sets the entity's partition key.
        /// </summary>
        /// <value>
        /// The entity's partition key.
        /// </value>
        public string PartitionKey { get; set; }

        /// <summary>
        /// Gets or sets the entity's row key.
        /// </summary>
        /// <value>
        /// The entity's row key.
        /// </value>
        public string RowKey { get; set; }

        /// <summary>
        /// Gets or sets the entity's timestamp.
        /// </summary>
        /// <value>
        /// The entity's timestamp. The property is populated by the Windows Azure Table Service.
        /// </value>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the entity's current ETag.  Set this value to '*'
        ///             in order to blindly overwrite an entity as part of an update
        ///             operation.
        /// </summary>
        /// <value>
        /// The entity's timestamp.
        /// </value>
        public string ETag { get; set; }

        /// <summary>
        /// Gets or sets the title of the blog.
        /// </summary>
        public string BlogTitle { get; set; }

        /// <summary>
        /// Gets or sets the content of the blog.
        /// </summary>
        public string BlogContent { get; set; }

        /// <summary>
        /// Gets or sets the poll of the blog.
        /// </summary>
        public Dictionary<string, int> Poll { get; set; }
    }
}
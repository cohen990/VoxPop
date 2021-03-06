namespace Site.Storage.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Security.Application;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
    using Services;
    using Site.Models;

    public class ResponseEntity : ITableEntity
    {
        public ResponseEntity()
        {
        }

        public ResponseEntity(
            string blogTitle,
            Uri imageUri,
            string blogContent,
            IEnumerable<string> pollOptions, 
            string blogImageCaption,
            string userIdentifier,
            DateTime currentTime,
            string author,
            string replyeeTitle,
            string replyee,
            string replyeeBlogIdentifier,
            string replyeeIdentifier,
            string sharedBlogIdentifier)
        {
            PartitionKey = userIdentifier;
            RowKey = sharedBlogIdentifier;

            Title = blogTitle;
            ImageUri = imageUri;
            Content = blogContent;
            ImageCaption = blogImageCaption;
            Author = author;
            TimeCreated = currentTime;
            ReplyeeTitle = replyeeTitle;
            Replyee = replyee;
            ReplyeeRowKey = replyeeBlogIdentifier;
            ReplyeePartitionKey = replyeeIdentifier;

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
            Content = properties["Content"].StringValue;
            Title = properties["Title"].StringValue;
            ImageUri = new Uri(properties["ImageUri"].StringValue);
            ImageCaption = properties["ImageCaption"].StringValue;
            Author = properties["Author"].StringValue;
            TimeCreated = properties["TimeCreated"].DateTime.Value;
            ReplyeeTitle = properties["ReplyeeTitle"].StringValue;
            Replyee = properties["Replyee"].StringValue;
            ReplyeeRowKey = properties["ReplyeeRowKey"].StringValue;
            ReplyeePartitionKey = properties["ReplyeePartitionKey"].StringValue;

            var pollString = properties["Poll"].StringValue;

            var keyValuePairs = pollString.Split(',').Where(x => x != string.Empty);
            var pollDict = keyValuePairs
                .Select(pair => pair.Split(':')).ToDictionary(split => split[0], split => int.Parse(split[1]));

            Poll = pollDict;
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
            IDictionary<string, EntityProperty> result = new Dictionary<string, EntityProperty>();
            result.Add("Content", new EntityProperty(Content));
            result.Add("Title", new EntityProperty(Title));
            result.Add("ImageUri", new EntityProperty(ImageUri.ToString()));
            result.Add("ImageCaption", new EntityProperty(ImageCaption));
            result.Add("Author", new EntityProperty(Author));
            result.Add("TimeCreated", new EntityProperty(TimeCreated));
            result.Add("ReplyeeTitle", new EntityProperty(ReplyeeTitle));
            result.Add("Replyee", new EntityProperty(Replyee));
            result.Add("ReplyeeRowKey", new EntityProperty(ReplyeeRowKey));
            result.Add("ReplyeePartitionKey", new EntityProperty(ReplyeePartitionKey));


            string pollAsString = Poll.Select(pollPair => pollPair.Key + ":" + pollPair.Value)
                .Aggregate(string.Empty, (current, joined) => current + (joined + ","));

            result.Add("Poll", new EntityProperty(pollAsString));

            return result;
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
        /// Gets or sets the entity's timestamp. This value changes every time the entity is updated.
        /// </summary>
        /// <value>
        /// The entity's timestamp. The property is populated by the Windows Azure Table Service.
        /// </value>
        /// This changes when a Story is updated --> can be used a 'Last Edited on...' in Stories
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Does not change. Is guaranteed to be the <see cref="DateTime"/> when it was created.
        /// </summary>
        public DateTime TimeCreated { get; private set; }

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
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the blog's Image.
        /// </summary>
        public Uri ImageUri { get; set; }

        /// <summary>
        /// Gets or sets the blog's Image Capation.
        /// </summary>
        public string ImageCaption { get; set; }

        ///<summary>
        /// Gets or sets the content of the blog.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the poll of the blog.
        /// </summary>
        public Dictionary<string, int> Poll { get; set; }

        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the Title of the original blog this one is in response to.
        /// </summary>
        public string ReplyeeTitle { get; set; }

        /// <summary>
        /// Gets or sets the Author of the original blog this one is in response to.
        /// </summary>
        public string Replyee { get; set; }

        /// <summary>
        /// Gets or sets the Blog Identifier GUID of the original blog this one is in response to.
        /// </summary>
        public string ReplyeeRowKey { get; set; }

        /// <summary>
        /// Gets or sets the Username of the author of the original blog this one is in response to.
        /// </summary>
        public string ReplyeePartitionKey { get; set; }



        public void UpdateContent(string content)
        {
            Content = Sanitizer.GetSafeHtmlFragment(content);
        }

        public static ResponseEntity For(ResponseModel model)
        {
            var entity = new ResponseEntity(
                model.Title,
                model.ImageUri,
                Sanitizer.GetSafeHtmlFragment(model.Content),
                model.PollOptions.EncodePollOptions(),
                model.ImageCaption,
                model.AuthorIdentifier,
                DateTime.Now,
                model.Author,
                model.ReplyeeTitle,
                model.Replyee,
                model.ReplyeeRowKey,
                model.ReplyeePartitionKey,
                model.BlogIdentifier);


            return entity;
        }

        public ResponseModel ToModel()
        {

            return new ResponseModel
            {
                ImageCaption = ImageCaption,
                Author = Author,
                Content = Content,
                ImageUri = ImageUri,
                Poll = Poll.DecodePoll(),
                BlogIdentifier = RowKey,
                Title = Title,
                AuthorIdentifier = PartitionKey,
                TimeCreated = TimeCreated,
                ReplyeeTitle = ReplyeeTitle,
                Replyee = Replyee,
                ReplyeeRowKey = ReplyeeRowKey,
                ReplyeePartitionKey = ReplyeePartitionKey

            };
        }
    }
}
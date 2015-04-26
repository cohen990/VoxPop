namespace Site.Storage.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
    using Services;
    using Site.Models;

    public class CommentEntity : ITableEntity
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
            PollOptionKey = properties["PollOptionKey"].StringValue;
            CommenterUsername = properties["CommenterUsername"].StringValue;
            Commenter = properties["Commenter"].StringValue;
            Comment = properties["Comment"].StringValue;
            CommentId = properties["CommentId"].StringValue;
            AmIAReply = properties["AmIAReply"].BooleanValue;
            WhoDidIReply = properties["WhoDidIReply"].StringValue;
            WhoDidIReplyUsername = properties["WhoDidIReplyUsername"].StringValue;
            CommenterUserPic = properties["CommenterUserPic"].StringValue;
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
            result.Add("PollOptionKey", new EntityProperty(PollOptionKey));
            result.Add("CommenterUsername", new EntityProperty(CommenterUsername));
            result.Add("Commenter", new EntityProperty(Commenter));
            result.Add("Comment", new EntityProperty(Comment));
            result.Add("CommentId", new EntityProperty(CommentId));
            result.Add("AmIAReply", new EntityProperty(AmIAReply));
            result.Add("WhoDidIReply", new EntityProperty(WhoDidIReply));
            result.Add("WhoDidIReplyUsername", new EntityProperty(WhoDidIReplyUsername));
            result.Add("CommenterUserPic", new EntityProperty(CommenterUserPic));

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
        /// The identifying key for the poll option.
        /// </summary>
        public string PollOptionKey { get; set; }

        //Unique ID of the user commenting
        public string CommenterUsername { get; set; }

        //Unique ID of the user commenting
        public string Commenter { get; set; }

        //Their comment
        public string Comment { get; set; }

        //ID unique to comment, only shared by replies to that comments
        public string CommentId { get; set; }

        //Is the curernt comment a reply or not?
        public bool? AmIAReply { get; set; }

        //Who did commenter replied to? Should provide clarity if one comment sparks a reply battle with lots of users
        public string WhoDidIReply { get; set; }

        //Their Username
        public string WhoDidIReplyUsername { get; set; }

        //For future implementation - userPic URL will go here
        public string CommenterUserPic { get; set; }

        public static CommentEntity For(CommentModel model)
        {
            return new CommentEntity
            {
                PollOptionKey = model.PollItemKey.EncodePollOption(),
                CommenterUsername = model.UserId,
                Commenter = model.CommenterName,
                Comment = model.VotersComment,
                CommentId = model.CommentIdentifier,
                AmIAReply = model.ReplyYayOrNay,
                WhoDidIReply = model.RepliedTo,
                WhoDidIReplyUsername = model.RepliedToUN,
                CommenterUserPic = model.CommentPic,
                PartitionKey = model.BlogPostRowKey,
                RowKey = model.CommentTimestamp
            };
        }
    }
}
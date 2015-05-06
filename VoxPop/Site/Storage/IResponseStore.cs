namespace Site.Storage
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IResponseStore
    {
        IEnumerable<ResponseEntity> GetAllResponses(string blogRowKey);

        Task CreateResponseAsync(ResponseEntity entity);

        void MergeResponse(ResponseEntity entity);

        void DeleteResponse(ResponseEntity entity);

        ResponseEntity GetResponse(string entityRowKey, string entityPartitionKey);

    }
}
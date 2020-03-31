using Grpc.Core;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temp;
using static Temp.TempService;

namespace thermostat_server
{
    class TempServiceImpl : TempServiceBase
    {

        private static MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
        private static IMongoDatabase mongoDatabase = mongoClient.GetDatabase("temp_db");
        private static IMongoCollection<BsonDocument> mongoCollection = mongoDatabase.GetCollection<BsonDocument>("temp");
        public override Task<SetTempResponse> SetTemp(SetTempRequest request, ServerCallContext context)
        {
            var temp = request.Temp;
            BsonDocument doc = new BsonDocument("temp_setting", temp.TempSetting);

            mongoCollection.InsertOne(doc);

            String id = doc.GetValue("_id").ToString();

            temp.Id = id;

            return Task.FromResult(new SetTempResponse()
            {
                Temp = temp
            });
        }

        public override async Task<ViewTempResponse> ViewTemp(ViewTempRequest request, ServerCallContext context)
        {
            var tempId = request.TempId;

            var filter = new FilterDefinitionBuilder<BsonDocument>().Eq("_id", new ObjectId(tempId));
            var result = mongoCollection.Find(filter).FirstOrDefault();

            if (result == null)
                throw new RpcException(new Status(StatusCode.NotFound, "The temp id " + tempId + " wasn't found"));

            Temp.Temp temp = new Temp.Temp()
            {
                TempSetting = result.GetValue("temp_setting").AsString
            };

            return new ViewTempResponse() { Temp = temp };
        }
    }
}

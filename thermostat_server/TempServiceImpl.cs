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

        public override async Task<ChangeTempResponse> ChangeTemp(ChangeTempRequest request, ServerCallContext context)
        {
            var tempId = request.Temp.Id;

            var filter = new FilterDefinitionBuilder<BsonDocument>().Eq("_id", new ObjectId(tempId));
            var result = mongoCollection.Find(filter).FirstOrDefault();

            if (result == null)
                throw new RpcException(new Status(StatusCode.NotFound, "The temp id " + tempId + " wasn't found"));

            var doc = new BsonDocument("temp_setting", request.Temp.TempSetting);

            mongoCollection.ReplaceOne(filter, doc);

            var temp = new Temp.Temp()
            {
                TempSetting = doc.GetValue("temp_setting").AsString
            };

            temp.Id = tempId;

            return new ChangeTempResponse() { Temp = temp };
        }

        public override async Task ViewAllTemp(ViewAllTempRequest request, IServerStreamWriter<ViewAllTempResponse> responseStream, ServerCallContext context)
        {
            var filter = new FilterDefinitionBuilder<BsonDocument>().Empty;

            var result = mongoCollection.Find(filter);

            foreach (var item in result.ToList())
            {
                await responseStream.WriteAsync(new ViewAllTempResponse()
                {
                    Temp = new Temp.Temp()
                    {
                        Id = item.GetValue("_id").ToString(),
                        TempSetting = item.GetValue("temp_setting").AsString
                    }
                });
            }
        }
    }
}

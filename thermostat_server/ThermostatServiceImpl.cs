using Grpc.Core;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thermostat;
using static Thermostat.ThermostatService;

namespace thermostat_server
{
    public class ThermostatServiceImpl : ThermostatServiceBase
    {
        private static MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
        private static IMongoDatabase mongoDatabase = mongoClient.GetDatabase("thermostat_db");
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
    }
}

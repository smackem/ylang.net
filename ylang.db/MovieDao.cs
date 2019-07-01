using System;
using MongoDB.Driver;

namespace ylang.db
{
    public class MovieDao
    {
        private readonly MongoClient _mongo;
        private readonly IMongoDatabase _db;

        public MovieDao()
        {
            _mongo = new MongoClient("mongodb://localhost:21017");
            _db = _mongo.GetDatabase("imdb");
        }
    }
}

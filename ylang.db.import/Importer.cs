using System;
using System.Linq;
using System.IO.Compression;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Collections.Generic;
using System.IO;

namespace ylang.db.import
{
    public class Importer
    {
        private readonly IMongoDatabase db;

        public Importer(IMongoDatabase db)
        {
            this.db = db;
        }

        public void ImportCollection<TDto>(string collName, string tsvPath, Func<string[], TDto> generator)
        {
            var coll = CreateCollection<TDto>(collName);
            var chunks = EnumerateTsv(tsvPath, generator).ChunksOfSize(10 * 1000);
            var count = 0;

            foreach (var chunk in chunks)
            {
                coll.InsertMany(chunk);
                count += chunk.Count;
                Console.WriteLine($"{count} items inserted into {collName}");
            }
        }

        private IMongoCollection<T> CreateCollection<T>(string collectionName)
        {
            try
            {
                this.db.DropCollection(collectionName);
            }
            catch
            {
                Console.WriteLine($"Collection {collectionName} does not exist");
            }

            this.db.CreateCollection(collectionName);
            return this.db.GetCollection<T>(collectionName);
        }

        private static IEnumerable<T> EnumerateTsv<T>(string path, Func<string[], T> generator)
        {
            using (var stream = File.OpenRead(path))
            using (var reader = new StreamReader(stream))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    yield return generator(line.Split('\t'));
                }
            }
        }
    }
}

using System;
using System.Linq;
using System.IO.Compression;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Collections.Generic;
using System.IO;

namespace ymov
{
    class Program
    {
        static void Main(string[] args)
        {
            var mongo = new MongoClient("mongodb://localhost:27017");
            var db = mongo.GetDatabase("ymov");

            ImportCollection(db, "titleBasics", @"/home/philip/Downloads/title.basics.tsv", CreateTitleBasic);
            ImportCollection(db, "titleAkas", @"/home/philip/Downloads/title.akas.tsv", CreateTitleAka);

            //var title = titleBasics.Find(x => x.Name.StartsWith("Rio")).FirstOrDefault();

            Console.WriteLine("Done!");
        }

        static void ImportCollection<TDto>(IMongoDatabase db, string collName, string tsvPath, Func<string[], TDto> generator)
        {
            var coll = CreateCollection<TDto>(db, collName);
            var chunks = ChunksOfSize(EnumerateTsv(tsvPath, generator), 10*1000);
            var count = 0;

            foreach (var chunk in chunks)
            {
                coll.InsertMany(chunk);
                count += chunk.Count;
                Console.WriteLine($"{count} items inserted into {collName}");
            }
        }

        static IMongoCollection<T> CreateCollection<T>(IMongoDatabase db, string collectionName)
        {
            try
            {
                db.DropCollection(collectionName);
            }
            catch
            {
                Console.WriteLine($"Collection {collectionName} does not exist");
            }

            db.CreateCollection(collectionName);
            return db.GetCollection<T>(collectionName);
        }

        static int? TryParseInt(string str)
        {
            return int.TryParse(str, out var n) ? n : default(int?);
        }

        static TitleBasicDto CreateTitleBasic(string[] tokens)
        {
            var dto = new TitleBasicDto
            {
                Id = tokens[0],
                TitleType = tokens[1],
                PrimaryTitle = tokens[2],
                OriginalTitle = tokens[3],
                IsAdult = TryParseInt(tokens[4]) != 0,
                StartYear = TryParseInt(tokens[5]),
                EndYear = TryParseInt(tokens[6]),
                Genres = tokens[8].Split(','),
            };
            if (int.TryParse(tokens[7], out var minutes))
            {
                dto.Runtime = TimeSpan.FromMinutes(minutes);
            }
            return dto;
        }

        static TitleAkaDto CreateTitleAka(string[] tokens)
        {
            return new TitleAkaDto
            {
            };
        }

        static IEnumerable<T> EnumerateTsv<T>(string path, Func<string[], T> generator)
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

        static IEnumerable<IReadOnlyCollection<T>> ChunksOfSize<T>(IEnumerable<T> collection, int size)
        {
            List<T> chunk = null;

            foreach (var item in collection)
            {
                if (chunk == null)
                {
                    chunk = new List<T>(size);
                }

                chunk.Add(item);

                if (chunk.Count == size)
                {
                    yield return chunk;
                    chunk = null;
                }
            }

            if (chunk != null)
            {
                yield return chunk;
            }
        }
    }
}

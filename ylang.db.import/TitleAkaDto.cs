using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ylang.db.import
{
    internal class TitleAkaDto
    {
#pragma warning disable 0169
        [BsonId]
        private ObjectId _id;
#pragma warning restore 0169

        public string Id { get; set; }

        public int? Ordering { get; set; }

        public string Title { get; set; }

        public string Region { get; set; }

        public string Language { get; set; }

        public string[] Types { get; set; }

        public string[] Attributes { get; set; }

        public bool IsOriginalTitle { get; set; }
    }
}
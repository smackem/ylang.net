using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ylang.db.import
{
    public class NameBasicDto
    {
#pragma warning disable 0169
        [BsonId]
        private ObjectId _id;
#pragma warning restore 0169

        public string Id { get; set; }

        public string Name { get; set; }

        public int BirthYear { get; set; }

        public int? DeathYear { get; set; }

        public string[] Professions { get; set; }

        public string[] KnownForTitleIds { get; set; }
    }
}
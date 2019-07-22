using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ylang.db.import
{
    public class TitlePrincipalDto
    {
#pragma warning disable 0169
        [BsonId]
        private ObjectId _id;
#pragma warning restore 0169

        public string Id { get; set; }

        public int? Ordering { get; set; }

        public string NameId { get; set; }

        public string Category { get; set; }

        public string Job { get; set; }

        public string[] Characters { get; set; }
    }
}

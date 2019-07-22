using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ylang.db.import
{
    public class TitleCrewDto
    {
#pragma warning disable 0169
        [BsonId]
        private ObjectId _id;
#pragma warning restore 0169
        
        public string Id { get; set; }

        public string[] DirectorNameIds { get; set; }

        public string[] WriterNameIds { get; set; }
    }
}
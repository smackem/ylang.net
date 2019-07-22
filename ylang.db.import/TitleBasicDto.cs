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
    internal class TitleBasicDto
    {
#pragma warning disable 0169
        [BsonId]
        private ObjectId _id;
#pragma warning restore 0169

        public string Id { get; set; }

        public string TitleType { get; set; }

        public string PrimaryTitle { get; set; }

        public string OriginalTitle { get; set; }

        public bool IsAdult { get; set; }

        public int? StartYear { get; set; }

        public int? EndYear { get; set; }

        public TimeSpan? Runtime { get; set; }

        public string[] Genres { get; set; }
    }
}

using System;
using System.Linq;
using System.IO.Compression;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Collections.Generic;
using System.IO;

namespace ylang.net.db.import
{
    static class Mapping
    {
        public static TitleBasicDto CreateTitleBasic(string[] tokens)
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

        public static TitleAkaDto CreateTitleAka(string[] tokens)
        {
            return new TitleAkaDto
            {
                Id = tokens[0],
                Ordering = TryParseInt(tokens[1]),
                Title = tokens[2],
                Region = tokens[3],
                Language = tokens[4],
                Types = tokens[5].Split(','),
                Attributes = tokens[6].Split(','),
                IsOriginalTitle = TryParseInt(tokens[7]) != 0,
            };
        }

        private static int? TryParseInt(string str)
        {
            return int.TryParse(str, out var n) ? n : default(int?);
        }
    }
}

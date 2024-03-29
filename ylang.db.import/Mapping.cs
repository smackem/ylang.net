using System;
using System.Linq;
using System.IO.Compression;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Collections.Generic;
using System.IO;
using ylang.db.import;

namespace ylang.db.import
{
    static class Mapping
    {
        public static TitleBasicDto CreateTitleBasic(string[] tokens)
        {
            return new TitleBasicDto
            {
                Id = tokens[0],
                TitleType = tokens[1],
                PrimaryTitle = tokens[2],
                OriginalTitle = tokens[3],
                IsAdult = TryParseInt(tokens[4]) != 0,
                StartYear = TryParseInt(tokens[5]),
                EndYear = TryParseInt(tokens[6]),
                Runtime = TryParseMinutes(tokens[7]),
                Genres = tokens[8].Split(','),
            };
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

        public static TitleCrewDto CreateTitleCrew(string[] tokens)
        {
            return new TitleCrewDto
            {
                Id = tokens[0],
                DirectorNameIds = tokens[1].Split(','),
                WriterNameIds = tokens[2].Split(','),
            };
        }

        public static TitlePrincipalDto CreateTitlePrincipal(string[] tokens)
        {
            return new TitlePrincipalDto
            {
                Id = tokens[0],
                Ordering = TryParseInt(tokens[1]),
                NameId = tokens[2],
                Category = tokens[3],
                Job = tokens[4],
                Characters = tokens[5].Split(','),
            };
        }

        public static NameBasicDto CreateNameBasic(string[] tokens)
        {
            return new NameBasicDto
            {
                Id = tokens[0],
                Name = tokens[1],
                BirthYear = TryParseInt(tokens[2]) ?? -1,
                DeathYear = TryParseInt(tokens[3]),
                Professions = tokens[4].Split(','),
                KnownForTitleIds = tokens[5].Split(','),
            };
        }

        private static int? TryParseInt(string str)
        {
            return int.TryParse(str, out var n)
                ? n
                : default(int?);
        }

        private static TimeSpan? TryParseMinutes(string str)
        {
            return int.TryParse(str, out var minutes)
                ? TimeSpan.FromMinutes(minutes)
                : default(TimeSpan?);
        }
    }
}

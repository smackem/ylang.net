﻿using System;
using System.Linq;
using MongoDB.Driver;
using System.Collections.Generic;
using System.IO;

namespace ylang.db.import
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var mongo = new MongoClient("mongodb://localhost:27017");
            var db = mongo.GetDatabase("ymov");
            var importer = new Importer(db);

            importer.ImportCollection("titleBasics", @"/home/philip/Downloads/title.basics.tsv", Mapping.CreateTitleBasic);
            importer.ImportCollection("titleAkas", @"/home/philip/Downloads/title.akas.tsv", Mapping.CreateTitleAka);
            importer.ImportCollection("titleCrew", @"/home/philip/Downloads/title.crew.tsv", Mapping.CreateTitleCrew);
            importer.ImportCollection("titlePrincipals", @"/home/philip/Downloads/title.principals.tsv", Mapping.CreateTitlePrincipal);
            importer.ImportCollection("nameBasics", @"/home/philip/Downloads/name.basics.tsv", Mapping.CreateNameBasic);

            //var title = titleBasics.Find(x => x.Name.StartsWith("Rio")).FirstOrDefault();

            Console.WriteLine("Done!");
        }
    }
}

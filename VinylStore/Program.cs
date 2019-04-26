using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VinylStore.Entities;

namespace VinylStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            //Seed();
        }

        //private static void Seed()
        //{
        //    List<Vinyl> vinyls = new List<Vinyl>()
        //    {
        //        new Vinyl
        //        {
        //            AlbumName = "Television City Dream",
        //            BandName = "Screeching Weasel",
        //            Genre = "Punk Rock",
        //            ReleaseYear = "1998",
        //            Price = 20
        //        }
        //    };
        //}

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

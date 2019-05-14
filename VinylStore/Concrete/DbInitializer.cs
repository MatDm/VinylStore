using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStore.Models;

namespace VinylStore.Abstract
{
    public static class DbInitializer
    {
        public static void Seed(VinylStoreDbContext db)
        {
            if (!db.Vinyls.Any())
            {
                db.AddRange
                (
                    //new Vinyl { AlbumName = "Punk In Drublic", ArtistName = "NOFX", ReleaseYear = "1994", Genres = new string[] {"Punk Rock" }, ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ0cDl1LkktzsFepNDEAZcRUgGpblzC1GemvkYSIoIG4zYbJhXRJw", Price = 15, TrackList = new string[] { "Linoleum", "The Brews", "Truc" } },
                    //new Vinyl { AlbumName = "Nude With Boots", ArtistName = "Melvins", ReleaseYear = "2008", Genres = ["Stoner Rock/Grunge"], ImageUrl = "", Price = 26 },
                    //new Vinyl { AlbumName = "Television City Dream", ArtistName = "Screeching Weasel", ReleaseYear = "1998", Genres = ["Punk Rock"], ImageUrl = "", Price = 18 },
                    //new Vinyl { AlbumName = "Fuzz", ArtistName = "Fuzz", ReleaseYear = "2013", Genres = ["Garage Rock"], ImageUrl = "", Price = 25 },
                    //new Vinyl { AlbumName = "Too Sorry For Any Sorrow", ArtistName = "Mountain Bike", ReleaseYear = "2017", Genres = ["Garage Rock"], ImageUrl = "", Price = 15 },
                    //new Vinyl { AlbumName = "No Surrender", ArtistName = "Kickback", ReleaseYear = "2009", Genres = ["Hardcore"], ImageUrl = "", Price = 17 },
                    //new Vinyl { AlbumName = "United States Of Horror", ArtistName = "Ho99o9", ReleaseYear = "2017", Genres = ["Hiphop/Punk"], ImageUrl = "", Price = 22 },
                    //new Vinyl { AlbumName = "Cartouche", ArtistName = "Cartouche", ReleaseYear = "2019", Genres = ["Electro punk/Mutant Hiphop"], ImageUrl = "", Price = 15 },
                    //new Vinyl { AlbumName = "Vaffan", ArtistName = "Immigrants", ReleaseYear = "2014", Genres = ["Hardcore/Punk"], ImageUrl = "", Price = 10 },
                    //new Vinyl { AlbumName = "Al Dente", ArtistName = "Slovenians", ReleaseYear = "2017", Genres = ["Punk Rock"], ImageUrl = "", Price = 15 }
                );
                db.SaveChanges();
            }

        }
    }
}

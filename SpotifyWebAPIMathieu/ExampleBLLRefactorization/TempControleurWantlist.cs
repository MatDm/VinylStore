using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyWebAPIMathieu.ExampleBLLRefactorization
{
    class TempControleurWantlist
    {
        public async Task<List<..>> AddToUserWantlist(string spotifyAlbumId)
        {
            //requete par id de l'album pour avoir les données complètes à sauver dans la table
            string queryString = "https://api.spotify.com/v1/albums/" + spotifyAlbumId;

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _userService.RefreshToken());
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(queryString);
                var output = await client.SendAsync(request);

                //on récupère le json de spotify
                var result = await output.Content.ReadAsAsync<AlbumIdSearchResultJsonModel>();

                //on vérifie si c'est pas vide
                if (result != null)
                {
                    VinylMTO vinyl = new Vinyl()
                    {
                        AlbumName = result.name,
                        ReleaseYear = result.release_date,
                        ArtistName = result.artists[0].name,
                        ImageUrl = result.images[0].url,
                        Label = result.label,
                        SpotifyAlbumId = spotifyAlbumId,
                        //todo : recupérer les tracks via methode
                        TrackList = _spotifyService.GetTracks(result),
                        Genres = await _spotifyService.GetGenres(result)
                    };

                    //on insère le VinylMTO dans la db
                    _vinylRepo.Insert(vinyl);

                    //on met à jour la wantlist du user
                    var currentUser = await _userManager.GetUserAsync(User);
                    var wantlist = new Wantlist()
                    {
                        UserId = currentUser.Id,
                        VinylId = vinyl.Id
                    };

                    //on insère dans la db
                    _listRepositoryAccessor("Wantlist").Insert(wantlist);

                    //succès et redirection vers la collection mise à jour
                    TempData["SuccessMessage"] = "Vinyl added successfully";
                    return RedirectToAction("DisplayMyWantlist");
                }

                //échec et redirection vers la collection non mise à jour
                TempData["ErrorMessage"] = "Vinyl not added, something went wrong";
                return RedirectToAction("DisplayMyWantlist");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStore.DAL.ExternalServices.JsonModels
{

    public class AlbumIdSearchResultJsonModel
    {
        public string album_type { get; set; }
        public Artist[] artists { get; set; }
        public string[] available_markets { get; set; }
        public Copyright[] copyrights { get; set; }
        public External_Ids external_ids { get; set; }
        public External_Urls external_urls { get; set; }
        public object[] genres { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public Image[] images { get; set; }
        public string label { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string release_date { get; set; }
        public string release_date_precision { get; set; }
        public int total_tracks { get; set; }
        public Tracks tracks { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

}

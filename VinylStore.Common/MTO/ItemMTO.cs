namespace VinylStore.Common.MTO
{
    public class ItemMTO
    {
        public string album_type { get; set; }
        public ArtistMTO[] artists { get; set; }
        public string[] available_markets { get; set; }
        public External_UrlsMTO external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public ImageMTO[] images { get; set; }
        public string name { get; set; }
        public string release_date { get; set; }
        public string release_date_precision { get; set; }
        public int total_tracks { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

}


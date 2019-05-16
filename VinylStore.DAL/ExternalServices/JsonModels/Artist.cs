using VinylStore.DAL.ExternalServices.JsonModels;

namespace VinylStore.DAL.ExternalServices.JsonModels
{
    

    public class Artist
    {
        public External_Urls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

}


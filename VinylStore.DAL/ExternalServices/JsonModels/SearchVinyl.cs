using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStore.DAL.ExternalServices.JsonModels
{

    public class SearchVinyl
    {
        public Pagination pagination { get; set; }
        public Result[] results { get; set; }
    }

    public class Pagination
    {
        public int per_page { get; set; }
        public int pages { get; set; }
        public int page { get; set; }
        public Urls urls { get; set; }
        public int items { get; set; }
    }

    public class Urls
    {
        public string last { get; set; }
        public string next { get; set; }
    }

    public class Result
    {
        //public string thumb { get; set; }
        public string title { get; set; }
        //public string uri { get; set; }
        //    public string master_url { get; set; }
            public string cover_image { get; set; }
        //    public string resource_url { get; set; }
        //    public int? master_id { get; set; }
        //    public string type { get; set; }
            public int id { get; set; }
        //    public string[] style { get; set; }
            public string[] format { get; set; }
        //    public string country { get; set; }
            public string[] barcode { get; set; }
        //    public string[] label { get; set; }
            public string[] genre { get; set; }
        //    public string catno { get; set; }
        //    public Community community { get; set; }
            public string year { get; set; }
        //    public int format_quantity { get; set; }
        public int release_id { get; set; }
    }

    public class Community
    {
        public int want { get; set; }
        public int have { get; set; }
    }

}

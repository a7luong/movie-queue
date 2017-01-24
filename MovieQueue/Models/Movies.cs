using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieQueue.Models
{
    public class Movies
    {
        public int id { get; set; }
        public int movieDB_id { get; set; }
        public int imdb_id { get; set; }
        public int runtime { get; set; }

        public string title { get; set; }
        public string description { get; set; }
        public string poster_path { get; set; }
        public string backdrop_path { get; set; }

        public float vote_average { get; set; }

        public DateTime release_date { get; set; }
        public string[] genres { get; set; }
    }
}
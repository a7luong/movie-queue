using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MovieQueue.Helpers;

namespace MovieQueue.Models
{
    public class LookupResults
    {
        public Result[] results { get; set; }
        public string queryString { get; set; }
        public int currentPage { get; set; }
        public int totalPages { get; set; }
    }
}
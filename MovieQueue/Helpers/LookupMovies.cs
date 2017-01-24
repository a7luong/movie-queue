using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Http;
using System.Net.Http.Headers;

using MovieQueue.Models;

namespace MovieQueue.Helpers
{


    public class MovieObject
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public object belongs_to_collection { get; set; }
        public int budget { get; set; }
        public Genre[] genres { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
        public Production_Companies[] production_companies { get; set; }
        public Production_Countries[] production_countries { get; set; }
        public string release_date { get; set; }
        public int revenue { get; set; }
        public int runtime { get; set; }
        public Spoken_Languages[] spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
    }

    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Production_Companies
    {
        public string name { get; set; }
        public int id { get; set; }
    }

    public class Production_Countries
    {
        public string iso_3166_1 { get; set; }
        public string name { get; set; }
    }

    public class Spoken_Languages
    {
        public string iso_639_1 { get; set; }
        public string name { get; set; }
    }



    public class DataObject
    {
        public int page { get; set; }
        public Result[] results { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
    }

    public class LookupMovies
    {
        private const string URL = "https://api.themoviedb.org";
        private string urlParameters = "/3/search/movie?api_key=54bbd953ac47c9ba8bb9351af3fce31a&query=";

        public Movies getMovieDetails(int lookupID) {
            string urlParameters = URL + "/3/movie/"+lookupID+"?api_key=54bbd953ac47c9ba8bb9351af3fce31a";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var parse_movie = response.Content.ReadAsAsync<MovieObject>().Result;
                Movies movie = new Movies {
                    movieDB_id = parse_movie.id,
                    runtime = parse_movie.runtime,

                    title = parse_movie.title,
                    description = parse_movie.overview,
                    poster_path = parse_movie.poster_path,
                    backdrop_path = parse_movie.backdrop_path,

                    vote_average = parse_movie.vote_average,

                    release_date = DateTime.Parse(parse_movie.release_date),
                };
                return movie;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }

        }

        public Result[] fetchFromAPI(string lookupName)
        {
            string urlParameters = "/3/search/movie?api_key=54bbd953ac47c9ba8bb9351af3fce31a&query=" + lookupName;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var dataObjects = response.Content.ReadAsAsync<DataObject>().Result;
                var results_pre_trim = new List<Result>(dataObjects.results);
                var results_post_trim = new List<Result>();

                for (int i = 0; i < results_pre_trim.Count; i++)
                {
                    if (results_pre_trim[i].poster_path != null && results_pre_trim[i].overview != null && results_pre_trim[i].adult == false && results_pre_trim[i].popularity > 1.3)
                    {
                        results_post_trim.Add(results_pre_trim[i]);
                    }
                }

                return results_post_trim.ToArray();
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }
        }


    }
}
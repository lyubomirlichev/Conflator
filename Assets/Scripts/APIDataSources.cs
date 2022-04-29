using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanyeResponse
{
    public string quote;
}

public class CatResponse
{
    public string url;
}

public class OwenWilsonResponse
{
    public string movie;
    public int year;
    public string release_date;
    public string director;
    public string character;
    public string movie_duration;
    public string timestamp;
    public string full_line;
    public int current_wow_in_movie;
    public int total_wows_in_movie;
    public string poster;
}

public static class APIDataSources
{
    public const string GET_KANYE_QUOTE = "https://api.kanye.rest/";
    public const string GET_RANDOM_CAT = "https://api.thecatapi.com/v1/images/search";
    public const string GET_RANDOM_OWEN_QUOTE = "https://owen-wilson-wow-api.herokuapp.com/wows/random";

}

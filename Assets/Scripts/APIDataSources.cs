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
public static class APIDataSources
{
    public const string GET_KANYE_QUOTE = "https://api.kanye.rest/";
    public const string GET_RANDOM_CAT = "https://api.thecatapi.com/v1/images/search";

}

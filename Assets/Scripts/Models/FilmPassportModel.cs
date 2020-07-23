using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class FilmPassportModel
{
    [JsonProperty("title")]
    public string Title { get; set; }
    
    [JsonProperty("release_date")]
    public string ReleaseDate { get; set; }
    
    [JsonProperty("vote_average")]
    public float VoteAverage  { get; set; }
    
    [JsonProperty("overview")]
    public string Overview  { get; set; }
    
    [JsonProperty("poster_path")]
    public string PosterPath { get; set; }
    
}

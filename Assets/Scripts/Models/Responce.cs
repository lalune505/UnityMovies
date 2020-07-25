using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Responce
{
    [JsonProperty("total_results")] 
    public int TotalResults;
    
    [JsonProperty("total_pages")] 
    public int TotalPages;
    
    [JsonProperty("results")]
    public List<FilmPassportModel> Films;
    
    
}

using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class FilmsResponce
{
    [JsonProperty("results")]
    public List<FilmPassportModel> Films;
}

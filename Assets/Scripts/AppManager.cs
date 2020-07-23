using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public ProjectSettings settings;
    async void Init()
    {
        NetworkManager networkManager = new NetworkManager(settings);
        var result = await networkManager.RequestPopularFilmsPassports(1,2019);
        
        foreach (var film in result.Films)
        {
            Debug.Log(film.Title);
        }

    }
    private void Start()
    {
        Init();
    }
}

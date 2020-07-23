using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public ProjectSettings settings;
    public FilmPassportView testView;
    async void Init()
    {
        NetworkManager networkManager = new NetworkManager(settings);
        var result = await networkManager.RequestPopularFilmsPassports(1,2019);
        
        testView.Init(result.Films.First());

    }
    private void Start()
    {
        Init();
    }
}

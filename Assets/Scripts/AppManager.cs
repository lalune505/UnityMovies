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
        NetworkManager.NetworkSettings = settings.networkSettings;
        var result = await NetworkManager.RequestPopularFilmsPassports(1,2019);
        
        testView.Init(result.Films.First());

    }
    private void Start()
    {
        Init();
    }
}

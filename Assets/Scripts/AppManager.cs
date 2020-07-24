using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public ProjectSettings settings;
    public List<InitializableMonoBehaviour> initializableMonoBehaviours;
    private void Start()
    {
        NetworkManager.NetworkSettings = settings.networkSettings;
        
        foreach (var item in initializableMonoBehaviours)
        {
            item.Init();
        }
    }
}

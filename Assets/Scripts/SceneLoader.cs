using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    private const string MainScenePath = "Assets/Scenes/MainScene.unity";
    private const string ArScenePath = "Assets/Scenes/ARScene.unity";
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
      
        DontDestroyOnLoad(gameObject);
    }

    public void LoadArScene()
    {
        SceneManager.LoadSceneAsync(ArScenePath);
    }
    public void LoadMainScene()
    {
        SceneManager.LoadSceneAsync(MainScenePath);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager
{
    private NetworkSettings _networkSettings;
    
    public NetworkManager(ProjectSettings projectSettings)
    {
        _networkSettings = projectSettings.networkSettings;
    }
    
    public async UniTask<FilmsResponce> RequestPopularFilmsPassports(int pageNumber, int year)
    {
        var url = $"{_networkSettings.tmdbApiUrl}?api_key={_networkSettings.apiKey}&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page={pageNumber}&year={year}";
        return await Request<FilmsResponce>(url);
    }
    
    private async UniTask<T> Request<T>(string urlMethod)
    {
        Debug.LogFormat("Request '{0}'", urlMethod);
        using (var webRequest = UnityWebRequest.Get(urlMethod)) 
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");

            await webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError($"Request {webRequest.url} error: {webRequest.error}");
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(webRequest.downloadHandler.text);
        }
    }
}

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
            await webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError($"Request {webRequest.url} error: {webRequest.error}");
                return default(T);
            }
            
            return JsonConvert.DeserializeObject<T>(webRequest.downloadHandler.text);
        }
    }
    
    private async UniTask<Texture2D> GetTexture(string urlMethod)
    {
        using (var webRequest = UnityWebRequestTexture.GetTexture(urlMethod))
        {
            webRequest.downloadHandler = new DownloadHandlerTexture();
            await webRequest.SendWebRequest();
            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                Debug.LogError($"Request {webRequest.url} error: {webRequest.error}");
                return null;
            }

            return DownloadHandlerTexture.GetContent(webRequest);
        }
    }
}

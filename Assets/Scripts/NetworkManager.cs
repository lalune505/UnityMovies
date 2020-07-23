using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Networking;

public static class NetworkManager
{
    public static NetworkSettings NetworkSettings;

    public static async UniTask<FilmsResponce> RequestPopularFilmsPassports(int pageNumber, int year)
    {
        var url = $"{NetworkSettings.tmdbApiUrl}?api_key={NetworkSettings.apiKey}&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page={pageNumber}&year={year}";
        return await Request<FilmsResponce>(url);
    }

    public static async UniTask<Texture> RequestFilmPoster(string urlMethod)
    {
        var url = $"{NetworkSettings.filmsPostersUrl}{urlMethod}";
        return await GetTexture(url);
    }
    
    private static async UniTask<T> Request<T>(string urlMethod)
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
    
    private static async UniTask<Texture> GetTexture(string urlMethod)
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

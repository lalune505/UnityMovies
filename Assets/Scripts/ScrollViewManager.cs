using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewManager : InitializableMonoBehaviour
{
    public GameObject filmViewPrefab;
    public RectTransform scrollContent;
    public GameObject errorPanel;
    public GameObject loadingPanel;
    public float thrScrollRectY = 0.1f;
    public ScrollRect scrollRect;
    
    private bool isUpdating = false;
    private List<FilmPassportModel> results = new List<FilmPassportModel>();
    private int _pageNumber = 0;
    private const int Year = 2019;
    private async void UpdateItems()
    {
        isUpdating = true;
        _pageNumber++;
        var newResults = await NetworkManager.RequestPopularFilmsPassports(_pageNumber, Year);
        loadingPanel.SetActive(false);

        if (newResults != null)
        {
            CreateNewFilmsPrefabs(newResults.Films);
        }
        else
        {
            Debug.Log("Oops... Please Try later!");
            errorPanel.SetActive(true);
        }
        isUpdating = false;
    }
    
    public void OnScrollRectMove()
    {
        if (scrollRect != null && !isUpdating)
        {
            if (scrollRect.normalizedPosition.y < thrScrollRectY)
            {
                UpdateItems();
                
            }
        }
    }

    public override void Init()
    {
         UpdateItems();
    }

    private void CreateNewFilmsPrefabs(List<FilmPassportModel> models)
    {
        foreach (var filmModel in models)
        {
            var scrollViewElement = Instantiate(filmViewPrefab, scrollContent, false);
            scrollViewElement.GetComponent<FilmPassportView>().Init(filmModel);
        }
    }
}

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
    public float thrScrollRectY = 0.1f;
    public ScrollRect scrollRect;
    
    private bool _isRequesting = false;
    private readonly List<FilmPassportModel> _results = new List<FilmPassportModel>();
    private int _pageNumber = 1;
    private const int Year = 2019;
    
    private Responce _currentResponce;
    private async void RequestItems()
    {
        _currentResponce = await NetworkManager.RequestPopularFilmsPassports(_pageNumber, Year);

        if (_currentResponce != null && _currentResponce.Films.Count > 0)
        {
            if (_results.Count < _currentResponce.TotalPages)
            {
                _results.AddRange(_currentResponce.Films);
                CreateNewFilmsPrefabs(_currentResponce.Films);
            }
        }
        else
        {
            Debug.Log("Oops... Please Try later!");
            errorPanel.SetActive(true);
        }
    }
    
    public void OnScrollRectMove()
    {
        if (scrollRect != null && !_isRequesting)
        {
            if (scrollRect.normalizedPosition.y < thrScrollRectY)
            {
                StartCoroutine(RequestNewResults());
            }
        }
    }

    public override void Init()
    {
        StartCoroutine(RequestNewResults());
    }

    private void CreateNewFilmsPrefabs(List<FilmPassportModel> models)
    {
        foreach (var filmModel in models)
        {
            var scrollViewElement = Instantiate(filmViewPrefab, scrollContent, false);
            scrollViewElement.GetComponent<FilmPassportView>().Init(filmModel);
        }
    }

    IEnumerator RequestNewResults()
    {
        _isRequesting = true;
        if (_currentResponce != null && _results.Count >= _currentResponce.TotalPages)
        {
            yield return new WaitForSeconds(1f);
            _isRequesting = false;
            yield break;
        }
        
        if (_currentResponce != null && _pageNumber < _currentResponce.TotalPages)
        {
            _pageNumber++;
            Debug.Log("Loaded new page");
        }else
        {
            Debug.Log("There's no pages left or no request in progress");
        }
        
        RequestItems();

        yield return new WaitForSeconds(1f);
        _isRequesting = false;
    }
}

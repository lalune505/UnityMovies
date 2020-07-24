using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Async;
using UnityEngine;

public class ScrollViewManager : InitializableMonoBehaviour
{
    public GameObject filmViewPrefab;
    public RectTransform scrollContent;
    public GameObject errorPanel;
    public GameObject loadingPanel;

    private async void GetItems()
    {
        var result = await NetworkManager.RequestPopularFilmsPassports(1,2019);
        loadingPanel.SetActive(false);

        if (result != null)
        {
            foreach (var filmModel in result.Films)
            {
                var scrollViewElement = Instantiate(filmViewPrefab, scrollContent, false);
                scrollViewElement.GetComponent<FilmPassportView>().Init(filmModel);
            }
        }
        else
        {
            Debug.Log("Oops... PleaseTry later!");
            errorPanel.SetActive(true);
        }
    }

    public override void Init()
    {
         GetItems();
    }
}

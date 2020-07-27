using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class FilmPassportView : MonoBehaviour
{
    public RawImage image;
    public Text title;
    public Text overView;
    public Text year;
    public Text rating;
    public Button filmButton;

    public async void Init(FilmPassportModel filmPassportModel)
    {
        title.text = filmPassportModel.Title;
        overView.text = filmPassportModel.Overview;
        year.text = DateTime.ParseExact(filmPassportModel.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MMMM dd, yyyy");
        rating.text = filmPassportModel.VoteAverage.ToString();

        var posterTexture = await NetworkManager.RequestFilmPoster(filmPassportModel.PosterPath);
        if (image != null)
        {
            image.texture = posterTexture;
        }

        if (filmPassportModel.Title.Equals("Ad Astra"))
        {
            filmButton.onClick.AddListener(SceneLoader.instance.LoadArScene);
        }
    }
    
}

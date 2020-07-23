using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilmPassportView : MonoBehaviour
{
    public RawImage image;
    public Text title;
    public Text overView;
    public Text year;

    public async void Init(FilmPassportModel filmPassportModel)
    {
        title.text = filmPassportModel.Title;
        overView.text = filmPassportModel.Overview;
        year.text = filmPassportModel.ReleaseDate;
        image.texture = await NetworkManager.RequestFilmPoster(filmPassportModel.PosterPath);
    }
    
}

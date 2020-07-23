using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilmPassportView : MonoBehaviour
{
    public Image image;
    public Text title;
    public Text overView;
    public Text year;

    public void Init(FilmPassportModel filmPassportModel)
    {
        title.text = filmPassportModel.Title;
        overView.text = filmPassportModel.Overview;
        year.text = filmPassportModel.ReleaseDate;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovieUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] Image image;

    public void Show(Movie movie)
    {
        title.text = movie.title;
        GameManager.Instance.LoadImage(movie.posterUrl, (Sprite sprite) =>
        {
            image.sprite = sprite;
        });
    }
}

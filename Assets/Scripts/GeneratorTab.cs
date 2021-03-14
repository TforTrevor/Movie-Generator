using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GeneratorTab : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] Image image;

    public void Pick()
    {
        Movie picked = GameManager.Instance.queuedMovies[Random.Range(0, GameManager.Instance.queuedMovies.Count)];
        title.text = picked.title;
        GameManager.Instance.LoadImage(picked.posterUrl, (Sprite sprite) =>
        {
            image.sprite = sprite;
        });
    }
}

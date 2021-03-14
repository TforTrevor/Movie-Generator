using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteTab : MonoBehaviour
{
    [SerializeField] CompleteElement completeElement;
    [SerializeField] Transform parent;

    List<CompleteElement> elements = new List<CompleteElement>();
    bool firstRefresh;

    void Start()
    {
        Refresh();
        firstRefresh = true;
    }

    void OnEnable()
    {
        if (firstRefresh)
            Refresh();
    }

    public void Refresh()
    {
        foreach (CompleteElement element in elements)
        {
            Destroy(element.gameObject);
        }
        elements.Clear();

        foreach (Movie movie in GameManager.Instance.completedMovies)
        {
            CompleteElement element = Instantiate(completeElement.gameObject).GetComponent<CompleteElement>();
            element.completeTab = this;
            element.movie = movie;
            element.title.text = movie.title;
            element.transform.parent = parent;
            elements.Add(element);

            GameManager.Instance.LoadImage(movie.posterUrl, (Sprite sprite) =>
            {
                element.image.sprite = sprite;
            });
        }
    }

    public void Remove(CompleteElement element)
    {
        elements.Remove(element);
        GameManager.Instance.completedMovies.Remove(element.movie);
        GameManager.Instance.queuedMovies.Add(element.movie);
        //Destroy(element.gameObject);

        GameManager.Instance.SaveGame();
    }
}

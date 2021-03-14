using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueTab : MonoBehaviour
{
    [SerializeField] QueueElement queueElement;
    [SerializeField] Transform parent;

    List<QueueElement> elements = new List<QueueElement>();
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
        foreach (QueueElement element in elements)
        {
            Destroy(element.gameObject);
        }
        elements.Clear();

        foreach (Movie movie in GameManager.Instance.queuedMovies)
        {
            QueueElement element = Instantiate(queueElement.gameObject).GetComponent<QueueElement>();
            element.queueTab = this;
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

    public void AddToQueue(Movie movie)
    {
        GameManager.Instance.queuedMovies.Add(movie);
        GameManager.Instance.SaveGame();
        Refresh();
    }

    public void Remove(QueueElement element)
    {
        elements.Remove(element);
        GameManager.Instance.queuedMovies.Remove(element.movie);
        Destroy(element.gameObject);

        GameManager.Instance.SaveGame();
    }

    public void Complete(QueueElement element)
    {
        elements.Remove(element);
        GameManager.Instance.queuedMovies.Remove(element.movie);
        GameManager.Instance.completedMovies.Add(element.movie);
        Destroy(element.gameObject);

        GameManager.Instance.SaveGame();
    }
}

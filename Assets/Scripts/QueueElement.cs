using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class QueueElement : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Image image;
    public Movie movie;
    public QueueTab queueTab;

    public void Remove()
    {
        queueTab.Remove(this);
        transform.DOScaleY(0, 0.25f).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    public void Complete()
    {
        queueTab.Complete(this);
        transform.DOScaleY(0, 0.25f).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}

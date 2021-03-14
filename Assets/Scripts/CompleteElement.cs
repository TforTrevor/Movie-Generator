using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CompleteElement : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Image image;
    public Movie movie;
    public CompleteTab completeTab;

    public void Remove()
    {
        completeTab.Remove(this);
        transform.DOScaleY(0, 0.25f).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}

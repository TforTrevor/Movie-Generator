using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QueueElement : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Image image;
    public Movie movie;
    public QueueTab queueTab;

    public void Remove()
    {
        queueTab.Remove(this);
    }

    public void Complete()
    {
        queueTab.Complete(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CompleteElement : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Image image;
    public Movie movie;
    public CompleteTab completeTab;

    public void Remove()
    {
        completeTab.Remove(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTab : MonoBehaviour
{
    [SerializeField] MovieUI movieUI;

    public void Pick()
    {
        Movie picked = GameManager.Instance.queuedMovies[Random.Range(0, GameManager.Instance.queuedMovies.Count)];
        movieUI.Show(picked);
    }
}

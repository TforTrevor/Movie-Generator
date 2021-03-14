using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Objects/Movie")]
public class Movie : ScriptableObject
{
    public string title;
    public string posterUrl;

    public MovieSerializable ToSerializable()
    {
        MovieSerializable movie = new MovieSerializable();
        movie.title = title;
        movie.posterUrl = posterUrl;
        //if (sprite != null)
        //{
        //    movie.posterUrl = sprite.name;
        //}        

        return movie;
    }
}

[System.Serializable]
public class MovieSerializable
{
    public string title;
    public string posterUrl;

    public Movie ToMovie()
    {
        Movie movie = ScriptableObject.CreateInstance<Movie>();
        movie.title = title;
        movie.posterUrl = posterUrl;
        //movie.sprite = Resources.Load<Sprite>(posterUrl);

        return movie;
    }
}

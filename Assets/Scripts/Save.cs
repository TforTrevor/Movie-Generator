using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public List<MovieSerializable> completedMovies = new List<MovieSerializable>();
    public List<MovieSerializable> queuedMovies = new List<MovieSerializable>();

    public Save(List<Movie> complete, List<Movie> queue)
    {
        completedMovies.Clear();
        foreach (Movie movie in complete)
        {
            MovieSerializable serializable = movie.ToSerializable();
            completedMovies.Add(serializable);
        }

        queuedMovies.Clear();
        foreach (Movie movie in queue)
        {
            MovieSerializable serializable = movie.ToSerializable();
            queuedMovies.Add(serializable);
        }
    }
}

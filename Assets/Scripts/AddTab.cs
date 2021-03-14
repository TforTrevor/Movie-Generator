using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class AddTab : MonoBehaviour
{
    [SerializeField] TMP_InputField titleInput;
    [SerializeField] TabButton queueTab;
    [SerializeField] TabGroup tabGroup;
    [SerializeField] Image movieImage;
    [SerializeField] Button addButton;

    string movieTitle;
    string posterUrl;

    public void AddMovie()
    {
        Movie movie = ScriptableObject.CreateInstance<Movie>();
        movie.title = movieTitle;
        movie.posterUrl = posterUrl;
        GameManager.Instance.queuedMovies.Add(movie);

        GameManager.Instance.SaveGame();

        tabGroup.ClickTab(queueTab);
    }

    public void Query()
    {
        StartCoroutine(GetRequest());
    }

    IEnumerator GetRequest()
    {
        addButton.interactable = false;

        string name = System.Uri.EscapeUriString(titleInput.text);

        UnityWebRequest searchRequest = UnityWebRequest.Get("https://en.wikipedia.org/w/api.php?action=opensearch&search=" + name + "&limit=1&namespace=0&format=json");
        yield return searchRequest.SendWebRequest();

        if (searchRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(searchRequest.downloadHandler.text);

            JSONObject searchJson = new JSONObject(searchRequest.downloadHandler.text);
            movieTitle = searchJson.list[1].list[0].str;
            string titles = System.Uri.EscapeUriString(searchJson.list[1].list[0].str);

            UnityWebRequest webRequest = UnityWebRequest.Get("https://en.wikipedia.org/w/api.php?action=query&format=json&formatversion=2&prop=pageimages|pageterms&piprop=original&pilicense=any" +
                                                             "&titles=" + titles);
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.downloadHandler.text);

                JSONObject json = new JSONObject(webRequest.downloadHandler.text);
                JSONObject pages = json.GetField("query").GetField("pages").list[0];
                if (pages.HasField("original"))
                {
                    posterUrl = pages.GetField("original").GetField("source").str;

                    GameManager.Instance.LoadImage(posterUrl, (Sprite sprite) =>
                    {
                        movieImage.sprite = sprite;
                    });
                }
                else
                {
                    posterUrl = "";
                    GameManager.Instance.LoadImage(posterUrl, (Sprite sprite) =>
                    {
                        movieImage.sprite = sprite;
                    });
                }
            }
        }

        addButton.interactable = true;
    }
}
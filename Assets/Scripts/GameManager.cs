using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Movie> completedMovies;
    public List<Movie> queuedMovies;

    [SerializeField] TabGroup tabGroup;
    [SerializeField] TabButton queueTab;
    [SerializeField] Sprite nullSprite;

    Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();

    void Awake()
    {
        Instance = this;
        LoadGame();
    }

    void Start()
    {
        tabGroup.HideAll();
        tabGroup.Close();
        tabGroup.ClickTab(queueTab);
    }

    public void SaveGame()
    {
        Save save = new Save(completedMovies, queuedMovies);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            completedMovies.Clear();
            foreach (MovieSerializable movie in save.completedMovies)
            {
                completedMovies.Add(movie.ToMovie());
            }

            queuedMovies.Clear();
            foreach (MovieSerializable movie in save.queuedMovies)
            {
                queuedMovies.Add(movie.ToMovie());
            }
        }
    }

    public void LoadImage(string imageUrl, System.Action<Sprite> callback)
    {
        if (imageUrl != null && imageUrl != "")
        {
            if (spriteDictionary.ContainsKey(imageUrl))
            {
                callback.Invoke(spriteDictionary[imageUrl]);
            }
            else
            {
                StartCoroutine(LoadImageRoutine(imageUrl, callback));
            }
        }
        else
        {
            callback.Invoke(nullSprite);
        }
    }

    IEnumerator LoadImageRoutine(string imageUrl, System.Action<Sprite> callback)
    {
        UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return textureRequest.SendWebRequest();

        if (textureRequest.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(textureRequest);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);
            if (!spriteDictionary.ContainsKey(imageUrl))
                spriteDictionary.Add(imageUrl, sprite);
            callback.Invoke(sprite);
        }
    }
}

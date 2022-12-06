using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public string playerName;

    public string highScoreName = "Nobody";
    public int highScore = 0;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Serializable]
    class SaveData
    {
        public string playerName;
        public int playerScore;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.playerName = highScoreName;
        data.playerScore = highScore;

        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath+ "/savefile.json";

        File.WriteAllText(path, json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highScoreName = data.playerName;
            highScore = data.playerScore;
        }

    }


}

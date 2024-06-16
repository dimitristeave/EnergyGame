using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int score_total;
    public float energie_total;
}

[System.Serializable]
public class KeyValuePair
{
    public string pseudo;
    public PlayerInfo value;
}

[System.Serializable]
public class PlayerDatabase
{
    public List<KeyValuePair> playersList = new List<KeyValuePair>();

    // Method to convert List to Dictionary
    public Dictionary<string, PlayerInfo> ToDictionary()
    {
        Dictionary<string, PlayerInfo> dict = new Dictionary<string, PlayerInfo>();
        foreach (var pair in playersList)
        {
            dict[pair.pseudo] = pair.value;
        }
        return dict;
    }

    // Method to convert Dictionary to List
    public void FromDictionary(Dictionary<string, PlayerInfo> dict)
    {
        playersList.Clear();
        foreach (var kvp in dict)
        {
            playersList.Add(new KeyValuePair { pseudo = kvp.Key, value = kvp.Value });
        }
    }
}

public class GameManagerDb : MonoBehaviour
{
    private string filePath;

    void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "base.json");
    }

    public PlayerDatabase LoadPlayerDatabase()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<PlayerDatabase>(json);
        }
        else
        {
            return new PlayerDatabase();
        }
    }

    public bool SavePlayerDatabase(PlayerDatabase db)
    {
        string json = JsonUtility.ToJson(db, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Save successful: " + filePath);
        return true;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int defaultScore;
    public int Score { get; set;}
    void OnEnable()
    {
        Score = defaultScore;
    }

    public void Save()
    {
        string s = JsonUtility.ToJson(this);
        if (FileManager.WriteToFile("playerdata.json", s))
        {
            Debug.Log("Save player data successful");
        }
    }
    public void Load()
    {
        string s;
        if (FileManager.LoadFromFile("playerdata.json",
        out s))
        {
            JsonUtility.FromJsonOverwrite(s, this);
            Debug.Log("Saved Score = " + Score);
        }
    }


}
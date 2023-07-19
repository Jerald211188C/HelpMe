using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreUIManager : MonoBehaviour
{
    [SerializeField] private PlayerData data;
    [SerializeField] private TMP_Text scoreText;

    void Start()
    {
        data.Load();
        scoreText.text = "" + data.Score;
    }

    public void OnSaveDataButtonClicked()
    {
        data.Save();
    }
}


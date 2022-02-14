using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Leadboard : MonoBehaviour
{
    private ScoreManager scoreManager;
    private UIManager uiManager;

    public bool _isPlayerDead = false;
    private bool _wasPlayerAdded = false;

    public class ScoreClass
    {
        public List<string> names = new List<string>();
        public List<float> scores = new List<float>();
    }

    ScoreClass scoreClassObject;


    //[System.NonSerialized] public List<ScoreClass> scores = new List<ScoreClass>();

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        uiManager = FindObjectOfType<UIManager>();

        scoreClassObject = new ScoreClass();
        LoadFromJson();
        Sort();
        SetUIText();
    }

    private void Update()
    {
        if (uiManager._isNameAdded && !_wasPlayerAdded)
        {
            /* Debug.Log(uiManager.playerName);
             Debug.Log(scoreManager.score);*/
            scoreClassObject.names.Add(uiManager.playerName);
            scoreClassObject.scores.Add(scoreManager.score);

            SaveToJson();

            _wasPlayerAdded = true;
        }
    }

    public void SaveToJson()
    {
        Debug.Log("it's working!");

        string json = JsonUtility.ToJson(scoreClassObject);


        File.WriteAllText(Application.dataPath + "/savedata/leadboard.json", json);
    }

    public void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/savedata/leadboard.json");

        scoreClassObject = JsonUtility.FromJson<ScoreClass>(json);
    }

    private void Sort()
    {
        for (int i = 0; i < scoreClassObject.scores.Count; i++)
        {
            for (int j = i + 1; j < scoreClassObject.scores.Count; j++)
            {
                if (scoreClassObject.scores[j] > scoreClassObject.scores[i])
                {
                    string tempName = scoreClassObject.names[i];
                    float tempScore = scoreClassObject.scores[i];
                    scoreClassObject.names[i] = scoreClassObject.names[j];
                    scoreClassObject.scores[i] = scoreClassObject.scores[j];
                    scoreClassObject.names[j] = tempName;
                    scoreClassObject.scores[j] = tempScore;
                }
            }
        }
    }

    private void SetUIText()
    {
        for (int i = 0; i < scoreClassObject.scores.Count; i++)
        {
            if (i >= 9)
            {
                return;
            }
            uiManager.ChangeText(uiManager.rankingNames[i], scoreClassObject.names[i]);
            uiManager.ChangeText(uiManager.rankingScores[i], scoreClassObject.scores[i].ToString());
        }
    }
}
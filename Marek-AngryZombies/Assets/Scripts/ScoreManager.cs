using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float score = 0;

    private void Update()
    {
        Debug.Log(score);
    }

    public void AddScore(float ammount)
    {
        score += ammount;
    }
}

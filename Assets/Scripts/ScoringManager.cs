using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoringManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI players, health, total, time, high;
    private void Start()
    {
        var st = FindObjectOfType<ScoreTransfer>();
        players.text = "Survivors: " + st.players.ToString();
        health.text = "Health: " + st.health.ToString();
        time.text = "Time: " + ((int)st.time).ToString();
        var score = (int)((1 + (st.health * 2) + (st.players * 200)) * st.time);
        total.text = "Score: " + score.ToString();
        if(PlayerPrefs.GetFloat("hs") < score) { PlayerPrefs.SetFloat("hs", score); }
        high.text = "High Score: " + PlayerPrefs.GetFloat("hs").ToString();

    }
}

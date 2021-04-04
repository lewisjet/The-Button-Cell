using System.Collections;
using System.Collections.Generic;
using TMPro;
using math = Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class DisasterSystem : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] float timeLeft = 720f;
    [SerializeField] math::int2 interval;
    [SerializeField] UnityEvent[] disasters;
    float currentInterval;
    float quartermark;
    float sttl;
    private void Start()
    {
        timeLeft = PlayerPrefs.GetInt("time");
        if(timeLeft == 0) { timeLeft = 120; }
        quartermark = timeLeft * 0.25f;
        sttl = timeLeft;
    }

    // Update is called once per frame
    void Update()
    {

        timer.text = ((int)timeLeft).ToString();
        if (currentInterval <= 0) { currentInterval = Random.Range(interval.x, interval.y); }
        timeLeft -= Time.deltaTime;
        currentInterval -= Time.deltaTime;
        if(timeLeft <= 0) { EndGame(); }
        else if(FindObjectsOfType<Player>() == null) { EndGame(); }
        if(currentInterval <= 0)
        {
            for (int i = 0; i < 4-(timeLeft / quartermark); i++)
            {
                disasters[Random.Range(0, disasters.Length)].Invoke();
            }
        }
        

    }

    public void EndGame()
    {
        var st = FindObjectOfType<ScoreTransfer>();
        st.time = sttl - timeLeft;
        var pl = FindObjectsOfType<Player>();
        if(pl != null)
        {
            st.players = pl.Length;
            float totalHealth = 0;
            for (int i = 0; i < pl.Length; i++)
            {
                totalHealth += pl[i].Health;
            }
            st.health = totalHealth;
        }
        StaticSceneLoader.LoadNextScene();
    }
}

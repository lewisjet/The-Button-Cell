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
    private void Start()
    {
        quartermark = timeLeft * 0.25f;
    }

    // Update is called once per frame
    void Update()
    {

        timer.text = ((int)timeLeft).ToString();
        if (currentInterval <= 0) { currentInterval = Random.Range(interval.x, interval.y); }
        timeLeft -= Time.deltaTime;
        currentInterval -= Time.deltaTime;

        if(currentInterval <= 0)
        {
            for (int i = 0; i < 4-(timeLeft / quartermark); i++)
            {
                disasters[Random.Range(0, disasters.Length)].Invoke();
            }
        }
        

    }
}

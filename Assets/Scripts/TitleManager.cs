using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    private void Start()
    {
        inputField.text = PlayerPrefs.GetInt("time").ToString();
        if(inputField.text == "120" || inputField.text == "0") { inputField.text = string.Empty; }
    }

    public void Package()
    {
        int.TryParse(inputField.text,out int time);
        if(time <= 0) { time = 120; }

        PlayerPrefs.SetInt("time", time);
    }
}


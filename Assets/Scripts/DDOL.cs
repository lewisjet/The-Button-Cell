using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour
{
  public byte id;


    private void Awake()
    {
        int quantity = 0;
       foreach (DDOL dDOL in FindObjectsOfType<DDOL>())
        {
            if (dDOL.id == id) { quantity++; }
        }
       if (quantity > 1) { Destroy(gameObject); }
        else { DontDestroyOnLoad(gameObject); }
    }
}

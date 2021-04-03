using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTransfer : MonoBehaviour
{
    public int players;
    public float health;
    public float time;

    public void Clear() => (players, health, time) = (0, 0, 0);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystemForClouds;
    [SerializeField] ButtonSlot bs;

    // Update is called once per frame
    void Update()
    {
        if(bs.IsEngaged)
        {
            particleSystemForClouds.gameObject.SetActive(false);
            StopAllCoroutines();
        }
    }
    public void Invoke() => particleSystemForClouds.gameObject.SetActive(true);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() && particleSystemForClouds.gameObject.activeInHierarchy) { StartCoroutine(Die(collision.GetComponent<Player>())); }
    }
    IEnumerator Die(Player player)
    {
        yield return new WaitForSeconds(2.5f);
        player.Health = 0;
    }
}

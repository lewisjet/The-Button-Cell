using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] int damage = 100;
    [SerializeField] float destructionTime = 3f;
    [SerializeField] float time = 1f;
    [SerializeField] ButtonSlot bs;
    [SerializeField] ParticleSystem sprinkler;
    float remaining;
    private void Start()
    {
        remaining = time;
    }

    // Update is called once per frame
    void Update()
    {
        if(bs.IsEngaged)
        {
            remaining -= Time.deltaTime;
            sprinkler.gameObject.SetActive(true);
            if (remaining <= 0)
            {
                remaining = time;
                particles.gameObject.SetActive(false);
                StopAllCoroutines();
            }
        }
        else
        {
            sprinkler.gameObject.SetActive(false);
        }
    }
    public void Invoke() => particles.gameObject.SetActive(true);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() && particles.gameObject.activeInHierarchy) { StartCoroutine(Die(collision.GetComponent<Player>())); }
    }
    IEnumerator Die(Player player)
    {
        while (true)
        {
            yield return new WaitForSeconds(destructionTime);
            if (!player) { goto PlayerAlreadyDead; }
            player.Health -= damage;
            PlayerAlreadyDead: ;
        }
    }
}

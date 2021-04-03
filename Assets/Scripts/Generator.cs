using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] float timeBetweenDamage = 1f;
    [SerializeField] float timeToDeactivate = 5f;
    [SerializeField] int damage = 5;
    float currentTime;
    float deactivationTime = 5f;
    [SerializeField] ParticleSystem particle;
    [SerializeField] ButtonSlot bs;
    bool pauseInjury = false;
    // Update is called once per frame
    void Update()
    {
        if (bs.IsEngaged)
        {
            pauseInjury = true;
            deactivationTime -= Time.deltaTime;
            if(deactivationTime <= 0)
            {
                particle.gameObject.SetActive(false);

              //  var people = FindObjectsOfType<Player>();
              //  for (int i = 0; i < people.Length; i++)
              //  {
                  //  people[i].Animator.SetBool("InDanger",false);
             //   }
                deactivationTime = timeToDeactivate;
               
            }
        }
        else
        {
            pauseInjury = false;
        }
        if (particle.gameObject.activeInHierarchy && !pauseInjury)
        {
            if (currentTime <= 0)
            {
                var people = FindObjectsOfType<Player>();
                for (int i = 0; i < people.Length; i++)
                {
                    people[i].Health -= damage;
                  //  people[i].Animator.SetBool("InDanger", true);
                }
                currentTime = timeBetweenDamage;
            }
            currentTime -= Time.deltaTime;
        }
    }
    public void Initialise() => particle.gameObject.SetActive(true);
}

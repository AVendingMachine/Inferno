using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crawler : MonoBehaviour
{
    public float moveSpeed = 7;
    GameObject player;
    public NavMeshAgent agent;
    public Animator anim;
    bool playerInRange = false;



    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<NavMeshAgent>().speed = moveSpeed;

    }
 
    void Update()
    {
        agent.destination = player.transform.position;
        if (playerInRange)
        {

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        

        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(AttackCycle());
            anim.SetBool("walking", false);
            playerInRange = true;
            agent.speed = 0.1f;
        } 

    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            anim.SetBool("walking", true);
            agent.speed = moveSpeed;
        }

    }
    private IEnumerator AttackCycle()
    {
        anim.SetBool("attacking", true);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("attacking", false);
        Debug.Log("bazomples");
        yield return new WaitForSeconds(2);
        if (playerInRange)
        {
            StartCoroutine(AttackCycle());
        }
    }
}

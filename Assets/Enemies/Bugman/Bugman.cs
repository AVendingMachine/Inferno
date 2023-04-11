using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;


public class Bugman : MonoBehaviour
{
    private GameObject player;
    public Animator anim;
    public NavMeshAgent agent;
    public bool attacking = false;
    public float attackDamage = 1f;
    public float moveSpeed = 7;
    public GameObject rig;




    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<NavMeshAgent>().speed = moveSpeed;

    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<NavMeshAgent>().speed = 0.1f;
            if (!attacking)
            {
                StartCoroutine(Attack(attackDamage));
                attacking = true;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<NavMeshAgent>().speed = moveSpeed;
        }

    }

    IEnumerator Attack(float damage)
    {
        anim.SetBool("attacking", true);
        player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("attacking", false);
        attacking = false;
    }

}

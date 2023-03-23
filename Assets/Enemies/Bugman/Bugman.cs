using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bugman : MonoBehaviour
{
    private GameObject player;
    public NavMeshAgent agent;

    
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
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
}

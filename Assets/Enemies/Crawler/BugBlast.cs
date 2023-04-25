using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBlast : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       Rigidbody rb = GetComponent<Rigidbody>();
       // transform.position = Vector3.MoveTowards(player.transform.position, 1);
        rb.AddRelativeForce(transform.forward);

    }
}

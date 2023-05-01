using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rJumpDummy : MonoBehaviour
{
    GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            player.GetComponent<PlayerMovement>().StopJumping();
        }
    }
}

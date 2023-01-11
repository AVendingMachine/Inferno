using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTipCollider : MonoBehaviour
{
    public bool colliding = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ground")
        {
            colliding = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Ground")
        {
            colliding = false;
        }
    }
}

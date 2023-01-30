using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCollider : MonoBehaviour
{
    public Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("ground triggeredd mf");
            animator.SetBool("inWall", true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("inWall", false);
    }
}

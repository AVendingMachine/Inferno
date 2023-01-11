using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWallAvoid : MonoBehaviour
{
    bool colliding = false;
    public Transform hand;
    public float viewModelTime;
    public GameObject rifleTip;
    public float avoidingSpeed = 4;
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
    private void FixedUpdate()
    {
        if (colliding)
        {
            transform.localPosition -= new Vector3(0, 0, avoidingSpeed*Time.deltaTime);
            Debug.Log("Hello everybody my name is Markiplier");
            viewModelTime = 0;
        }
        if (!colliding && rifleTip.GetComponent<GunTipCollider>().colliding == false)
        {
            viewModelTime = Mathf.Clamp(viewModelTime+Time.deltaTime,0,1);
            transform.position = Vector3.Lerp(transform.position, hand.position, viewModelTime);
        }
    }
}


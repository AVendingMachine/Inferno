using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewModelBump : MonoBehaviour
{
    public float avoidingSpeed = 5;
    public GameObject objectTip;
    private float adsTime = 0;
    private bool colliding = false;
    private float viewModelTime;
    private Vector3 startingPos;


    private void Start()
    {
        startingPos = transform.localPosition;
    }
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
            transform.localPosition -= new Vector3(0, 0, avoidingSpeed * Time.deltaTime);
           // Debug.Log("Hello everybody my name is Markiplier");
            viewModelTime = 0;
        }
        if (!colliding && objectTip.GetComponent<GunTipCollider>().colliding == false)
        {
            viewModelTime = Mathf.Clamp(viewModelTime + Time.deltaTime, 0, 1);
            if (adsTime == 0 && transform.localPosition.z != startingPos.z)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, startingPos, viewModelTime);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBob : MonoBehaviour
{
    public GameObject player;
    private float viewBobTime = 0f;
    private bool viewBobDirection = true;
    private bool colliding = false;
    private Vector3 startingPos;

    private void Start()
    {
        startingPos = transform.localPosition;
    }
    void Update()
    {
        if (!Input.GetKey(KeyCode.Mouse1) && colliding == false && player.GetComponent<PlayerMovement>().isMoving == true)
        {
            if (viewBobDirection == true)
            {
                transform.localPosition += Vector3.up * Time.deltaTime * 0.1f;
                viewBobTime = Mathf.Clamp(viewBobTime + Time.deltaTime * 5, 0, 1);
            }
            if (viewBobDirection == false)
            {
                transform.localPosition -= Vector3.up * Time.deltaTime * 0.1f;
                viewBobTime = Mathf.Clamp(viewBobTime - Time.deltaTime * 5, 0, 1);
            }
            if (viewBobTime >= 1)
            {
                viewBobDirection = false;
            }
            if (viewBobTime <= 0)
            {
                viewBobDirection = true;
            }

        }
        else
        {
            viewBobDirection = true;
            viewBobTime = Mathf.Clamp(viewBobTime - Time.deltaTime * 5, 0, 1);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, startingPos.y, transform.localPosition.z), Time.deltaTime);
        }
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
}

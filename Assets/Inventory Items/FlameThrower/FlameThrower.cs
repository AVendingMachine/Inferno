using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public ParticleSystem flames;
    public GameObject playerBody;
    public GameObject mainCam;
    private void OnEnable()
    {
        flames.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("flame on");
            flames.Play();
            playerBody.GetComponent<PlayerMovement>().aimingDown = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            flames.Stop();
            playerBody.GetComponent<PlayerMovement>().aimingDown = false;
        }
    }
}

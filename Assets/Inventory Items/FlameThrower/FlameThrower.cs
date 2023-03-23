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
            flames.Play();
            playerBody.GetComponent<PlayerMovement>().aimingDown = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            flames.Stop();
            playerBody.GetComponent<PlayerMovement>().aimingDown = false;
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().CatchFire(1);
            Debug.Log("fire has been sent");
        }
    }
}

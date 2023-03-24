using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public ParticleSystem flames;
    public GameObject playerBody;
    public GameObject mainCam;
    public float fireDamage = 1;
    private void OnEnable()
    {
        if (flames.isPlaying)
        {
            flames.Stop();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (!flames.isPlaying)
            {
                flames.Play();
            }
            
            playerBody.GetComponent<PlayerMovement>().aimingDown = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (flames.isPlaying)
            {
                flames.Stop();
            }
            
            playerBody.GetComponent<PlayerMovement>().aimingDown = false;
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().CatchFire(fireDamage*0.01f);
            Debug.Log("fire has been sent");
        }
    }
}

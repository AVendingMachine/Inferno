using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public ParticleSystem flames;
    public GameObject playerBody;
    public GameObject mainCam;
    public float fireDamage = 1;
    bool reloading = false;
    
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
        if (GetComponent<AmmoSystem>().currentAmmo <= 0 && !reloading)
        {
            StartCoroutine(Reload());
        }

        if (Input.GetKey(KeyCode.Mouse0) && !reloading)
        {
            if (!flames.isPlaying)
            {
                flames.Play();
            }
            
            playerBody.GetComponent<PlayerMovement>().aimingDown = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) || reloading)
        {
            if (flames.isPlaying)
            {
                flames.Stop();
            }
            
            playerBody.GetComponent<PlayerMovement>().aimingDown = false;
        }
    }

    private void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.Mouse0))
        {

            GetComponent<AmmoSystem>().LoseAmmo(1);
        }
    }

    private IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(GetComponent<AmmoSystem>().reloadTime);
        reloading = false;
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

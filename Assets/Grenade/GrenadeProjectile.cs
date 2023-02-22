using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    public float throwForce = 4f;
    float climbSpeed;
    public ParticleSystem ps;
    bool exploded = false;

    void Start()
    {
        climbSpeed = throwForce;
        GetComponent<Rigidbody>().AddForce(transform.forward * climbSpeed * 0.2f, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(-transform.up * climbSpeed, ForceMode.Impulse);
    }

    private void Update()
    {
        transform.Rotate(-1, 0, 0);
        if (exploded && ps.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }
    void Explode()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<MeshRenderer>().enabled = false;
        ps.Play();
        exploded = true;

    }
}

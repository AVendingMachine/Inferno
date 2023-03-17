using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    public float throwForce = 4f;
    float climbSpeed;
    public ParticleSystem ps;
    bool exploded = false;
    public GameObject parent;
    public GameObject light2;
    bool fadeLight = false;

    void Start()
    {
        light2.SetActive(false);
        StartCoroutine(DeathTimer());
        climbSpeed = throwForce;
        GetComponent<Rigidbody>().AddForce(transform.forward * climbSpeed * 0.2f, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(-transform.up * climbSpeed, ForceMode.Impulse);
    }

    private void Update()
    {
        transform.Rotate(-0.4f, 0, 0);
        if (exploded && ps.isPlaying == false)
        {
            Destroy(parent);
        }
        if (fadeLight)
        {
            light2.GetComponent<Light>().intensity -= 7 * Time.deltaTime;
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
        fadeLight = true;
        light2.SetActive(true);

    }
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(10f);
        Destroy(parent);
    }
}

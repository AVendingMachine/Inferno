using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    public ParticleSystem ps;
    bool exploded = false;
    public GameObject rocketModel;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (exploded && ps.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            Explode();
        }

    }
    void Explode()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        rocketModel.GetComponent<MeshRenderer>().enabled = false;
        ps.Play();
        exploded = true;

    }
}

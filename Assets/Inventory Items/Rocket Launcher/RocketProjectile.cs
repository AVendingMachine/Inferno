using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    public ParticleSystem ps;
    public ParticleSystem fire;
    bool exploded = false;
    public GameObject rocketModel;
    public float speed = 1;
    public GameObject light;
    public GameObject light2;
    bool fadeLight = false;
    // Start is called before the first frame update
    void Start()
    {
        light2.SetActive(false);
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
        if (collision.transform.tag != "Player")
        {
            Explode();
        }

    }
    void Explode()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        rocketModel.GetComponent<MeshRenderer>().enabled = false;
        ps.Play();
        fire.Stop();
        Destroy(light);
        light2.SetActive(true);
        exploded = true;
        fadeLight = true;
        StartCoroutine(ExplosionDamage());

    }
    private void Update()
    {
        if (fadeLight)
        {
            light2.GetComponent<Light>().intensity -= 10*Time.deltaTime;
        }
    }

    IEnumerator ExplosionDamage()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, 3);
        foreach (Collider target in hitEnemies)
        {
            if (target.transform.gameObject.CompareTag("Enemy"))
            {
                target.GetComponent<EnemyHealth>().TakeDamage(10);
            }
        }
        yield return new WaitForSeconds(0.1f);
        Collider[] hitEnemies2 = Physics.OverlapSphere(transform.position, 3);
        foreach (Collider target in hitEnemies2)
        {
            if (target.transform.gameObject.GetComponent<Rigidbody>() != null)
            {
                target.GetComponent<Rigidbody>().AddExplosionForce(3000, transform.position,3);
            }
        }
    }
}

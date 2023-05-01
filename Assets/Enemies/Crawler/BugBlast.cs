using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBlast : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathTimer());
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
       Rigidbody rb = GetComponent<Rigidbody>();
       // transform.position = Vector3.MoveTowards(player.transform.position, 1);
        rb.AddForce(transform.forward*0.3f, ForceMode.Impulse);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(5);
            Destroy(gameObject);
        }
        if (other.transform.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}

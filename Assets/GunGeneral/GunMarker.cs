using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMarker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}

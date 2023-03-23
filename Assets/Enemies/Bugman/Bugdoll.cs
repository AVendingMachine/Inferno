using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bugdoll : MonoBehaviour
{
    bool fade = false;
    public GameObject model;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GoAway());
    }

    IEnumerator GoAway()
    {
        yield return new WaitForSeconds(10);
        fade = true;
    }
    private void Update()
    {
        if (fade)
        {
            Destroy(gameObject);
        }
    }
}

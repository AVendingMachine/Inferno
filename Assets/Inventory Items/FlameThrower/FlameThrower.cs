using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public ParticleSystem flames;
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
        }
        else
        {
            flames.Stop();
        }
    }
}

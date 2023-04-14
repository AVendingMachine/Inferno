using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public float ammoPercent = 0.2f;
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            foreach (GameObject item in other.gameObject.GetComponent<PlayerInventory>().items)
            {
                item.GetComponent<AmmoSystem>().currentReserve += Mathf.Round(ammoPercent * item.GetComponent<AmmoSystem>().maxReserve);
            }
            Destroy(gameObject);
        }
    }

}

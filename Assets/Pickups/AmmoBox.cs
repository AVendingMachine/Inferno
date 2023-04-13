using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            foreach (GameObject item in other.gameObject.GetComponent<PlayerInventory>().items)
            {
                item.GetComponent<AmmoSystem>().currentReserve += (0.1f * item.GetComponent<AmmoSystem>().maxReserve);
            }
            Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cristalBehaviour : MonoBehaviour
{
    inventoryCollider Player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player = other.gameObject.GetComponent<inventoryCollider>();
            Player.cristals++;
            Destroy(gameObject);

        }
    }
}

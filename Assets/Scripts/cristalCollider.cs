using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cristalCollider : MonoBehaviour
{
    inventoryCollider Player;
    private void Start()
    {
        Player = FindObjectOfType<inventoryCollider>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<inventoryCollider>() && Player.healt < 100f)
        {
            Player.healt = Player.healt + 0.02f;
        }
    }
}

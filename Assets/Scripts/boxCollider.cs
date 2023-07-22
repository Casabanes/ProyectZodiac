using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxCollider : MonoBehaviour
{
    inventoryCollider Player;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && gameObject.activeSelf)
        {
            Player = other.gameObject.GetComponent<inventoryCollider>();
            Player.healt = Player.healt - 30;
        }
    }
}

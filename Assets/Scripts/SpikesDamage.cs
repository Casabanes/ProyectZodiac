using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesDamage : MonoBehaviour
{
    [SerializeField]
    private int _layerPlayer = 9;
    [SerializeField]
    private int _spikesDamage=10;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer== _layerPlayer)
        {
            other.GetComponent<PlayerLiveObject>().TakeDamage(_spikesDamage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponArm : MonoBehaviour
{
    [SerializeField]
    private Jabali_Enemy _jabaliEnemy;
    [SerializeField]
    private int _damage;
    [SerializeField]
    private int _layerPlayer=9;
    private void Start()
    {
        if (_jabaliEnemy == null)
            _jabaliEnemy=GetComponentInParent<Jabali_Enemy>();
        _damage = _jabaliEnemy._damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _layerPlayer)
        {
            if(_damage!=_jabaliEnemy._damage)
                _damage = _jabaliEnemy._damage;
            other.GetComponent<PlayerLiveObject>().TakeDamage(_damage);
        }
    }
}

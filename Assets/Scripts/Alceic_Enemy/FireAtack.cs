using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAtack : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;
    [SerializeField]
    private List<ParticleCollisionEvent> _particlecollisionEvent = new List<ParticleCollisionEvent>();
    [SerializeField]
    private int _layerPlayer;
    [SerializeField]
    public int _damage;
    private int _defaultDamage =5;
    [SerializeField]
    public float _fireTime;

    private PlayerLiveObject _liveObject;
    private float _defaultFireTime = 3;
    private const int _constZero = 0;
    void Start()
    {
        if (!_particleSystem)
            _particleSystem = GetComponent<ParticleSystem>();
        if (_damage == _constZero)
            _damage = _defaultDamage;
        if (_fireTime == _constZero)
            _fireTime = _defaultFireTime;

    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == _layerPlayer)
        {
            _liveObject=other.GetComponent<PlayerLiveObject>();
            _liveObject.InFire(_damage, _fireTime,true);
            _liveObject.StartFireDamageSound();
        }
        
    }
   
}

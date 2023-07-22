using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryAttack : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;
    [SerializeField]
    private int _layerEnemy=8;
    private LiveObject _liveObject;
    [SerializeField]
    public int _damage;
    [SerializeField]
    private float _timeToDisapear;
    void Start()
    {
        if (!_particleSystem)
            _particleSystem = GetComponent<ParticleSystem>();
        StartCoroutine(DisapearAfterTime());
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == _layerEnemy)
        {
            _liveObject = other.GetComponent<LiveObject>();
            _liveObject.TakeDamage (_damage);
        }

    }
    private IEnumerator DisapearAfterTime()
    {
        yield return new WaitForSeconds(_timeToDisapear);
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class shootBehaviour : MonoBehaviour
{
    public float speed;
    enemyTest enemytest;
    public Explosion explosion;
    [SerializeField]
    private int _layerEnemy=8;
    private LiveObject _enemy;
    public int _damage=40;
    void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _layerEnemy)
        {
            _enemy = other.gameObject.GetComponent<LiveObject>();
            _enemy.TakeDamage(_damage);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaraState : MonoBehaviour
{
    public bool _isShoting;
    private PlayerFunctions _playerFunctions;
    [SerializeField]
    private int _layerEnemy = 8;
    [SerializeField]
    private int _damage;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _hitSound;
    [SerializeField]
    private AudioClip _missHitSound;
    [SerializeField]
    private LiveObject _liveObject;
    [SerializeField]
    private bool _didHit;
    private const int _constZero = 0;
    private void Start()
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==_layerEnemy )
        {
            if (!_didHit)
            {
                _liveObject=other.GetComponent<LiveObject>();
                if (_liveObject._invulnerable == true)
                {
                    _liveObject._hitsRecived = _constZero;
                    _liveObject._invulnerable = false;
                    _liveObject.SoundInvulnerable();
                }
                else
                {
                if (_audioSource.isPlaying)
                    _audioSource.Stop();
                _audioSource.clip = _hitSound;
                _audioSource.Play();
                _liveObject.TakeDamage(_damage);
                }
                _didHit = true;
            }
        }
    }
    public void MissHit()
    {
        if (_didHit)
        {
            return;
        }
        if (_audioSource.isPlaying)
            _audioSource.Stop();
        _audioSource.clip = _missHitSound;
        _audioSource.Play();
    }
    public void ResetHit()
    {
        _didHit = false;
    }
}

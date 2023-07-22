using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveObject : MonoBehaviour
{
    [SerializeField]
    private float _life;
    const int _constZero=0;
    const int _constOne = 1;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    public AudioClip _damagedSound;
    [SerializeField]
    public AudioClip _deadSound;

    public bool _takingDamage;
    public bool _isDead;
    private bool _isInFire;
    private float _inFireTimer;
    private float _fireDamage;
    private float _fireTime;
    private int _tickCount;
    public bool _invulnerable;
    private float _timeToBackVulnerable;
    [SerializeField]
    private float _invulnerableTime;
    public int _hitsRecived;
    [SerializeField]
    private int _hitsBlockInInvulnerableState=2;
    [SerializeField]
    private AudioClip _fireSound;
    [SerializeField]
    private AudioClip _invulnerableHitSound;

    void Start()
    {
        if(_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        _isDead = false;
        _isInFire = false;
        _fireDamage = _constZero;
        _fireTime = _constZero;

    }

    void Update()
    {
        InvulenrableStuff();
        InFire(_fireDamage, _fireTime, _isInFire);
    }

    public void InvulenrableStuff()
    {
        if (!_invulnerable)
            return;
        _timeToBackVulnerable += Time.deltaTime;
        if(_timeToBackVulnerable>_invulnerableTime)
        {
            _invulnerable = false;
            _timeToBackVulnerable = _constZero;
        }

    }
    public void TakeDamage(int _damage)
    {
        if (_invulnerable)
        {
            if (_hitsRecived < _hitsBlockInInvulnerableState)
            {
                SoundInvulnerable();
                _hitsRecived++;
            return;
            }
            else
            {
                _hitsRecived = _constZero;
                _timeToBackVulnerable = _constZero;
                _invulnerable = false;
            }
        }
        else
        {

            _life -= _damage;
        if(_life<= _constZero)
        {
            _audioSource.Stop();
            _audioSource.clip = _deadSound;
            _audioSource.Play();
            _isDead = true;
        }
        else
        {
            _takingDamage = true;
            if (_audioSource.clip != _damagedSound)
                _audioSource.clip = _damagedSound;
            _audioSource.clip = _damagedSound;
            _audioSource.Play();
            _invulnerable = true;
        }
        }
    }
    public void InFire(float _firedamage, float _firetime,bool _isinfire)
    {
        if (_isinfire)
        {
            _isInFire = _isinfire;
      
        }
        else
        {
            return;
        }
       
        _fireDamage = _firedamage;
        _fireTime = _firetime;
        _inFireTimer += Time.deltaTime;
        
        if (_inFireTimer >=_constOne)
        {
            _tickCount++;
            _life -= _firedamage;
            _inFireTimer = _constZero;
        }
        if (_tickCount >= _firetime)
        {
            if (_audioSource.clip == _fireSound && _audioSource.isPlaying)
                _audioSource.Stop();
            _isInFire = false;
            _inFireTimer = _constZero;
            _fireDamage = _constZero;
            _fireTime = _constZero;
            _tickCount=_constZero;

        }

    }
    public void StartFireDamageSound()
    {
        if (_fireSound != null)
        {
            _audioSource.clip = _fireSound;
            _audioSource.Play();
        }
    }
    public void SoundInvulnerable()
    {
        _audioSource.clip = _invulnerableHitSound;
        _audioSource.Play();
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerLiveObject : MonoBehaviour
{
    
    [SerializeField]
    private float _maxLife;
    [SerializeField]
    private float _life;
    const int _constZero = 0;
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
    private int _hitsBlockInInvulnerableState = 2;
    [SerializeField]
    private AudioClip _fireSound;
    [SerializeField]
    private AudioClip _invulnerableHitSound;

    [SerializeField]
    private Image _lifeBar;
    [SerializeField]
    private Image _bloodMark;
    [SerializeField]
    private float _timeOfFadeOfMark;
    [SerializeField]
    private float _actualTimeOfFadeOfMark;
    [SerializeField]
    private Image _fire;
    private bool _markIsActive;

    void Start()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        _isDead = false;
        _isInFire = false;
        _fireDamage = _constZero;
        _fireTime = _constZero;
        _life = _maxLife;
    }

    void Update()
    {
        InFire(_fireDamage, _fireTime, _isInFire);
        MarkApearAndFade();
    }

    
    public void TakeDamage(int _damage)
    {
        _life -= _damage;
        _lifeBar.fillAmount = _life / _maxLife;
        _markIsActive = true;
        _actualTimeOfFadeOfMark = _constZero;
        if (_life <= _constZero)
            {
                _audioSource.Stop();
                _audioSource.clip = _deadSound;
                _audioSource.Play();
                _isDead = true;
            }
            else
            {
                if (_audioSource.clip != _damagedSound)
                    _audioSource.clip = _damagedSound;
                if (_audioSource.isPlaying)
                    _audioSource.Stop();
                _audioSource.clip = _damagedSound;
                _audioSource.Play();
            }
        
    }
    public void InFire(float _firedamage, float _firetime, bool _isinfire)
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

        if (_inFireTimer >= _constOne)
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
            _fire.enabled = false;
            _inFireTimer = _constZero;
            _fireDamage = _constZero;
            _fireTime = _constZero;
            _tickCount = _constZero;

        }

    }
    public void StartFireDamageSound()
    {
        if (_fireSound != null)
        {
            _audioSource.clip = _fireSound;
            _audioSource.Play();
            _fire.enabled = true;
        }
    }
    public void SoundInvulnerable()
    {
        _audioSource.clip = _invulnerableHitSound;
        _audioSource.Play();
    }
    private void MarkApearAndFade()
    {
        if (!_markIsActive)
            return;
        _actualTimeOfFadeOfMark += Time.deltaTime;
        _bloodMark.color = new Color(_bloodMark.color.r, _bloodMark.color.g, _bloodMark.color.b, _constOne  - _constOne / (_timeOfFadeOfMark - _actualTimeOfFadeOfMark));
        if (_actualTimeOfFadeOfMark >= _timeOfFadeOfMark)
        {
            _bloodMark.color = new Color(_bloodMark.color.r, _bloodMark.color.g, _bloodMark.color.b,_constZero);
            _actualTimeOfFadeOfMark = _constZero;
            _markIsActive = false;
        }
    }
}

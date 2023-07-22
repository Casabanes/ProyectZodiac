using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alceic : MonoBehaviour
{
    [SerializeField]
    private PlayerControler _player;
    [SerializeField]
    private float _distance;
    [SerializeField]
    private float _rangeToPersuit;
    [SerializeField]
    private float _rangeToAtack;
    [SerializeField]
    private Rigidbody _rigidBody;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private float _refreshLook;
    [SerializeField]
    private LiveObject _liveObject;
    [SerializeField]
    private float _stunTime;
    [SerializeField]
    private float _atackCooldown;
    [SerializeField]
    private AudioSource _audiosource;
    [SerializeField]
    private ParticleSystem _particleSystem;
    [SerializeField]
    private bool _particleSystemOnOff;
    [SerializeField]
    private AudioClip[] _audioClip;
    [SerializeField]
    private bool _isAtacking;
    [SerializeField]
    private CapsuleCollider _capsuleCollider;
    [SerializeField]
    private float _corpseDuration;
    [SerializeField]
    private int _layerPlayer = 9;



    [SerializeField]
    private float _rotationSpeed;
    private Quaternion _lookRotation;
    private Vector3 _direction;




    private float _actualAtackCooldown;
    private const int _constZero = 0;
    private bool _isTakingDamage;
    private Vector3 _distanceWithoutY ;

    void Start()
    {
        if (_player == null)
            _player = FindObjectOfType<PlayerControler>();
        if (_particleSystem == null)
            _particleSystem = GetComponentInChildren<ParticleSystem>();
        if(_rigidBody==null)
            _rigidBody = GetComponent<Rigidbody>();
        if (_animator == null)
            _animator = GetComponent<Animator>();
        if (_liveObject == null)
            _liveObject = GetComponent<LiveObject>();
        if(_capsuleCollider==null)
            _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (!_liveObject._isDead)
        {

            if (_liveObject._takingDamage)
            {
                StartCoroutine(Stun());
                TakeDamageAnimation();
            }
            if (!_isTakingDamage)
            {
                if (!_isAtacking)
                {
                    Movment();
                }
                Atack();
            }
            StartCoroutine(DistanceAndLook());
            AtackCheckAndCall();
            ParticleOnOff();

            if (_distance < _rangeToAtack)
                ClampY();
        }
        else
        {
            StartCoroutine(Dead());
        }
    }
    private IEnumerator DistanceAndLook()
    {
        yield return new WaitForSeconds(_refreshLook);
        _distance = Vector3.Distance(transform.position, _player.transform.position);
        AtackCheckAndCall();
        if (!_isAtacking && _distance > _rangeToAtack)
        {
            _distanceWithoutY = new Vector3(transform.position.x - _player.transform.position.x, _constZero, transform.position.z - _player.transform.position.z);
            transform.forward = -Vector3.Normalize(_distanceWithoutY);
        }
    }
    private void Movment()
    {
        if (_distance < _rangeToPersuit && _distance > _rangeToAtack)
        {
            _rigidBody.velocity = new Vector3(transform.forward.x, _rigidBody.velocity.y/_speed, transform.forward.z) * _speed;
            _animator.SetBool("_running", true);
        }
        else
        {
            _rigidBody.velocity = new Vector3 (_constZero, _rigidBody.velocity.y,_constZero);
            _animator.SetBool("_running", false);
            
        }
    }
    private void AtackCheckAndCall()
    {
        if (_isAtacking == false && _distance < _rangeToAtack)
        {
            _animator.SetTrigger("_atack");
            if (!_audiosource.isPlaying && _audiosource.clip!=_liveObject._deadSound)
            {
                if(_audiosource.clip != _audioClip[1])
                    _audiosource.clip = _audioClip[1];
                _audiosource.Play();
            }
        }
    }
    private void Atack()
    {
        if (_isAtacking)
        {
            _rigidBody.velocity = new Vector3(_constZero, _rigidBody.velocity.y, _constZero);
            _animator.SetBool("_running", !_isAtacking);
            _actualAtackCooldown += Time.deltaTime;
            RotateWhenAtack();
            if (_actualAtackCooldown > _atackCooldown)
            {
                _actualAtackCooldown = _constZero;
                _isAtacking = false;
            }
        }
        else
        {
            _distanceWithoutY = new Vector3(transform.position.x - _player.transform.position.x, _constZero, transform.position.z - _player.transform.position.z);
            transform.forward = -Vector3.Normalize(_distanceWithoutY);
        }
    }
    private void RotateWhenAtack()
    {
        _direction = (_player.transform.position - transform.position).normalized;
        _direction.y = _constZero;
        _lookRotation = Quaternion.LookRotation(_direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * _rotationSpeed);
    }

    private void ParticleOnOff()
    {
        if (_particleSystemOnOff)
        {
            if (!_particleSystem.isPlaying)
            {
                _particleSystem.Stop();
                _particleSystem.Play();
            }
        }
        else
        {
            if (_particleSystem.isPlaying)
            {
                _particleSystem.Stop();
            }
        }
    }

    private IEnumerator Stun()
    {
        yield return new WaitForSeconds(_stunTime);
        _isTakingDamage = false;
    }
    private void TakeDamageAnimation()
    {
        if (_isTakingDamage)
            return;
        
        _isTakingDamage = true;
        _actualAtackCooldown = _constZero;
        _isAtacking = false;


        _audiosource.Stop();
        _audiosource.clip = _audioClip[0];
        _audiosource.Play();

        _distanceWithoutY = new Vector3(transform.position.x - _player.transform.position.x, _constZero, transform.position.z - _player.transform.position.z);
        transform.forward = -Vector3.Normalize(_distanceWithoutY);
        _rigidBody.velocity = new Vector3(_constZero, _rigidBody.velocity.y, _constZero);
        
        _animator.SetBool("_running", false);
        _animator.SetTrigger("_damaged");

        _particleSystemOnOff = false;

        _liveObject._takingDamage = false;

    }
    private IEnumerator Dead()
    {
        _animator.SetBool("_running", false);
        _particleSystemOnOff = true;
        _animator.SetTrigger("_damaged");
        _rigidBody.isKinematic = true;
        _capsuleCollider.enabled = false;
        _liveObject.enabled = false;
        _animator.SetBool("_dead",true);

        yield return new WaitForSeconds(_corpseDuration);
        Destroy(gameObject);
    }
    private void ClampY()
    {
        if (_rigidBody.velocity.y > _constZero)
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, _constZero, _rigidBody.velocity.z);
    }
}

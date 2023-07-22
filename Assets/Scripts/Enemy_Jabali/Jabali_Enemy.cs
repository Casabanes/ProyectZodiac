using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jabali_Enemy : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider _capsuleCollider;
    [SerializeField]
    private SphereCollider _sphereCollider;
    [SerializeField]
    private float _rangeToPursuit;
    [SerializeField]
    private float _rangeToAttack;
    [SerializeField]
    private LiveObject _liveObject;
    [SerializeField]
    private PlayerControler _player;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rigidbody _rigidBody;
    [SerializeField]
    private int _layerPlayer = 9;
    [SerializeField]
    private float _distanceToPlayer;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private bool _isAttacking;
    [SerializeField]
    private bool _canRotate;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _deadSound;
    [SerializeField]
    private AudioClip _attackSound;
    [SerializeField]
    private AudioClip _moveSound;
    [SerializeField]
    private float _stunTime = 2;
    [SerializeField]
    private float _corpseDuration = 5;
    [SerializeField]
    private float _attackSpeedIncrement = 0.4f;
    [SerializeField]
    private float _speedIncrement = 2;

    private float _originalSpeed;
    private int _auxiliarHitsRecived;
    private float _originalAttackSpeedMultiplier;
    private bool _takingDamage;
    public int _damage;
    private Vector3 _rotationDirection;
    private Quaternion _lookRotation;
    private bool _isMoving;
    private const int _constZero = 0;
    void Start()
    {
        if (_capsuleCollider == null)
            _capsuleCollider = GetComponent<CapsuleCollider>();
        if (_sphereCollider)
            _sphereCollider = GetComponentInChildren<SphereCollider>();
        if (_liveObject)
            _liveObject = GetComponent<LiveObject>();
        if (_player == null)
            _player = FindObjectOfType<PlayerControler>();
        if (_animator == null)
            _animator = GetComponent<Animator>();
        if (_rigidBody == null)
            _rigidBody = GetComponent<Rigidbody>();
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        _originalAttackSpeedMultiplier = _animator.GetFloat("_attackSpeedMultiplier");
        _originalSpeed = _speed;
    }

    void Update()
    {
        if (_liveObject._isDead)
        {
            StartCoroutine(Dead());
        }
        else
        {
            if (!_liveObject._takingDamage)
            {
                Movment();
                Attack();
            }
            else
            {
                StartCoroutine(Stun(_stunTime));
                TakeDamage();
            }
        }
            if (_rigidBody.velocity.y > _constZero)
                _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, _constZero, _rigidBody.velocity.z);
        Berserk();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _layerPlayer)
        {
            _isMoving = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == _layerPlayer)
        {
            _isMoving = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _layerPlayer)
        {
            _isMoving = false;
        }
    }
    private void Movment()
    {
        _distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
        if (!_isMoving || _isAttacking || _distanceToPlayer > _rangeToPursuit || _distanceToPlayer <= _rangeToAttack)
        {
            _rigidBody.velocity = new Vector3(_constZero, _rigidBody.velocity.y, _constZero);
            _animator.SetBool("_running", false);
            if (_audioSource.isPlaying && _audioSource.clip == _moveSound)
            {
                _audioSource.Stop();
            }
            return;
        }
        else
        {
            if (_distanceToPlayer < _rangeToPursuit && _distanceToPlayer > _rangeToAttack)
            {
                transform.forward = new Vector3(_player.transform.position.x - transform.position.x, _constZero, _player.transform.position.z - transform.position.z);
                _animator.SetBool("_running", true);
                _rigidBody.velocity = new Vector3(transform.forward.x, _rigidBody.velocity.y / _speed, transform.forward.z) * _speed;
                if (!_audioSource.isPlaying && _audioSource.clip != _deadSound)
                {
                    _audioSource.clip = _moveSound;
                    _audioSource.Play();
                }
            }
        }
    }
    private void Attack()
    {
        _distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
        if (_isAttacking)
        {
            if (_canRotate)
            {
                _rotationDirection = (_player.transform.position - transform.position).normalized;
                _rotationDirection.y = _constZero;
                _lookRotation = Quaternion.LookRotation(_rotationDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * _rotationSpeed);
            }
            _animator.SetBool("_attack", false);
            return;
        }
        if (_distanceToPlayer <= _rangeToAttack)
        {
            if (!_audioSource.clip != _deadSound)
            {
                if (_audioSource.clip != _attackSound)
                {
                    _audioSource.clip = _attackSound;
                }
                else
                {
                    if(!_audioSource.isPlaying)
                        _audioSource.Play();
                }
            }
            _animator.SetBool("_attack", true);
            transform.forward = new Vector3(_player.transform.position.x - transform.position.x, _constZero, _player.transform.position.z - transform.position.z);
            _isAttacking = true;
        }
    }
    private void TakeDamage()
    {
        if (_takingDamage)
            return;
        transform.forward = new Vector3(_player.transform.position.x - transform.position.x, _constZero, _player.transform.position.z - transform.position.z);
        _rigidBody.velocity = new Vector3(_constZero, _rigidBody.velocity.y, _constZero);
        _animator.SetBool("_running", false);
        _animator.SetBool("_attack", false);
        _animator.SetTrigger("_damaged");
        _takingDamage = true;
    }
    private IEnumerator Stun(float _stuntime)
    {
        if (_rigidBody.velocity.y > _constZero)
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, _constZero, _rigidBody.velocity.z);
        yield return new WaitForSeconds(_stuntime);
        _takingDamage = false;
        _liveObject._takingDamage = false;
        _distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
        if (_distanceToPlayer > _rangeToPursuit)
            _animator.SetTrigger("_idle");
        if (_distanceToPlayer <= _rangeToAttack)
        {
            _animator.SetBool("_attack", true);
            transform.forward = new Vector3(_player.transform.position.x - transform.position.x, _constZero, _player.transform.position.z - transform.position.z);
            _isAttacking = true;
        }
        if (_distanceToPlayer > _rangeToAttack && _distanceToPlayer <= _rangeToPursuit)
            _animator.SetBool("_running", true);
        _isAttacking = false;
    }
    private IEnumerator Dead()
    {
        _animator.SetBool("_running", false);
        _animator.SetBool("_attack", false);
        _animator.SetTrigger("_damaged");
        _rigidBody.isKinematic = true;
        _capsuleCollider.enabled = false;
        _animator.SetTrigger("_dead");
        yield return new WaitForSeconds(_corpseDuration);
        Destroy(gameObject);
    }
    private void Berserk()
    {
        if (!_liveObject._invulnerable)
        {
            if (_animator.GetFloat("_attackSpeedMultiplier") == _originalAttackSpeedMultiplier)
            {
                return;
            }
            else
            {
                _animator.SetFloat("_attackSpeedMultiplier", _originalAttackSpeedMultiplier);
                _speed = _originalSpeed;
                return;
            }
        }
        else
        {
            if(_auxiliarHitsRecived!=_liveObject._hitsRecived)
            {
                _animator.SetFloat("_attackSpeedMultiplier", _originalAttackSpeedMultiplier + _liveObject._hitsRecived*_attackSpeedIncrement);
                _speed += _speedIncrement;
                _auxiliarHitsRecived = _liveObject._hitsRecived;
            }
        }

    }

}

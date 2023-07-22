using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunctions : MonoBehaviour
{
    private const int _constZero = 0;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Rigidbody _rigidBody;
    [SerializeField]
    private float _movmentSpeed;
    [SerializeField]
    private float _sprintSpeed;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private JumpBoolSetter _jumpBoolSetter;
    [SerializeField]
    private float _jumpCooldDown=0.2f;
    [SerializeField]
    private Animator _animatorController;
    [SerializeField]
    private string _primaryAttack = "_attack";
    [SerializeField]
    private string _secondaryAttack = "_attack2";
    [SerializeField]
    private VaraState _varaState;
    [SerializeField]
    private PrimaryAttack _shoot;
    [SerializeField]
    private int _originalDamage;
    [SerializeField]
    private int _actualDamage;
    [SerializeField]
    private Transform _shootSpawnPoint;
    public float _attackCoolDown=1;
    [SerializeField]
    private AudioClip _attackSound;


    private bool _attackIsInCoolDown; 
    private float _currentAttackCoolDown;
    private bool _canJump;
    private float _jumpCoolDownTime;


    void Start()
    {
        if (_camera == null)
            _camera = GetComponentInChildren<Camera>();
        if (_rigidBody == null)
            _rigidBody = GetComponent<Rigidbody>();
        if (_jumpBoolSetter == null)
            _jumpBoolSetter = GetComponentInChildren<JumpBoolSetter>();
        if (_animatorController == null)
            _animatorController = GetComponentInChildren<Animator>();
        if (_varaState == null)
            _varaState = GetComponentInChildren<VaraState>();
        _actualDamage = _originalDamage;
       
    }

    void Update()
    {
        JumpCoolDown();
        AttackCoolDown();
    }
    public void Movment(float _x, float _z, bool _isSprinting)
    {
        if (_isSprinting)
        {
            _rigidBody.MovePosition(transform.position+((transform.forward*_z)+(transform.right*_x))*(_sprintSpeed*Time.deltaTime));
        }
        else
        {
            _rigidBody.MovePosition(transform.position+((transform.forward*_z)+(transform.right*_x))*(_movmentSpeed*Time.deltaTime));
        }
    }
    public void Rotation(float _rotY, Vector3 _localEulerCamera)
    {
        transform.Rotate(_constZero, _rotY, _constZero);
        _camera.transform.localEulerAngles = _localEulerCamera;
    }
    public void Jump()
    {
        if (_canJump)
        {
            if (_jumpBoolSetter._touchingFloor)
            {
                _rigidBody.AddForce(transform.up* _jumpForce,ForceMode.Impulse);
                _canJump = false;
            }
        }
    }
    private void JumpCoolDown() 
    {
        if (_canJump)
            return;
        _jumpCoolDownTime += Time.deltaTime;
        if (_jumpCoolDownTime > _jumpCooldDown)
        {
            _canJump = true;
        }
    }
    public void PrimaryAttack()
    {
        if (!_varaState._isShoting && !_attackIsInCoolDown)
        {

            _animatorController.SetTrigger(_primaryAttack);
            _varaState._isShoting = true;
            _attackIsInCoolDown = true;
            _shoot._damage = _actualDamage;
            Instantiate(_shoot, _shootSpawnPoint.position,_shootSpawnPoint.rotation);
        }
        else
        {
            return;
        }    
    }
    private void AttackCoolDown()
    {
        if (!_attackIsInCoolDown)
            return;
        _currentAttackCoolDown += Time.deltaTime;
        if (_currentAttackCoolDown > _attackCoolDown)
        {
            _currentAttackCoolDown = _constZero;
            _attackIsInCoolDown = false;
        }
    }
    public void SecondaryAttack()
    {
        if (!_varaState._isShoting)
        {
            _animatorController.SetTrigger(_secondaryAttack);
            _varaState._isShoting = true;
        }
        else
        {
            return;
        }
    }
    public IEnumerator DamageBuffed(int _damage, float _buffDuration)
    {
        
        _actualDamage = _damage;
        yield return new WaitForSeconds(_buffDuration);
        _actualDamage = _originalDamage;

    }
}

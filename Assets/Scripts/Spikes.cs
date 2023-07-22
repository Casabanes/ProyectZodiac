using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private int _layerPlayer=9;
    public bool _enableDisable;
    private void Start()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }
    private void Update()
    {
                _animator.SetBool("_attack",_enableDisable);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == _layerPlayer)
        {
            if (!_enableDisable)
            {
                _enableDisable = true;
                _animator.SetBool("_attack", _enableDisable);
            }
            else
            {
                _enableDisable = false;
                _animator.SetBool("_attack", _enableDisable);
            }
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer == _layerPlayer)
        {
                _enableDisable = true;
                _animator.SetBool("_attack", _enableDisable);
        }
    }
}

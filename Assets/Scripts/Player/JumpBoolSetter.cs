using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoolSetter : MonoBehaviour
{
    private int _tounchingObjectsCount;
    [SerializeField]
    private int _layerFloor=11;
    private const int _constZero = 0;
    public bool _touchingFloor;

    void Update()
    {
        if (_tounchingObjectsCount > _constZero)
        {
            _touchingFloor = true;
        }
        else
        {
            _touchingFloor = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==_layerFloor)
        {
            _tounchingObjectsCount++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _layerFloor)
        {
            _tounchingObjectsCount--;
        }
    }
}

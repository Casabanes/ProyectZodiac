using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    #region Variables
    public string rotationAxis = "Horizontal";
    public float rotationSpeed = 90f;
    public string movementAxis = "Vertical";
    public float movementSpeed = 2f;
    
    public Rigidbody myRigidBody;
    private float rotationInput;
    private float movementInput;
    #endregion 


    private void Awake()
    {
        if (myRigidBody == null)
        {
            myRigidBody = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        rotationInput = Input.GetAxis(rotationAxis);
        movementInput = Input.GetAxis(movementAxis);

    }

    private void FixedUpdate()
    {
        

        myRigidBody.MovePosition(transform.position + transform.forward * (movementSpeed * movementInput * Time.deltaTime));
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePosition : MonoBehaviour
{
    // Script para mover objetos desde una tecla
    //
    //Valores a cambiar
    //oldPosition; newPosition; oldRotation; newRotation;
    //
    //Opcional
    //ifLog; elseLog;
    #region Variables
    public KeyCode Key = KeyCode.Space;
    public Vector3 oldPosition;
    public Vector3 newPosition;

    public Quaternion oldRotation;
    public Quaternion newRotation;

    int counter = 0;
    public string ifLog;
    public string elseLog;
    #endregion
    void Update()
    {
        if (Input.GetKeyDown(Key) && counter == 0)
        {
            counter++;
            transform.localPosition = newPosition;
            transform.localRotation = newRotation;
            Debug.Log(ifLog);
        }
        else if (Input.GetKeyDown(Key) && counter > 0)
        {
            counter--;
            transform.localPosition = oldPosition;
            transform.localRotation = oldRotation;
            Debug.Log(elseLog);
        }
    }
}

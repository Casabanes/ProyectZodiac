using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialText : MonoBehaviour
{
    public int State;
    public Sprite[] imagenes;
    void Start()
    {
        State = 0;
    }

    
    void Update()
    {
        switch (State)
        {
            case 1:
                gameObject.GetComponent<Image>().sprite = imagenes[1];
                break;
            case 2:
                gameObject.GetComponent<Image>().sprite = imagenes[2];
                break;
            case 3:
                gameObject.GetComponent<Image>().sprite = imagenes[3];
                break;
            case 4:
                gameObject.GetComponent<Image>().sprite = imagenes[4];
                break;
        }

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            State = 1;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            State = 2;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            State = 3;
        }
        if (State == 3)
        {
            Destroy(gameObject, 2f);
        }
    }
}

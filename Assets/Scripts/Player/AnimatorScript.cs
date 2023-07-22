using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    public string varBool;
    public string varBool2;
    enemyTest enemytest;
    public raycastTest raycast;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gameObject.GetComponent<Animator>().SetBool(varBool, true);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            gameObject.GetComponent<Animator>().SetBool(varBool2, true);
        }
        else
        {
            EndAnimation();
        }
    }

    void EndAnimation()
    {
        gameObject.GetComponent<Animator>().SetBool(varBool2, false);
        gameObject.GetComponent<Animator>().SetBool(varBool, false);
    }    
 
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.GetComponent<Animator>().GetBool(varBool2) == true && other.gameObject.tag == "Enemy")
        {
            enemytest = other.gameObject.GetComponent<enemyTest>();
            enemytest.lifeCharacter = enemytest.lifeCharacter - 30;
            
        }
    }

    void Shoot()
    {
        raycast.isShooting = true;
    }
}

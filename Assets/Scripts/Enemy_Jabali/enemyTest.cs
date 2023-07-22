using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTest : MonoBehaviour
{
    public int lifeCharacter;
    public void Life(int rest)
    {
        lifeCharacter -= rest;
    }

    void Update()
    {
        if(lifeCharacter < 0)
        {
            Destroy(gameObject);
        }
    }
}

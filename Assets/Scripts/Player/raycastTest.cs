using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class raycastTest : MonoBehaviour
{
    //Script para cámara. Esto debido a que el transform.forward es mas preciso.
    #region Variables
    public string buttonFire = "Fire1";   //Boton de disparo
    public float _range; //Rango del Raycast
    public static RaycastHit hit;
    public int damange; //Daño para el enemigo
    public ParticleSystem particle1; //Partícula para impacto
    
    public Camera newCamera;
    public GameObject Bullet;
    public GameObject firePoint;
    public bool isShooting = false;
   
    #endregion
    void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        transform.rotation = newCamera.transform.rotation;
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(isShooting == true)
        {
            Shooting();
        }

    }

    void EndAnimation()
    {
        gameObject.GetComponent<Animator>().SetBool("Attack", false);
    }

    public void Shooting()
    {

        if (Physics.Raycast(newCamera.transform.position, newCamera.transform.forward, out hit, _range) && isShooting == true)
        {
            Instantiate(Bullet, firePoint.transform.position, firePoint.transform.rotation);
            isShooting = false;
        }
    }
}


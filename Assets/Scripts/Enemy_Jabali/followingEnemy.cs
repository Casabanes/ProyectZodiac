using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followingEnemy : MonoBehaviour
{
    public int speed;
    public GameObject player;
    public Transform PlayerLook;
    public GameObject Cube;
    public float _range;
    public bool State;
    
    private void Start()
    {
          
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 1.2f, 0), transform.forward, out hit, _range))
        {
            
            if (hit.transform.gameObject.tag == "Player")
            {
                State = true;
                gameObject.GetComponent<Animator>().SetInteger("State", 2);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && State == false)
        {
            gameObject.GetComponent<Animator>().SetInteger("State", 1);
            transform.position = Vector3.MoveTowards(transform.position, (player.transform.position), speed * Time.deltaTime);
            transform.LookAt(PlayerLook);
        }

    }
    void ActiveCube()
    {
        Cube.SetActive(true);
    }
    void UnactiveCube()
    {
        Cube.SetActive(false);
    }
    void EndAnimation()
    {
        State = false;
        gameObject.GetComponent<Animator>().SetInteger("State", 0);
        
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + new Vector3(0, 1.2f, 0), transform.forward * _range);
    }

}

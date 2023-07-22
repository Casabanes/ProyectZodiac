using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inventoryCollider : MonoBehaviour
{
    inventoryBehaviour inventory;
    //0 Hongo, 1 Flor, 2 Roca
    public float healt = 1000f;
    public int flowers;
    public int rocks;
    public int mushrooms;

    public int cristals; 

    public KeyCode key1;
    public KeyCode key2;
    public KeyCode key3;

    public GameObject Flor;
    public GameObject Roca;
    public GameObject Hongo;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<inventoryBehaviour>())
        {
            inventory = other.gameObject.GetComponent<inventoryBehaviour>();

            if (inventory.quantity != 4)
            {
                switch (inventory.identify)
                {
                    case 0:
                        mushrooms++;
                        //inventory.quantity++;
                        break;
                    case 1:
                        flowers++;
                        //inventory.quantity++;
                        break;
                    case 2:
                        rocks++;
                        //inventory.quantity++;
                        break;
                }
            }
        }

    }

    private void Update()
    {
        Flor.GetComponent<inventoryBehaviour>().quantity = flowers;
        Roca.GetComponent<inventoryBehaviour>().quantity = rocks;
        Hongo.GetComponent<inventoryBehaviour>().quantity = mushrooms;

        if (Input.GetKeyDown(key1))
        {
            Crafting1();
        }
        if (Input.GetKeyDown(key2))
        {
            Crafting2();
        }
        if (Input.GetKeyDown(key3))
        {
            Crafting3();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && flowers != 4)
        {
            flowers++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && mushrooms != 4)
        {
            mushrooms++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && rocks != 4)
        {
            rocks++;
        }

        if(healt < 0)
        {
            SceneManager.LoadScene("Game");
        }
        if (cristals == 5)
        {
            SceneManager.LoadScene("Victory");
        }

        
    }


        #region Crafteos
        void Crafting1()
        {
            if (flowers >= 2 && rocks >= 1)
            {
                flowers = flowers - 2;
                rocks--;
                Flor.GetComponent<inventoryBehaviour>().Rest(flowers);
                Roca.GetComponent<inventoryBehaviour>().Rest(rocks);
            }
        }
        void Crafting2()
        {
            if (flowers == 4 && rocks == 4 && mushrooms == 4)
            {
                flowers = flowers - 4;
                rocks = rocks - 4;
                mushrooms = mushrooms - 4;

                Flor.GetComponent<inventoryBehaviour>().Rest(flowers);
                Roca.GetComponent<inventoryBehaviour>().Rest(rocks);
                Hongo.GetComponent<inventoryBehaviour>().Rest(mushrooms);
            }
        }
        void Crafting3()
        {
            if (flowers >= 1 && rocks >= 4 && mushrooms >= 2)
            {
                flowers--;
                rocks = rocks - 4;
                mushrooms = mushrooms - 2;
                Flor.GetComponent<inventoryBehaviour>().Rest(flowers);
                Roca.GetComponent<inventoryBehaviour>().Rest(rocks);
                Hongo.GetComponent<inventoryBehaviour>().Rest(mushrooms);
            }
        }
    #endregion


}

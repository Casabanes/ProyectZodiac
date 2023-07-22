using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryBehaviour : MonoBehaviour
{
    public int identify; //0 Hongo, 1 Flor, 2 Roca
    public int quantity;

    public Sprite[] imagenes;

    void Update()
    {
        gameObject.GetComponent<Image>().sprite = imagenes[quantity];

    }

    public void Rest(int minus)
    {
        quantity = quantity - minus;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healt : MonoBehaviour
{
    public Image HealtBar;
    public float CurrentHealt;
    private float MaxHealt = 100f;
    public inventoryCollider Player;
    void Start()
    {
        HealtBar = GetComponent<Image>();
        Player = FindObjectOfType<inventoryCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealt = Player.healt;
        HealtBar.fillAmount = CurrentHealt / MaxHealt;
    }
}

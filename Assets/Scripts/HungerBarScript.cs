using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBarScript : MonoBehaviour
{
    private Image hungerBar;
    private float maxHunger = 100f;
    private float currentHunger;
    PlayerLogic Player;

    private void Start()
    {
        hungerBar = GetComponent<Image>();
        Player = FindObjectOfType<PlayerLogic>();
    }

    private void Update()
    {
        currentHunger = Player.hungerLevel;
        hungerBar.fillAmount = currentHunger / maxHunger;
    }
}


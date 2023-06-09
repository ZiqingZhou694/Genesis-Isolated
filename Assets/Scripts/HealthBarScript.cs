using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private Image healthBar;
    private float maxHealth = 100f;
    private float currentHealth;
    PlayerLogic Player;

    private void Start()
    {
        healthBar = GetComponent<Image>();
        Player = FindObjectOfType<PlayerLogic>();
    }

    private void Update()
    {
        currentHealth = Player.health;
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}

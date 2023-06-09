using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirstBarScript : MonoBehaviour
{
    private Image thirstBar;
    private float maxThirst = 100f;
    private float currentThirst;
    PlayerLogic Player;
    public bool useWater;
    private void Start()
    {
        thirstBar = GetComponent<Image>();
        Player = FindObjectOfType<PlayerLogic>();
        useWater = false;
    }

    private void Update()
    {
            currentThirst = Player.thirstLevel;
            thirstBar.fillAmount = currentThirst / maxThirst;

    }
}


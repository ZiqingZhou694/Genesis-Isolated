using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechDepBarScript : MonoBehaviour
{
    private Image techDepBar;
    private float maxTechDep = 1000f;
    private float currentTechDep;
    PlayerLogic Player;


    // Start is called before the first frame update
    void Start()
    {
        techDepBar = GetComponent<Image>();
        Player = FindObjectOfType<PlayerLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTechDep = Player.techDepLevel;
        techDepBar.fillAmount = currentTechDep / maxTechDep;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    public float health;
    public float thirstLevel;
    public float hungerLevel;
    public float techDepLevel;
    public float timeToFade;

    private bool techBarActive;
    private bool fadeStart = false;

    public GameObject techDepBar;
    [SerializeField] private CanvasGroup canvasGroup;
    


    private void Start()
    {
        health = 100f;
        thirstLevel = 100f;
        hungerLevel = 100f;
        techDepLevel = 1000f;
        timeToFade = 0.5f;
        techBarActive = false;
    }
    private void Update()
    {
        // Decrease thirst and hunger level with time
        thirstLevel = thirstLevel - (float)0.1 * Time.deltaTime;
        hungerLevel = hungerLevel - (float)0.06 * Time.deltaTime;
        techDepLevel = techDepLevel + (float)0.05 * Time.deltaTime;

        // Showing the techDepBar and fade it away
        if (techBarActive)
        {
            techDepBar.SetActive(true);
        }

        if (fadeStart)
        {
            canvasGroup.alpha = canvasGroup.alpha - timeToFade * Time.deltaTime;
        }

        if (canvasGroup.alpha <= 0)
        {
            techBarActive = false;
            fadeStart = false;
            canvasGroup.alpha = 1;
            techDepBar.SetActive(false);
        }

    }

    public void DecreaseTechDepLevel(float num)
    {
        techDepLevel = techDepLevel - num;
        StartCoroutine(DisplayTime());
    }

    IEnumerator DisplayTime()
    {
        techBarActive = true;
        yield return new WaitForSeconds(5);
        fadeStart = true;
    }

    public void UseWaterBottle()
    {
        thirstLevel = 100f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaterTimer : MonoBehaviour
{
    public float timeLeft = 60.0f; // The amount of time in seconds
    private GameObject camManager;
    public TextMeshProUGUI time;
    public GameObject player;
    public bool isRunning = false;
    public GameObject canvas;
    public GameObject questMan;
    public TextMeshProUGUI cmt;
    public PlayerLogic logic;
    private float waterEXP = 200;
    public void Start()
    {
        logic = FindObjectOfType<PlayerLogic>();
    }
    void Update()
    {
        if (isRunning)
        {
            canvas.SetActive(true);
            time.text = string.Format("{00}", Mathf.RoundToInt(timeLeft));
            timeLeft -= Time.deltaTime; // Subtract the elapsed time since the last frame
            if (Mathf.RoundToInt(timeLeft) == 30f)
            {
                cmt.text = "Elon: This would be so much faster with a water filter";
                StartCoroutine(Wait());
            }

            if (timeLeft <= 0.0f)
            {
                camManager = GameObject.Find("CameraManager");
                camManager.SendMessage("SwitchToWaterCam", false);
                player.GetComponent<InventoryInit>().inventory.AddItem(new Item { itemType = Item.ItemType.WaterFilled, amount = 1 });
                player.GetComponent<PlayerLogic>().DecreaseTechDepLevel(200);
                canvas.SetActive(false);
                enabled = false; // Disable the script
                questMan.GetComponentInChildren<QuestManager>().CompleteQuest();
                logic.DecreaseTechDepLevel(waterEXP);
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(3);
        cmt.text = "";
    }
}

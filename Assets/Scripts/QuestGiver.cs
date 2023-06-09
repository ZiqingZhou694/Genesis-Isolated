using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public bool isFireQuestActive = false;
    public bool fireComplete = false;
    public bool fireStarterMade = false;
    public GameObject questWindow;
    private Inventory inventory;
    public Text sticksText;
    public Text woodsText;
    public Text rocksText;
    public Text dryGrassText;
    public Text fireStarterText;
    public Text fireText;
    int requiredSticks = 20;
    int requiredWoods = 11;
    int requiredRocks = 5;
    int requiredDryGrass = 10;

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        MissonProgress();
    }

    private void Update()
    {
        if (fireStarterMade)
        {
            fireStarterText.color = Color.yellow;
            sticksText.text = "Collect 20 Sticks";
            sticksText.color = Color.yellow;
            woodsText.text = "Collect 11 Woods";
            woodsText.color = Color.yellow;
            rocksText.text = "Collect 5 Rocks";
            rocksText.color = Color.yellow;
            dryGrassText.text = "Collect 10 Dry Grass";
            dryGrassText.color = Color.yellow;
        }

        if (fireComplete)
        {
            fireText.color = Color.yellow;
            StartCoroutine(Delay());
        }
    }

    public void MissonProgress()
    {
        TasksRefresh();

        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.Stick)
            {
                sticksText.text = string.Format("{0}/{1} Sticks Collected", item.amount, requiredSticks);
                IsTaskComplete(item.amount, requiredSticks, sticksText);
            }
            else if (item.itemType == Item.ItemType.WoodLog)
            {
                woodsText.text = string.Format("{0}/{1} Wood Collected", item.amount, requiredWoods);
                IsTaskComplete(item.amount, requiredWoods, woodsText);
            }
            else if (item.itemType == Item.ItemType.Rock)
            {
                rocksText.text = string.Format("{0}/{1} Rocks Collected", item.amount, requiredRocks);
                IsTaskComplete(item.amount, requiredRocks, rocksText);
            }
            else if (item.itemType == Item.ItemType.Drygrass)
            {
                dryGrassText.text = string.Format("{0}/{1} Dry Grass Collected", item.amount, requiredDryGrass);
                IsTaskComplete(item.amount, requiredDryGrass, dryGrassText);
            }
        }

    }

    private void IsTaskComplete(int curAmt, int reqAmt, Text text)
    {
        if (curAmt >= reqAmt)
            text.color = Color.yellow;
    }

    private void TasksRefresh()
    {
        sticksText.text = string.Format("0/{0} Sticks Collected", requiredSticks);
        sticksText.color = Color.white;
        woodsText.text = string.Format("0/{0} Woods Collected", requiredWoods);
        woodsText.color = Color.white;
        rocksText.text = string.Format("0/{0} Rocks Collected", requiredRocks);
        rocksText.color = Color.white;
        dryGrassText.text = string.Format("0/{0} Dry Grass Collected", requiredDryGrass);
        dryGrassText.color = Color.white;
        fireStarterText.color = Color.white;
    }

    public void OpenQuestWindow()
    {
        isFireQuestActive = true;
        questWindow.SetActive(true);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(10);
        questWindow.SetActive(false);
    }
}

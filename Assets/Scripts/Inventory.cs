using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    private List<Item> itemList;
    public List<Item> selectedList = new List<Item>();
    public GameObject camManager;
    public QuestGiver quest;

    public Inventory()
    {
        itemList = new List<Item>();
        AddItem(new Item { itemType = Item.ItemType.PocketKnife, amount = 1 });
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach(Item inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if(!itemAlreadyInInventory)
                {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        Debug.Log(itemList.Count);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <=0 )
            {
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            itemList.Remove(item);
        }
        Debug.Log(itemList.Count);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }

    public List<Item> GetSelectList()
    {
        return selectedList;
    }

    public bool ToggleSelectedItem(Item item)
    {
        bool selected = false;
        if (selectedList.Contains(item))
        {
            selectedList.Remove(item);
            selected = false;
            Debug.Log("NOT in selected list");
        }
        else
        {
            selectedList.Add(item);
            selected = true;
            Debug.Log("In selected list");
        }
        return selected;
    }

    public Dictionary<string, List<KeyValuePair<string, int>>> validCombinations = new Dictionary<string, List<KeyValuePair<string, int>>>()
    {
        {"StickWoodLogRock", new List<KeyValuePair<string, int>>(){
                new KeyValuePair<string, int>("Stick", 20),
                new KeyValuePair<string, int>("WoodLog", 10),
                new KeyValuePair<string, int>("Rock", 5)
            }
        },
        //this combination becomes fire
        {"WoodLogDrygrassWoodpile", new List<KeyValuePair<string, int>>(){
                new KeyValuePair<string, int>("WoodLog", 1),
                new KeyValuePair<string, int>("Drygrass", 5),
                new KeyValuePair<string, int>("Woodpile", 1)
            }
        },
        {"WoodLogWaterFilled", new List<KeyValuePair<string, int>>(){
                new KeyValuePair<string, int>("WoodLog", 3),
                new KeyValuePair<string, int>("WaterFilled", 1)
            }
        },
        //this combination start the water bottle animation
        {"WaterEmptyWaterEmpty", new List<KeyValuePair<string, int>>(){ 
                new KeyValuePair<string, int>("WaterEmpty", 1),
                new KeyValuePair<string, int>("WaterEmpty", 1)
            } 
        },
        {"WoodLogPocketKnife", new List<KeyValuePair<string, int>>(){
                new KeyValuePair<string, int>("WoodLog", 1),
                new KeyValuePair<string, int>("PocketKnife", 1)
            }
        },
        //firestarter
        {"WoodplankStickDrygrass", new List<KeyValuePair<string, int>>(){
                new KeyValuePair<string, int>("Woodplank", 1),
                new KeyValuePair<string, int>("Stick", 1),
                new KeyValuePair<string, int>("Drygrass", 3)
            }
        }
    };

    public Dictionary<string, Item.ItemType> combinationsItem = new Dictionary<string, Item.ItemType>() {
        {"StickWoodLogRock", Item.ItemType.Woodpile},
        {"WoodLogWaterFilled", Item.ItemType.Axe},
    };

    public void CombineItems(List<Item> itemsToCombine)
    {
        string key = "";
        List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
        foreach (Item item in itemsToCombine)
        {
            key += item.itemType;
        }

        if (validCombinations.TryGetValue(key, out result))
        {
            foreach (KeyValuePair<string, int> comb in result)
            {
                for (int i = selectedList.Count - 1; i >= 0; i--)
                {
                    Item item = selectedList[i];
                    if (selectedList[i].itemType.ToString() == comb.Key)
                    {
                        selectedList.RemoveAt(i);
                        if (item.amount > comb.Value)
                        {
                            item.amount -= comb.Value;
                        }
                        else
                        {
                            RemoveItem(item);
                        }
                    }
                }
            }
        }

        if (key == "WoodplankStickDrygrass")
        {
            camManager = GameObject.Find("CameraManager");
            quest = GameObject.FindGameObjectWithTag("questGiver").GetComponent<QuestGiver>();
            quest.fireStarterMade = true;
            camManager.SendMessage("SwitchToFireStarterCam", true);
        }
        else if(key == "WaterEmptyWaterEmpty")
        {
            camManager = GameObject.Find("CameraManager");
            camManager.SendMessage("SwitchToWaterCam", true);
            camManager.GetComponentInChildren<WaterTimer>().isRunning = true;
        }
        else if(key == "WoodLogPocketKnife")
        {
            AddItem(new Item { itemType = Item.ItemType.Woodplank, amount = 1 });
        }

        Item.ItemType combinedItem;
        if(combinationsItem.TryGetValue(key, out combinedItem))
        {
            AddItem(new Item { itemType = combinedItem, amount = 1 }); ;
        }
        else
        {
            Debug.Log("Wrong Combination");
        }

    }
}

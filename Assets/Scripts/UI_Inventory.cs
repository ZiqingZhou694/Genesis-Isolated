using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    public GameObject select_button;
    bool isSelectModeOn = false;
    [SerializeField] private Terrain terrain;
    Vector3 original_pos = new Vector3(52, 870, 0);
    Vector3 hiding_pos = new Vector3(-100, 200, 0);
 
    [SerializeField] private GameObject player;
    QuestGiver questGiver;
    private PlayerCollectOnCollider collector;
    private EyePointer pointer;
    private void Start()
    {
        collector = FindObjectOfType<PlayerCollectOnCollider>();
        gameObject.transform.position = hiding_pos;
        itemSlotContainer = transform.Find("item-slot-container");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        questGiver = FindObjectOfType<QuestGiver>();
        collector.SetInventory(inventory);
        pointer = FindObjectOfType<EyePointer>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            gameObject.transform.position = original_pos;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void OnCloseButtonClick()
    {
        gameObject.transform.position = hiding_pos;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    public void OnSelectButton()
    {
        if (isSelectModeOn)
        {
            isSelectModeOn = false;
            select_button.GetComponent<Image>().color = Color.white;
        }
        else
        {
            isSelectModeOn = true;
            select_button.GetComponent<Image>().color = Color.red;
        }
    }

    public void OnCombineItems()
    {
        inventory.CombineItems(inventory.GetSelectList());
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 12f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.GetComponent<Button>().onClick.AddListener(() => {
                if(!isSelectModeOn)
                {
                    Debug.Log("DROPPING ITEM");
                    Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                    inventory.RemoveItem(item);
                    Vector3 spawn_pos = new Vector3(player.transform.position.x, 0f, player.transform.position.z + 5);
                    spawn_pos.y = terrain.SampleHeight(spawn_pos) + terrain.transform.position.y;
                    if(item.itemType == Item.ItemType.WaterFilled)
                    {
                        player.GetComponentInChildren<PlayerLogic>().UseWaterBottle();
                    } else
                    {
                        duplicateItem.GetPrefab().GetComponent<CollectableItem>().DropItem(spawn_pos, duplicateItem);
                    }
                }
                else {
                    if (inventory.ToggleSelectedItem(item))
                    {
                        itemSlotRectTransform.Find("selected-ui").gameObject.SetActive(true);
                    }
                    else
                    {
                        itemSlotRectTransform.Find("selected-ui").gameObject.SetActive(false);
                    }
                }
            });
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI textUI = itemSlotRectTransform.Find("amount").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                textUI.SetText(item.amount.ToString());
            }
            else
            {
                textUI.SetText("");
            }

            x++;
            if (x > 0)
            {
                x = 0;
                y--;
            }
        }
        questGiver.SetInventory(inventory);
        pointer.SetInventory(inventory);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollectOnCollider : MonoBehaviour
{
    public TextMeshProUGUI display;
    private Inventory inventory;
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "axe")
        {
            display.text = "Collect?";
            if (Input.GetMouseButtonDown(0))
            {
                inventory.AddItem(new Item { itemType = Item.ItemType.Axe, amount = 1 });
                collision.gameObject.GetComponent<CollectableItem>().DestroySelf();
            }
        }
        else if(collision.gameObject.tag == "pickaxe")
        {
            display.text = "Collect?";
            if (Input.GetMouseButtonDown(0))
            {
                inventory.AddItem(new Item { itemType = Item.ItemType.PickAxe, amount = 1 });
                collision.gameObject.GetComponent<CollectableItem>().DestroySelf();
            }
        }
        else
        {
            display.text = "";
        }
    }
}

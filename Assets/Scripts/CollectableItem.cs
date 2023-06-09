using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public CollectableItem SpawnItemWorld(Vector3 position, Item item)
    {
        GameObject child = Instantiate(this.gameObject, position, this.gameObject.transform.rotation);
        CollectableItem itemWorld = child.transform.GetComponent<CollectableItem>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public CollectableItem DropItem(Vector3 dropPosition, Item item)
    {
        CollectableItem itemWord = SpawnItemWorld(dropPosition, item);
        return itemWord;
    }

    private Item item;
    public void SetItem(Item item)
    {
        this.item = item;
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

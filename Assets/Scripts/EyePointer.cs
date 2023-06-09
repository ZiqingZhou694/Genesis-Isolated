using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EyePointer : MonoBehaviour
{
    private const float _maxDistance = 5;
    GameObject _selected = null;
    public TextMeshProUGUI display;
    int chop_count = 3;
    public GameObject log;
    public Terrain terrain;
    public GameObject questManager;
    public GameObject root;

    PlayerLogic logic;
    private float chopEXP = 10f;
    private float collectEXP = 5f;

    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        logic = FindObjectOfType<PlayerLogic>();
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            if (hit.collider.tag == "tree")
            {
                display.text = "Chop Down Tree? [C]";
                if (Input.GetKeyDown(KeyCode.C))
                {
                    chop_count--;
                    if (chop_count == 0)
                    {
                        _selected = hit.transform.gameObject;
                        Vector3 spawn_position = new Vector3(_selected.transform.position.x, 0 , _selected.transform.position.z);
                        spawn_position.y = terrain.SampleHeight(spawn_position) + terrain.transform.position.y;
                        _selected.SetActive(false);
                        log.GetComponent<CollectableItem>().SpawnItemWorld(spawn_position, new Item { itemType = Item.ItemType.WoodLog, amount = 2 });
                        chop_count = 3;
                        display.text = "";
                        logic.DecreaseTechDepLevel(chopEXP);
                    }
                }
            }
            else if(hit.collider.tag == "log"){
                display.text = "Collect Wood Log? [C]";
                if (Input.GetKeyDown(KeyCode.C))
                {
                    _selected = hit.transform.gameObject;
                    inventory.AddItem(_selected.GetComponent<CollectableItem>().GetItem());
                    _selected.GetComponent<CollectableItem>().DestroySelf();
                    logic.DecreaseTechDepLevel(collectEXP);
                }
            }
            else if(hit.collider.tag == "axe" || hit.collider.tag == "pickaxe")
            {
                display.text = "Collect Axe? [C]";
                if (Input.GetKeyDown(KeyCode.C))
                {
                    _selected = hit.transform.gameObject;
                    inventory.AddItem(new Item { itemType = Item.ItemType.Axe, amount = 1});
                    questManager.GetComponent<QuestManager>().CompleteQuest();
                    root.GetComponent<DialogueManager>().dialogbuttonInit(0);
                    _selected.GetComponent<CollectableItem>().DestroySelf();
                    logic.DecreaseTechDepLevel(collectEXP);
                }
            }
            else if (hit.collider.tag == "bottle")
            {
                display.text = "Collect Empty Bottle? [C]";
                if (Input.GetKeyDown(KeyCode.C))
                {
                    _selected = hit.transform.gameObject;
                    inventory.AddItem(new Item { itemType = Item.ItemType.WaterEmpty, amount = 1 });
                    _selected.GetComponent<CollectableItem>().DestroySelf();
                    logic.DecreaseTechDepLevel(collectEXP);
                }
            }
            else if (hit.collider.tag == "drygrass")
            {
                display.text = "Collect Drygrass? [C]";
                if (Input.GetKeyDown(KeyCode.C))
                {
                    _selected = hit.transform.gameObject;
                    inventory.AddItem(new Item { itemType = Item.ItemType.Drygrass, amount = 5 });
                    _selected.GetComponent<CollectableItem>().DestroySelf();
                    logic.DecreaseTechDepLevel(collectEXP);
                }
            }
            else if (hit.collider.tag == "Stick")
            {
                display.text = "Collect Wood Stick? [C]";
                if (Input.GetKeyDown(KeyCode.C))
                {
                    _selected = hit.transform.gameObject;
                    inventory.AddItem(new Item { itemType = Item.ItemType.Stick, amount = 5 });
                    _selected.GetComponent<CollectableItem>().DestroySelf();
                    logic.DecreaseTechDepLevel(collectEXP);
                }
            }
            else if (hit.collider.tag == "rock")
            {
                display.text = "Collect Rock? [C]";
                if (Input.GetKeyDown(KeyCode.C))
                {
                    _selected = hit.transform.gameObject;
                    inventory.AddItem(new Item { itemType = Item.ItemType.Rock, amount = 1 });
                    _selected.GetComponent<CollectableItem>().DestroySelf();
                    logic.DecreaseTechDepLevel(collectEXP);
                }
            }
            else
            {
                display.text = "";
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    public void Start()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite woodlog;
    public Sprite waterEmpty;
    public Sprite waterFilled;
    public Sprite axe;
    public Sprite pickAxe;
    public Sprite longWood;
    public Sprite dryGrass;
    public Sprite pocketKnife;
    public Sprite rock;
    public Sprite woodStick;
    public Sprite woodpile;
    public Sprite woodplank;


    public GameObject log_prefab;
    public GameObject waterEmpty_prefab;
    public GameObject waterFilled_prefab;
    public GameObject pickAxe_prefab;
    public GameObject longWood_prefab;
    public GameObject dryGrass_prefab;
    public GameObject rock_prefab;
    public GameObject woodStick_prefab;
    public GameObject woodpile_prefab;
    public GameObject axe_prefab;
    public GameObject pickaxe;
    public GameObject spear;
    public GameObject woodplank_prefab;
}

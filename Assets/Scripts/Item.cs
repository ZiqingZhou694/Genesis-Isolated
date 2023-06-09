using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        WoodLog,
        WaterEmpty,
        WaterFilled,
        Axe,
        PickAxe,
        Stick,
        LongWood,
        Drygrass,
        Woodpile,
        Rock,
        PocketKnife,
        Spear,
        Woodplank
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.WoodLog:      return ItemAssets.Instance.woodlog;
            case ItemType.WaterEmpty:    return ItemAssets.Instance.waterEmpty;
            case ItemType.WaterFilled: return ItemAssets.Instance.waterFilled;
            case ItemType.Axe:      return ItemAssets.Instance.axe;
            case ItemType.PickAxe: return ItemAssets.Instance.pickAxe;
            case ItemType.Stick: return ItemAssets.Instance.woodStick;
            case ItemType.LongWood: return ItemAssets.Instance.longWood;
            case ItemType.Drygrass: return ItemAssets.Instance.dryGrass;
            case ItemType.Woodpile: return ItemAssets.Instance.woodpile;
            case ItemType.Rock: return ItemAssets.Instance.rock;
            case ItemType.PocketKnife: return ItemAssets.Instance.pocketKnife;
            case ItemType.Woodplank: return ItemAssets.Instance.woodplank;
        }
    }

    public GameObject GetPrefab()
    {
        switch (itemType)
        {
            default:
            case ItemType.WoodLog: return ItemAssets.Instance.log_prefab;
            case ItemType.Axe: return ItemAssets.Instance.axe_prefab;
            case ItemType.PickAxe: return ItemAssets.Instance.pickAxe_prefab;
            case ItemType.Stick: return ItemAssets.Instance.woodStick_prefab;
            case ItemType.Drygrass: return ItemAssets.Instance.dryGrass_prefab;
            case ItemType.Woodpile: return ItemAssets.Instance.woodpile_prefab;
            case ItemType.Rock: return ItemAssets.Instance.rock_prefab;
            case ItemType.Woodplank: return ItemAssets.Instance.woodplank_prefab;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default: return false;
            case ItemType.WoodLog: return true;
            case ItemType.Stick: return true;
            case ItemType.Drygrass: return true;
            case ItemType.Rock: return true;
            case ItemType.Woodplank: return true;
        }
    }
}

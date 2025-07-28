using UnityEngine;
using System;

public class BItem : Item
{
    [SerializeField] private GameObject combinedItemPrefab;
    
    public override bool CanPickup(GameObject itemObject)
    {
        Item otherItem = itemObject.GetComponent<Item>();
        if (otherItem == null) return false;

        return Array.Exists(canInteractItemList, obj => obj == itemObject);
    }
    public override GameObject CreateCombinedItem(GameObject item_1, GameObject item_2){
        //TO-DO complete function
        return null;
    }
}

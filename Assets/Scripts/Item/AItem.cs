using UnityEngine;
using System;

public class AItem : Item
{
    public override GameObject GetCombinedItem(GameObject itemObject){
        if (itemObject.GetComponent<Item>().itemName ==
             canInteractItemList[0].GetComponent<Item>().itemName) return combinedItemPrefabList[0];
        return null;
    }
}

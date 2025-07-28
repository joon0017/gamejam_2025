using UnityEngine;
using System;

public class AItem : Item
{
    public override GameObject GetCombinedItem(GameObject itemObject){
        if (itemObject == canInteractItemList[0]) return combinedItemPrefabList[0];
        return null;
    }
}

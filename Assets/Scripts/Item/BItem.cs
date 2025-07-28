using UnityEngine;
using System;

public class BItem : Item
{    
     public override GameObject GetCombinedItem(GameObject itemObject){
        if (itemObject == canInteractItemList[0]) return combinedItemPrefabList[0];
        else if (itemObject == canInteractItemList[1]) return combinedItemPrefabList[0];
        //etc..
        return null;
    }
}

using UnityEngine;
using System;

public class DItem : Item
{    
    [SerializeField] private GameObject doorObject;

    public override GameObject GetCombinedItem(GameObject itemObject)
    {
        throw new NotImplementedException("DItem does not support combination.");
    }

    public override void ItemUniqueEffect()
    {
        throw new NotImplementedException("Not implemented");
    }


    public void OpenDoorFunction(){
        Destroy(doorObject);
        Debug.Log("Removed Wall");
    }
}

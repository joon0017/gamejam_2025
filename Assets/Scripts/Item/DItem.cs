using UnityEngine;
using System;

public class DItem : Item
{    
    [SerializeField] private GameObject doorObject;

    public override GameObject GetCombinedItem(GameObject itemObject)
    {
        throw new NotImplementedException("DItem does not support combination.");
    }
    
    public override void ItemUniqueEffect(){
        doorObject.GetComponent<Door>().openDoor();
        Debug.Log("Destroy door obstacle.");
    }

    //must first call this function before ItemUniqueEffect (to set which door to remove/unlock)
    public void setUniqueItem(GameObject door){
        this.doorObject = door;
    }
}

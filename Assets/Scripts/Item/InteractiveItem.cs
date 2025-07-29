using UnityEngine;
using System;

public class InteractiveItem : Item
{    
    [SerializeField] private GameObject interactingObject;

    public override GameObject GetCombinedItem(GameObject itemObject)
    {
        throw new NotImplementedException("DItem does not support combination.");
    }
    
    public override void ItemUniqueEffect(){
        interactingObject.GetComponent<Door>().openDoor();
        Debug.Log("Destroy door obstacle.");
    }

    //must first call this function before ItemUniqueEffect (to set which door to remove/unlock)
    public void SetUniqueItem(GameObject obj){
        this.interactingObject = obj;
    }
}

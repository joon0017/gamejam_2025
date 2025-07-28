using UnityEngine;
using System;

public abstract class Item : MonoBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] protected GameObject itemObjectPrefab;
    // [SerializeField] protected GameObject[] cannotInteractItemList; // no need
    [SerializeField] protected GameObject[] canInteractItemList;    
    [SerializeField] protected GameObject[] combinedItemPrefabList;

/*
Function for picking up Items in general. This function does not
check legitimacy.
*/
    public void PickupItem(GameObject player){
        //set this item's parent to the player
        gameObject.transform.SetParent(player.transform);
        gameObject.transform.localPosition = new Vector3(-1,1,0);
        gameObject.tag = "Untagged";

        //remove collider
        SphereCollider col = gameObject.GetComponent<SphereCollider>();
        if (col != null)
        {
            Destroy(col);
        }
    }
    public bool CanPickup(GameObject itemObject)
    {
        Debug.Log(itemObject);
        if(Array.Exists(canInteractItemList, obj => obj.GetComponent<Item>().itemName == itemObject.GetComponent<Item>().itemName)){
            Debug.Log("Pickedup item");
            return true;
        }
        Debug.Log("cannot pickup item");
        return false;
    }

    public GameObject GetItemObjectPrefab(){
        return itemObjectPrefab;
    }

    public abstract GameObject GetCombinedItem(GameObject itemObject);
} 
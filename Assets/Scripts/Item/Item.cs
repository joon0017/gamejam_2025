using UnityEngine;
using System;

public abstract class Item : MonoBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] protected GameObject itemObjectPrefab;
    // [SerializeField] protected GameObject[] cannotInteractItemList; // no need
    [SerializeField] protected GameObject[] canInteractItemList;    
    [SerializeField] protected GameObject[] combinedItemPrefabList;
    [SerializeField] protected GameObject lightComponent;
    [SerializeField] protected GameObject sparkleEffect;

/*
Function for picking up Items in general. This function does not
check legitimacy.
*/
    public void PickupItem(GameObject player){
        //set this item's parent to the player
        gameObject.transform.SetParent(player.transform);
        gameObject.transform.localPosition = new Vector3(-1,1,0);
        gameObject.tag = "Untagged";

        //if the item has a light component, disable it
        if (lightComponent != null)
        {
            lightComponent.SetActive(false);
        }

        //if the item has a sparkle effect, disable it
        if (sparkleEffect != null)
        {
            sparkleEffect.SetActive(false);
        }

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
    public abstract void ItemUniqueEffect();
} 
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField]public string itemName;
    // [SerializeField] protected GameObject[] cannotInteractItemList; // no need
    [SerializeField] protected GameObject[] canInteractItemList;

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
    public abstract bool CanPickup(GameObject itemObject);

    public abstract GameObject CreateCombinedItem(GameObject item_1, GameObject item_2);
} 
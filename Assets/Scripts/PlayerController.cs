using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private GameObject currentItem;
    private GameObject nearbyItem = null;   // item object를 저장할 변수
    private GameObject nearbyObstacle = null;   // 벽 접촉 여부를 판별해줄 변수 


    void Update()
    {
        Move();
        Interact();
    }

    private void Move(){
        rigid.linearVelocity = new Vector3(InputManager.Instance.move.x*speed,0,InputManager.Instance.move.y*speed);
    }

    private void Interact()
    {
        if (InputManager.Instance.interact)
        {
            InputManager.Instance.interact = false;
            // 1. 아이템 상호작용
            if (nearbyItem)
            {
                string itemName = nearbyItem.GetComponent<Item>().itemName;
                Debug.Log("상호작용: " + itemName);
                if (CanPickup(nearbyItem))
                {
                    if (currentItem)
                    {
                        //remove current items and replace with new item
                        GameObject newItem = Instantiate(currentItem.GetComponent<Item>().GetCombinedItem(nearbyItem)) as GameObject;
                        Destroy(currentItem);
                        Destroy(nearbyItem);
                        newItem.GetComponent<Item>().PickupItem(gameObject);
                        SetCurrentItem(newItem);
                        Debug.Log("new item created");
                    }
                    else
                    {
                        nearbyItem.GetComponent<Item>().PickupItem(gameObject);
                        SetCurrentItem(nearbyItem);
                        Debug.Log("아이템 줍기 성공: " + itemName);
                    }

                    nearbyItem = null;
                }
                else
                {
                    Debug.Log("조건이 맞지 않아 " + itemName + "을(를) 주울 수 없음");
                }
            }

            // 2. 벽 상호작용
            // 주변에 벽이 있고 현 상태가 D일 경우 
            if (nearbyObstacle && currentItem != null)
            {
                // Debug.Log("벽이랑 상호작용 시작");
                if (currentItem.GetComponent<Item>().itemName == "D")
                {
                    currentItem.GetComponent<DItem>().setUniqueItem(nearbyObstacle);
                    currentItem.GetComponent<DItem>().ItemUniqueEffect();
                    Destroy(currentItem);
                }
                // Debug.Log("벽이랑 상호작용 끝");
            }
            else
            {
                Debug.Log("Cannot pass door because player is not holding D item");
            }
        }
    }

/*
Check if it is possible to pick up the item in parameter.
if the player is not holding an item, the default return value is true. 
The player is able to pickup anything if not holding anything.
*/
    private bool CanPickup(GameObject item)
    {
        if(currentItem) {
            Debug.Log("Currently holding an item... check if can pick up another");
            return currentItem.GetComponent<Item>().CanPickup(item);
        }
        
        return true;
    }

    private void SetCurrentItem(GameObject item){
        currentItem = item;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            nearbyItem = other.gameObject;
            // Debug.Log("감지 시작: " + other.name);
        }

        if (other.CompareTag("Obstacle"))
        {
            nearbyObstacle = other.gameObject;
            // Debug.Log("벽 접촉");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item") && other.gameObject == nearbyItem)
        {
            nearbyItem = null;
            // Debug.Log("감지 종료: " + other.name);
        }

        if (other.CompareTag("Obstacle") && other.gameObject == nearbyObstacle)
        {
            nearbyObstacle = null;
            // Debug.Log("벽 이탈");
        }
    }

}

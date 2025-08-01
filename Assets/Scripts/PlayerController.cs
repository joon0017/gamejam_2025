using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private GameObject currentItem;
    private GameObject nearbyItem = null;   // item object를 저장할 변수
    private GameObject nearbyObstacle = null;   // 벽 접촉 여부를 판별해줄 변수 

    [SerializeField] public float fadeInDuration;            // 페이드 인 지속 시간
    [SerializeField] public float stayDuration;              // 완전히 보인 후 유지 시간
    [SerializeField] public float fadeOutDuration;           // 페이드 아웃 지속 시간
    [SerializeField] private GameObject[] whoIsFirst;       // 순서대로 등장할 오브젝트들

    void Update()
    {
        Move();
        Interact();
    }

    private void Move()
    {
        rigid.linearVelocity = new Vector3(InputManager.Instance.move.x * speed, 0, InputManager.Instance.move.y * speed);
    }

    private void Interact()
    {
        if (InputManager.Instance.interact)
        {
            if (nearbyItem == null && nearbyObstacle == null)
            {
                // Debug.Log("I cant 상호작용..");
                // 여기에 물음표 일시적으로 표시되고 사라지는 메서드 적으면 됨
                TriggerSequence();
            }
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
                        if (newItem.GetComponent<Item>().itemName == "Torch") gameObject.GetComponent<FieldOfViewChanger>().LargeFOV();
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
                if (currentItem.GetComponent<Item>().itemName == "Torch")
                {
                    currentItem.GetComponent<InteractiveItem>().SetUniqueItem(nearbyObstacle);
                    currentItem.GetComponent<InteractiveItem>().ItemUniqueEffect();
                    Destroy(currentItem);
                }
                // Debug.Log("벽이랑 상호작용 끝");
            }
            else
            {
                // Debug.Log("Cannot pass door because player is not holding Torch item");
            }
        }
        else
        {
            // Debug.Log("I have not any item..");
        }
    }

    /*
    Check if it is possible to pick up the item in parameter.
    if the player is not holding an item, the default return value is true. 
    The player is able to pickup anything if not holding anything.
    */
    private bool CanPickup(GameObject item)
    {
        if (currentItem)
        {
            Debug.Log("Currently holding an item... check if can pick up another");
            return currentItem.GetComponent<Item>().CanPickup(item);
        }

        return true;
    }

    private void SetCurrentItem(GameObject item)
    {
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

    // question Mark
    public void TriggerSequence()
    {
        // Debug.Log("TriggerSequence 실행됨");
        StopAllCoroutines();
        StartSequence();
    }

    private void StartSequence()
    {
        StartCoroutine(onAndoff(whoIsFirst[0]));
        StartCoroutine(onAndoff(whoIsFirst[1]));
        StartCoroutine(onAndoff(whoIsFirst[2]));
    }
    private IEnumerator onAndoff(GameObject obj)
    {
        if (obj == null)
            yield break;
        obj.SetActive(true);
        // Fade In
        float timer = 0f;
        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        // Stay
        yield return new WaitForSeconds(stayDuration);
        // Fade Out
        timer = 0f;
        while (timer < fadeOutDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        obj.SetActive(false);
    }

}

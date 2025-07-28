using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody rigid;

    public enum PlayerState
    {
        None, A, B, C, D
    }

    private GameObject currentItem;
    private GameObject nearbyItem = null;   // item object를 저장할 변수
    private GameObject nearbyObstacle = null;   // 벽 접촉 여부를 판별해줄 변수 

    private PlayerState currentState = PlayerState.None;

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
            // 1. 아이템 상호작용
            if (nearbyItem != null)
            {
                string itemName = nearbyItem.name;
                Debug.Log("상호작용: " + itemName);

                if (CanPickup(nearbyItem))
                {
                    Debug.Log("아이템 줍기 성공: " + itemName);

                    foreach (Transform child in transform)
                    {
                        Destroy(child.gameObject);
                    }

                    // item의 태그 삭제 
                    nearbyItem.transform.SetParent(this.transform);
                    nearbyItem.transform.localPosition = new Vector3(-1, 1, 0);
                    nearbyItem.tag = "Untagged";

                    // item의 Collider 제거
                    SphereCollider col = nearbyItem.GetComponent<SphereCollider>();
                    if (col != null)
                    {
                        Destroy(col);
                    }


                    ApplyState(itemName);

                    nearbyItem = null;
                }
                else
                {
                    Debug.Log("조건이 맞지 않아 " + itemName + "을(를) 주울 수 없음");
                }
            }

            // 2. 벽 상호작용
            if (nearbyObstacle != null)
            {
                if (currentState == PlayerState.D)
                {
                    Debug.Log("D 상태로 벽 통과!");
                    openTheDoor();
                    RemoveD();
                }
                else
                {
                    Debug.Log("D 상태가 아니라 벽 통과 불가");
                }
            }

            InputManager.Instance.interact = false;
        }
    }

/*
Check if it is possible to pick up the item in parameter.
if the player is not holding an item, the default return value is true. 
The player is able to pickup anything if not holding anything.
*/
    private bool CanPickup(GameObject item)
    {
        if(currentItem) return currentItem.GetComponent<Item>().CanPickup(item);
        
        return true;
    }



    // private bool CanPickup(string item) // 집을 수 있는 상태인지 확인
    // {
    //     if(item)
    //     switch (item)
    //     {
    //         case "A":
    //             return currentState == PlayerState.None || currentState == PlayerState.B;
    //         case "B":
    //             return currentState == PlayerState.None || currentState == PlayerState.A || currentState == PlayerState.C;
    //         case "C":
    //             return currentState == PlayerState.None || currentState == PlayerState.B;
    //         default:
    //             return false;
    //     }
    // }
    private void ApplyState(string picked)
    {
        if (picked == "A" && currentState == PlayerState.B ||
            picked == "B" && currentState == PlayerState.A)
        {
            currentState = PlayerState.D;
            nearbyItem.name = "D";
            Debug.Log("A + B 조합 → D 상태 진입");
        }
        else if (picked == "C" && currentState == PlayerState.B ||
                 picked == "B" && currentState == PlayerState.C)
        {
            currentState = PlayerState.D;
            nearbyItem.name = "D";
            Debug.Log("B + C 조합 → D 상태 진입");
        }
        else
        {
            // 조합이 안 되면 현재 아이템 상태로 변경
            currentState = picked switch
            {
                "A" => PlayerState.A,
                "B" => PlayerState.B,
                "C" => PlayerState.C,
                _ => currentState
            };
        }
    }

    private void SetCurrentItem(GameObject item){
        currentItem = item;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            nearbyItem = other.gameObject;
            Debug.Log("감지 시작: " + other.name);
        }

        if (other.CompareTag("Obstacle"))
        {
            nearbyObstacle = other.gameObject;
            Debug.Log("벽 접촉");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item") && other.gameObject == nearbyItem)
        {
            nearbyItem = null;
            Debug.Log("감지 종료: " + other.name);
        }

        if (other.CompareTag("Obstacle") && other.gameObject == nearbyObstacle)
        {
            nearbyObstacle = null;
            Debug.Log("벽 이탈");
        }
    }

    private void RemoveD()
    {
        currentState = PlayerState.None;

        foreach (Transform child in transform)
        {
            if (child.name == "D")
            {
                Destroy(child.gameObject);
                break;
            }
        }

        Debug.Log("D 아이템 제거 완료");
    }

    private void openTheDoor() {
         // 벽의 자식 제거
        if (nearbyObstacle != null)
        {
            foreach (Transform child in nearbyObstacle.transform)
            {
                if (child.name == "RealWall")
                {
                    Destroy(child.gameObject);
                }
            }
            Debug.Log("벽 제거 완료");
        }
    }

}

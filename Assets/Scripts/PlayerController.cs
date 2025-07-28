using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody rigid;

    public enum PlayerState
    {
        None, A, B, C, D
    }
    private GameObject nearbyItem = null;   // item object를 저장할 변수
    private PlayerState currentState = PlayerState.None;
    
    void Update()
    {
        Move();
        Interact();
    }

    private void Move()
    {
        rigid.linearVelocity = new Vector2(InputManager.Instance.move.x * speed, InputManager.Instance.move.y * speed);
    }

    private void Interact()
    {
        if (InputManager.Instance.interact && nearbyItem != null)
        {
            string itemName = nearbyItem.name;

            if (CanPickup(itemName))    // 집을 수 있는 상태인지 확인
            {
                Debug.Log("✅ 아이템 줍기 성공: " + itemName);

                // 기존 아이템 제거
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }

                // 아이템 붙이기
                nearbyItem.transform.SetParent(this.transform);
                nearbyItem.transform.localPosition = new Vector3(-1, 1, 0);
                nearbyItem.tag = "Untagged";

                // 상태 업데이트 및 조합 검사
                ApplyState(itemName);

                nearbyItem = null;
            }
            else
            {
                Debug.Log("❌ 조건이 맞지 않아 " + itemName + "을(를) 주울 수 없음");
            }

            InputManager.Instance.interact = false;
        }
    }

    /*
        상시 계속 감시하는 함수 
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Item"))
            {
                Debug.Log("감지된 아이템: " + other.name);
            }
        }
    */

    private bool CanPickup(string item) // 집을 수 있는 상태인지 확인
    {
        switch (item)
        {
            case "A":
                return currentState == PlayerState.None || currentState == PlayerState.B;
            case "B":
                return currentState == PlayerState.None || currentState == PlayerState.A || currentState == PlayerState.C;
            case "C":
                return currentState == PlayerState.None || currentState == PlayerState.B;
            default:
                return false;
        }
    }
    private void ApplyState(string picked)
    {
        if (picked == "A" && currentState == PlayerState.B ||
            picked == "B" && currentState == PlayerState.A)
        {
            currentState = PlayerState.D;
            Debug.Log("✨ A + B 조합 → D 상태 진입");
        }
        else if (picked == "C" && currentState == PlayerState.B ||
                 picked == "B" && currentState == PlayerState.C)
        {
            currentState = PlayerState.D;
            Debug.Log("✨ B + C 조합 → D 상태 진입");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            nearbyItem = other.gameObject;
            Debug.Log("감지 시작: " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item") && other.gameObject == nearbyItem)
        {
            nearbyItem = null;
            Debug.Log("감지 종료: " + other.name);
        }
    }
}

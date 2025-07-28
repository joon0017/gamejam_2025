using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody rigid;


    // Update is called once per frame
    void Update()
    {
        Move();
        Interact();
    }

    private void Move(){
        rigid.linearVelocity=new Vector2(InputManager.Instance.move.x*speed,InputManager.Instance.move.y*speed);
    }

    private void Interact(){
        if(InputManager.Instance.interact){
            Debug.Log("interacted");
            InputManager.Instance.interact = false;
        }
    }
}

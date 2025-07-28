using UnityEngine;

public class Player_2D_TopView : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed;
    


    [SerializeField]
    private Rigidbody2D rigid;

    void Update()
    {
        Move();
    }


    private void Move()
    {
        rigid.linearVelocity=new Vector2(Input_Value.Instance.move.x*MoveSpeed,Input_Value.Instance.move.y*MoveSpeed);
        
    }
   
}

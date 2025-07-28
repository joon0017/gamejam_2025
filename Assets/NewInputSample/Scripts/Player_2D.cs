using UnityEngine;

public class Player_2D : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed;
    [SerializeField]
    private float JumpHeight;
    [SerializeField]
    private float GroundDetectLength;
    


    [SerializeField]
    private Rigidbody2D rigid;
    private bool isGround;

    void Update()
    {
        Move();
        Jump();
    }


    private void Move()
    {
        rigid.linearVelocityX=Input_Value.Instance.move.x*MoveSpeed;
        
    }
    private void Jump()
    {
        GroundCheck();
        if(Input_Value.Instance.jump&&isGround)
        {
            float jumpVelocity = Mathf.Sqrt(2*Mathf.Abs(Physics2D.gravity.y)*JumpHeight);
            rigid.linearVelocityY =  ( jumpVelocity);
            isGround = false;
            
        }
    }
    private void GroundCheck()
    {
        LayerMask layerMask = LayerMask.GetMask("Default");

        RaycastHit2D hitInfo;
        hitInfo = Physics2D.BoxCast(
            new Vector2(transform.position.x, transform.position.y-0.51f),//origin 
            new Vector2(0.5f,GroundDetectLength),//size
            0.0f,//float angle
            Vector2.down,//Vector2 Direction
            GroundDetectLength,//float distance
            LayerMask.NameToLayer("Player"), //int layer mask
            0, //Min depth
            0//Max depth
        );  
        if(hitInfo)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

}

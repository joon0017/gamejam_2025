using UnityEngine;

public class Player_3D : MonoBehaviour
{

    [SerializeField]
    private Rigidbody rigid;
    //Move Vars
        [Header("Move")]
        [SerializeField]
        private bool MoveWithAccelation;
        [SerializeField]
        private float MoveSpeed;
        [SerializeField]
        private float SprintSpeed;
    //
    //Camera Vars
        [Header("Camera")]
        [SerializeField]
        private Camera camera;

        [SerializeField]
        private float CameraVerticalAngle;

        [SerializeField]
        private float RotationSpeed;

        private float CameraThresHold = 0.01f;
        private float MoveThresHold = 0.01f;
        private float verticalRotation = 0.0f;
    //
    //Jump Vars
        [Header("Jump")]
        [SerializeField]
        private bool isGround;
        [SerializeField]
        private float GroundDetectLength;

        [SerializeField]
        private float JumpHeight;

    //

    private void LateUpdate()
    {
        CameraRotation();
    }
    private void CameraRotation()
    {
        if(Input_Value.Instance.mouseDelta.sqrMagnitude >= CameraThresHold)
        {
            float deltaTimeMultiplier = Time.deltaTime;        // Mouse input for rotation
            
            verticalRotation -= Input_Value.Instance.mouseDelta.y;
            verticalRotation = Mathf.Clamp(verticalRotation, -1*CameraVerticalAngle, CameraVerticalAngle);
            camera.transform.localEulerAngles = new Vector3(verticalRotation*RotationSpeed, 0, 0);
            transform.Rotate(Vector3.up * Input_Value.Instance.mouseDelta.x*RotationSpeed);

        }
    }

    private void Update()
    {
        Move();
        Jump();
    } 


     private void Move()
    {
        if(Input_Value.Instance.move.sqrMagnitude>=MoveThresHold||!MoveWithAccelation)
        {
            Vector3 moveVector = transform.forward*Input_Value.Instance.move.y+transform.right*Input_Value.Instance.move.x;
            moveVector = moveVector.normalized;
            if(Input_Value.Instance.sprint)
            {
                moveVector *= SprintSpeed;
            }
            else
            {
                moveVector *=MoveSpeed;
            }
            rigid.linearVelocity = new Vector3 (moveVector.x, rigid.GetPointVelocity(this.transform.position).y,moveVector.z);
        }
    }

    private void Jump()
    {
        GroundCheck();
        if(Input_Value.Instance.jump&&isGround)
        {
            float jumpVelocity = Mathf.Sqrt(2*Mathf.Abs(Physics.gravity.y)*JumpHeight);
            rigid.linearVelocity = new Vector3 (rigid.GetPointVelocity(this.transform.position).x, jumpVelocity,rigid.GetPointVelocity(this.transform.position).z);
            isGround = false;
            
        }
    }
    private void GroundCheck()
    {
        LayerMask layerMask = LayerMask.GetMask("Default");
        RaycastHit hitInfo;
        bool hit =Physics.BoxCast(
                transform.position -new Vector3(0,-1,0) ,//상자의 중심 좌표 
                new Vector3(0.25f,GroundDetectLength,0.25f),//상자의 사이즈
                Vector3.up*-1,//상자의 방향
                out hitInfo,
                Quaternion.Euler(0,0,0),
                GroundDetectLength,//인식 최대거리
                layerMask,
                QueryTriggerInteraction.Ignore
            );
            
        if(hit)
        {
            isGround = true;
        }
    }

}

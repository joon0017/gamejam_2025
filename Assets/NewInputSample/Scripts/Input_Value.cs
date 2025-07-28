using UnityEngine;
using UnityEngine.InputSystem;
public class Input_Value : MonoBehaviour
{
    public Vector2 move;
    public Vector2 mouseDelta;
    public Vector2 mousePosition;
    public bool jump;
    public bool sprint;

    //for Singleton
        private static Input_Value instance;
        public static Input_Value Instance{
            get{
                return instance;
            }
            set{
                instance = value;
            }
        }
    //

    void Awake()
    {
        //For singleton
            if(instance !=null)
            {
                Destroy(gameObject);
                return;
            }        
            instance = this;
            DontDestroyOnLoad(gameObject);
        //
    }
    
    public void OnMove(InputAction.CallbackContext value)
    {
        MoveSetter(value.ReadValue<Vector2>());
    }
    public void OnJump(InputAction.CallbackContext value)
    {
        if(value.started)
        {
            JumpSetter(true);
        }
        else if(value.canceled)
        {
            JumpSetter(false);
        }
    }
    public void OnSprit(InputAction.CallbackContext value)
    {
        if(value.started)
        {
            SprintSetter(true);
        }
        else if(value.canceled)
        {
            SprintSetter(false);
        }
    }
    public void OnMousePosition(InputAction.CallbackContext value)
    {
        mousepositionSetter(value.ReadValue<Vector2>());
    }
    public void OnMouseDelta(InputAction.CallbackContext value)
    {
        mouseDeltaSetter(value.ReadValue<Vector2>());
    
    }




    private void MoveSetter(Vector2 moveValue)
    {
        move = moveValue;
    }
    
    private void JumpSetter(bool jumpValue)
    {
        jump = jumpValue;
    }
    
    private void SprintSetter(bool sprintValue)
    {
        sprint = sprintValue;
    }
    
    private void mouseDeltaSetter(Vector2 mousedeltaValue)
    {
        mouseDelta = mousedeltaValue;
    }
    
    private void mousepositionSetter(Vector2 mousepositionValue)
    {
        mousePosition = mousepositionValue;
    }

}

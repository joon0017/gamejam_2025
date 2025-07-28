using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //singleton
    private static InputManager instance;
    public static InputManager Instance{
        get{ return instance;} set{instance = value;}
    }
    private void Awake() {
        if(instance){
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }



    public Vector2 move;
    public bool interact;


    public void OnMove(InputAction.CallbackContext value){
        MoveSetter(value.ReadValue<Vector2>());
    }
    public void OnInteract(InputAction.CallbackContext value){
        if(value.started){
            InteractSetter(true);
        }
        else if(value.canceled){
            InteractSetter(false);
        }
    }
    
    private void MoveSetter(Vector2 value){
        move = value;
    }
    private void InteractSetter(bool value){
        interact = value;
    }
}

using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private string lastState = "isWalkLeft";
    void Start(){
        anim = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(InputManager.Instance.move == new Vector2(0,0)){
            anim.SetBool("isWalk",false);
            anim.SetBool("isWalkLeft",false);
            anim.SetBool("isWalkRight",false);
            
        }
        else{
            anim.SetBool("isWalk",true);
            if(InputManager.Instance.move.x < 0) {
                anim.SetBool("isWalkLeft",true);
                anim.SetBool("isWalkRight",false);
                lastState = "isWalkLeft";
            }
            else if(InputManager.Instance.move.x > 0) {
                anim.SetBool("isWalkRight",true);
                anim.SetBool("isWalkLeft",false);
                lastState = "isWalkRight";
            }

            anim.SetBool(lastState,true);
        }
    }
}

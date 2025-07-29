using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;
    void Start(){
        anim = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(InputManager.Instance.move == new Vector2(0,0)){
            anim.SetBool("isWalk",false);
            
        }
        else{
            if(InputManager.Instance.move.x < 0) gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            else if(InputManager.Instance.move.x > 0) gameObject.transform.eulerAngles = new Vector3(0,0,0);
            anim.SetBool("isWalk",true);
        }
    }
}

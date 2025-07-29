using UnityEngine;

public class FlickeringLights : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private bool isActive = false;
    [SerializeField] private Light pLight;

    private void Start() {
        anim = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        //when player picked up an item, set isActive to true.
        if(isActive){
            anim.SetTrigger("Illuminate");
        }
        else{
            anim.SetTrigger("Idle");
        }
    }

    public void SetActive(bool act){
        isActive = act;
    }
    public void SetActive(){
        isActive = true;
    }

    public void ItemPickedUp(){
        pLight.enabled=false;
    }
}

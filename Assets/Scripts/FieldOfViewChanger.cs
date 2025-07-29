using UnityEngine;

public class FieldOfViewChanger : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject smallImage;

    public void LargeFOV(){
        smallImage.SetActive(false);
    }
    public void SmallFOV(){
        smallImage.SetActive(true);
    }
}

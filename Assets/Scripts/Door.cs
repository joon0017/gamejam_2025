using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject burnTree;
    [SerializeField] private float Duration = 3f;
    /*
        child 컴포넌트를 active로 바꾸고 3초후 openDoor
    */
    public void openDoor()
    {
        StartCoroutine(burn(burnTree));
    }
    private IEnumerator burn(GameObject obj)
    {
        Debug.Log("1");
        if (obj == null)
            yield break;

        Debug.Log("2");
        obj.SetActive(true);
        // Stay
        yield return new WaitForSeconds(Duration);
        Debug.Log("3");
        
        Destroy(gameObject);
    }
}

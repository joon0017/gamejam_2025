using UnityEngine;

public class WallPassThrough : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("벽 접촉");
        }
    }
}

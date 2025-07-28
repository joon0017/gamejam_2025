using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 positionOffset = new Vector3(0,2,-7);

    //for debugging
    [SerializeField] private float tiltAroundX;
    [SerializeField] private float tiltAroundY;
    [SerializeField] private float tiltAroundZ;

    private Vector3 playerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() {
        //
    }

    // Update is called once per frame
    void Update()
    {
        //follow player
        playerPosition = player.transform.position;
        gameObject.transform.position = playerPosition + positionOffset;

        //camera roation
        Quaternion target = Quaternion.Euler(tiltAroundX, tiltAroundY, tiltAroundZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * 5f);
    }
}

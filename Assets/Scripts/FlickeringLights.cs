using UnityEngine;

public class FlickeringLights : MonoBehaviour
{
    private Light torchLight;
    private float baseIntensity;
    private float timer;

    [Header("Flicker Settings")]
    public float flickerSpeed = 0.05f;    // 깜빡임 속도
    public float intensityVariation = 0.3f; // 밝기 변화 폭

    void Start()
    {
        Debug.Log("깜박깜박 시작");
        torchLight = GetComponent<Light>();
        if (torchLight == null)
        {
            Debug.LogWarning("FlickeringLights: Light 컴포넌트가 없습니다.");
            enabled = false;
            return;
        }

        baseIntensity = torchLight.intensity;
        timer = Random.Range(0f, flickerSpeed);
    }

    void Update()
    {
        Debug.Log("깜박1");
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            torchLight.intensity = baseIntensity + Random.Range(-intensityVariation, intensityVariation);
            timer = Random.Range(0.01f, flickerSpeed);
        }
        Debug.Log("깜박2");
    }
}

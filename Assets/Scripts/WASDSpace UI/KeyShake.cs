using UnityEngine;
using System.Collections;

public class KeyShakeEffect : MonoBehaviour
{
    public string targetKey = "w";  // 대응 키
    public float shakeAmount = 5f;  // 흔들림 세기
    public float shakeSpeed = 20f;  // 흔들림 빈도

    private RectTransform rectTransform;
    private Vector3 originalPos;
    private float timeCounter = 0f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPos = rectTransform.anchoredPosition;
    }

    void Update()
    {
        if (Input.GetKey(targetKey))
        {
            timeCounter += Time.deltaTime * shakeSpeed;

            float offsetX = Mathf.PerlinNoise(timeCounter, 0f) * 2 - 1f;
            float offsetY = Mathf.PerlinNoise(0f, timeCounter) * 2 - 1f;

            Vector3 shakeOffset = new Vector3(offsetX, offsetY, 0) * shakeAmount;
            rectTransform.anchoredPosition = originalPos + shakeOffset;
        }
        else
        {
            rectTransform.anchoredPosition = originalPos;
            timeCounter = 0f;
        }
    }
}

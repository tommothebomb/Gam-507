using UnityEngine;

public class RocketShake : MonoBehaviour
{
    [SerializeField] float shakeAmount = 0.05f;
    [SerializeField] float shakeSpeed = 15f;
    [SerializeField] float rotationAmount = 2f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        float time = Time.time * shakeSpeed;

        float x = (Mathf.PerlinNoise(time, 0f) - 0.5f) * 2f;
        float y = (Mathf.PerlinNoise(0f, time) - 0.5f) * 2f;
        float rotation = (Mathf.PerlinNoise(time, 10f) - 0.5f) * 2f;

        transform.localPosition = originalPosition + new Vector3(x, y, 0f) * shakeAmount;

        transform.localRotation = originalRotation * Quaternion.Euler(0f, 0f, rotation * rotationAmount);
    }
}

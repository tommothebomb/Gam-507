using TMPro;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range (0, 24)]
    public float currentTime = 8f;
    public float realMinutesPerGameHour = 1f;
    public Light sun;
    public float dayIntensity = 1.2f;
    public float nightIntensity = 0f;

    void Update()
    {
        currentTime += Time.deltaTime / (realMinutesPerGameHour * 60f);

        currentTime = Mathf.Repeat(currentTime, 24f);

        float sunRotation = (currentTime / 24f) * 360f - 90f;

        sun.transform.rotation = Quaternion.Euler(sunRotation, 170f, 0f);

        float sunHeight = Mathf.Sin((currentTime - 6f) / 12f * Mathf.PI);

        float intensity = Mathf.Lerp(nightIntensity, dayIntensity, Mathf.Clamp01(sunHeight));

        sun.intensity = intensity;

        Color sunriseColor = new Color(1f, 0.55f, 0.3f);
        Color daylightColor = Color.white;
        Color nightColor = new Color(0.15f, 0.2f, 0.4f);

        if (currentTime >= 5f && currentTime < 12f)
        {
            sun.color = Color.Lerp(sunriseColor, daylightColor, (currentTime - 5f) / 7f);
        }
        else if (currentTime >= 12f && currentTime < 19f)
        {
            sun.color = Color.Lerp(daylightColor, sunriseColor, (currentTime - 12f) / 7f);
        }
        else
        {
            sun.color = nightColor;
        }
    }
}


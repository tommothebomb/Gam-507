using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header ("Day Night Settings")]
    [Range (0, 24)]
    public float currentTime = 8f;
    [SerializeField] float realMinutesPerGameHour = 1f;
    [SerializeField] Light sun;
    [SerializeField] float dayIntensity = 1.2f;
    [SerializeField] float nightIntensity = 0f;

    [Header ("Ceiling Lights")]
    [SerializeField] float lightTurnOnTime = 19f;
    [SerializeField] float lightTurnOffTime = 7f;
    [SerializeField] Material lightOnMaterial;
    [SerializeField] Material lightOffMaterial;
    private bool isCeilingLightOn;

    [SerializeField] Renderer[] ceilingLightRenderer;
    [SerializeField] Light[] ceilingPointLight;
    [SerializeField] Light[] ceilingSpotLight;

    void Update()
    {
        currentTime += Time.deltaTime / (realMinutesPerGameHour * 60f);

        currentTime = Mathf.Repeat(currentTime, 24f);

        UpdateCeilingLight();

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

    void UpdateCeilingLight()
    {
        bool shouldBeOn = currentTime >= lightTurnOnTime || currentTime < lightTurnOffTime;

        if (shouldBeOn != isCeilingLightOn)
        {
            isCeilingLightOn = shouldBeOn;

            foreach (Renderer lightRenderer in ceilingLightRenderer)
            {
                if (lightRenderer != null)
                {
                    lightRenderer.sharedMaterial = isCeilingLightOn ? lightOnMaterial : lightOffMaterial;
                }
            }
        }

        foreach (Light pointLight in ceilingPointLight)
        {
            if (pointLight != null)
            {
                pointLight.enabled = isCeilingLightOn;
            }
        }

        foreach (Light spotLight in ceilingSpotLight)
        {
            if (spotLight != null)
            {
                spotLight.enabled = isCeilingLightOn;
            }
        }
    }
}


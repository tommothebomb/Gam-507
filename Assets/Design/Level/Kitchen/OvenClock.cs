using TMPro;
using UnityEngine;

public class OvenClock : MonoBehaviour
{
    private DayNightCycle dayNightCycle;
    public TMP_Text timeDisplay;

    void Awake()
    {
        dayNightCycle = GetComponent<DayNightCycle>();
    }

    void Update()
    {
        UpdateClock();
    }

    void UpdateClock()
    {
        if (dayNightCycle == null || timeDisplay == null)
            return;
        
        float currentTime = dayNightCycle.currentTime;

        int hours = Mathf.FloorToInt(currentTime);

        int minutes = Mathf.FloorToInt((currentTime - hours) * 60f);

        timeDisplay.text = $"<mspace=0.5em>{hours:00}</mspace>:<mspace=0.5em>{minutes:00}</mspace>";
    }
}

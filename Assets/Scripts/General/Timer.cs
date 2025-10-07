using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timerVal = 0f;
    public bool timerStarted = false;

    public List<float> times = new List<float>();

    [Header("UI Reference")]
    public TextMeshProUGUI timerText;

    public void StartTimer()
    {
        timerVal = 0f;
        timerStarted = true;
    }

    public void StopTimer()
    {
        timerStarted = false;
    }

    public void FinishTimer()
    {
        if (timerStarted) 
        {
            times.Add(timerVal);


            times.Sort();

            Debug.Log($"New time added: {timerVal:F2}s | Best: {times[0]:F2}s");


            timerStarted = false;
        }
        
    }

    public void ResetTimer()
    {
        timerVal = 0f;
        UpdateTimerText();
    }

    private void Update()
    {
        if (!timerStarted) return;

        timerVal += Time.deltaTime;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timerVal / 60f);
        int seconds = Mathf.FloorToInt(timerVal % 60f);
        int milliseconds = Mathf.FloorToInt((timerVal * 1000f) % 1000f / 10f);

        timerText.text = $"Timer: {minutes:00}:{seconds:00}:{milliseconds:00}";
    }
}

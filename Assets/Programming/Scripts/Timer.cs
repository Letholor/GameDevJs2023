using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public Image slider;
    public float timeLimit = 60f;
    public bool inMinutes;

    [Space]
    public UnityEvent OnStart, OnComplete;

    float time;
    float multiplierFactor;
    bool startTimer;

    TimeSpan timeConverter;

    private void Start()
    {
        time = timeLimit;
        slider.fillAmount = time * multiplierFactor;
        startTimer = false;

        if (inMinutes)
        {
            timeConverter = TimeSpan.FromSeconds(time);
            float minutes = timeConverter.Minutes;
            float seconds = timeConverter.Seconds;

            timerText.text = $"{minutes}:{seconds}";
        }
        else
        {
            timerText.text = Mathf.CeilToInt(time).ToString();
        }
    }

    public void StartTimer()
    {
        multiplierFactor = 1f / timeLimit;
        startTimer = true;
        slider.fillAmount = time * multiplierFactor;

        OnStart?.Invoke();
    }
    private void Update()
    {
        if (!startTimer) return;   

        if (time > 0f)
        {
            time -= Time.deltaTime;

            if (inMinutes)
            {
                timeConverter = TimeSpan.FromSeconds(time);
                float minutes = timeConverter.Minutes;
                float seconds = timeConverter.Seconds;

                timerText.text = $"{minutes}:{seconds}";
            }
            else
            {
                timerText.text = Mathf.CeilToInt(time).ToString();
            }
            slider.fillAmount = time * multiplierFactor;
        }
        else
        {
            startTimer = false;
            OnComplete?.Invoke();
        }
    }
    public void PauseTimer()
    {
        if (startTimer)
        {
            startTimer = false;
        }
    }
    public void ResetTimer()
    {
        time = timeLimit;

        if (inMinutes)
        {
            timeConverter = TimeSpan.FromSeconds(time);
            float minutes = timeConverter.Minutes;
            float seconds = timeConverter.Seconds;

            timerText.text = $"{minutes}:{seconds}";
        }
        else
        {
            timerText.text = Mathf.CeilToInt(time).ToString();
        }

        slider.fillAmount = time * multiplierFactor;
    }
}

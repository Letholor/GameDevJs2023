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
    public bool changeColor;

    [Space]
    public UnityEvent OnStart, OnComplete;

    private Text _timerText;
    private Image _slider;
    private Image _timerImage;

    public float _time;
    private float _multiplierFactor;
    private bool _startTimer;

    private TimeSpan _timeConverter;

    private void Start()
    {
        _timerText = timerText;
        _slider = slider;
        _timerImage = slider;

        _time = timeLimit;
        _multiplierFactor = 1f / timeLimit;
        _startTimer = false;

        // display initial timer value
        UpdateTimerDisplay();

        //StartTimer();
    }

    public void AddSeconds(float secondsToAdd)
    {
        _time += secondsToAdd;
        UpdateTimerDisplay();
        UpdateSliderFillAmount();
    }

    private IEnumerator RunTimer()
    {
        _startTimer = true;
        while (_time > 0f)
        {
            _time -= Time.deltaTime;
            UpdateTimerDisplay();
            UpdateSliderFillAmount();
            yield return null; // wait one frame
        }
        _startTimer = false;
        OnComplete?.Invoke();
    }

    public void StartTimer()
    {
        StartCoroutine(RunTimer());
        OnStart?.Invoke();
    }

    public void PauseTimer()
    {
        if (_startTimer)
        {
            StopAllCoroutines();
            _startTimer = false;
        }
    }

    public void ResetTimer()
    {
        StopAllCoroutines();
        _time = timeLimit;
        UpdateTimerDisplay();
        UpdateSliderFillAmount();
    }

    private void UpdateTimerDisplay()
    {
        if (inMinutes)
        {
            _timeConverter = TimeSpan.FromSeconds(_time);
            string timerString = string.Format("{0:D2}:{1:D2}", _timeConverter.Minutes, _timeConverter.Seconds);
            _timerText.text = timerString;
        }
        else if (_timerText != null)
        {
            _timerText.text = Mathf.CeilToInt(_time).ToString();
        }

        UpdateTimerColor();
    }

    private void UpdateSliderFillAmount()
    {
        _slider.fillAmount = _time * _multiplierFactor;
    }

    private void UpdateTimerColor()
    {
        if (changeColor)
        {
            float percentage = 1 - (_time / timeLimit);
            _timerImage.color = Color.Lerp(Color.green, Color.red, percentage);
        }
    }
}

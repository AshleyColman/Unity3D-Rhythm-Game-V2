using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using TMPro;
using AudioScripts;

[RequireComponent(typeof(AudioSource))]
public sealed class Metronome : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager = default;
    [SerializeField] private AudioSource metronomeAudioSource = default;
    [SerializeField] private AudioClip highClip = default;
    [SerializeField] private AudioClip lowClip = default;
    [SerializeField] private ToMainThread toMainThread = default;
    private int step = 4;
    private int root = 4;
    private int currentMeasure = 0;
    private int currentStep = 0;
    private int currentTick = 0;
    private float beatsPerMinute = 120f;
    private List<double> tickTimeList = new List<double>();
    private double interval = 0;
    private double offsetMilliseconds = 0f;
    private bool neverPlayed = true;
    private bool metronomeMuted = true;
    private bool active = false;

    public void SetTiming(float _beatsPerMinute = 120f, double _offsetMilliseconds = 0f)
    {
        beatsPerMinute = _beatsPerMinute;
        offsetMilliseconds = _offsetMilliseconds;
        CalculateIntervals();
        SetCurrentTick();
        SetCurrentMeasure();
        SetCurrentStep();
    }
    public void SetTimingWithDelay(float _beatsPerMinute = 120f, double _offsetMilliseconds = 0f)
    {
        SetTiming(_beatsPerMinute, _offsetMilliseconds);
        SetDelay();
    }
    public void Play(double _time = 0)
    {
        audioManager.PlayAudio(_time);
        neverPlayed = false;
        active = true;
    }
    private void SetDelay()
    {
        Pause();
        CalculateIntervals();
        SetCurrentTick();
        SetCurrentMeasure();
        SetCurrentStep();
        Play();
    }
    private void Pause() => active = false;
    private void Stop()
    {
        active = false;
        currentMeasure = 0;
        currentStep = 4;
        currentTick = 0;
    }
    private void MuteMetronome(bool _mute) => metronomeMuted = _mute;
    private void PlayMetronome()
    {
        if (metronomeMuted == false)
        {
            metronomeAudioSource.Play();
        }
    }
    private void SetLowClip() => metronomeAudioSource.clip = lowClip;
    private void SetHighClip() => metronomeAudioSource.clip = highClip;
    private void CalculateIntervals(int _intervalOption = 0)
    {
        try
        {
            active = false;
            var multiplier = (root / step);
            var tmpInterval = (60f / beatsPerMinute);
            interval = (tmpInterval / multiplier);

            interval = tmpInterval / multiplier;
            //interval = _intervalOption switch
            //{
            //    1 => interval = (interval / 2),
            //    2 => interval = (interval / 3),
            //    3 => interval = (interval / 4),
            //    _ => tmpInterval / multiplier
            //};

            tickTimeList.Clear();
            int i = 0;
            while (interval * i <= audioManager.SongAudioClipLength)
            {
                tickTimeList.Add((interval * i) + (offsetMilliseconds / 1000f));
                i++;
            }
        }
        catch
        {
            Debug.LogWarning("There isn't an Audio Clip assigned in the Player.");
        }
    }
    private void SetCurrentTick()
    {
        for (int i = 0; i < tickTimeList.Count; i++)
        {
            if (audioManager.SongAudioSource.time <= tickTimeList[i])
            {
                currentTick = i;
                break;
            }
        }
    }
    private void SetCurrentMeasure()
    {
        for (int i = 0; i < tickTimeList.Count; i++)
        {
            if (audioManager.SongAudioSource.time <= tickTimeList[i])
            {
                currentMeasure = (i / 4);
                break;
            }
        }
    }
    private void SetCurrentStep()
    {
        for (int i = 0; i < currentTick; i++)
        {
            if (currentStep >= step)
            {
                currentStep = 1;
            }
            else
            {
                currentStep++;
            }
        }
    }
    public IEnumerator CalculateTicks()
    {
        if (active == false)
            yield return null;
        {
            if (currentTick < tickTimeList.Count)
            {
                if (audioManager.SongAudioSourceTime >= tickTimeList[currentTick])
                {
                    currentTick++;
                    if (currentStep >= step)
                    {
                        currentStep = 1;
                        currentMeasure++;
                        SetHighClip();
                    }
                    else
                    {
                        currentStep++;
                        SetLowClip();
                    }
                    StartCoroutine(OnTick());
                }
            }
        }
        yield return null;
    }
    private IEnumerator OnTick()
    {
        PlayMetronome();
        yield return null;
    }
    private void OnAudioFilterRead(float[] _data, int _channels)
    {
        if (!active)
            return;
        toMainThread.AssignNewAction().ExecuteOnMainThread(CalculateTicks());
    }
}
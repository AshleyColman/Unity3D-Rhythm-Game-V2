namespace TimingScripts
{
    using AudioScripts;
    using System.Collections.Generic;
    using UnityEngine;

    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private AudioManager audioManager = default;
        private const byte Step = 4;
        private const byte Base = 4;
        private byte currentStep = 0;
        private ushort currentMeasure = 0;
        private ushort currentTick = 0;
        private double[] tickTimeArr;
        private double interval = 0;
        private double beatsPerMinute = 120;
        private double offsetMilliseconds = 0;
        private bool timerStarted = false;

        public void SetTimeManager(double _beatsPerMinute, double _offsetMilliseconds)
        {
            SetInitialValues(_beatsPerMinute, _offsetMilliseconds);
            StartTimer();
            UpdateTimingPosition();
        }
        private void SetInitialValues(double _beatsPerMinute, double _offsetMilliseconds)
        {
            this.beatsPerMinute = _beatsPerMinute;
            this.offsetMilliseconds = _offsetMilliseconds;
        }
        private void StartTimer() => timerStarted = true;
        private void StopTimer() => timerStarted = false;
        private void UpdateTimingPosition()
        {
            CalculateIntervals();
            SetClosestTickAndMeasure();
            SetStepBasedOnCurrentAudioTime();
        }
        private void CheckIfOnTick()
        {
            if (currentTick < tickTimeArr.Length)
            {
                if (audioManager.SongAudioSource.time >= tickTimeArr[currentTick])
                {
                    OnTick();
                    CheckIfMeasure();
                }
            }
        }
        private void OnTick() => currentTick++;
        private void OnMeasure() => currentMeasure++;
        private void Update()
        {
            if (timerStarted == true)
            {
                CheckIfOnTick();
            }
        }
        private void SetClosestTickAndMeasure()
        {
            for (ushort i = 0; i < tickTimeArr.Length; i++)
            {
                if (audioManager.SongAudioSource.time <= tickTimeArr[i])
                {
                    currentMeasure = (ushort)(i / 4);
                    currentTick = i;
                    break;
                }
            }
        }
        private void SetStepBasedOnCurrentAudioTime()
        {
            for (ushort i = 0; i < currentTick; i++)
            {
                if (currentStep >= Step)
                {
                    currentStep = 1;
                }
                else
                {
                    currentStep++;
                }
            }
        }
        private void CalculateIntervals()
        {
            if (audioManager.SongAudioSource.clip != null)
            {
                int i = 0;
                int multiplier = (Base / Step);
                double tmpInterval = (60 / beatsPerMinute);
                interval = (tmpInterval / multiplier);
                var tickTimeList = new List<double>();

                while (interval * i <= audioManager.SongAudioSource.clip.length)
                {
                    tickTimeList.Add((interval * i) + (offsetMilliseconds / 1000f));
                    i++;
                }

                tickTimeArr = tickTimeList.ToArray();
            }
        }
        private void CheckIfMeasure()
        {
            if (currentTick == 1)
            {
                OnMeasure();
            }
            else
            {
                if (currentStep >= Step)
                {
                    currentStep = 1;
                    OnMeasure();
                }
                else
                {
                    currentStep++;
                }
            }
        }
    }
}
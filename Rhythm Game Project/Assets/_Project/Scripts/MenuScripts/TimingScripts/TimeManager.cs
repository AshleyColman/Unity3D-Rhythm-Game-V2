namespace TimingScripts
{
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class TimeManager : MonoBehaviour
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
        private double songTime = 0;
        private bool timerStarted = false;

        public void SetTimeManager(double _beatsPerMinute, double _offsetMilliseconds,
            double _playTime = 0)
        {
            SetInitialValues(_beatsPerMinute, _offsetMilliseconds, _playTime);
            StartTimer();
            UpdateTimingPosition();
        }
        private void SetInitialValues(double _beatsPerMinute, double _offsetMilliseconds, double _playTime)
        {
            this.beatsPerMinute = _beatsPerMinute;
            this.offsetMilliseconds = _offsetMilliseconds;
            this.songTime = _playTime;
        }
        private void StartTimer() => timerStarted = true;
        private void StopTimer() => timerStarted = false;
        private void UpdateTimer()
        {
            UpdateTimerToAudioTime();
            CheckIfOnTick();
        }
        private void UpdateTimingPosition()
        {
            CalculateIntervals();
            SetClosestTickAndMeasure();
            SetStepBasedOnCurrentAudioTime();
        }
        private void UpdateTimerToAudioTime()
        {
            //songTime = AudioSettings.dspTime - audioManager.SongAudioStartTime;
            songTime = audioManager.SongAudioSource.time;
        }
        private void CheckIfOnTick()
        {
            if (currentTick < tickTimeArr.Length)
            {
                if (songTime >= tickTimeArr[currentTick])
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
                UpdateTimer();
            }
        }
        private void SetClosestTickAndMeasure()
        {
            for (ushort i = 0; i < tickTimeArr.Length; i++)
            {
                if (songTime <= tickTimeArr[i])
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
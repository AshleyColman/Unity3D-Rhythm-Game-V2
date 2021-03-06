namespace FileScripts
{
    using StaticDataScripts;
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;

    public sealed class FileManager : MonoBehaviour
    {
        private const string BeatmapFolderName = "Beatmaps";
        public string BeatmapDirectoryPath { get; private set; }
        public string[] BeatmapDirectoryArr { get; private set; }
        private Beatmap beatmap;

        public Beatmap Load(string _path)
        {
            if (File.Exists(_path) == true)
            {
                using var stream = new FileStream(_path, FileMode.Open);
                var binaryFormatter = new BinaryFormatter();
                beatmap = (Beatmap)binaryFormatter.Deserialize(stream);
                return beatmap;
            }
            else
            {
                return null;
            }
        }
        public void OpenBeatmapDirectory() => Application.OpenURL(BeatmapDirectoryPath);
        public string GetFirstDirectoryImageFile(string _directory)
        {
            string[] allFiles = GetAllDirectoryImageFiles(_directory);
            if (allFiles.Length != 0)
            {
                return allFiles[0];
            }
            return string.Empty;
        }
        public string GetFirstDirectoryAudioFile(string _directory)
        {
            string[] allFiles = GetAllDirectoryAudioFiles(_directory);
            if (allFiles.Length != 0)
            {
                return allFiles[0];
            }
            return string.Empty;
        }
        private void Awake()
        {
            SetBeatmapDirectoryPath();
            CreateBeatmapFolder();
            SetBeatmapDirectories();
        }
        private string[] GetAllDirectoryImageFiles(string _directory)
        {
            string[] pngFiles = Directory.GetFiles(_directory, $"*{FileTypes.Image[0]}");
            string[] jpgFiles = Directory.GetFiles(_directory, $"*{FileTypes.Image[1]}");
            string[] allFiles = new string[pngFiles.Length + jpgFiles.Length];
            pngFiles.CopyTo(allFiles, 0);
            jpgFiles.CopyTo(allFiles, pngFiles.Length);
            return allFiles;
        }
        private string[] GetAllDirectoryAudioFiles(string _directory)
        {
            string[] mp3Files = Directory.GetFiles(_directory, $"*{FileTypes.Audio[0]}");
            string[] oggFiles = Directory.GetFiles(_directory, $"*{FileTypes.Audio[1]}");
            string[] allFiles = new string[mp3Files.Length + oggFiles.Length];
            mp3Files.CopyTo(allFiles, 0);
            oggFiles.CopyTo(allFiles, mp3Files.Length);
            return allFiles;
        }
        private void CreateBeatmapFolder() => Directory.CreateDirectory(BeatmapDirectoryPath);
        private void CreateFolderInBeatmapFolder(string folder) => Directory.CreateDirectory($"{BeatmapDirectoryPath}/{folder}");
        private void SetBeatmapDirectoryPath() => BeatmapDirectoryPath = $"{Application.persistentDataPath}/Beatmaps";
        private void SetBeatmapDirectories() => BeatmapDirectoryArr = Directory.GetDirectories(BeatmapDirectoryPath);
        private void CreateBeatmap(int _objectCount, Vector2[] _positionArr, float _beatsPerMinute,
            float _audioStartTime, double[] _timeArr, double[] _spawnTimeArr, double _offsetMilliseconds, string _title, string _artist,
            string _creator, string _table, string _modeName, string _difficulty, string _folderName, Mode _mode, ApproachRate _approachRate, ObjectSize _objectSize,
            DateTime _createdDate)
        {
            beatmap = new Beatmap();
            CreateFolderInBeatmapFolder(_folderName);
            string path = $"{Application.persistentDataPath}/{BeatmapFolderName}/{_folderName}/{FileNames.Beatmap}{FileTypes.BeatmapFileType}";
            using var stream = new FileStream(path, FileMode.Create);
            var binaryFormatter = new BinaryFormatter();
            beatmap.ObjectCount = _objectCount;
            beatmap.PositionArr = _positionArr;
            beatmap.BeatsPerMinute = _beatsPerMinute;
            beatmap.AudioStartTime = _audioStartTime;
            beatmap.HitTimeArr = _timeArr;
            beatmap.SpawnTimeArr = _spawnTimeArr;
            beatmap.OffsetMilliseconds = _offsetMilliseconds;
            beatmap.Title = _title;
            beatmap.Artist = _artist;
            beatmap.Creator = _creator;
            beatmap.Table = _table;
            beatmap.Mode = _mode;
            beatmap.ApproachRate = _approachRate;
            beatmap.ObjectSize = _objectSize;
            beatmap.CreatedDate = _createdDate;
            binaryFormatter.Serialize(stream, beatmap);
        }
    }
}
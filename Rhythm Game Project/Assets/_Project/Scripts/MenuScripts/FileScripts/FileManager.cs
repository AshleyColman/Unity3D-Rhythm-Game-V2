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
        private void Awake()
        {
            SetBeatmapDirectoryPath();
            CreateBeatmapFolder();
            SetBeatmapDirectories();
            CreateTestBeatmaps();
        }
        private void CreateBeatmapFolder() => Directory.CreateDirectory(BeatmapDirectoryPath);
        private void CreateFolderInBeatmapFolder(string folder) => Directory.CreateDirectory($"{BeatmapDirectoryPath}/{folder}");
        private void SetBeatmapDirectoryPath() => BeatmapDirectoryPath = $"{Application.persistentDataPath}/Beatmaps";
        private void SetBeatmapDirectories() => BeatmapDirectoryArr = Directory.GetDirectories(BeatmapDirectoryPath);
        private void CreateBeatmap(int _objectCount, float[] _positionArrX, float[] _positionArrY, float _beatsPerMinute,
            float _audioStartTime, double[] _spawnTimeArr, double _offsetMilliseconds, string _title, string _artist,
            string _creator, string _table, string _modeName, string _difficulty, string _folderName, Mode _mode, ApproachRate _approachRate, ObjectSize _objectSize,
            DateTime _createdDate)
        {
            beatmap = new Beatmap();
            CreateFolderInBeatmapFolder(_folderName);
            string path = $"{Application.persistentDataPath}/{BeatmapFolderName}/{_folderName}/{FileNames.Beatmap}{FileTypes.BeatmapFileType}";
            using var stream = new FileStream(path, FileMode.Create);
            var binaryFormatter = new BinaryFormatter();
            beatmap.ObjectCount = _objectCount;
            beatmap.PositionArrX = _positionArrX;
            beatmap.PositionArrY = _positionArrY;
            beatmap.BeatsPerMinute = _beatsPerMinute;
            beatmap.AudioStartTime = _audioStartTime;
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
        private void CreateTestBeatmaps() 
        {
            CreateBeatmap(1, new float[] { 1 }, new float[] { 1 }, 1, 1, new double[] { 1 }, 1, "Longinus", "xi", "Ashleyc97", "table", "3C", "Normal", "Longinus", Mode.Three, ApproachRate.Normal, ObjectSize.Normal, DateTime.Now);

            CreateBeatmap(1, new float[] { 1 }, new float[] { 1 }, 1, 1, new double[] { 1 }, 1, "Usotsuki Maker", "40mp", "RaineKitty", "table", "4C", "Hard", "Usotsuki Maker", Mode.Four, ApproachRate.Normal, ObjectSize.Normal, DateTime.Now);

            CreateBeatmap(1, new float[] { 1 }, new float[] { 1 }, 1, 1, new double[] { 1 }, 1, "Halycon", "xi", "Ashley", "table", "4C", "Hard", "Halcyon", Mode.Four, ApproachRate.Normal, ObjectSize.Normal, DateTime.Now);

            CreateBeatmap(1, new float[] { 1 }, new float[] { 1 }, 1, 1, new double[] { 1 }, 1, "Anima", "xi", "Tom", "table", "3C", "Normal", "Anima", Mode.Three, ApproachRate.Normal, ObjectSize.Normal, DateTime.Now);

            CreateBeatmap(1, new float[] { 1 }, new float[] { 1 }, 1, 1, new double[] { 1 }, 1, "Hesperides", "xi", "Mikolaj", "table", "3C", "Normal", "Hesperides", Mode.Three, ApproachRate.Normal, ObjectSize.Normal, DateTime.Now);

            CreateBeatmap(1, new float[] { 1 }, new float[] { 1 }, 1, 1, new double[] { 1 }, 1, "Murshroom", "pinocchio-p", "Miku", "table", "2C", "Easy", "Mushroom", Mode.Two, ApproachRate.Normal, ObjectSize.Normal, DateTime.Now);

            CreateBeatmap(1, new float[] { 1 }, new float[] { 1 }, 1, 1, new double[] { 1 }, 1, "Yellow", "Live-tune kz", "RaineKitty", "table", "2C", "Easy", "Yellow", Mode.Two, ApproachRate.Normal, ObjectSize.Normal, DateTime.Now);

            CreateBeatmap(1, new float[] { 1 }, new float[] { 1 }, 1, 1, new double[] { 1 }, 1, "Flower Dance", "DJ Owakari", "Ashleyc97", "table", "2C", "Easy", "Flower Dance", Mode.Two, ApproachRate.Normal, ObjectSize.Normal, DateTime.Now);

            CreateBeatmap(1, new float[] { 1 }, new float[] { 1 }, 1, 1, new double[] { 1 }, 1, "White Peak", "xi", "Embrashey", "table", "2C", "Easy", "WhitePeak", Mode.Two, ApproachRate.Normal, ObjectSize.Normal, DateTime.Now);
        }
    }
}
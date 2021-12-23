using FileScripts;
using StaticDataScripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BeatmapLoader : MonoBehaviour
{
    const string hitObjectLine = "[HitObjects]";
    const int ParamX = 0;
    const int ParamY = 1;
    const int ParamTime = 2;
    [SerializeField] private FileManager fileManager = default;
    [SerializeField] private DefaultAsset osuMapFile = default;
    [SerializeField] private Camera camera = default;

    public Beatmap LoadOsuFile()
    {
        var reader = new StreamReader(AssetDatabase.GetAssetPath(osuMapFile));
        return ReadOsuFile();
    }
    private void ReadUntilHitObjectLine(StreamReader _reader)
    {
        while (true)
        {
            if (_reader.ReadLine() == hitObjectLine)
            {
                break;
            }
        }
    }
    public Beatmap ReadOsuFile()
    {
        string osuMapFilePath = AssetDatabase.GetAssetPath(osuMapFile);
        string line = string.Empty;
        List<float> positionXList = new List<float>();
        List<float> positionYList = new List<float>();
        List<double> hitTimeList = new List<double>();
        List<double> spawnTimeList = new List<double>();
        string hitObjectLine = "[HitObjects]";
        StreamReader reader = new StreamReader(osuMapFilePath);

        // Skip to [HitObjects] part.
        while (true)
        {
            if (reader.ReadLine() == hitObjectLine)
            {
                break;
            }
        }
        int totalLines = 0;

        // Count all lines.
        while (!reader.EndOfStream)
        {
            reader.ReadLine();
            totalLines++;
        }
        reader.Close();

        reader = new StreamReader(osuMapFilePath);
        // Skip to [HitObjects] part again.
        while (true)
        {
            if (reader.ReadLine() == hitObjectLine)
            {
                break;
            }
        }

        while (!reader.EndOfStream)
        {
            line = reader.ReadLine();
            if (line == null)
            {
                break;
            }

            string[] lineParamsArray;
            lineParamsArray = line.Split(',');
            int FlipY = 384 - int.Parse(lineParamsArray[1]);
            int AdjustedX = Mathf.RoundToInt(Screen.height * 2);
            float Slices = 8f;
            float PaddingX = AdjustedX / Slices;
            float PaddingY = Screen.height / Slices;
            float NewRangeX = ((AdjustedX - PaddingX) - PaddingX);
            float NewValueX = ((int.Parse(lineParamsArray[0]) * NewRangeX) / 512f) + PaddingX + ((Screen.width - AdjustedX) / 2f);
            float NewRangeY = Screen.height;
            float NewValueY = ((FlipY * NewRangeY) / 512f) + PaddingY;
            Vector3 MainPos = camera.ScreenToWorldPoint(new Vector3(NewValueX, NewValueY, 0));
            positionXList.Add(MainPos.x * 100);
            positionYList.Add(MainPos.y * 100);
            // Convert to double milliseconds
            hitTimeList.Add(double.Parse(lineParamsArray[2]) / 1000);
        }

        // Combine x & y into positionList.
        List<Vector2> positionList = new List<Vector2>();
        for (int i = 0; i < positionXList.Count; i++)
        {
            positionList.Add(new Vector2(positionXList[i], positionYList[i]));
        }

        double approachRateMilliseconds = ApproachRateData.GetApproachRate(ApproachRate.Normal);
        // Calculate spawn times based on approach rate.
        for (int i = 0; i < hitTimeList.Count; i++)
        {
            spawnTimeList.Add(hitTimeList[i] - approachRateMilliseconds);
        }

        Beatmap beatmap = new Beatmap
        {
            ObjectCount = positionList.Count,
            PositionArr = positionList.ToArray(),
            HitTimeArr = hitTimeList.ToArray(),
            SpawnTimeArr = spawnTimeList.ToArray(),
            BeatsPerMinute = 120f,
            OffsetMilliseconds = 0,
            ApproachRate = ApproachRate.Normal
        };
        return beatmap;
    }

    //private Beatmap ReadOsuFile(StreamReader _reader, bool _readSliders = false)
    //{
    //    string line = string.Empty;
    //    List<float> positionXList = new List<float>();
    //    List<float> positionYList = new List<float>();
    //    List<double> timeList = new List<double>();
    //    while (_reader.EndOfStream == false)
    //    {
    //        ReadSliders(_reader, line);
    //        line = _reader.ReadLine();
    //        if (line == null) { break; }

    //        string[] lineParamsArr;
    //        lineParamsArr = line.Split(',');

    //        int FlipY = 384 - int.Parse(lineParamsArr[1]); // Flip Y axis
    //        int AdjustedX = Mathf.RoundToInt(Screen.height * 1.333333f); // Aspect Ratio
    //        float Slices = 8f;
    //        float PaddingX = AdjustedX / Slices;
    //        float PaddingY = Screen.height / Slices;
    //        float NewRangeX = ((AdjustedX - PaddingX) - PaddingX);
    //        float NewValueX = ((int.Parse(lineParamsArr[0]) * NewRangeX) / 512f) + PaddingX + ((Screen.width - AdjustedX) / 2f);
    //        float NewRangeY = Screen.height;
    //        float NewValueY = ((FlipY * NewRangeY) / 512f) + PaddingY;
    //        Vector3 MainPos = camera.ScreenToWorldPoint(new Vector3(NewValueX, NewValueY, 0)); // Convert from screen position to world position

    //        positionXList.Add(MainPos.x * 100);
    //        positionYList.Add(MainPos.y * 100);
    //        // Convert to milliseconds.
    //        timeList.Add(double.Parse(lineParamsArr[ParamTime]) / 1000);
    //    }

    //    List<Vector2> positionList = new List<Vector2>();
    //    for (int i = 0; i < positionXList.Count; i++)
    //    {
    //        positionList.Add(new Vector2(positionXList[i], positionYList[i]));
    //    }

    //    Beatmap beatmap = new Beatmap
    //    {
    //        ObjectCount = positionList.Count,
    //        PositionArr = positionList.ToArray(),
    //        TimeArr = timeList.ToArray(),
    //        BeatsPerMinute = 120f,
    //        OffsetMilliseconds = 0
    //    };
    //    return beatmap;
    //}


    //private Beatmap ReadOsuFile(StreamReader _reader, bool _readSliders = false)
    //{
    //    string line = string.Empty;
    //    List<float> positionXList = new List<float>();
    //    List<float> positionYList = new List<float>();
    //    List<double> timeList = new List<double>();
    //    while (_reader.EndOfStream == false)
    //    {
    //        ReadSliders(_reader, line);
    //        line = _reader.ReadLine();
    //        if (line == null) { break; }

    //        string[] lineParamsArr;
    //        lineParamsArr = line.Split(',');
    //        positionXList.Add(int.Parse(lineParamsArr[ParamX]));
    //        positionYList.Add(int.Parse(lineParamsArr[ParamY]));
    //        // Convert to milliseconds.
    //        timeList.Add(double.Parse(lineParamsArr[ParamTime]) / 1000);
    //    }

    //    List<Vector2> positionList = new List<Vector2>();
    //    for (int i = 0; i < positionXList.Count; i++)
    //    {
    //        positionList.Add(new Vector2(positionXList[i], positionYList[i]));
    //    }
    //    Beatmap beatmap = new Beatmap
    //    {
    //        ObjectCount = positionList.Count,
    //        PositionArr = positionList.ToArray(),
    //        TimeArr = timeList.ToArray(),
    //        BeatsPerMinute = 120f,
    //        OffsetMilliseconds = 0
    //    };
    //    return beatmap;
    //}
    private void ReadSliders(StreamReader _reader, string _line)
    {
        while (true)
        {
            _line = _reader.ReadLine();
            if (_line != null)
            {
                if (_line.Contains("|") == false)
                    break;
            }
            else
                break;
        }
    }
}

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

    public Beatmap LoadOsuFile()
    {
        var reader = new StreamReader(AssetDatabase.GetAssetPath(osuMapFile));
        ReadUntilHitObjectLine(reader);
        return ReadOsuFile(reader, false);
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
    private Beatmap ReadOsuFile(StreamReader _reader, bool _readSliders = false)
    {
        string line = string.Empty;
        List<float> positionXList = new List<float>();
        List<float> positionYList = new List<float>();
        List<double> timeList = new List<double>();
        while (_reader.EndOfStream == false)
        {
            ReadSliders(_reader, line);
            line = _reader.ReadLine();
            if (line == null) { break; }

            string[] lineParamsArr;
            lineParamsArr = line.Split(',');
            positionXList.Add(int.Parse(lineParamsArr[ParamX]));
            positionYList.Add(int.Parse(lineParamsArr[ParamY]));
            timeList.Add(double.Parse(lineParamsArr[ParamTime]));
        }

        List<Vector2> positionList = new List<Vector2>();
        for (int i = 0; i < positionXList.Count; i++)
        {
            positionList.Add(new Vector2(positionXList[i], positionYList[i]));
        }
        Beatmap beatmap = new Beatmap
        {
            PositionArr = positionList.ToArray(),
            TimeArr = timeList.ToArray(),
            BeatsPerMinute = 120f,
            OffsetMilliseconds = 0
        };
        return beatmap;
    }
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

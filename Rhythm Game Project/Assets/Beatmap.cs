using StaticDataScripts;
using System;

[Serializable]
public sealed class Beatmap
{
    public int ObjectCount { get; set; }
    public float[] PositionArrX { get; set; }
    public float[] PositionArrY { get; set; }
    public float BeatsPerMinute { get; set; }
    public float AudioStartTime { get; set; }
    public double[] SpawnTimeArr { get; set; }
    public double OffsetMilliseconds { get; set; }
    public string FolderName { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Creator { get; set; }
    public string Table { get; set; }
    public string ModeName { get; set; }
    public Mode Mode { get; set; }
    public ApproachRate ApproachRate { get; set; }
    public ObjectSize ObjectSize { get; set; }
    public DateTime CreatedDate { get; set; }
}
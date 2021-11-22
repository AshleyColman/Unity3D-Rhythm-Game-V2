using StaticDataScripts;
using System;
using UnityEngine;

[Serializable]
public sealed class Beatmap
{
    public int Id { get; set; }
    public int ObjectCount { get; set; }
    public Vector2[] PositionArr { get; set; }
    public float BeatsPerMinute { get; set; }
    public float AudioStartTime { get; set; }
    public double[] TimeArr { get; set; }
    public double OffsetMilliseconds { get; set; }
    public string FolderName { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Creator { get; set; }
    public string Table { get; set; }
    public Mode Mode { get; set; }
    public ApproachRate ApproachRate { get; set; }
    public ObjectSize ObjectSize { get; set; }
    public DateTime CreatedDate { get; set; }
}
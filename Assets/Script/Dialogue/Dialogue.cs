using System;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public string name;

    [TextArea(1, 10)] public string[] sentences;

    public bool isEnded;
}
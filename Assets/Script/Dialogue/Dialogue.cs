using System;
using UnityEngine;

<<<<<<< HEAD
[Serializable]
public class Dialogue
{
    public string name;

    [TextArea(1, 10)] public string[] sentences;

    public bool isEnded;
=======
namespace Script.Dialogue
{
    [Serializable]
    public class Dialogue
    {
        public string name;

        [TextArea(1, 10)] public string[] sentences;

        public bool isEnded;
    }
>>>>>>> main
}
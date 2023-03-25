using System;
using System.Collections.Generic;
using UnityEngine;

public class BloonHirachy : MonoBehaviour
{
    [Serializable]
    public class BloonInfo
    {
        public float speed = 1f;
        public float health = 1f;
        public Color color = Color.white;

        public int BloonIndex { get; set; }
    }

    public List<BloonInfo> Hirachy { get; private set; } = new();

    private void Start()
    {
        Hirachy.Clear();

        Hirachy.Add(red);
        red.BloonIndex = 0;

        Hirachy.Add(blue);
        blue.BloonIndex = 1;

        Hirachy.Add(green);
        green.BloonIndex = 2;

        Hirachy.Add(yellow);
        yellow.BloonIndex = 3;

        Hirachy.Add(pink);
        pink.BloonIndex = 4;
    }

    public BloonInfo red, blue, green, yellow, pink;

    public BloonInfo GetBloonInfo(BloonType type)
    {
        BloonInfo info = null;

        switch (type)
        {
            case BloonType.None:
                break;
            case BloonType.Red:
                info = red;
                break;
            case BloonType.Blue:
                info = blue;
                break;
            case BloonType.Green:
                info = green;
                break;
            case BloonType.Yellow:
                info = yellow;
                break;
            case BloonType.Pink:
                info = pink;
                break;
            case BloonType.Rainbow:
                break;
            case BloonType.Ceramic:
                break;
            default:
                break;
        }

        return info;
    }
}

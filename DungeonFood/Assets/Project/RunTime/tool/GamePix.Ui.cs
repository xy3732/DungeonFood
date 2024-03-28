using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePix.Ui
{
    // Ui Fade 방향
    public enum UiWay3
    {
        Top,
        Down,
        Left,
        Right,
        Center
    }

    // Ui 화면 위치
    public struct UiVector3
    {
        public static Vector3 Top { get { return new Vector3(0, Screen.height + 100, 0); } }
        public static Vector3 Down { get { return new Vector3(0, -(Screen.height + 100), 0); } }
        public static Vector3 Left { get { return new Vector3(-(Screen.width + 100), 0, 0); } }
        public static Vector3 Right { get { return new Vector3(Screen.width + 100, 0, 0); } }
        public static Vector3 Center { get { return Vector3.zero; } }
    }

    public struct UiColor
    { 
        public static Color AlphaOne { get { return new Color(1, 1, 1, 1); } }
        public static Color AlphaZero { get { return new Color(1, 1, 1, 0); } }
        public static Color32 white { get { return new Color(1, 1, 1, 1);} }
        public static Color32 Black { get { return new Color(0, 0, 0, 1); } }
        public static Color32 Clear { get { return new Color(0, 0, 0, 0); } }
        public static Color32 Cyan { get { return new Color(0, 1, 1, 1); } }
        public static Color32 Gray { get { return new Color(0.5f, 0.5f, 0.5f, 1); } }
        public static Color32 Green { get { return new Color(0, 1, 0, 1); } }
        public static Color32 Mangenta { get { return new Color(1, 0, 1, 1); } }
        public static Color32 Red { get { return new Color(1, 0, 0, 1); } }
        public static Color32 Yellow { get { return new Color(1, 1, 0, 1); } }
    }

}

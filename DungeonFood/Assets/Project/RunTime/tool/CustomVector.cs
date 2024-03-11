using UnityEngine;

namespace GamePix.CustomVector
{
    public struct FlipVector3
    {
        public static Vector3 Left { get { return new Vector3(-1f, 1f, 1f); } }
        public static Vector3 Right { get { return new Vector3(1f, 1f, 1f); } }

        public static Vector3 Default { get { return new Vector3(1f, 1f, 1f); } }
    }

    public enum UiWay3
    {
        Top,
        Down,
        Left,
        Right,
        Center
    }

    public enum UiEffectSelect
    {
        To,
        From
    }


    public struct UiVector3
    {
        public static Vector3 Top { get { return new Vector3(0, 1500, 0); } }
        public static Vector3 Down { get { return new Vector3(0, -1500, 0); } }
        public static Vector3 Left { get { return new Vector3(-2200, 0, 0); } }
        public static Vector3 Right { get { return new Vector3(2200, 0, 0); } }
        public static Vector3 Center { get { return Vector3.zero; } }
    }
}

using UnityEngine;

namespace Utility
{
    public static class Consts
    {
        public static readonly int BallShaderValue = Shader.PropertyToID("_Value");
        public static readonly int BallColor = Shader.PropertyToID("_BaseColor");

        public static readonly Vector3Int ScreenCenterPosition = new(Screen.width / 2, Screen.height / 2);
    }
}
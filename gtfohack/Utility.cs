using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace gtfohack
{
    public static class Utility
    {
        private static Texture2D _coloredLineTexture;
        private static Color _coloredLineColor;

        public static void DrawLine(Vector2 lineStart, Vector2 lineEnd, Color color)
        {
            DrawLine(lineStart, lineEnd, color, 1);
        }

        public static void DrawBox(float x, float y, float w, float h, Color color)
        {
            menu gfg = new menu();
            gfg.tofile("drawtest.ini", "blaaaaa1", true);
            DrawLine(new Vector2(x, y), new Vector2(x + w, y), color);
            gfg.tofile("drawtest.ini", "blaaaaa1", true);
            DrawLine(new Vector2(x, y), new Vector2(x, y + h), color);
            gfg.tofile("drawtest.ini", "blaaaaa1", true);
            DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), color);
            gfg.tofile("drawtest.ini", "blaaaaa1", true);
            DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), color);
            gfg.tofile("drawtest.ini", "blaaaaa1", true);
        }

        public static void DrawLine(Vector2 lineStart, Vector2 lineEnd, Color color, int thickness)
        {
            menu gfg = new menu();
            gfg.tofile("drawtest.ini", "wwwwwww1", true);
            if (_coloredLineTexture == null || _coloredLineColor != color)
            {
                _coloredLineColor = color;
                _coloredLineTexture = new Texture2D(1, 1);
                _coloredLineTexture.SetPixel(0, 0, _coloredLineColor);
                _coloredLineTexture.wrapMode = 0;
                _coloredLineTexture.Apply();
            }
            gfg.tofile("drawtest.ini", "wwwwwww2", true);
            DrawLineStretched(lineStart, lineEnd, _coloredLineTexture, thickness);
            gfg.tofile("drawtest.ini", "wwwwwww3", true);
        }

        public static void DrawLineStretched(Vector2 lineStart, Vector2 lineEnd, Texture2D texture, int thickness)
        {
            menu gfg = new menu();
            gfg.tofile("drawtest.ini", "zzzzzz1", true);
            var vector = lineEnd - lineStart;
            float pivot = 57.29578f * Mathf.Atan(vector.y / vector.x);
            gfg.tofile("drawtest.ini", "pivot:"  + pivot, true);
            if (vector.x < 0f)
            {
                pivot += 180f;
            }

            if (thickness < 1)
            {
                thickness = 1;
            }

            int yOffset = (int)Mathf.Ceil((float)(thickness / 2));
            gfg.tofile("drawtest.ini", "zzzzzz2", true);
            GUIUtility.RotateAroundPivot(pivot, lineStart);
            gfg.tofile("drawtest.ini", "zzzzzz3", true);
            GUI.DrawTexture(new Rect(lineStart.x, lineStart.y - (float)yOffset, vector.magnitude, (float)thickness), texture);
            gfg.tofile("drawtest.ini", "zzzzzz4", true);
            GUIUtility.RotateAroundPivot(-pivot, lineStart);
            gfg.tofile("drawtest.ini", "zzzzzz5", true);
        }

        public static void DrawLine(Vector2 lineStart, Vector2 lineEnd, Texture2D texture)
        {
            menu gfg = new menu();
            gfg.tofile("drawtest.ini", "dsdffs1", true);
            DrawLine(lineStart, lineEnd, texture, 1);
        }

        public static void DrawLine(Vector2 lineStart, Vector2 lineEnd, Texture2D texture, int thickness)
        {
            menu gfg = new menu();
            var vector = lineEnd - lineStart;
            float pivot = 57.29578f * Mathf.Atan(vector.y / vector.x);
            gfg.tofile("drawtest.ini", "dsdffs2", true);
            if (vector.x < 0f)
            {
                pivot += 180f;
            }

            if (thickness < 1)
            {
                thickness = 1;
            }

            int num2 = (int)Mathf.Ceil((float)(thickness / 2));
            var rect = new Rect(lineStart.x, lineStart.y - (float)num2, Vector2.Distance(lineStart, lineEnd), (float)thickness);
            GUIUtility.RotateAroundPivot(pivot, lineStart);
            GUI.BeginGroup(rect);
            gfg.tofile("drawtest.ini", "dsdffs3", true);
            int num3 = Mathf.RoundToInt(rect.width);
            int num4 = Mathf.RoundToInt(rect.height);

            for (int i = 0; i < num4; i += texture.height)
            {
                for (int j = 0; j < num3; j += texture.width)
                {
                    GUI.DrawTexture(new Rect((float)j, (float)i, (float)texture.width, (float)texture.height), texture);
                }
            }
            gfg.tofile("drawtest.ini", "dsdffs4", true);
            GUI.EndGroup();
            GUIUtility.RotateAroundPivot(-pivot, lineStart);
        }
    }
}

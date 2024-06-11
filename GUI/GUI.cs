using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace LC_Internal
{
    static internal class DrawUtilities
    {
        private static Texture2D lineTex;

        internal static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
        {
            Matrix4x4 matrix = GUI.matrix;
            if (!lineTex)
                lineTex = new Texture2D(1, 1);

            Color color2 = GUI.color;
            GUI.color = color;
            float num = Vector3.Angle(pointB - pointA, Vector2.right);

            if (pointA.y > pointB.y)
                num = -num;

            GUIUtility.ScaleAroundPivot(new Vector2((pointB - pointA).magnitude, width), new Vector2(pointA.x, pointA.y + 0.5f));
            GUIUtility.RotateAroundPivot(num, pointA);
            GUI.DrawTexture(new Rect(pointA.x, pointA.y, 1f, 1f), lineTex);
            GUI.matrix = matrix;
            GUI.color = color2;
        }

        internal static void DrawLabel(Vector2 position, string label, Color colour)
        {
            GUIStyle labelStyle = new(GUI.skin.label)
            {
                fontStyle = FontStyle.Bold
            };

            Vector2 size = labelStyle.CalcSize(new GUIContent(label));
            Vector2 newPosition = position - (size * 0.5f);

            labelStyle.normal.textColor = Color.black;
            GUI.Label(new Rect(newPosition.x, newPosition.y, size.x, size.y), label, labelStyle);

            labelStyle.normal.textColor = colour;
            GUI.Label(new Rect(newPosition.x + 1, newPosition.y + 1, size.x, size.y), label, labelStyle);
        }

        internal static Vector3[] BoundsToCorners(Bounds bounds)
        {

            Vector3[] corners =
                    [
            new Vector3(bounds.min.x, bounds.min.y, bounds.min.z), // Bottom-Front-Left Corner:
            new Vector3(bounds.min.x, bounds.min.y, bounds.max.z), // Bottom-Back-Left Corner:
            new Vector3(bounds.max.x, bounds.min.y, bounds.max.z), // Bottom-Back-Right Corner:
            new Vector3(bounds.max.x, bounds.min.y, bounds.min.z), // Bottom-Front-Right Corner:

            new Vector3(bounds.min.x, bounds.max.y, bounds.min.z), // Top-Front-Left Corner:
            new Vector3(bounds.min.x, bounds.max.y, bounds.max.z), // Top-Back-Left Corner:
            new Vector3(bounds.max.x, bounds.max.y, bounds.max.z), // Top-Back-Right Corner:
            new Vector3(bounds.max.x, bounds.max.y, bounds.min.z), // Top-Front-Right Corner:
           
                    ];

            return corners;

        }


    }
}

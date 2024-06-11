using System.Runtime.CompilerServices;
using UnityEngine;

namespace LC_Internal
{
    internal partial class Helper
    {
#nullable enable

        internal static Camera? CurrentCamera =>
                Helper.LocalPlayer?.gameplayCamera is Camera { enabled: true } gameplayCamera
                    ? gameplayCamera
                    : Helper.StartOfRound?.spectateCamera;
#nullable disable


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector3 WorldToScreen(this Camera camera, Vector3 worldPosition)
        {

            // Convert world position to viewport position
            // where (0, 0) is the bottom-left and (1, 1) is the top-right of the viewport.
            Vector3 screen = camera.WorldToViewportPoint(worldPosition);

            // Scale to Screen Coordinates:
            screen.x *= Screen.width;
            screen.y *= Screen.height;

            // This flips the y-coordinate because in screen space, the origin (0, 0) is at the top-left,
            //while in viewport space, the origin (0, 0) is at the bottom-left.
            screen.y = Screen.height - screen.y;

            return screen;
        }





    }
}

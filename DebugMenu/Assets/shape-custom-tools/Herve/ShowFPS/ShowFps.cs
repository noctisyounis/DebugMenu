using Shapes;
using UnityEngine;
using UnityEngine.UI;

public class ShowFps : ImmediateModeShapeDrawer
{
    #region Utils

    public static void DisplayFps()
    {
        Camera cam = Camera.main;
        using (Draw.Command(cam))
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            var pos = cam.ScreenToViewportPoint(new Vector3(1410, 1010, 1));
            var goodPos = cam.ViewportToWorldPoint(pos);
            Draw.Text(goodPos, cam.transform.forward, $"Framerate: {fps} FPS", TextAlign.BottomLeft, 0.5f, Color.red);            
        }
    }

    #endregion
}
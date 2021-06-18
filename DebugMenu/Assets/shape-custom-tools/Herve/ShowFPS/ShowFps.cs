using Shapes;
using UnityEngine;
using UnityEngine.UI;

public class ShowFps : ImmediateModeShapeDrawer
{
    #region Unity API

    private void Update()
    {
        if (!_isShowingFps) return;
        DisplayFps();
    }

    #endregion


    #region Utils

    public static void SetShowFps()
    {
        _isShowingFps = !_isShowingFps;
    }

    public static void DisplayFps()
    {
        Camera cam = Camera.main;
        using (Draw.Command(cam))
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            var pos = cam.ScreenToViewportPoint(new Vector3(cam.pixelWidth - 20, cam.pixelHeight - 20, 1));
            var goodPos = cam.ViewportToWorldPoint(pos);
            Draw.Text(goodPos, cam.transform.forward, $"Framerate: {fps} FPS", TextAlign.TopRight, 0.5f, Color.red);            
        }
    }

    #endregion


    #region Private and Protected

    public static bool _isShowingFps;

    #endregion
}
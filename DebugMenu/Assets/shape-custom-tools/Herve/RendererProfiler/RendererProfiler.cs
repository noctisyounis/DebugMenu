using System.Text;
using Unity.Profiling;
using UnityEngine;
using Shapes;
using DebugAttribute;

public class RendererProfiler : ImmediateModeShapeDrawer
{
    #region Unity API

    void Update()
    {
        var sb = new StringBuilder(500);
        if (_setPassCallsRecorder.Valid)
            sb.AppendLine($"SetPass Calls: {_setPassCallsRecorder.LastValue}");
        if (_drawCallsRecorder.Valid)
            sb.AppendLine($"Draw Calls: {_drawCallsRecorder.LastValue}");
        if (_verticesRecorder.Valid)
            sb.AppendLine($"Vertices: {_verticesRecorder.LastValue}");
        _statsText = sb.ToString();
        if (!_isShowingProfiler) return;
        ShowRendererProfiler();
    }

    #endregion


    #region Utils

    [DebugMenu("Settings/Performances/Renderer Profiler")]
    public static void SetShowProfiler()
    {
        _isShowingProfiler = !_isShowingProfiler;
    }

    private void ShowRendererProfiler()
    {
        _setPassCallsRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "SetPass Calls Count");
        _drawCallsRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
        _verticesRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Vertices Count");

        Camera cam = Camera.main;
        using (Draw.Command(cam))
        {
            var pos = cam.ScreenToViewportPoint(new Vector3(cam.pixelWidth - 20, cam.pixelHeight - 20, 1));
            var goodPos = cam.ViewportToWorldPoint(pos);
            Draw.Text(goodPos, cam.transform.forward, _statsText, TextAlign.TopRight, 0.5f, Color.red);
        }
    }

    #endregion


    #region Private and Protected

    private static bool _isShowingProfiler;
    private string _statsText;
    private ProfilerRecorder _setPassCallsRecorder;
    private ProfilerRecorder _drawCallsRecorder;
    private ProfilerRecorder _verticesRecorder;

    #endregion
}
using UnityEngine;
using Unity.Profiling;
using System.Text;
using Shapes;
using DebugAttribute;

public class RenderCountProfiler : ImmediateModeShapeDrawer
{
    #region Unity API

    private void Update()
    {       
        var sb = new StringBuilder(500);
        if (_batchesCount.Valid)
            sb.AppendLine($"Batches Count: {_batchesCount.LastValue}");
        if (_renderTexturesCount.Valid)
            sb.AppendLine($"Render Textures Count: {_renderTexturesCount.LastValue}");
        if (_indexBufferUploadInFrameCount.Valid)
            sb.AppendLine($"Index Buffer Upload In Frame Count: {_indexBufferUploadInFrameCount.LastValue}");
        if (_shadowCastersCount.Valid)
            sb.AppendLine($"Shadow Casters Count: {_shadowCastersCount.LastValue}");
        _statsText = sb.ToString();
        if (!_isShowingProfiler) return;
        ShowCountProfiler();
    }

    #endregion


    #region Utils

    [DebugMenu("Settings/Performances/Batches-Render Count Profiler")]
    public static void SetShowProfiler()
    {
        _isShowingProfiler = !_isShowingProfiler;
    }

    private static void ShowCountProfiler()
    {       
        _batchesCount = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Batches Count");
        _renderTexturesCount = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Render Textures Count");
        _indexBufferUploadInFrameCount = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Index Buffer Upload In Frame Count");
        _shadowCastersCount = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Shadow Casters Count");

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
    private static string _statsText;
    private static ProfilerRecorder _batchesCount;
    private static ProfilerRecorder _renderTexturesCount;
    private static ProfilerRecorder _indexBufferUploadInFrameCount;
    private static ProfilerRecorder _shadowCastersCount;

    #endregion
}
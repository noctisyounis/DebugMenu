using Shapes;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using System.Text;

public class MemoryProfiler : ImmediateModeShapeDrawer
{
    #region Unity API

    private void Update()
    {       
        var sb = new StringBuilder(500);
        if (_totalReservedMemoryRecorder.Valid)
            sb.AppendLine($"Total Reserved Memory: {_totalReservedMemoryRecorder.LastValue}");
        if (_gcReservedMemoryRecorder.Valid)
            sb.AppendLine($"GC Reserved Memory: {_gcReservedMemoryRecorder.LastValue}");
        if (_textureMemoryRecorder.Valid)
            sb.AppendLine($"Texture Used Memory: {_textureMemoryRecorder.LastValue}");
        if (_meshMemoryRecorder.Valid)
            sb.AppendLine($"Mesh Used Memory: {_meshMemoryRecorder.LastValue}");
        _statsText = sb.ToString();
        if (!_isShowingProfiler) return;
        ShowMemoryProfiler();
    }

    #endregion


    #region Utils

    public static void SetShowProfiler()
    {
        _isShowingProfiler = !_isShowingProfiler;
    }

    private static void ShowMemoryProfiler()
    {       
        _totalReservedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Reserved Memory");
        _gcReservedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
        _textureMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Texture Memory");
        _meshMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Mesh Memory");

        Camera cam = Camera.main;
        using (Draw.Command(cam))
        {
            var pos = cam.ScreenToViewportPoint(new Vector3(1000, 850, 1));
            var goodPos = cam.ViewportToWorldPoint(pos);
            Draw.Text(goodPos, cam.transform.forward, _statsText, TextAlign.BottomLeft, 0.5f, Color.red);
        }     
    }

    #endregion    


    #region Private and Protected

    private static bool _isShowingProfiler;
    private static string _statsText;
    private static ProfilerRecorder _totalReservedMemoryRecorder;
    private static ProfilerRecorder _gcReservedMemoryRecorder;
    private static ProfilerRecorder _textureMemoryRecorder;
    private static ProfilerRecorder _meshMemoryRecorder;

    #endregion
}

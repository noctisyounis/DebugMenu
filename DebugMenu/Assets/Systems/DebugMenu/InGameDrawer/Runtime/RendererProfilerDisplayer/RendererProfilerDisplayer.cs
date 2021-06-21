using UnityEngine;
using Unity.Profiling;
using System.Text;
using Shapes;
using DebugAttribute;

namespace DebugMenu.InGameDrawer.RendererProfilerDisplayer
{
    public class RendererProfilerDisplayer : ImmediateModeShapeDrawer
    {
        #region Unity API

        private void Update()
        {
            if (!_isShowingProfiler) return;
            
            RefreshStatistics();
            BuildStatisticsString();
            DisplayRendererProfiler();
        }

        #endregion


        #region Main

        [DebugMenu("Settings/Performances/Renderer Profiler")]
        public static void ToggleRendererProfilerDisplay()
        {
            _isShowingProfiler = !_isShowingProfiler;
        }

        private void DisplayRendererProfiler()
        {
            Camera camera = Camera.main;
            using (Draw.Command(camera))
            {
                var screenPosition = new Vector3(camera.pixelWidth - 20, camera.pixelHeight - 20, 1);
                var viewportPosition = camera.ScreenToViewportPoint(screenPosition);
                var worldPosition = camera.ViewportToWorldPoint(viewportPosition);

                Draw.Text(worldPosition, camera.transform.forward, _statsText, TextAlign.TopRight, 0.5f, Color.red);
            }
        }

        private void RefreshStatistics()
        {
            _passCallsRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "SetPass Calls Count");
            _drawCallsRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
            _verticesRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Vertices Count");
        }

        private void BuildStatisticsString()
        {
            var stringBuilder = new StringBuilder(500);
            if (_passCallsRecorder.Valid)
            {
                stringBuilder.AppendLine($"SetPass Calls: {_passCallsRecorder.LastValue}");
            }

            if (_drawCallsRecorder.Valid)
            {
                stringBuilder.AppendLine($"Draw Calls: {_drawCallsRecorder.LastValue}");
            }

            if (_verticesRecorder.Valid)
            {
                stringBuilder.AppendLine($"Vertices: {_verticesRecorder.LastValue}");
            }    
            
            _statsText = stringBuilder.ToString();
        }

        #endregion


        #region Private and Protected

        private static bool _isShowingProfiler;
        private string _statsText;
        private ProfilerRecorder _passCallsRecorder;
        private ProfilerRecorder _drawCallsRecorder;
        private ProfilerRecorder _verticesRecorder;

        #endregion
    }
}
using UnityEngine;
using Shapes;
using DebugAttribute;

namespace DebugMenu.InGameDrawer.FPSDisplayer
{
    public class FPSDisplayer : ImmediateModeShapeDrawer
    {
        #region Unity API

        private void Update()
        {
            if (!_isShowingFps) return;

            RefreshFPS();
            DisplayFPS();
        }

        #endregion


        #region Main

        [DebugMenu("Settings/Performances/Show Framerate")]
        public static void ToggleFPSDisplay()
        {
            _isShowingFps = !_isShowingFps;
        }

        private void RefreshFPS()
        {
            _fps = (int)(1f / Time.unscaledDeltaTime);
        }

        private void DisplayFPS()
        {
            Camera camera = Camera.main;
            using (Draw.Command(camera))
            {
                var screenPosition = new Vector3(camera.pixelWidth - 20, camera.pixelHeight - 20, 1);
                var viewportPosition = camera.ScreenToViewportPoint(screenPosition);
                var worldPosition = camera.ViewportToWorldPoint(viewportPosition);

                Draw.Text(worldPosition, camera.transform.forward, $"Framerate: {_fps} FPS", TextAlign.TopRight, 0.5f, Color.red);            
            }
        }

        #endregion


        #region Private Fields

        private static bool _isShowingFps;
        private int _fps;

        #endregion
    }    
}

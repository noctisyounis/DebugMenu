using UnityEngine;

namespace DebugUI
{
    public class DebugMenuVisibility : MonoBehaviour
    {
        #region Main

        public void Hide()
        {
            Debug.Log("Hide");
            gameObject.SetActive(false);
        }

        public void Show()
        {
            Debug.Log("Show");
            gameObject.SetActive(true);
        }

        #endregion Main
    }
}
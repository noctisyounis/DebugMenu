
using UnityEngine;
using UnityEngine.EventSystems;

namespace DebugUI
{
    public class NavigationManager : MonoBehaviour
    {

        #region exposed

        [SerializeField]
        private EventSystem _event;

        #endregion


        #region Unity API

        void Update()
        {
            if (Input.GetButtonDown("Submit"))
            {
                _event.firstSelectedGameObject = DebugMenu.m_menuDebugButton[0].gameObject;
            }
        }

        #endregion
    }
}
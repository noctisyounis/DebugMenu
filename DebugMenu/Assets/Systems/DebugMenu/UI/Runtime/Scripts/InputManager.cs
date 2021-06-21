using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DebugUI
{
    public class InputManager : MonoBehaviour
    {
        #region Exposed

        public UnityEvent OnTripleClick;
        public UnityEvent OnHideDebugMenu;

        #endregion


        #region Unity API

        private void Update()
        {
            ShowDebugMenuOnClick();
            HideDebugMenu();
        }

        #endregion

        #region Utils

        private void ShowDebugMenuOnClick()
        {
            if (Input.GetButtonDown("ShowDebugMenu"))
            {
                _lastPressTime = Time.time;

                if ((Time.time - _lastPressTime) < _buttonPressSpeed)
                {
                    _clickCount++;
                    if (_clickCount >= 3)
                    {
                        OnTripleClick.Invoke();
                        _clickCount = 0;
                    }
                }
            }

            if ((Time.time - _lastPressTime) > _buttonPressSpeed)
            {
                if (_clickCount == 2)
                {
                    _clickCount = 0;
                }
                else if (_clickCount == 1)
                {
                    _clickCount = 0;
                }
                _clickCount = 0;
            }
        }

        private void HideDebugMenu()
        {
            if (Input.GetButtonDown("ExitDebugMenu"))
            {
                OnHideDebugMenu.Invoke();
            }
        }


        #endregion


        #region Privates

        private int _clickCount = 0;
        private float _buttonPressSpeed = 0.4f;
        private float _lastPressTime = -10f;

        #endregion
    }
}
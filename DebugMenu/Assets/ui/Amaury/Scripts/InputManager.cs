using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Unity API

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        ShowDebugMenuOnClick();
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
                    gameObject.SetActive(true);
                    _clickCount = 0;
                }
            }
        }

        if ((Time.time - _lastPressTime) > _buttonPressSpeed)
        {
            if (_clickCount == 2)
            {
                Debug.Log("GrosNul");
                _clickCount = 0;
            }
            else if (_clickCount == 1)
            {
                Debug.Log("GrosNul1");
                _clickCount = 0;
            }
            _clickCount = 0;
        }
    }

    private void HideDebugMenu()
    {

    }


    #endregion


    #region Privates
    private int _clickCount = 0;
    private float _buttonPressSpeed = 0.2f;
    private float _lastPressTime = -10f;
    #endregion
}

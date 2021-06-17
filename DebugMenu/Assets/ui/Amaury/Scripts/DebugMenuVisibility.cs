using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    #endregion
}

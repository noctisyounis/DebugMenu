using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DebugMenu;

public class GameDebug : MonoBehaviour
{
    public string Path{get; set;}

    public void OnClick()
    {
        DebugCall.InvokeMethod(Path);
    }
}

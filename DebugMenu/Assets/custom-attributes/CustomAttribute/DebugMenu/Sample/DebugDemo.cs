using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DebugMenu;

public class DebugDemo : MonoBehaviour
{
    public void OnClick()
    {
        DebugCall.InvokeMethod("Debug/Test/Static");

        Debug.Log(DebugCall.InvokeMethod<int>("Debug/Test/Return"));

        DebugCall.InvokeMethod("Debug/Test/Parameter", new object[] { "5", 4 });

        Debug.Log(DebugCall.InvokeMethod<string>("Debug/Test/Parameter And Return", new object[] { "Bonjour" }));
    }

    [DebugMenu("Debug/Test/Instance")]
    public static void HelloWorld()
    {
        Debug.Log("Hello World from instance");
    }

    [DebugMenu("Debug/Test/Static")]
    public static string HelloStaticWorld()
    {
        string result = "Hello World from Static";
        Debug.Log(result);
        return result;
    }

    [DebugMenu("Debug/Test/Return")]
    public static int ReturnTwo()
    {
        return 2;
    }

    [DebugMenu("Debug/Test/Parameter")]
    public static void PrintParam(string value, int myvalue)
    {
        Debug.Log($"value string = {value}, int value = {myvalue}");
    }

    [DebugMenu("Debug/Test/Parameter And Return")]
    public static string ReturnParam(string value)
    {
        return value;
    }
}
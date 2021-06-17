using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DebugMenu;

using static UnityEngine.Debug;

public class DebugDemo : MonoBehaviour
{
    public void OnClick()
    {
        Log($"<color=orange>How to interface with API Examples</color>");

        Log($"<color=orange>Always Call Static method</color>");
        DebugCall.InvokeMethod("Debug/Test/Static");

        Log($"<color=orange>Call a method with a return</color>");
        Log(DebugCall.InvokeMethod<int>("Debug/Test/Return"));

        Log($"You can Set your parameters");
        DebugCall.InvokeMethod("Debug/Test/Parameter", new object[] { "your string", 4 });

        Log($"You can set your parameters and even have a return");
        Log(DebugCall.InvokeMethod<string>("Debug/Test/Parameter And Return", new object[] { "Bonjour" }));
    }

    [DebugMenu("Debug/Test/Instance")]
    public void HelloWorld()
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
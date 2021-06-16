using UnityEditor;
using DebugMenu;

public class DebugDictionaryValidation
{
    [MenuItem("Debug/Validate methods")]
    public static void TryValidate()
    {
        DebugCall.ValidateMethods();
    }
}

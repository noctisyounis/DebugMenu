#if UNITY_EDITOR
using UnityEditor;
#endif

using DebugAttribute;

public class DebugDictionaryValidation
{

#if UNITY_EDITOR
    [MenuItem("Debug/Validate methods")]
#endif

    public static void TryValidate()
    {
        DebugAttributeRegistry.ValidateMethods();
    }
}

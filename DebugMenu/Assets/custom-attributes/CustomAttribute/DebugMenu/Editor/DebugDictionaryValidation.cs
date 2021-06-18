using UnityEditor;
using DebugAttribute;

public class DebugDictionaryValidation
{
    [MenuItem("Debug/Validate methods")]
    public static void TryValidate()
    {
        DebugAttributeRegistry.ValidateMethods();
    }
}

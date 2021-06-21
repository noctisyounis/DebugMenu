#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Diagnostics;
using DebugAttribute;

namespace DebugMenu.CustomAttribute.Editor
{
    public class DictionaryValidatorMenuItem
    {

#if UNITY_EDITOR
        [MenuItem("Debug Menu/Validate Methods")]
        [Conditional("DEBUG")]
#endif
        public static void TryValidate()
        {
            DebugAttributeRegistry.ValidateMethods();
            
            
        } 
    }
}
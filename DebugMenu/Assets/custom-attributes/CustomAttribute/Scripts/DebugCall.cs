using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace DebugMenu
{
    public class DebugCall
    {
        #region Main
        
        [MenuItem("Debug/Validate methods")]
        public static void ValidateMethods()
        {
            InitializeDictionnary();
            ValidateDictionary();
        }

        public static void InvokeMethod(string path)
        {
            if(!Methods.ContainsKey(path) || Methods[path].IsPrivate) return;
            
            var method = Methods[path];
            try
            {
                method.Invoke(method.ReflectedType, new object[0]);
            }
            catch(Exception e)
            {
                Debug.LogError(e.StackTrace);
            }
        }

        public static T InvokeMethod<T>(string path)
        {
            if(!Methods.ContainsKey(path) || Methods[path].IsPrivate) return default(T);
            
            var result = default(T);
            var method = Methods[path];
            try
            {
                result = (T)method.Invoke(method.ReflectedType, new object[0]);
            }
            catch(Exception e)
            {
                Debug.LogError(e.StackTrace);
            }

            return result;
        }

        public static void InvokeMethod(string path, object[] parameters)
        {
            if(!Methods.ContainsKey(path) || Methods[path].IsPrivate) return;
            
            var method = Methods[path];
            try
            {
                method.Invoke(method.ReflectedType, parameters);
            }
            catch(Exception e)
            {
                Debug.LogError(e.StackTrace);
            }
        }

        public static T InvokeMethod<T>(string path, object[] parameters)
        {
            if(!Methods.ContainsKey(path) || Methods[path].IsPrivate) return default(T);
            
            var result = default(T);
            var method = Methods[path];
            try
            {
                result = (T)method.Invoke(method.ReflectedType, parameters);
            }
            catch(Exception e)
            {
                Debug.LogError(e.StackTrace);
            }

            return result;
        }

        public static string[] GetPaths()
        {
            return Methods.Keys.ToArray();
        }

        public static string[] GetQuickPaths()
        {
            var result = new List<string>();

            foreach (var item in Methods)
            {
                if(item.Value.GetCustomAttribute<DebugMenuAttribute>().IsQuickMenu)
                {
                    result.Add(item.Key);
                }
            }

            return result.ToArray();
        }

        private static void InitializeDictionnary()
        {
            _methods = new MergeableDictionary<string, MethodInfo>();

            for (int i = 0; i < Assemblies.Length; i++)
            {
                var assembly = Assemblies[i];
                var assemblyDictionary = assembly
                            .GetTypes()
                            .SelectMany(classType => classType.GetMethods())
                            .Where(classMethod => classMethod.GetCustomAttributes().OfType<DebugMenuAttribute>().Any())
                            .ToDictionary(methodInfo => methodInfo.GetCustomAttributes().OfType<DebugMenuAttribute>().First<DebugMenuAttribute>().Path);
                              
                _methods.Merge(assemblyDictionary);
            }
        }

        private static void ValidateDictionary()
        {
            var validCount = 0;
            var initialMethodCount = _methods.Count;

            for (int i = _methods.Count-1; i >= 0; i--)
            {
                var item = _methods.Keys.ToArray()[i];

                if(!_methods[item].IsStatic)
                {
                    Debug.LogError($"<color=orange>{_methods[item].Name} of class {_methods[item].ReflectedType} must be static</color>");
                    _methods.Remove(item);
                }else
                {
                    validCount++;
                }
            }

            var multiplicity = (validCount > 0) ? $"{validCount} {(validCount > 1 ? "were" : "was")}" : "none was";

            Debug.Log($"<color=cyan>{initialMethodCount} methods were tested and {multiplicity} valid</color>");
        }

        private static void InitializeAssemblies()
        {
            _assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
        }

        #endregion


        #region Private

        private static Assembly[] _assemblies;

        private static Assembly[] Assemblies
        {
            get
            {
                if(_assemblies == null)
                {
                    InitializeAssemblies();
                }

                return _assemblies;
            }
        }
        private static MergeableDictionary<string, MethodInfo> _methods;
        private static MergeableDictionary<string, MethodInfo> Methods
        {
            get 
            {
                if(_methods == null)
                {
                    InitializeDictionnary();
                    ValidateDictionary();
                }

                return _methods;
            }
        }

        #endregion

    }
}
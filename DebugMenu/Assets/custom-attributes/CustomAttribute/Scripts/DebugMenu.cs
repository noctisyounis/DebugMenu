using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DebugMenu
{
    [AttributeUsage(AttributeTargets.Method,
                    Inherited = true, 
                    AllowMultiple = false)]
    public class DebugMenuAttribute : Attribute
    {
        #region Public
        public string Path{
            get
            {
                return _path;
            } 
        }

        public bool IsQuickMenu{get; set;}

        #endregion


        #region Main

        public DebugMenuAttribute(string path)
        {
            _path = path;
        }

        #endregion


        #region Private

        private string _path;

        #endregion
    }
}
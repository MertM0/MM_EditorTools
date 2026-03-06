using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Provides a scene picker dropdown for string or int fields.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_Scene]
    /// public string sceneName = "";
    /// 
    /// [MM_Scene]
    /// public int sceneIndex = 0;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_SceneAttribute : PropertyAttribute
    {
        #region Constructor
        
        /// <summary>
        /// Creates a scene picker
        /// </summary>
        public MM_SceneAttribute()
        {
        }
        
        #endregion
    }
}

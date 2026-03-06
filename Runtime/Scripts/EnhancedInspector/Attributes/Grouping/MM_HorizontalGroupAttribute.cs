using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Arranges properties horizontally in the Inspector.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_HorizontalGroup("Position")]
    /// public float x = 0f;
    /// 
    /// [MM_HorizontalGroup("Position")]
    /// public float y = 0f;
    /// 
    /// [MM_HorizontalGroup("Position")]
    /// public float z = 0f;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_HorizontalGroupAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Name of the horizontal group
        /// </summary>
        public string GroupName { get; private set; }
        
        /// <summary>
        /// Width of each element (0 = auto)
        /// </summary>
        public float Width { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Creates a horizontal group
        /// </summary>
        /// <param name="groupName">Name of the horizontal group</param>
        /// <param name="width">Width of each element (0 = auto)</param>
        public MM_HorizontalGroupAttribute(string groupName, float width = 0f)
        {
            GroupName = groupName;
            Width = width;
        }
        
        #endregion
    }
}

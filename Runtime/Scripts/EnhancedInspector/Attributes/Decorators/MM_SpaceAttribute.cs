using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Adds vertical space before a field in the Inspector.
    /// </summary>
    /// <example>
    /// <code>
    /// public int health = 100;
    /// 
    /// [MM_Space(20)]
    /// public float speed = 5f;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class MM_SpaceAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Height of the space in pixels
        /// </summary>
        public float Height { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Creates vertical space
        /// </summary>
        /// <param name="height">Height in pixels (default: 10)</param>
        public MM_SpaceAttribute(float height = 10f)
        {
            Height = height;
        }
        
        #endregion
    }
}

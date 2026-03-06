using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Displays a layer dropdown for integer fields in the Inspector.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_Layer]
    /// public int targetLayer = 0;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_LayerAttribute : PropertyAttribute
    {
        #region Constructor
        
        /// <summary>
        /// Creates a layer dropdown field
        /// </summary>
        public MM_LayerAttribute()
        {
        }
        
        #endregion
    }
}

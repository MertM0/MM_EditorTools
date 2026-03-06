using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Displays a tag dropdown for string fields in the Inspector.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_Tag]
    /// public string playerTag = "Player";
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_TagAttribute : PropertyAttribute
    {
        #region Constructor
        
        /// <summary>
        /// Creates a tag dropdown field
        /// </summary>
        public MM_TagAttribute()
        {
        }
        
        #endregion
    }
}

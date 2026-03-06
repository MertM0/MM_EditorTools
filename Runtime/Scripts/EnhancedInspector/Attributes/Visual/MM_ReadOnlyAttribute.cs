using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Makes this field read-only in the Inspector.
    /// Value can still be modified through code.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_ReadOnly]
    /// public int currentScore = 0;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_ReadOnlyAttribute : PropertyAttribute
    {
        #region Constructor
        
        /// <summary>
        /// Makes field read-only in Inspector
        /// </summary>
        public MM_ReadOnlyAttribute()
        {
        }
        
        #endregion
    }
}

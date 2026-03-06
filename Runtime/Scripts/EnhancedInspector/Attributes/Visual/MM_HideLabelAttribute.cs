using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Hides the label of a field in the Inspector.
    /// Useful for fields that don't need labels.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_HideLabel]
    /// public string description = "No label shown";
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_HideLabelAttribute : PropertyAttribute
    {
        #region Constructor
        
        /// <summary>
        /// Hides the field label
        /// </summary>
        public MM_HideLabelAttribute()
        {
        }
        
        #endregion
    }
}

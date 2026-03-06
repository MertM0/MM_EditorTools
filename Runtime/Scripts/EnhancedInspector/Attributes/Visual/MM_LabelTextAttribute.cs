using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Displays a custom label for a field in the Inspector.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_LabelText("Player HP")]
    /// public int health = 100;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_LabelTextAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Custom label text
        /// </summary>
        public string Label { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Sets custom label for the field
        /// </summary>
        /// <param name="label">Custom label text</param>
        public MM_LabelTextAttribute(string label)
        {
            Label = label;
        }
        
        #endregion
    }
}

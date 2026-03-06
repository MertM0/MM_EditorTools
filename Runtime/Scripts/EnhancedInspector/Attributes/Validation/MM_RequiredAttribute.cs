using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Displays a warning in the Inspector if this field is null or empty.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_Required]
    /// public GameObject player;
    /// 
    /// [MM_Required]
    /// public string playerName;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_RequiredAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Custom message to display when field is empty
        /// </summary>
        public string Message { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Marks field as required
        /// </summary>
        /// <param name="message">Custom warning message (optional)</param>
        public MM_RequiredAttribute(string message = "This field is required!")
        {
            Message = message;
        }
        
        #endregion
    }
}

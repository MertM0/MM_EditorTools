using System;
using UnityEngine;

namespace MM.EditorTools.EnhancedInspector
{
    /// <summary>
    /// Displays warning if reference field is null.
    /// </summary>
    /// <example>
    /// <code>
    /// [MM_NotNull]
    /// public Transform target;
    /// 
    /// [MM_NotNull("Player reference is required!")]
    /// public GameObject player;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MM_NotNullAttribute : PropertyAttribute
    {
        #region Fields
        
        /// <summary>
        /// Custom error message
        /// </summary>
        public string Message { get; private set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Validates field is not null
        /// </summary>
        /// <param name="message">Custom error message (optional)</param>
        public MM_NotNullAttribute(string message = "Field cannot be null")
        {
            Message = message;
        }
        
        #endregion
    }
}
